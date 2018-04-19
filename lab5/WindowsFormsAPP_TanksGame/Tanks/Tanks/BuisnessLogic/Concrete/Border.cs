using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.BuisnessLogic.Concrete
{
    public class Border : GameObject
    {
        private Rectangle _rect;

        public Border(PointF p1, PointF p2)
        {
            transform.position = p1;
            var size = new Size((int)Math.Abs(p1.X - p2.X), (int)Math.Abs(p1.Y - p2.Y));

            _rect = new Rectangle(
                    Point.Round(transform.position),
                    size);

            var collider = new Collider(this)
            {
                rectangle = _rect
            };
            //_rect.Offset(Point.Round(p1));

            var mesh = new Mesh(this, GameController.instance.mainGraphics)
            {
                draw = (graphics) =>
                {
                    graphics.DrawRectangle(
                        new Pen(Color.Black) { Width = -10 },
                        _rect);
                }
            };
        }   
    }
}
