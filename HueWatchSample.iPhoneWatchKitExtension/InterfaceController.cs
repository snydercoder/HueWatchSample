using System;

using WatchKit;
using Foundation;
using System.Collections.Generic;
using Hue.Core;

namespace HueWatchSample.iPhoneWatchKitExtension
{
	public partial class InterfaceController : WKInterfaceController
	{
		List<Light> _lights;

		public InterfaceController (IntPtr handle) : base (handle)
		{
		}

		public override void Awake (NSObject context)
		{
			base.Awake (context);

			// Configure interface objects here.
			Console.WriteLine ("{0} awake with context", this);

			_lights = Lights.Discover ();
		}

		public override void WillActivate ()
		{
			// This method is called when the watch view controller is about to be visible to the user.
			Console.WriteLine ("{0} will activate", this);

			this.LoadTableRows (_lights);
		}

		public override void DidDeactivate ()
		{
			// This method is called when the watch view controller is no longer visible to the user.
			Console.WriteLine ("{0} did deactivate", this);
		}

		private void LoadTableRows (List<Light> lights)
		{
			this.lightListTable.SetNumberOfRows((nint)lights.Count, "default");
			//lightListTable.SetRowTypes (new [] {"default", "type1", "type2", "default", "default"});
			// Create all of the table rows.
			for (var i = 0; i < lights.Count; i++) {
				var elementRow = (RowController)this.lightListTable.GetRowController (i);
				Light currentRowLight = lights [i];

				elementRow.lightLabel.SetText (string.Format("{0}: {1}", currentRowLight.Id.ToString(), currentRowLight.Description));
			}

		
		}
	}
}

