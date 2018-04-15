using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MegaPaint.Draw.DrawAction
{
    class DrawActionPie : DrawAction2Points
    {
        private const float rotationScale = 3;
        private Point anglePoint;

        // 0 = nothing
        // 1 = the ellipse
        // 2 = wait for click
        // 3 = the angles
        private int phase = 0;

        protected override PaintEventData _PaintEventData
        {
            get
            {
                var rs = new PaintEventData()
                {
                    width = form1.InsertedWidth,
                    color = form1.InsertedColor,
                };
                if (phase >= 3)
                    rs.points = new Point[] { point1, point2, anglePoint };
                else
                    rs.points = new Point[] { point1, point2 };
                return rs;
            }
        }

        public override void OnMouseDown(MouseEventArgs e)
        {
            phase++;
            if (phase >= 4)
                phase = 0;

            if (phase == 1)
                base.OnMouseDown(e);
        }

        public override void OnMouseMove(MouseEventArgs e)
        {
            if (phase == 0)
                return;

            if (phase == 1)
                base.OnMouseMove(e);
            else if (phase == 3)
            {
                anglePoint = e.Location;
                RefreshPainting();
            }
        }

        public override void OnMouseUp(MouseEventArgs e)
        {
            switch (phase)
            {
                case 0:
                    return;
                case 1:
                    phase++;
                    break;
                case 3:
                    base.OnMouseUp(e);
                    phase = 0;
                    break;
            }
        }

        protected override void Draw(PaintEventArgs e, PaintEventData data)
        {
            if (data.points[0].X == data.points[1].X || data.points[0].Y == data.points[1].Y)
                return;

            AvoidNegativeRectStuff(data);
            var width = data.points[1].X - data.points[0].X;
            var height = data.points[1].Y - data.points[0].Y;
            
            var rect = new RectangleF(
                data.points[0].X,
                data.points[0].Y,
                width,
                height);

            // Create start and sweep angles.
            GetAngles(data, out float startAngle, out float sweepAngle);

            // Draw pie to screen.
            e.Graphics.DrawPie(
                data.DefaultPen,
                rect,
                startAngle,
                sweepAngle);
        }

        private void GetAngles(PaintEventData data, out float startAngle, out float sweepAngle)
        {
            if (data.points.Length == 2)
            {
                startAngle = 0;
                sweepAngle = 360;
            }
            else
            {
                startAngle = GetAngle(data.points[0].X, data.points[2].X);
                sweepAngle = GetAngle(data.points[0].Y, data.points[2].Y);
            }
        }

        private float GetAngle(float origin, float p)
        {
            return (p - origin) * rotationScale;
        }

        public override string ToString()
        {
            return "Pie";
        }
    }
}
