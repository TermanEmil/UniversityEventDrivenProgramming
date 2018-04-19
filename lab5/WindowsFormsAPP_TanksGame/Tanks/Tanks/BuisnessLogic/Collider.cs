using System;
using System.Drawing;

namespace Tanks.BuisnessLogic
{
    public class Collider : Property
    {
        public Rectangle rectangle;
        public EventHandler<Collider> OnColliding;
        public bool isTrigger = false;
        public bool isStatic = false;
        
        public Collider(GameObject gmObj) : base(gmObj)
        {
        }

        public override void Update()
        {
            base.Update();

            if (OnColliding == null)
                return;

            var testingRect = new Rectangle(
                Point.Round(MyTransform.position),
                rectangle.Size);

            foreach (var collider in ColliderCtrl.Instance.colliders)
            {
                if (collider == this)
                    continue;

                var rect = new Rectangle(
                    Point.Round(collider.MyTransform.position),
                    collider.rectangle.Size);

                if (testingRect.IntersectsWith(rect))
                    OnColliding.Invoke(this, collider);
            }
        }
    }
}
