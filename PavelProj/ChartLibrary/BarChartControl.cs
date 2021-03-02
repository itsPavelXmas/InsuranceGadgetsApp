using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChartLibrary
{
    public partial class BarChartControl : Control
    {
        public BarChartValue[] Data { get; set; }

        public BarChartControl()
        {
            Data = new BarChartValue[]
            {
                new BarChartValue("",3),
                new BarChartValue("",7)
        };

        InitializeComponent();
       
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics graphics = e.Graphics;
            Rectangle clipRectangle = e.ClipRectangle;

            // determine the width of the bars
             var barWidth = clipRectangle.Width / Data.Length;
            //compute the maximum bar height
            var maxBarHeight = clipRectangle.Height * 0.9;
            //compute the scaling factor based on the maximum value that we want to represent
            var scalingFactor = maxBarHeight / Data.Max(x => x.Value);

            Brush redBrush = new SolidBrush(Color.Gold);

            for (int i = 0; i < Data.Length; i++)
            {
                var barHeight = Data[i].Value * scalingFactor;

                graphics.FillRectangle(
                    redBrush,
                    i * barWidth,
                    (float)(clipRectangle.Height - barHeight),
                    (float)(0.8 * barWidth),
                    (float)barHeight);
            }
        }


    }
}
