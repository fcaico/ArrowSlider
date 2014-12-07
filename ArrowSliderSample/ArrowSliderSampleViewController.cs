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
            DiscreteArrowSlider.Values = new List<Tuple<string, object>>
            {
                new Tuple<string, object> ( "January", 1 ),
                new Tuple<string, object> ( "February", 2 ),
                new Tuple<string, object> ( "March", 3),
                new Tuple<string, object> ( "April", 4),
                new Tuple<string, object> ( "May", 5),
                new Tuple<string, object> ( "June", 6 ),
                new Tuple<string, object> ( "July", 7 ),
                new Tuple<string, object> ( "August", 8 ),
                new Tuple<string, object> ( "September", 9 ),
                new Tuple<string, object> ( "October", 10 ),
                new Tuple<string, object> ( "November", 11 ),
                new Tuple<string, object> ( "December", 12 )
            };

            List<Tuple<string, object>> durations = new List<Tuple<string, object>>();
            for (int i = 1; i < 12; i++)
            {
                durations.Add(new Tuple<string, object>(string.Format("{0} Hours", i), i));
            }
            DurationSlider.Values = durations;
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

