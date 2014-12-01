using System;
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
        private float _percentFilled = 0f;
        private UIFont _labelFont = UIFont.SystemFontOfSize(22);
        private List<Tuple<string, object>> _values;
        private bool _isDiscrete;
        private int _numSteps = 100;

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
                    _arrow.NumSteps = _numSteps;
                    SetNeedsDisplay();
                }
            }
        }


        private float PercentFilled
        {
            get
            {
                return _percentFilled;
            }
            set
            {
                _percentFilled = value;
                SetNeedsDisplay();
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
                if (IsDiscrete)
                {
                    _arrow.NumSteps = _numSteps;
                }

                SetNeedsDisplay();
            }
        }

        public object CurrentValue
        {
            get
            {
                return GetCurrentValueFromPercent();
            }
            set
            {
                UpdatePercentFromValue();
                SetNeedsDisplay();
            }
        }

        private int GetStepFromPercent()
        {
            float percentPerStep = 100f / ((float) (_numSteps -1));
            float percent = (float) Math.Round(PercentFilled * 100f);

            float a = percent / percentPerStep;
            int index = (int) Math.Truncate(a);

            int curStep = index;

            Console.WriteLine("index: {0}, CurStep: {1}, PercentFilled: {2}, Percent: {3}, PercentPerStep: {4}", index, curStep, PercentFilled, percent, percentPerStep);
            return curStep;

        }

        object GetCurrentValueFromPercent ()
        {
            return _values[GetStepFromPercent()].Item2;
        }

        string GetCurrentTextFromPercent ()
        {
            return _values[GetStepFromPercent()].Item1;
        }

        void UpdatePercentFromValue ()
        {

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

            _valueLabel.Text = PercentFilled.ToString();
            _valueLabel.TextColor = UIColor.White;
            _valueLabel.Font = _labelFont;
            _valueLabel.TextAlignment = UITextAlignment.Left;
			_arrow.PercentFilled = 0.10f;

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
            PercentFilled = (sender as ArrowSlider).PercentFilled;


        }

		public override void Draw (System.Drawing.RectangleF rect)
		{
            _valueLabel.Text = GetCurrentTextFromPercent();

            _arrow.Color = _arrowColor;
            _arrow.PercentFilled = _percentFilled;

			base.Draw (rect);
		}
	}


}

