using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;

namespace ArrowSliderSample
{
	public partial class ArrowSliderSampleViewController : UIViewController
	{
		public ArrowSliderSampleViewController (IntPtr handle) : base (handle)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
            MyArrowSlider.Values = new List<Tuple<string, object>>
            {
                new Tuple<string, object> ( "One", 1 ),
                new Tuple<string, object> ( "Two", 2 ),
                new Tuple<string, object> ( "Three", 3),
                new Tuple<string, object> ( "Four", 4),
                new Tuple<string, object> ( "Five", 5)
            };

		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}

		#endregion
	}
}

