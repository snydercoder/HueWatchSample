using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Json;
using System.Threading;
using System.Text;

namespace Hue.Core
{
	public class HueConnector
	{
		private static HueConnector _connector = null;

		string _baseUrl;

		private HueConnector (string url, string apiKey)
		{
			_baseUrl = string.Format ("{0}/api/{1}", url, apiKey);
		}

		public static HueConnector GetConnector(string url, string apiKey)
		{
			if (_connector == null)
				_connector = new HueConnector (url, apiKey);

			return _connector;
		}

		public static HueConnector GetConnector()
		{
			if (_connector == null)
				throw new Exception ("No connector has been initialized yet.  Use GetConnector(url, apiKey) to initialize it.");

			return _connector;
		}

		public List<Light> Discover()
		{
			List<Light> lights = new List<Light> ();

			HttpWebRequest request = new HttpWebRequest (new Uri (_baseUrl));
			request.ContentType = "application/json";
			request.Method = "GET";

			using (WebResponse response = request.GetResponse())
			{
				using (Stream responseStream = response.GetResponseStream ())
				{
					JsonValue jsonResponse = JsonObject.Load(responseStream);

					//Loop through all the discovered lights and create Light objects.
					JsonValue jsonLights = jsonResponse ["lights"];
					for (int i = 0; i < jsonLights.Count; i++) 
					{
						//TODO: READ ID FROM JSON VALUE NAME INSTEAD OF INFERRING IT.
						string lightId = (i+1).ToString();
						JsonValue jsonLight = jsonLights[lightId];
						string lightName = jsonLight["name"];
						bool onOff;
						if (!bool.TryParse (jsonLight["state"]["on"].ToString(), out onOff))
							onOff = false;
						int brightness;
						if (!int.TryParse (jsonLight["state"]["bri"].ToString(), out brightness))
							brightness = 0;

						lights.Add(new Light(lightId, lightName, onOff, brightness));
					}
				}
			}
			return lights;
		}
			
		public void FlipSwitch(Light light, bool turnOn)
		{
			string requestData = string.Format("{{\"on\" : {0}}}", turnOn.ToString().ToLower());
			this.SendCommand (light, requestData);
		}

		public void AdjustBrightness(Light light, int brightnessLevel)
		{
			string requestData = string.Format("{{\"bri\" : {0}}}", brightnessLevel.ToString());
			this.SendCommand (light, requestData);
		}

		private void SendCommand(Light light, string requestData)
		{
			ASCIIEncoding encoding = new ASCIIEncoding ();
			byte[] data = encoding.GetBytes (requestData);

			HttpWebRequest request = new HttpWebRequest (new Uri (string.Format("{0}/lights/{1}/state", _baseUrl, light.Id)));
			request.ContentType = "application/json";
			request.Method = "PUT";

			// Set the content length of the string being posted.
			request.ContentLength = data.Length;

			Stream newStream = request.GetRequestStream ();
			newStream.Write (data, 0, data.Length);
		}
	}
}

