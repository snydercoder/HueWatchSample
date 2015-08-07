using System;

using WatchKit;
using Foundation;
using Hue.Core;

namespace HueWatchSample.iPhoneWatchKitExtension
{
	public partial class LightDetailController : WKInterfaceController
	{
		Light _light;

		public LightDetailController (IntPtr handle) : base (handle)
		{
		}

		public override void Awake (NSObject context)
		{
			base.Awake (context);

			// Configure interface objects here.
			Console.WriteLine ("{0} awake with context", this);

			_light = ((NSObjectWrapper<Light>)context).Context;

			this.lightNameLabel.SetText (_light.Description);
			this.onOffSwitch.SetOn (_light.On);
			this.brightnessSlider.SetValue (_light.Brightness);
		}

		public override void WillActivate ()
		{
			// This method is called when the watch view controller is about to be visible to the user.
			Console.WriteLine ("{0} will activate", this);
		}

		public override void DidDeactivate ()
		{
			// This method is called when the watch view controller is no longer visible to the user.
			Console.WriteLine ("{0} did deactivate", this);
		}			

		partial void voiceInputButton_Activated (WKInterfaceButton sender)
		{
			PresentTextInputController (new string[4] {"Turn Off", "Turn On", "Half Brightness", "Full Brightness"}, WatchKit.WKTextInputMode.Plain, (result) => {
				// action when the "text input" is complete
				if (result != null && result.Count > 0) {
					var dictatedText = result.GetItem<NSObject>(0).ToString();
					Console.WriteLine (dictatedText);
				}
			});
		}

		partial void onOffSwitch_Activated (WKInterfaceSwitch sender)
		{
			throw new NotImplementedException ();
		}
	}
}

