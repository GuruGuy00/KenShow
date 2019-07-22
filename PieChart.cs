using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KenShow
{
    public partial class PieChart : UserControl
    {
        private List<PieChartData> defaultPieChartData = new List<PieChartData>();
        private List<PieChartData> pieChartData = new List<PieChartData>();
        private List<Brush> themeBlue = new List<Brush>();

        public PieChart()
        {
            InitializeComponent();
            LoadBrush();
            LoadDefaultData();
        }

        private void PieChart_Load(object sender, EventArgs e)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {

            if (pieChartData.Count == 0)
            {
                pieChartData = defaultPieChartData;
            }

            Pen blackPen = new Pen(new SolidBrush(Color.Black), 5);

            int buffer = 10;

            int width = this.Width;
            int height = this.Height;

            //panel1.SendToBack();

            if (width > height)
            {
                width = height;
            }
            else
            {
                height = width;
            }

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            base.OnPaint(e);

            //Rectangle to draw the circle in
            int newWidth = width - (buffer*2);   // 100
            int newHight = height - (buffer*2);  // 100

            Rectangle rect = new Rectangle(buffer, buffer, newWidth, newHight);

            //e.Graphics.DrawRectangle(blackPen, rect);

            //Create a boarder for the circle
            e.Graphics.DrawArc(new Pen(new SolidBrush(Color.Black), 5), rect, 90, 360);
            //e.Graphics.DrawArc(new Pen(new SolidBrush(Color.White), 10), rect, 270, 180);

            //draws the pie segments 
            float totalValue = (from x in pieChartData select x.Value).Sum();
            int i = 0;
            float startAngle = 0;
            float sweepAngle = 0;

            foreach (PieChartData pieData in pieChartData)
            {
                sweepAngle = (pieData.Value / totalValue) * 360f ;

                e.Graphics.FillPie(themeBlue[i++], rect, startAngle, sweepAngle);

                if (themeBlue.Count == i)
                {
                    i = 0;
                }

                startAngle += sweepAngle;
            }

            //e.Graphics.FillPie(themeBlue[0], rect, 0, 18);
            //e.Graphics.FillPie(themeBlue[1], rect, 18, 90);
            //e.Graphics.FillPie(themeBlue[2], rect, 108, 198);
            //e.Graphics.FillPie(themeBlue[3], rect, 306, 46.8f);
            //e.Graphics.FillPie(themeBlue[4], rect, 352.8f, 7.2f);


            //e.Graphics.DrawEllipse(new Pen(new SolidBrush(Color.Red), 10), rect);

            //rect.Inflate(-5, -5);

        }

        private void LoadDefaultData()
        {
            defaultPieChartData.Add(new PieChartData("Value 1", 5));
            defaultPieChartData.Add(new PieChartData("Value 2", 25));
            defaultPieChartData.Add(new PieChartData("Value 3", 55));
            defaultPieChartData.Add(new PieChartData("Value 4", 13));
            defaultPieChartData.Add(new PieChartData("Value 5", 2));

            //if (pieChartData.Count == 0)
            //{
            //    pieChartData = defaultPieChartData;
            //}
        }

        private void PieChart_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        public void AddData (string name, float value)
        {
            pieChartData.Add(new PieChartData(name, value));
        }

        private void LoadBrush()
        {
            themeBlue.Add(Brushes.Red);
            themeBlue.Add(Brushes.Orange);
            themeBlue.Add(Brushes.Yellow);
            themeBlue.Add(Brushes.Green);
            themeBlue.Add(Brushes.Blue);
            themeBlue.Add(Brushes.Indigo);
            themeBlue.Add(Brushes.Violet);
        }
    }
}
