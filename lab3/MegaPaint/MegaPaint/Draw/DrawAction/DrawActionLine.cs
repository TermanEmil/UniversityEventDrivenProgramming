using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MegaPaint.Draw.DrawAction
{
    public class DrawActionLine : DrawAction2Points
    {
        protected override void Draw(PaintEventArgs e, PaintEventData data)
        {
            e.Graphics.DrawLine(
                data.DefaultPen,
                data.points[0],
                data.points[1]);
        }

        public override string ToString()
        {
            return "Line";
        }
    }
}
