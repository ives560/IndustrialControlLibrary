namespace Examples
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lbKnob1 = new IndustrialControlLibrary.Knob();
            this.led1 = new IndustrialControlLibrary.Led();
            this.sevenSegmentArray1 = new IndustrialControlLibrary.SevenSegmentArray();
            this.sevenSegment1 = new IndustrialControlLibrary.SevenSegment();
            this.thermometer1 = new IndustrialControlLibrary.Thermometer();
            this.roundButton1 = new IndustrialControlLibrary.RoundButton();
            this.SuspendLayout();
            // 
            // lbKnob1
            // 
            this.lbKnob1.BackColor = System.Drawing.Color.Transparent;
            this.lbKnob1.IndicatorColor = System.Drawing.Color.DarkRed;
            this.lbKnob1.IndicatorOffset = 10F;
            this.lbKnob1.KnobColor = System.Drawing.Color.DarkOrange;
            this.lbKnob1.Location = new System.Drawing.Point(166, 152);
            this.lbKnob1.MaxValue = 1F;
            this.lbKnob1.MinValue = 0F;
            this.lbKnob1.Name = "lbKnob1";
            this.lbKnob1.ScaleColor = System.Drawing.Color.DarkGray;
            this.lbKnob1.Size = new System.Drawing.Size(100, 100);
            this.lbKnob1.StepValue = 0.1F;
            this.lbKnob1.Style = IndustrialControlLibrary.Knob.KnobStyle.Circular;
            this.lbKnob1.TabIndex = 4;
            this.lbKnob1.Value = 0F;
            // 
            // led1
            // 
            this.led1.Location = new System.Drawing.Point(408, 79);
            this.led1.Name = "led1";
            this.led1.OffColor = System.Drawing.Color.DarkGray;
            this.led1.OnColor = System.Drawing.Color.Lime;
            this.led1.Size = new System.Drawing.Size(50, 50);
            this.led1.TabIndex = 3;
            this.led1.Value = true;
            // 
            // sevenSegmentArray1
            // 
            this.sevenSegmentArray1.ArrayCount = 4;
            this.sevenSegmentArray1.ColorBackground = System.Drawing.Color.DarkGray;
            this.sevenSegmentArray1.ColorDark = System.Drawing.Color.DimGray;
            this.sevenSegmentArray1.ColorLight = System.Drawing.Color.Red;
            this.sevenSegmentArray1.DecimalShow = true;
            this.sevenSegmentArray1.ElementPadding = new System.Windows.Forms.Padding(4);
            this.sevenSegmentArray1.ElementWidth = 10;
            this.sevenSegmentArray1.ItalicFactor = 0F;
            this.sevenSegmentArray1.Location = new System.Drawing.Point(249, 58);
            this.sevenSegmentArray1.Name = "sevenSegmentArray1";
            this.sevenSegmentArray1.Size = new System.Drawing.Size(128, 64);
            this.sevenSegmentArray1.TabIndex = 2;
            this.sevenSegmentArray1.TabStop = false;
            this.sevenSegmentArray1.Value = null;
            // 
            // sevenSegment1
            // 
            this.sevenSegment1.ColonOn = false;
            this.sevenSegment1.ColonShow = false;
            this.sevenSegment1.ColorBackground = System.Drawing.Color.DarkGray;
            this.sevenSegment1.ColorDark = System.Drawing.Color.DimGray;
            this.sevenSegment1.ColorLight = System.Drawing.Color.Red;
            this.sevenSegment1.CustomPattern = 0;
            this.sevenSegment1.DecimalOn = false;
            this.sevenSegment1.DecimalShow = true;
            this.sevenSegment1.ElementWidth = 10;
            this.sevenSegment1.ItalicFactor = 0F;
            this.sevenSegment1.Location = new System.Drawing.Point(166, 58);
            this.sevenSegment1.Name = "sevenSegment1";
            this.sevenSegment1.Padding = new System.Windows.Forms.Padding(4);
            this.sevenSegment1.Size = new System.Drawing.Size(32, 64);
            this.sevenSegment1.TabIndex = 1;
            this.sevenSegment1.TabStop = false;
            this.sevenSegment1.Value = null;
            // 
            // thermometer1
            // 
            this.thermometer1.BigScale = 5;
            this.thermometer1.BigScaleColor = System.Drawing.Color.Black;
            this.thermometer1.DialBackColor = System.Drawing.Color.Gray;
            this.thermometer1.DialOutLineColor = System.Drawing.Color.Gray;
            this.thermometer1.DrawColor = System.Drawing.Color.Black;
            this.thermometer1.DrawFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.thermometer1.HighTemperature = 50F;
            this.thermometer1.Location = new System.Drawing.Point(29, 12);
            this.thermometer1.LowTemperature = -20F;
            this.thermometer1.MercuryBackColor = System.Drawing.Color.LightGray;
            this.thermometer1.MercuryColor = System.Drawing.Color.Red;
            this.thermometer1.Name = "thermometer1";
            this.thermometer1.Size = new System.Drawing.Size(60, 240);
            this.thermometer1.SmallScale = 5;
            this.thermometer1.SmallScaleColor = System.Drawing.Color.Black;
            this.thermometer1.TabIndex = 0;
            this.thermometer1.TempColor = System.Drawing.Color.Black;
            this.thermometer1.Temperature = 0F;
            this.thermometer1.TempFont = new System.Drawing.Font("宋体", 12F);
            // 
            // roundButton1
            // 
            this.roundButton1.BackColor = System.Drawing.Color.Transparent;
            this.roundButton1.ButtonColor = System.Drawing.Color.Red;
            this.roundButton1.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.roundButton1.Label = "stop";
            this.roundButton1.Location = new System.Drawing.Point(332, 159);
            this.roundButton1.Margin = new System.Windows.Forms.Padding(14);
            this.roundButton1.Name = "roundButton1";
            this.roundButton1.Size = new System.Drawing.Size(89, 93);
            this.roundButton1.State = IndustrialControlLibrary.RoundButton.ButtonState.Normal;
            this.roundButton1.Style = IndustrialControlLibrary.RoundButton.ButtonStyle.Circular;
            this.roundButton1.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 423);
            this.Controls.Add(this.roundButton1);
            this.Controls.Add(this.lbKnob1);
            this.Controls.Add(this.led1);
            this.Controls.Add(this.sevenSegmentArray1);
            this.Controls.Add(this.sevenSegment1);
            this.Controls.Add(this.thermometer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private IndustrialControlLibrary.Thermometer thermometer1;
        private IndustrialControlLibrary.SevenSegment sevenSegment1;
        private IndustrialControlLibrary.SevenSegmentArray sevenSegmentArray1;
        private IndustrialControlLibrary.Led led1;
        private IndustrialControlLibrary.Knob lbKnob1;
        private IndustrialControlLibrary.RoundButton roundButton1;
    }
}

