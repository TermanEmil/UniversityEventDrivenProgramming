using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MegaPaint.Draw.DrawAction
{
    class DrawActionRect : DrawAction2Points
    {
        protected override void Draw(PaintEventArgs e, PaintEventData data)
        {
            AvoidNegativeRectStuff(data);
            var width = data.points[1].X - data.points[0].X;
            var height = data.points[1].Y - data.points[0].Y;
            
            e.Graphics.DrawRectangle(
                data.DefaultPen,
                data.points[0].X,
                data.points[0].Y,
                width,
                height);
            
        }

        public override string ToString()
        {
            return "Rectangle";
        }
    }
}
