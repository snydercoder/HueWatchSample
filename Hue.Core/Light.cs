using System;

namespace Hue.Core
{
	public class Light
	{
		public Light (string id, string description, bool onOff, int brightness)
		{
			this.Id = id;
			this.Description = description;
			this.On = onOff;
			this.Brightness = brightness;
		}

		public string Id {
			get;
			set;
		}

		public string Description {
			get;
			set;
		}

		public int Brightness {
			get;
			set;
		}

		public bool On {
			get;
			set;
		}
	}
}

