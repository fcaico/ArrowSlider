using System;
using System.Linq;
using UIKit;
using Foundation;
using System.ComponentModel;
using CoreGraphics;
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
        private float _padding = 1.0f;
        private float _borderWidth = 1.0f;
        private bool _isEnabled = true;
        private float _fontBaselineOffset = 0;
        private NSLayoutConstraint _fontBaselineConstraint;

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


        [Export("Font"), Browsable(true)]
        public UIFont Font
        {
            get
            {
                return _labelFont;
            }
            set
            {
                if (_labelFont != value)
                {
                    _labelFont = value;
                    _valueLabel.Font = value;
                    SetNeedsDisplay();
                }
            }
        }


        [Export("FontBaselineOffset"), Browsable(true)]
        public float FontBaselineOffset
        {
            get
            {
                return _fontBaselineOffset;
            }
            set
            {
                RemoveConstraint(_fontBaselineConstraint); 

                _fontBaselineOffset = value;
                _fontBaselineConstraint = CreateFontBaselineConstraint(_fontBaselineOffset);

                AddConstraint(_fontBaselineConstraint);
                SetNeedsUpdateConstraints();
                LayoutIfNeeded();
            }
        }
           

        [Export("Padding"), Browsable(true)]
        public float Padding
        {
            get
            {
                return _padding;
            }
            set
            {
                _padding = value;
                SetNeedsDisplay();
            }
        }

        [Export("BorderWidth"), Browsable(true)]
        public float BorderWidth
        {
            get
            {
                return _borderWidth;
            }
            set
            {
                _borderWidth = value;
                SetNeedsDisplay();
            }
        }


        [Export("IsEnabled"), Browsable(true)]
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
                _arrow.IsEnabled = value;
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
                    _arrow.SetNeedsDisplay();
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
                if (_values.ElementAt(i).Item2.Equals(newValue))
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

		public ArrowSliderView(CGRect rect) : base(rect)
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

            _arrow.IsEnabled = _isEnabled;
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

        private NSLayoutConstraint CreateFontBaselineConstraint(float offset)
        {
            return NSLayoutConstraint.Create(_valueLabel, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, this, NSLayoutAttribute.CenterY, 1, offset);
        }


		private void SetupConstraints()
		{
            _valueLabel.TranslatesAutoresizingMaskIntoConstraints = false;
			_arrow.TranslatesAutoresizingMaskIntoConstraints = false;

            _fontBaselineConstraint = CreateFontBaselineConstraint(_fontBaselineOffset);
            AddConstraint (_fontBaselineConstraint);

            AddConstraint (NSLayoutConstraint.Create (_valueLabel, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, this, NSLayoutAttribute.CenterX, 1, 0f));
            AddConstraint (NSLayoutConstraint.Create (_valueLabel, NSLayoutAttribute.Width, NSLayoutRelation.Equal, this, NSLayoutAttribute.Width, 1, - 20f - (2.0f * Padding)));

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

		public override void Draw (CGRect rect)
		{
            _valueLabel.Text = GetCurrentTextFromStep();

            _arrow.Color = _arrowColor;
            _arrow.BorderWidth = BorderWidth;
            _arrow.Padding = Padding;

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

