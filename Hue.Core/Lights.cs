using System;
using System.Collections.Generic;

namespace Hue.Core
{
	public class Lights
	{
		public Lights ()
		{
		}

		public static List<Light> Discover()
		{
			List<Light> lights = new List<Light> ();
			lights.Add (new Light (1, "Family Room"));
			lights.Add (new Light (2, "Kitchen"));
			lights.Add (new Light (3, "Back Patio"));
			lights.Add (new Light (4, "Front Porch"));
			lights.Add (new Light (5, "Garage"));
			lights.Add (new Light (6, "Master Bedroom"));
			lights.Add (new Light (7, "Main Bathroom"));
			lights.Add (new Light (8, "Back Balcony"));
			return lights;
		}
	}
}

