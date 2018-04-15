using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MegaPaint.Draw.DrawAction
{
    class DrawActionEllipse : DrawAction2Points
    {
        protected override void Draw(PaintEventArgs e, PaintEventData data)
        {
            var width = data.points[1].X - data.points[0].X;
            var height = data.points[1].Y - data.points[0].Y;
            
            e.Graphics.DrawEllipse(
                data.DefaultPen,
                data.points[0].X + data.xFocus,
                data.points[0].Y + data.yFocus,
                width * data.scale,
                height * data.scale);
        }

        public override string ToString()
        {
            return "Ellipse";
        }
    }
}
