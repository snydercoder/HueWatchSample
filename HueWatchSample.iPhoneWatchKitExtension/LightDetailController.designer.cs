// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace HueWatchSample.iPhoneWatchKitExtension
{
	[Register ("LightDetailController")]
	partial class LightDetailController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		WatchKit.WKInterfaceSlider brightnessSlider { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		WatchKit.WKInterfaceLabel lightNameLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		WatchKit.WKInterfaceSwitch onOffSwitch { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		WatchKit.WKInterfaceButton voiceInputButton { get; set; }

		[Action ("onOffSwitch_Activated:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void onOffSwitch_Activated (WatchKit.WKInterfaceSwitch sender);

		[Action ("voiceInputButton_Activated:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void voiceInputButton_Activated (WatchKit.WKInterfaceButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (brightnessSlider != null) {
				brightnessSlider.Dispose ();
				brightnessSlider = null;
			}
			if (lightNameLabel != null) {
				lightNameLabel.Dispose ();
				lightNameLabel = null;
			}
			if (onOffSwitch != null) {
				onOffSwitch.Dispose ();
				onOffSwitch = null;
			}
			if (voiceInputButton != null) {
				voiceInputButton.Dispose ();
				voiceInputButton = null;
			}
		}
	}
}
