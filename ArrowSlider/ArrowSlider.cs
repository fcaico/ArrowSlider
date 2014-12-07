using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace Fcaico.Controls.ArrowSlider
{
	internal class ArrowSlider : UIView
	{
        private float _percentFilled = 0f;
        private float _discretePercentFilled = 0f;
        private int _currentStep = 0;

		public UIColor Color 
		{
			get;
			set;
		}

        public bool IsDiscrete
        {
            get;
            set;
        }

        public int NumSteps
        {
            get;
            set;
        }

        public int CurrentStep
        {
            get
            {
                return _currentStep;
            }
            set
            {
                _currentStep = value;
                SetNeedsDisplay();
            }
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

                    CalculateCurrentStep(location.X / Frame.Width, NumSteps);

                    FirePositionChanged();
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
                location.X = 0;
            }
            else if (location.X > Frame.Right)
            {
                location.X = Frame.Right;
            }

            CalculateCurrentStep(location.X / Frame.Width, NumSteps);

            FirePositionChanged();
            SetNeedsDisplay();
        }

        private void CalculateCurrentStep(float percentFilled, int totalSteps)
        {
            _percentFilled = percentFilled;

            float percentPerStep = 100f / ((float) (totalSteps -1));
            float percent = (float) Math.Round(percentFilled * 100f);

            _currentStep = (int) Math.Truncate(percent / percentPerStep);
            _discretePercentFilled = (((float) CurrentStep) * percentPerStep) / 100f;
        }

        public override void TouchesEnded (MonoTouch.Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            CalculateCurrentStep(_percentFilled, NumSteps);
            _percentFilled = _discretePercentFilled;

            FirePositionChanged();
            SetNeedsDisplay();
        }


        private void FirePositionChanged()
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

            float filled = _percentFilled;

            if (IsDiscrete)
            {
                filled = _discretePercentFilled;
            }

            ArrowSliderStyleKit.DrawArrowSlider (Color, Color, filled, rect.Location, rect.Size);
		}
	}
}

