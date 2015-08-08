using System;

using WatchKit;
using Foundation;
using Hue.Core;

namespace HueWatchSample.iPhoneWatchKitExtension
{
	public partial class LightDetailController : WKInterfaceController
	{
		Light _light;
		bool _onOffSwitch;

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

			_onOffSwitch = _light.On;
			this.onOffSwitch.SetOn (_onOffSwitch);

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
			PresentTextInputController (new string[5] {"Turn Off", "Turn On", "Brightness Low", "Brightness Medium", "Brightness High"}, 
										WatchKit.WKTextInputMode.Plain, 
									    (result) => {
				// action when the "text input" is complete
				if (result != null && result.Count > 0) {
					var dictatedText = result.GetItem<NSObject>(0).ToString();
					Console.WriteLine (dictatedText);
					this.ProcessTextCommand(dictatedText);
				}
			});
		}

		private void ToggleSwitchState()
		{
			_onOffSwitch = !_onOffSwitch;
		}

		partial void onOffSwitch_Activated (WKInterfaceSwitch sender)
		{
			this.ToggleSwitchState();
			HueConnector.GetConnector().FlipSwitch(_light, _onOffSwitch);
		}

		private void ProcessTextCommand(string textCommand)
		{
			if (textCommand.ToLower().Contains("on"))
				HueConnector.GetConnector().FlipSwitch(_light, true);

			if (textCommand.ToLower().Contains("off"))
				HueConnector.GetConnector().FlipSwitch(_light, false);

			if (textCommand.ToLower().Contains("low"))
				HueConnector.GetConnector().AdjustBrightness(_light, 80);

			if (textCommand.ToLower().Contains("medium"))
				HueConnector.GetConnector().AdjustBrightness(_light, 170);

			if (textCommand.ToLower().Contains("high"))
				HueConnector.GetConnector().AdjustBrightness(_light, 255);
		}

//		[Action ("sliderAction:")]
//		void SliderAction (Single value)
//		{
//			Console.WriteLine ("Slider value is now: {0}", value);
//		}
	}
}

