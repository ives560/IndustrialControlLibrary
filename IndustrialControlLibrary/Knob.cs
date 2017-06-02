using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace IndustrialControlLibrary
{
    //    public partial class LBKnob : UserControl
    //    {
    //        public LBKnob()
    //        {
    //            InitializeComponent();
    //        }
    //    }
    /// <summary>
    /// Description of LBKnob.
    /// </summary>
    public partial class Knob : UserControl
    {
        #region Enumerators
        public enum KnobStyle
        {
            Circular = 0,
        }
        #endregion

        #region Properties variables
        private float minValue = 0.0F;
        private float maxValue = 1.0F;
        private float stepValue = 0.1F;
        private float currValue = 0.0F;
        private KnobStyle style = KnobStyle.Circular;
        private Color scaleColor = Color.DarkGray;
        private Color knobColor = Color.DarkOrange;
        private Color indicatorColor = Color.DarkRed;
        private float indicatorOffset = 10F;
        #endregion

        #region Class variables
        private RectangleF drawRect;
        private RectangleF rectScale;
        private RectangleF rectKnob;
        private float drawRatio;
        private bool isKnobRotating = false;
        private PointF knobCenter;
        private PointF knobIndicatorPos;
        #endregion

        #region Constructor
        public Knob()
        {
            InitializeComponent();

            // Set the styles for drawing
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer |
                ControlStyles.SupportsTransparentBackColor,
                true);

            // Transparent background
            this.BackColor = Color.Transparent;

            this.CalculateDimensions();
        }
        #endregion

        #region Properties
        [Category("Knob"),Description("Minimum value of the knob")]
        public float MinValue
        {
            set
            {
                this.minValue = value;
                this.Invalidate();
            }
            get { return this.minValue; }
        }

        [
            Category("Knob"),
            Description("Maximum value of the knob")
        ]
        public float MaxValue
        {
            set
            {
                this.maxValue = value;
                this.Invalidate();
            }
            get { return this.maxValue; }
        }

        [
            Category("Knob"),
            Description("Step value of the knob")
        ]
        public float StepValue
        {
            set
            {
                this.stepValue = value;
                this.Invalidate();
            }
            get { return this.stepValue; }
        }

        [
            Category("Knob"),
            Description("Current value of the knob")
        ]
        public float Value
        {
            set
            {
                if (value != this.currValue)
                {
                    this.currValue = value;
                    this.knobIndicatorPos = this.GetPositionFromValue(this.currValue);
                    this.Invalidate();

                    LBKnobEventArgs e = new LBKnobEventArgs();
                    e.Value = this.currValue;
                    this.OnKnobChangeValue(e);
                }
            }
            get { return this.currValue; }
        }

        [
            Category("Knob"),
            Description("Style of the knob")
        ]
        public KnobStyle Style
        {
            set
            {
                this.style = value;
                this.Invalidate();
            }
            get { return this.style; }
        }

        [
            Category("Knob"),
            Description("Color of the knob")
        ]
        public Color KnobColor
        {
            set
            {
                this.knobColor = value;
                this.Invalidate();
            }
            get { return this.knobColor; }
        }

        [
            Category("Knob"),
            Description("Color of the scale")
        ]
        public Color ScaleColor
        {
            set
            {
                this.scaleColor = value;
                this.Invalidate();
            }
            get { return this.scaleColor; }
        }

        [Category("Knob"),Description("Color of the indicator")]
        public Color IndicatorColor
        {
            set
            {
                this.indicatorColor = value;
                this.Invalidate();
            }
            get { return this.indicatorColor; }
        }

        [Category("Knob"),Description("Offset of the indicator from the kob border")]
        public float IndicatorOffset
        {
            set
            {
                this.indicatorOffset = value;
                this.CalculateDimensions();
                this.Invalidate();
            }
            get { return this.indicatorOffset; }
        }

        [Browsable(false)]
        public PointF KnobCenter
        {
            get { return this.knobCenter; }
        }
        #endregion

        #region Events delegates

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool blResult = true;

            /// <summary>
            /// Specified WM_KEYDOWN enumeration value.
            /// </summary>
            const int WM_KEYDOWN = 0x0100;

            /// <summary>
            /// Specified WM_SYSKEYDOWN enumeration value.
            /// </summary>
            const int WM_SYSKEYDOWN = 0x0104;

            float val = this.Value;

            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
            {
                switch (keyData)
                {
                    case Keys.Up:
                        val += this.StepValue;
                        if (val <= this.MaxValue)
                            this.Value = val;
                        break;

                    case Keys.Down:
                        val -= this.StepValue;
                        if (val >= this.MinValue)
                            this.Value = val;
                        break;

                    case Keys.PageUp:
                        if (val < this.MaxValue)
                        {
                            val += (this.StepValue * 10);
                            this.Value = val;
                        }
                        break;

                    case Keys.PageDown:
                        if (val > this.MinValue)
                        {
                            val -= (this.StepValue * 10);
                            this.Value = val;
                        }
                        break;

                    case Keys.Home:
                        this.Value = this.MinValue;
                        break;

                    case Keys.End:
                        this.Value = this.MaxValue;
                        break;

                    default:
                        blResult = base.ProcessCmdKey(ref msg, keyData);
                        break;
                }
            }

            return blResult;
        }

        [System.ComponentModel.EditorBrowsableAttribute()]
        protected override void OnClick(EventArgs e)
        {
            this.Focus();
            this.Invalidate();
            base.OnClick(e);
        }

        private void Knob_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.rectKnob.Contains(e.Location) == false)
                return;

            this.isKnobRotating = true;

            this.Focus();
        }

        private void Knob_MouseUp(object sender, MouseEventArgs e)
        {
            this.isKnobRotating = false;

            if (this.rectKnob.Contains(e.Location) == false)
                return;

            float val = this.GetValueFromPosition(e.Location);
            if (val != this.Value)
            {
                this.Value = val;
                this.Invalidate();
            }
        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.isKnobRotating == false)
                return;

            float val = this.GetValueFromPosition(e.Location);
            if (val != this.Value)
            {
                this.Value = val;
                this.Invalidate();
            }
        }

        private void Knob_KeyDown(object sender, KeyEventArgs e)
        {
            float val = this.Value;

            switch (e.KeyCode)
            {
                case Keys.Up:
                    val = this.Value + this.StepValue;
                    break;

                case Keys.Down:
                    val = this.Value - this.StepValue;
                    break;
            }

            if (val < this.MinValue)
                val = this.MinValue;

            if (val > this.MaxValue)
                val = this.MaxValue;

            this.Value = val;
        }


        [System.ComponentModel.EditorBrowsableAttribute()]
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            this.CalculateDimensions();

            this.Invalidate();
        }

        [System.ComponentModel.EditorBrowsableAttribute()]
        protected override void OnPaint(PaintEventArgs e)
        {
            RectangleF _rc = new RectangleF(0, 0, this.Width, this.Height);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            this.DrawBackground(e.Graphics, _rc);
            this.DrawScale(e.Graphics, this.rectScale);
            this.DrawKnob(e.Graphics, this.rectKnob);
            this.DrawKnobIndicator(e.Graphics, this.rectKnob, this.knobIndicatorPos);

        }
        #endregion

        #region Virtual functions		
        protected virtual void CalculateDimensions()
        {
            // Rectangle
            float x, y, w, h;
            x = 0;
            y = 0;
            w = this.Size.Width;
            h = this.Size.Height;

            // Calculate ratio
            drawRatio = (Math.Min(w, h)) / 200;
            if (drawRatio == 0.0)
                drawRatio = 1;

            // Draw rectangle
            drawRect.X = x;
            drawRect.Y = y;
            drawRect.Width = w - 2;
            drawRect.Height = h - 2;

            if (w < h)
                drawRect.Height = w;
            else if (w > h)
                drawRect.Width = h;

            if (drawRect.Width < 10)
                drawRect.Width = 10;
            if (drawRect.Height < 10)
                drawRect.Height = 10;

            this.rectScale = this.drawRect;
            this.rectKnob = this.drawRect;
            this.rectKnob.Inflate(-20 * this.drawRatio, -20 * this.drawRatio);

            this.knobCenter.X = this.rectKnob.Left + (this.rectKnob.Width * 0.5F);
            this.knobCenter.Y = this.rectKnob.Top + (this.rectKnob.Height * 0.5F);

            this.knobIndicatorPos = this.GetPositionFromValue(this.Value);
        }

        public virtual float GetValueFromPosition(PointF position)
        {
            float degree = 0.0F;
            float v = 0.0F;

            PointF center = this.KnobCenter;

            if (position.X <= center.X)
            {
                degree = (center.Y - position.Y) / (center.X - position.X);
                degree = (float)Math.Atan(degree);
                degree = (float)((degree) * (180F / Math.PI) + 45F);
                v = (degree * (this.MaxValue - this.MinValue) / 270F);
            }
            else
            {
                if (position.X > center.X)
                {
                    degree = (position.Y - center.Y) / (position.X - center.X);
                    degree = (float)Math.Atan(degree);
                    degree = (float)(225F + (degree) * (180F / Math.PI));
                    v = (degree * (this.MaxValue - this.MinValue) / 270F);
                }
            }

            if (v > this.MaxValue)
                v = this.MaxValue;

            if (v < this.MinValue)
                v = this.MinValue;

            return v;
        }

        public virtual PointF GetPositionFromValue(float val)
        {
            PointF pos = new PointF(0.0F, 0.0F);

            // Elimina la divisione per 0
            if ((this.MaxValue - this.MinValue) == 0)
                return pos;

            float _indicatorOffset = this.IndicatorOffset * this.drawRatio;

            float degree = 270F * val / (this.MaxValue - this.MinValue);
            degree = (degree + 135F) * (float)Math.PI / 180F;

            pos.X = (int)(Math.Cos(degree) * ((this.rectKnob.Width * 0.5F) - indicatorOffset) + this.rectKnob.X + (this.rectKnob.Width * 0.5F));
            pos.Y = (int)(Math.Sin(degree) * ((this.rectKnob.Width * 0.5F) - indicatorOffset) + this.rectKnob.Y + (this.rectKnob.Height * 0.5F));

            return pos;
        }
        #endregion

        #region renderer
        /// <summary>
        /// Draw the background of the control
        /// </summary>
        /// <param name="Gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public bool DrawBackground(Graphics Gr, RectangleF rc)
        {

            Color c = this.BackColor;
            SolidBrush br = new SolidBrush(c);
            Pen pen = new Pen(c);

            Rectangle _rcTmp = new Rectangle(0, 0, this.Width, this.Height);
            Gr.DrawRectangle(pen, _rcTmp);
            Gr.FillRectangle(br, rc);

            br.Dispose();
            pen.Dispose();

            return true;
        }

        /// <summary>
        /// Draw the scale of the control
        /// </summary>
        /// <param name="Gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public bool DrawScale(Graphics Gr, RectangleF rc)
        {
            //Color cKnobDark = LBColorManager.StepColor(cKnob, 60);

            LinearGradientBrush br = new LinearGradientBrush(rc, this.ScaleColor, this.ScaleColor, 45);

            Gr.FillEllipse(br, rc);

            br.Dispose();

            return true;
        }

        /// <summary>
        /// Draw the knob of the control
        /// </summary>
        /// <param name="Gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public bool DrawKnob(Graphics Gr, RectangleF rc)
        {

            //Color cKnobDark = LBColorManager.StepColor(cKnob, 60);

            LinearGradientBrush br = new LinearGradientBrush(rc, this.KnobColor, this.KnobColor, 45);

            Gr.FillEllipse(br, rc);

            br.Dispose();

            return true;
        }

        public bool DrawKnobIndicator(Graphics Gr, RectangleF rc, PointF pos)
        {
            RectangleF _rc = rc;
            _rc.X = pos.X - 4;
            _rc.Y = pos.Y - 4;
            _rc.Width = 8;
            _rc.Height = 8;

            //Color cKnob = this.IndicatorColor;
            //Color cKnobDark = LBColorManager.StepColor(cKnob, 60);

            LinearGradientBrush br = new LinearGradientBrush(_rc, this.IndicatorColor, this.IndicatorColor, 45);

            Gr.FillEllipse(br, _rc);

            br.Dispose();

            return true;
        }
        #endregion

        #region Fire events
        public event KnobChangeValue KnobChangeValue;
        protected virtual void OnKnobChangeValue(LBKnobEventArgs e)
        {
            if (this.KnobChangeValue != null)
                this.KnobChangeValue(this, e);
        }
        #endregion
    }

    #region Classes for event and event delagates args

    #region Event args class
    /// <summary>
    /// Class for events delegates
    /// </summary>
    public class LBKnobEventArgs : EventArgs
    {
        private float val;

        public LBKnobEventArgs()
        {
        }

        public float Value
        {
            get { return this.val; }
            set { this.val = value; }
        }
    }
    #endregion

    #region Delegates
    public delegate void KnobChangeValue(object sender, LBKnobEventArgs e);
    #endregion

    #endregion
}
