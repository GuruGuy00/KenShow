using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenShow
{
    public class PieChartData
    {
        public string Name { get; set; }
        public float Value { get; set; }

        public PieChartData() { }

        public PieChartData(string name, float value)
        {
            this.Name = name;
            this.Value = value;
        }

    }
}
