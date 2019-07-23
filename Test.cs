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

namespace KenShow
{
    public partial class Test : UserControl
    {
        ToolTip toolTip = new ToolTip();
        private Rectangle _testRect;
        private List<Point> _TestPoly;
        

        public Test()
        {
            InitializeComponent();
            Test_A();
        }


        private void Test_A()
        {

            List<Point> points = new List<Point>();
            points.Add(new Point(50, 50));
            points.Add(new Point(60, 65));
            points.Add(new Point(40, 70));
            points.Add(new Point(50, 90));
            points.Add(new Point(30, 95));
            points.Add(new Point(20, 60));
            points.Add(new Point(40, 55));

            _TestPoly = points;

            using (GraphicsPath gp = new GraphicsPath())
            {
                gp.AddClosedCurve(points.ToArray());
                //panel1.Region = new Region(gp);
            }

            //int minX = points.Select(x => x.X).Min();
            //int minY = points.Select(x => x.Y).Min();

            //panel1.Location = new Point(minX, minY);

            //toolTip.SetToolTip(panel1, "Working?");
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            MakeSquare(e);
            FillPolygonPoint(e);
        }

        private void MakeSquare(PaintEventArgs e)
        {
            SolidBrush redBrush = new SolidBrush(Color.Red);

            Rectangle rect = new Rectangle(20, 20, 20, 20);
            _testRect = rect;
            
            e.Graphics.FillRectangle(redBrush, rect);

            //Panel panel2 = new Panel();
            //panel2.Width = rect.Width;
            //panel2.Height = rect.Height;

            //panel2.Region = new Region(rect);
            //toolTip.SetToolTip(panel2, "Red Square");


        }

        public void FillPolygonPoint(PaintEventArgs e)
        {

            // Create solid brush.
            SolidBrush blueBrush = new SolidBrush(Color.Blue);

            // Create points that define polygon.
            Point point1 = new Point(50, 50);
            Point point2 = new Point(60, 65);
            Point point3 = new Point(40, 70);
            Point point4 = new Point(50, 90);
            Point point5 = new Point(30, 95);
            Point point6 = new Point(20, 60);
            Point point7 = new Point(40, 55);
            Point[] curvePoints = { point1, point2, point3, point4, point5, point6, point7 };

            // Draw polygon to screen.
            e.Graphics.FillPolygon(blueBrush, curvePoints);

        }

        private void Panel1_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("Working?  Double click?");
        }

        private void Test_MouseHover(object sender, EventArgs e)
        {
            Point mousePt = System.Windows.Forms.Cursor.Position;
            Point offsetPt = mousePt;


            offsetPt.Offset(this.Location.X*-1, this.Location.Y*-1);

            label1.Text = mousePt.ToString();
            label2.Text = offsetPt.ToString();

            if (this._testRect.Contains(mousePt))
            {
                label1.Text = "_testRect";
            }else if (IsInPolygon(_TestPoly.ToArray(), mousePt))
            {
                label1.Text = "_TestPoly";
            }
            else
            {
                
            }


        }

        private void Test_MouseDown(object sender, MouseEventArgs e)
        {
            if (this._testRect.Contains(e.Location))
            {
                MessageBox.Show("_testRect Clicked");
            }
            if (IsInPolygon(_TestPoly.ToArray(), e.Location))
            {
                MessageBox.Show("_TestPoly Clicked");
            }
        }

        public static bool IsInPolygon(Point[] poly, Point p)
        {
            Point p1, p2;
            bool inside = false;

            if (poly.Length < 3)
            {
                return inside;
            }

            var oldPoint = new Point(
                poly[poly.Length - 1].X, poly[poly.Length - 1].Y);

            for (int i = 0; i < poly.Length; i++)
            {
                var newPoint = new Point(poly[i].X, poly[i].Y);

                if (newPoint.X > oldPoint.X)
                {
                    p1 = oldPoint;
                    p2 = newPoint;
                }
                else
                {
                    p1 = newPoint;
                    p2 = oldPoint;
                }

                if ((newPoint.X < p.X) == (p.X <= oldPoint.X)
                    && (p.Y - (long)p1.Y) * (p2.X - p1.X)
                    < (p2.Y - (long)p1.Y) * (p.X - p1.X))
                {
                    inside = !inside;
                }

                oldPoint = newPoint;
            }

            return inside;
        }
    }
}
