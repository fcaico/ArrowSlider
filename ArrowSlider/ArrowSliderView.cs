using System;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.ComponentModel;
using System.Drawing;
using System.Collections.Generic;

namespace Fcaico.Controls.ArrowSlider
{
	[Register("ArrowSliderView"), DesignTimeVisible(true)]
	public class ArrowSliderView : UIView, IComponent
	{
        private UILabel _valueLabel = new UILabel();
        private ArrowSlider  _arrow = new ArrowSlider();
        private UIColor _arrowColor = UIColor.Yellow;
        private UIFont _labelFont = UIFont.SystemFontOfSize(22);
        private List<Tuple<string, object>> _values;
        private bool _isDiscrete;
        private int _numSteps = 0;

        public event EventHandler CurrentValueChanged;

        [Export("ArrowColor"), Browsable(true)]
        public UIColor ArrowColor
        {
            get
            {
                return _arrowColor;
            }
            set
            {
                if (_arrowColor != value)
                {
                    _arrowColor = value;
                    SetNeedsDisplay();
                }
            }
        }

        [Export("IsDiscrete"), Browsable(true)]
        public bool IsDiscrete
        {
            get
            {
                return _isDiscrete;
            }
            set
            {
                if (_isDiscrete != value)
                {
                    _isDiscrete = value;
                    _arrow.IsDiscrete = value;
                    _arrow.NumSteps = _numSteps;
                    SetNeedsDisplay();
                }
            }
        }

        public List<Tuple<string, object>> Values
        {
            get
            {
                return _values;
            }
            set
            {
                _values = value;
                _numSteps = _values.Count;
                _arrow.NumSteps = _numSteps;

                SetNeedsDisplay();
            }
        }

        public object CurrentValue
        {
            get
            {
                return GetCurrentValueFromStep();
            }
            set
            {
                if (UpdateStepFromNewValue(value))
                {
                    SetNeedsDisplay();
                }
            }
        }

        string GetCurrentTextFromStep ()
        {
            return _values[_arrow.CurrentStep].Item1;
        }

        private object GetCurrentValueFromStep()
        {
            return _values[_arrow.CurrentStep].Item2;
        }

        private bool UpdateStepFromNewValue(object newValue)
        {
            bool retVal = false;

            for (int i = 0; i < _values.Count; i++)
            {
                if (_values.ElementAt(i).Item2 == newValue)
                {
                    _arrow.CurrentStep = i;
                    retVal = true;
                    break;
                }
            }

            return retVal;
        }


       	public ArrowSliderView(IntPtr handle) : base(handle)
		{
		}

		public ArrowSliderView() : base()
		{
            Initialize ();
		}

		public ArrowSliderView(RectangleF rect) : base(rect)
		{
		}


		private void Initialize()
		{
            SetupSubviews ();
			SetupConstraints ();
		}

		public override void AwakeFromNib ()
		{
            base.AwakeFromNib ();
			Initialize ();
		}        
		

		public event EventHandler Disposed;

		public ISite Site 
		{
			get;
			set;
		}

		private void SetupSubviews()
		{
			this.ClipsToBounds = true;
            Add (_arrow);
            Add(_valueLabel);

            _arrow.PositionChanged += OnPositionChanged;

            SetDefaultValues();

            _arrow.NumSteps = _numSteps;
            _arrow.CurrentStep = 0;

            _valueLabel.Text = GetCurrentTextFromStep();
            _valueLabel.TextColor = UIColor.White;
            _valueLabel.Font = _labelFont;
            _valueLabel.TextAlignment = UITextAlignment.Left;

		}

        private void SetDefaultValues()
        {
            _values = new List<Tuple<string, object>>();
            for (int i = 0; i < 101; i++)
            {
                float val = (i / 100f);
                _values.Add(new Tuple<string,object>(val.ToString("P0"), i));
            }

            _numSteps = 101; 
        }

		private void SetupConstraints()
		{
            _valueLabel.TranslatesAutoresizingMaskIntoConstraints = false;
			_arrow.TranslatesAutoresizingMaskIntoConstraints = false;

            AddConstraint (NSLayoutConstraint.Create (_valueLabel, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, this, NSLayoutAttribute.CenterY, 1, 0f));
            AddConstraint (NSLayoutConstraint.Create (_valueLabel, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, this, NSLayoutAttribute.CenterX, 1, 0f));
            AddConstraint (NSLayoutConstraint.Create (_valueLabel, NSLayoutAttribute.Width, NSLayoutRelation.Equal, this, NSLayoutAttribute.Width, 1, -20f));

			AddConstraint (NSLayoutConstraint.Create (_arrow, NSLayoutAttribute.Top, NSLayoutRelation.Equal, this, NSLayoutAttribute.Top, 1, 0f));
			AddConstraint (NSLayoutConstraint.Create (_arrow, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, this, NSLayoutAttribute.Bottom, 1, 0f));
			AddConstraint (NSLayoutConstraint.Create (_arrow, NSLayoutAttribute.Width, NSLayoutRelation.Equal, this, NSLayoutAttribute.Width, 1, 0f));
			AddConstraint (NSLayoutConstraint.Create (_arrow, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, this, NSLayoutAttribute.CenterX, 1, 0f));

		}

        private void OnPositionChanged(object sender, EventArgs e)
        {
            CurrentValue = GetCurrentValueFromStep();
            FireCurrentValueChanged();
        }

		public override void Draw (System.Drawing.RectangleF rect)
		{
            _valueLabel.Text = GetCurrentTextFromStep();

            _arrow.Color = _arrowColor;
           // _arrow.PercentFilled = _percentFilled;

			base.Draw (rect);
		}

        private void FireCurrentValueChanged()
        {
            // Make a temporary copy of the event to avoid possibility of 
            // a race condition if the last subscriber unsubscribes 
            // immediately after the null check and before the event is raised.
            EventHandler  handler = CurrentValueChanged;

            // Event will be null if there are no subscribers 
            if (handler != null)
            {
                // Use the () operator to raise the event.
                handler(this, new EventArgs());
            }
        }
	}


}

