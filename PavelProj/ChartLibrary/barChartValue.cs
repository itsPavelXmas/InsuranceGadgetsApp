using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartLibrary
{
    public class BarChartValue
    {
         public string Label { get; set; }
        public double Value { get; set; }

        public BarChartValue(string label, double value)
        {
            Label = label;
            Value = value;
        }
    }
}
