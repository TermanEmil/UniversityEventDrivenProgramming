using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MegaPaint.Draw.DrawAction
{
    class DrawBezier : DrawAction2Points
    {
        private int phase = 0;
        private Point point3, point4;

        private bool isClicking = false;

        public override void OnDestroy()
        {
            form1.BezierCtrl.Visible = false;
        }

        public void Init()
        {
            form1.BezierCtrl.Click += OnBezierDone;
        }

        protected override PaintEventData _PaintEventData
        {
            get
            {
                var rs = new PaintEventData()
                {
                    width = form1.InsertedWidth,
                    color = form1.InsertedColor
                };

                if (phase == 3)
                    rs.points = new Point[] { point1, point3, point4, point2 };
                else
                    rs.points = new Point[] { point1, point1, point2, point2 };
                return rs;
            }
        }

        public override void OnMouseDown(MouseEventArgs e)
        {
            isClicking = true;

            if (phase != 3)
            {
                phase++;
                if (phase >= 4)
                    phase = 0;
            }
            

            if (phase == 1)
                base.OnMouseDown(e);
            else if (phase == 3)
            {
                form1.BezierCtrl.Visible = true;
            }

        }

        public override void OnMouseMove(MouseEventArgs e)
        {
            if (!isClicking)
                return;

            if (phase == 3)
            {
                if (form1.RadioBtn1.Checked)
                    point3 = e.Location;
                else if (form1.RadioBtn2.Checked)
                    point4 = e.Location;
            }
            else if (phase == 1)
                point2 = e.Location;

            if (phase != 3)
            {
                point3 = point1;
                point4 = point2;
            }

            RefreshPainting();
        }

        public override void OnMouseUp(MouseEventArgs e = null)
        {
            isClicking = false;
            if (phase == 1)
                phase = 2;
        }

        protected override void Draw(PaintEventArgs e, PaintEventData data)
        {
            Console.WriteLine("hey");
            e.Graphics.DrawBezier(
                data.DefaultPen,
                data.points[0],
                data.points[1],
                data.points[2],
                data.points[3]);
        }

        public void OnBezierDone(object sender, EventArgs e)
        {
            form1.BezierCtrl.Visible = false;
            
            base.OnMouseUp();
            phase = 0;
        }

        public override string ToString()
        {
            return "Bezier";
        }
    }
}
