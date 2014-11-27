using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.ComponentModel;
using System.Drawing;

namespace Fcaico.Controls.ArrowSlider
{
	[Register("ArrowSliderView"), DesignTimeVisible(true)]
	public class ArrowSliderView : UIView, IComponent
	{
		UILabel _valueLabel = new UILabel();
		ArrowSlider  _arrow = new ArrowSlider();
        UIColor _arrowColor = UIColor.Yellow;
        float _percentFilled = 0.5f;
        private UIFont _labelFont = UIFont.SystemFontOfSize(22);


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


        public float PercentFilled
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

            _valueLabel.Text = PercentFilled.ToString();
            _valueLabel.TextColor = UIColor.White;
            _valueLabel.Font = _labelFont;
            _valueLabel.TextAlignment = UITextAlignment.Left;
			_arrow.PercentFilled = 0.10f;

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
            _valueLabel.Text = PercentFilled.ToString("P");

            _arrow.Color = _arrowColor;
            _arrow.PercentFilled = _percentFilled;

			base.Draw (rect);
		}
	}


}

