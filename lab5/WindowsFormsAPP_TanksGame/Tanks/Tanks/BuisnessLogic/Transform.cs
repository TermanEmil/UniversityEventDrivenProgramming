using System.Drawing;

namespace Tanks.BuisnessLogic
{
    public class Transform : Property
    {
        public PointF position = new PointF(0, 0);
        public PointF scale = new PointF(1, 1);
        public float rotation = 0;

        public Transform(GameObject gmObj) : base(gmObj)
        {
        }

        public void Translate(PointF vect)
        {
            position.X += vect.X;
            position.Y += vect.Y;
        }
    }
}
