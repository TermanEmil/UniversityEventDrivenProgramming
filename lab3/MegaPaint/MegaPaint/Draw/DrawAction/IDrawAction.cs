using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MegaPaint.Draw.DrawAction
{
    public interface IDrawAction
    {
        Form1 form1 { get; set; }
        void OnMouseDown(MouseEventArgs e);
        void OnMouseUp(MouseEventArgs e);
        void OnMouseMove(MouseEventArgs e);
        void TempPaint(PaintEventArgs e);
        void OnDestroy();
    }
}
