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
		HueConnector _hueConnector = new HueConnector("http://192.168.0.20", "290e46a369a4577253d1ed61fb85a93");

		public InterfaceController (IntPtr handle) : base (handle)
		{
		}

		public override void Awake (NSObject context)
		{
			base.Awake (context);

			// Configure interface objects here.
			Console.WriteLine ("{0} awake with context", this);

			_lights = _hueConnector.Discover ();
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

			// Create all of the table rows.
			for (var i = 0; i < lights.Count; i++) {
				var lightRow = (RowController)this.lightListTable.GetRowController (i);
				Light currentRowLight = lights [i];

				//Set the underlying row object as the current light in the loop
				lightRow.LightObject = currentRowLight;

				//Set the display
				lightRow.lightLabel.SetText (string.Format("{0}: {1}", currentRowLight.Id.ToString(), currentRowLight.Description));
			}				
		}

		public override NSObject GetContextForSegue (string segueIdentifier)
		{
			return base.GetContextForSegue (segueIdentifier);
		}

		public override NSObject GetContextForSegue (string segueIdentifier, WKInterfaceTable table, nint rowIndex)
		{
			//return base.GetContextForSegue (segueIdentifier, table, rowIndex);

			var lightRow = (RowController)table.GetRowController (rowIndex);
			return new NSObjectWrapper<Light> (lightRow.LightObject);
		}


	}
}

