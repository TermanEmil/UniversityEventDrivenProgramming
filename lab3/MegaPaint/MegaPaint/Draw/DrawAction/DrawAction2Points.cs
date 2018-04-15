using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MegaPaint.Draw.DrawAction
{
    public abstract class DrawAction2Points : IDrawAction
    {
        public Form1 form1 { get; set; }

        protected bool isDrawing = false;
        protected Point point1;
        protected Point point2;

        protected virtual PaintEventData _PaintEventData
        {
            get
            {
                return new PaintEventData()
                {
                    width = form1.InsertedWidth,
                    color = form1.InsertedColor,
                    points = new Point[] { point1, point2 }
                };
            }
        }

        public virtual void OnMouseDown(MouseEventArgs e)
        {

            point1 = e.Location;
            point2 = e.Location;
            RefreshPainting();
            isDrawing = true;
        }

        public virtual void OnMouseUp(MouseEventArgs e = null)
        {
            var drawAction = _PaintEventData;
            drawAction.action = (paintEventArgs, drawData) =>
            {
                Draw(paintEventArgs, drawData);
            };
            form1.AddPaintAction(drawAction);
            isDrawing = false;
        }

        public virtual void OnMouseMove(MouseEventArgs e)
        {
            point2 = e.Location;
            RefreshPainting();
        }

        public void TempPaint(PaintEventArgs e)
        {
            if (!isDrawing)
                return;

            Draw(e, _PaintEventData);
        }

        protected void AvoidNegativeRectStuff(PaintEventData data)
        {
            if (data.points[0].X > data.points[1].X)
            {
                var tmp = data.points[0].X;
                data.points[0].X = data.points[1].X;
                data.points[1].X = tmp;
            }

            if (data.points[0].Y > data.points[1].Y)
            {
                var tmp = data.points[0].Y;
                data.points[0].Y = data.points[1].Y;
                data.points[1].Y = tmp;
            }
        }

        protected abstract void Draw(PaintEventArgs e, PaintEventData data);

        public virtual void OnDestroy()
        {
        }

        protected void RefreshPainting()
        {
            form1.RefreshImg();
        }
    }
}
