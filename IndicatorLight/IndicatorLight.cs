using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EMapping.Page
{
    /// <summary>
    /// 指示燈控件
    /// </summary>
    [DesignerCategory("Code")]
    public partial class IndicatorLight : UserControl
    {
        private bool isOn = false;

        public bool IsOn {
            get { return isOn; }
            set
            {
                isOn = value;
                this.Invalidate();
            }
        }

        public IndicatorLight()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw
                          | ControlStyles.OptimizedDoubleBuffer
                          | ControlStyles.AllPaintingInWmPaint, true);
            this.Width = 30;
            this.Height = 30;

            if (this.DesignMode) {
                this.IsOn = false;  // 或預設狀態
                this.Invalidate();
            }
        }

        public void SetIsOn(bool state)
        {
            IsOn = state;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(2, 2, this.Width - 4, this.Height - 4);

            // 畫外框底色 (背底)
            g.FillEllipse(Brushes.Gray, rect);

            // 產生漸層畫刷
            using (System.Drawing.Drawing2D.PathGradientBrush brush = new System.Drawing.Drawing2D.PathGradientBrush(new PointF[]
            {
            new PointF(rect.Left, rect.Top),
            new PointF(rect.Right, rect.Top),
            new PointF(rect.Right, rect.Bottom),
            new PointF(rect.Left, rect.Bottom)
            })) {
                brush.CenterPoint = new PointF(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);

                if (isOn) {
                    // 綠燈設定
                    brush.CenterColor = Color.FromArgb(255, 180, 255, 180);  // 中心亮綠
                    brush.SurroundColors = new Color[] { Color.FromArgb(255, 0, 150, 0) }; // 外圍深綠
                }
                else {
                    // 紅燈設定
                    brush.CenterColor = Color.FromArgb(255, 255, 100, 100); // 中心淡紅
                    brush.SurroundColors = new Color[] { Color.FromArgb(255, 120, 0, 0) }; // 外圍深紅
                }

                g.FillEllipse(brush, rect);
            }

            // 畫黑色外框
            g.DrawEllipse(Pens.Black, rect);
        }
    }
}
