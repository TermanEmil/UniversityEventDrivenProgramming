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
            return;
            var graphics = data.pictureBox.CreateGraphics();
            var bitmap = new Bitmap(data.pictureBox.Width, data.pictureBox.Height, graphics);
            var areaColor = bitmap.GetPixel(data.points[0].X, data.points[0].Y);
            
            ColorBitmap(bitmap, areaColor, data.color, data.points[0].X, data.points[0].Y);
            data.pictureBox.Image = bitmap;
        }

        private void ColorBitmap(Bitmap target, Color areaColor, Color targetColor, int x, int y)
        {
            if (areaColor.Equals(targetColor))
                return;
            
            var points = new List<Point>();
            points.Add(new Point(x, y));

            while (points.Any())
            {
                var point = points.First();
                points.Remove(point);

                var pixel = target.GetPixel(point.X, point.Y);
                //Console.WriteLine(pixel + " / " + areaColor + pixel.Equals(areaColor));
                if (!pixel.Equals(areaColor))
                    continue;
               
                target.SetPixel(point.X, point.Y, targetColor);
                
                if (point.X > 0)
                    points.Add(new Point(point.X - 1, point.Y));
                /*if (point.Y > 0)
                    points.Add(new Point(point.X, point.Y - 1));
                if (point.X <target.Width - 1)
                    points.Add(new Point(point.X + 1, point.Y));
                if (point.Y < target.Height - 1)
                    points.Add(new Point(point.X, point.Y + 1));
                */
                //Console.WriteLine(points.Count);
            }


            //Console.WriteLine(points.Count);
        }

        public override string ToString()
        {
            return "Fill";
        }
    }
}
