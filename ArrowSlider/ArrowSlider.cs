using System;
using UIKit;
using CoreGraphics;

namespace Fcaico.Controls.ArrowSlider
{
	internal class ArrowSlider : UIView
	{
        private nfloat _percentFilled = 0f;
        private nfloat _discretePercentFilled = 0f;
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

        public bool IsEnabled
        {
            get;
            set;
        }

        public int NumSteps
        {
            get;
            set;
        }

        public float Padding
        {
            get;
            set;
        }

        public float BorderWidth
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
                CalculatePercentForCurrentStep(_currentStep, NumSteps);
                SetNeedsDisplay();
            }
        }

        public event EventHandler PositionChanged;

		public ArrowSlider () : base()
		{
            BackgroundColor = UIColor.Clear;
		}

        public override void TouchesBegan (Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesBegan (touches, evt);

            if (!IsEnabled)
            {
                return;
            }

            UITouch touch = touches.AnyObject as UITouch;
            if (touch != null)
            {
                if (Frame.Contains (touch.LocationInView (this)))
                {
                    CGPoint location = touch.LocationInView(this);

                    CalculateCurrentStep((location.X) / Frame.Width, NumSteps);

                    FirePositionChanged();
                    SetNeedsDisplay();
                }
            }
        }


        public override void TouchesMoved (Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);

            if (!IsEnabled)
            {
                return;
            }

            UITouch touch = touches.AnyObject as UITouch;


            CGPoint location = touch.LocationInView(this);

            if (location.X < Frame.Left)
            {
                location.X = 0;
            }
            else if (location.X > Frame.Right)
            {
                location.X = Frame.Right;
            }

            CalculateCurrentStep((location.X) / Frame.Width, NumSteps);

            FirePositionChanged();
            SetNeedsDisplay();
        }

        private void CalculateCurrentStep(nfloat percentFilled, int totalSteps)
        {
            _percentFilled = percentFilled;

            nfloat percentPerStep = 100f / ((nfloat) (totalSteps -1));
            nfloat percent = (nfloat) percentFilled * 100f;

            _currentStep = (int) Math.Round (percent / percentPerStep);
            _discretePercentFilled = (((nfloat) CurrentStep) * percentPerStep) / 100f;
        }

        void CalculatePercentForCurrentStep (int currentStep, int totalSteps)
        {
            nfloat percentPerStep = 100f / ((nfloat) (totalSteps -1));
            nfloat percentFilled = (nfloat) (currentStep * percentPerStep);
            _percentFilled = (percentFilled / 100f);
            _discretePercentFilled = _percentFilled;
        }

        public override void TouchesEnded (Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            if (!IsEnabled)
            {
                return;
            }

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


		public override void Draw (CGRect rect)
		{
			base.Draw (rect);

            nfloat filled = _percentFilled;

            if (IsDiscrete)
            {
                filled = _discretePercentFilled;
            }

            nfloat totalPadding = 2.0f * Padding;
            CGPoint location = new CGPoint(rect.Location.X + Padding, rect.Location.Y + Padding);

            CGSize size = new CGSize(rect.Size.Width - totalPadding , rect.Size.Height - totalPadding );

            ArrowSliderStyleKit.DrawArrowSlider (Color, Color, Padding, BorderWidth, filled, location, size);
		}
	}
}

