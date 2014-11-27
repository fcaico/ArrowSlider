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
		Fcaico.Controls.ArrowSlider.ArrowSliderView MyArrowSlider { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (MyArrowSlider != null) {
				MyArrowSlider.Dispose ();
				MyArrowSlider = null;
			}
		}
	}
}
