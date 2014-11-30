using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace Fcaico.Controls.ArrowSlider
{
	internal class ArrowSlider : UIView
	{
		public UIColor Color 
		{
			get;
			set;
		}

        public float PercentFilled
        {
            get;
            set;
        }


        public event EventHandler PositionChanged;

		public ArrowSlider () : base()
		{
            BackgroundColor = UIColor.Clear;

		}

        public override void TouchesBegan (MonoTouch.Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesBegan (touches, evt);
            UITouch touch = touches.AnyObject as UITouch;
            if (touch != null)
            {
                if (Frame.Contains (touch.LocationInView (this)))
                {
                    PointF location = touch.LocationInView(this);
                    PercentFilled = location.X / Frame.Width;
                    FirePercentChanged();
                    SetNeedsDisplay();
                }
            }
        }
        public override void TouchesMoved (MonoTouch.Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);
            UITouch touch = touches.AnyObject as UITouch;


            PointF location = touch.LocationInView(this);

            if (location.X < Frame.Left)
            {
                PercentFilled = 0f;
            }
            else if (location.X > Frame.Right)
            {
                PercentFilled = 1f;
            }
            else
            {
                PercentFilled = location.X / Frame.Width;
            }
            FirePercentChanged();
            SetNeedsDisplay();
        }

        public override void TouchesEnded (MonoTouch.Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);
        }


        private void FirePercentChanged()
        {
            // Make a temporary copy of the event to avoid possibility of 
            // a race condition if the last subscriber unsubscribes 
            // immediately after the null check and before the event is raised.
            EventHandler  handler = PositionChanged;

            // Event will be null if there are no subscribers 
            if (handler != null)
            {
                // Use the () operator to raise the event.
                handler(this, new EventArgs());
            }
        }


		public override void Draw (System.Drawing.RectangleF rect)
		{
			base.Draw (rect);
            ArrowSliderStyleKit.DrawArrowSlider (Color, Color, PercentFilled, rect.Location, rect.Size);
		}
	}
}

