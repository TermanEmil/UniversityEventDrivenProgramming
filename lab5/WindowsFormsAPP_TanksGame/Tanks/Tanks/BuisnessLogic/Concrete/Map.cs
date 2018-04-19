using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.BuisnessLogic.Concrete
{
    public class Map : GameObject
    {
        public float width;
        public float height;

        public Map(float width_, float height_)
        {
            InitGameBorders();
            width = width_;
            height = height_;

            InitGameBorders();
        }

        private void InitGameBorders()
        {
            GameObject.Instantiate(new Border(
                new PointF(0, -100),
                new PointF(width, 0)));

            GameObject.Instantiate(new Border(
                new PointF(-100, 0),
                new PointF(0, height)));

            GameObject.Instantiate(new Border(
                new PointF(width - 1, 0),
                new PointF(width, height + 100)));

            GameObject.Instantiate(new Border(
                new PointF(0, height - 1),
                new PointF(width + 100, height)));
        }
    }
}
