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

						lights.Add(new Light(lightId, lightName));
					}
				}
			}

			/*
			List<Light> lights = new List<Light> ();
			lights.Add (new Light (1, "Family Room"));
			lights.Add (new Light (2, "Kitchen"));
			lights.Add (new Light (3, "Back Patio"));
			lights.Add (new Light (4, "Front Porch"));
			lights.Add (new Light (5, "Garage"));
			lights.Add (new Light (6, "Master Bedroom"));
			lights.Add (new Light (7, "Main Bathroom"));
			lights.Add (new Light (8, "Back Balcony"));
			*/

			return lights;
		}
	}
}

