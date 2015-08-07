using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Json;
using System.Threading;

namespace Hue.Core
{
	public class HueConnector
	{
		string _baseUrl;

		public HueConnector (string url, string apiKey)
		{
			_baseUrl = string.Format ("{0}/api/{1}", url, apiKey);
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

		//TODO: Turn on/off

		//TODO: Adjust brightness
	}
}

