using System;

namespace Hue.Core
{
	public class Light
	{
		public Light (int id, string description)
		{
			this.Id = id;
			this.Description = description;
		}

		public int Id {
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

		public ColorType Color {
			get;
			set;
		}
	}
}

