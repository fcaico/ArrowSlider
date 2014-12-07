// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace ArrowSliderSample
{
	[Register ("ArrowSliderSampleViewController")]
	partial class ArrowSliderSampleViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		Fcaico.Controls.ArrowSlider.ArrowSliderView ContinuousArrowSlider { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		Fcaico.Controls.ArrowSlider.ArrowSliderView DiscreteArrowSlider { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		Fcaico.Controls.ArrowSlider.ArrowSliderView DistanceSlider { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		Fcaico.Controls.ArrowSlider.ArrowSliderView DurationSlider { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ContinuousArrowSlider != null) {
				ContinuousArrowSlider.Dispose ();
				ContinuousArrowSlider = null;
			}
			if (DiscreteArrowSlider != null) {
				DiscreteArrowSlider.Dispose ();
				DiscreteArrowSlider = null;
			}
			if (DistanceSlider != null) {
				DistanceSlider.Dispose ();
				DistanceSlider = null;
			}
			if (DurationSlider != null) {
				DurationSlider.Dispose ();
				DurationSlider = null;
			}
		}
	}
}
