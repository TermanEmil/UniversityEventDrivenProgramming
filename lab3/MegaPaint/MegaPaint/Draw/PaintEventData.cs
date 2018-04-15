using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MegaPaint.Draw
{
    public class PaintEventData
    {
        public Action<PaintEventArgs, PaintEventData> action;
        public Point[] points;
        public int width;
        public Color color;
        public float scale = 1;
        public float xFocus = 0, yFocus = 0;
        public PictureBox pictureBox;

        public Pen DefaultPen
        {
            get
            {
                return new Pen(color)
                {
                    Width = width * scale
                };
            }
        }

        public void Draw(PaintEventArgs e, float zoom, float deltaX, float deltaY)
        {
            scale *= zoom;
            xFocus += deltaX;
            yFocus += deltaY;
            action.Invoke(e, this);
        }
    }
}
