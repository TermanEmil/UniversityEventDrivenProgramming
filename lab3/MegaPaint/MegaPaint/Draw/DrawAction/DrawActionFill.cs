using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MegaPaint.Draw.DrawAction
{
    class DrawActionFill : IDrawAction
    {
        public Form1 form1 { get; set; }

        private bool isProcessing = false;
        private Point clickPoint;

        PaintEventData _PaintEventData
        {
            get
            {
                return new PaintEventData()
                {
                    points = new Point[] { clickPoint },
                    color = form1.InsertedColor,
                    pictureBox = form1.MainImage,
                    action = Draw
                };
            }
        }

        public void OnMouseDown(MouseEventArgs e)
        {
            clickPoint = e.Location;
            form1.RefreshImg();
            isProcessing = true;
        }

        public void OnMouseUp(MouseEventArgs e)
        {
            isProcessing = false;
            form1.AddPaintAction(_PaintEventData);
        }

        public void OnMouseMove(MouseEventArgs e)
        {
            clickPoint = e.Location;
            form1.RefreshImg();
        }

        public void TempPaint(PaintEventArgs e)
        {
            if (isProcessing)
                Draw(e, _PaintEventData);
        }

        public void OnDestroy()
        {

        }

        private void Draw(PaintEventArgs e, PaintEventData data)
        {
            var graphics = data.pictureBox.CreateGraphics();
            //var bitmap = new Bitmap(data.pictureBox.Image);
            //var areaColor = bitmap.GetPixel(data.points[0].X, data.points[0].Y);

            //ColorBitmap(bitmap, areaColor, data.color, data.points[0].X, data.points[0].Y);
            //data.pictureBox.Image = bitmap;
        }

        private void ColorBitmap(Bitmap target, Color areaColor, Color targetColor, int x, int y)
        {
            if (target.GetPixel(x, y).Equals(areaColor))
                target.SetPixel(x, y, targetColor);

            if (x > 0)
                ColorBitmap(target, areaColor, targetColor, x - 1, y);
            if (x < target.Width - 1)
                ColorBitmap(target, areaColor, targetColor, x + 1, y);
            if (y > 0)
                ColorBitmap(target, areaColor, targetColor, x, y - 1);
            if (y < target.Height - 1)
                ColorBitmap(target, areaColor, targetColor, x, y + 1);
        }

        public override string ToString()
        {
            return "Fill";
        }
    }
}
