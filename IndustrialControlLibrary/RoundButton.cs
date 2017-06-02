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
    //    public partial class LBButton : UserControl
    //    {
    //        public LBButton()
    //        {
    //            InitializeComponent();
    //        }
    //    }
    /// <summary>
    /// Description of LBButton.
    /// </summary>
    public partial class RoundButton : UserControl
    {
        #region Enumeratives
        /// <summary>
        /// Button styles
        /// </summary>
        public enum ButtonStyle
        {
            Circular = 0,
        }

        /// <summary>
        /// Button states
        /// </summary>
        public enum ButtonState
        {
            Normal = 0,
            Pressed,
        }
        #endregion

        #region Properties variables
        private ButtonStyle buttonStyle = ButtonStyle.Circular;
        private ButtonState buttonState = ButtonState.Normal;
        private Color buttonColor = Color.Red;
        private string label = String.Empty;
        #endregion

        #region Class variables
        private RectangleF drawRect;
        protected float drawRatio = 1.0F;
        #endregion

        #region Constructor
        public RoundButton()
        {
            // Initialization
            InitializeComponent();

            // Properties initialization
            this.buttonColor = Color.Red;

            // Set the styles for drawing
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer |
                ControlStyles.SupportsTransparentBackColor,
                true);

            // Transparent background
            this.BackColor = Color.Transparent;


            // Calculate the initial dimensions
            this.CalculateDimensions();
        }
        #endregion

        #region Properties
        [
            Category("Button"),
            Description("Style of the button")
        ]
        public ButtonStyle Style
        {
            set
            {
                this.buttonStyle = value;
                this.Invalidate();
            }
            get { return this.buttonStyle; }
        }

        [
            Category("Button"),
            Description("Color of the body of the button")
        ]
        public Color ButtonColor
        {
            get { return buttonColor; }
            set
            {
                buttonColor = value;
                Invalidate();
            }
        }

        [
            Category("Button"),
            Description("Label of the button"),
            Browsable(true)
        ]
        public string Label
        {
            get { return this.label; }
            set
            {
                this.label = value;
                Invalidate();
            }
        }

        [
            Category("Button"),
            Description("State of the button")
        ]
        public ButtonState State
        {
            set
            {
                this.buttonState = value;
                this.Invalidate();
            }
            get { return this.buttonState; }
        }

        #endregion

        #region Public methods
        public float GetDrawRatio()
        {
            return this.drawRatio;
        }
        #endregion

        #region Events delegates

        /// <summary>
        /// Font change event
        /// </summary>
        /// <param name="e"></param>
        [System.ComponentModel.EditorBrowsableAttribute()]
        protected override void OnFontChanged(EventArgs e)
        {
            // Calculate dimensions
            CalculateDimensions();

            // Redraw the control
            this.Invalidate();
        }

        /// <summary>
        /// Size change event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            // Calculate dimensions
            CalculateDimensions();

            // Redraw the control
            this.Invalidate();
        }

        /// <summary>
        /// Paint event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Control rectangle
            RectangleF _rc = new RectangleF(0, 0, this.Width, this.Height);

            // Set the drawing mode
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw with default renderer ?

            DrawBackground(e.Graphics, _rc);
            DrawBody(e.Graphics, drawRect);
            DrawText(e.Graphics, drawRect);
        }

        #region
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
        /// Draw the body of the control
        /// </summary>
        /// <param name="Gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public bool DrawBody(Graphics Gr, RectangleF rc)
        {

            Color bodyColor = this.ButtonColor;
            Color cDark = ColorManager.StepColor(bodyColor, 20);

            LinearGradientBrush br1 = new LinearGradientBrush(rc,bodyColor,cDark,45);
            Gr.FillEllipse(br1, rc);

            br1.Dispose();

            if (this.State == RoundButton.ButtonState.Pressed)
            {
                float drawRatio = this.GetDrawRatio();

                RectangleF _rc = rc;
                _rc.Inflate(-15F * drawRatio, -15F * drawRatio);
                LinearGradientBrush br2 = new LinearGradientBrush(_rc,cDark,bodyColor,45);
                Gr.FillEllipse(br2, _rc);

                br2.Dispose();
            }

            return true;
        }

        /// <summary>
        /// Draw the text of the control
        /// </summary>
        /// <param name="Gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
		public virtual bool DrawText(Graphics Gr, RectangleF rc)
        {

            float drawRatio = this.GetDrawRatio();

            //Draw Strings
            Font font = new Font(this.Font.FontFamily, this.Font.Size * drawRatio);

            String str = this.Label;

            Color bodyColor = this.ButtonColor;
            Color cDark = ColorManager.StepColor(bodyColor, 20);

            SizeF size = Gr.MeasureString(str, font);

            SolidBrush br1 = new SolidBrush(bodyColor);
            SolidBrush br2 = new SolidBrush(cDark);

            Gr.DrawString(str,
                            font,
                            br1,
                            rc.Left + ((rc.Width * 0.5F) - (float)(size.Width * 0.5F)) + (1 * drawRatio),
                            rc.Top + ((rc.Height * 0.5F) - (float)(size.Height * 0.5)) + (1 * drawRatio));

            Gr.DrawString(str,
                            font,
                            br2,
                            rc.Left + ((rc.Width * 0.5F) - (float)(size.Width * 0.5F)),
                            rc.Top + ((rc.Height * 0.5F) - (float)(size.Height * 0.5)));

            br1.Dispose();
            br2.Dispose();
            font.Dispose();

            return false;
        }
        #endregion

        /// <summary>
        /// Mouse down event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnMouseDown(object sender, MouseEventArgs e)
        {
            // Change the state
            this.State = ButtonState.Pressed;
            this.Invalidate();

            // Call the delagates
            LBButtonEventArgs ev = new LBButtonEventArgs();
            ev.State = this.State;
            this.OnButtonChangeState(ev);
        }

        /// <summary>
        /// Mouse up event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnMuoseUp(object sender, MouseEventArgs e)
        {
            // Change the state
            this.State = ButtonState.Normal;
            this.Invalidate();

            // Call the delagates
            LBButtonEventArgs ev = new LBButtonEventArgs();
            ev.State = this.State;
            this.OnButtonChangeState(ev);
        }

        #endregion

        #region Virtual functions	
        /// <summary>
        /// Calculate the dimensions of the drawing rectangles
        /// </summary>
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
        }
        #endregion

        #region Fire events
        /// <summary>
        /// Event for the state changed
        /// </summary>
        public event ButtonChangeState ButtonChangeState;

        /// <summary>
        /// Method for call the delagetes
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnButtonChangeState(LBButtonEventArgs e)
        {
            if (this.ButtonChangeState != null)
                this.ButtonChangeState(this, e);
        }
        #endregion
    }

    #region Classes for event and event delagates args

    #region Event args class
    /// <summary>
    /// Class for events delegates
    /// </summary>
    public class LBButtonEventArgs : EventArgs
    {
        private RoundButton.ButtonState state;

        public LBButtonEventArgs()
        {
        }

        public RoundButton.ButtonState State
        {
            get { return this.state; }
            set { this.state = value; }
        }
    }
    #endregion

    #region Delegates
    public delegate void ButtonChangeState(object sender, LBButtonEventArgs e);
    #endregion

    #endregion
}
