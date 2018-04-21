using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tanks.BuisnessLogic
{
    public abstract class CharacterController : Property
    {
        public bool lastMovWasVert = true;
        public Point lastMovement = new Point();

        public Point MovementAxis(bool left, bool right, bool up, bool down)
        {
            int x = 0;
            int y = 0;

            x += left ? -1 : 0;
            x += right ? 1 : 0;

            y += up ? -1 : 0;
            y += down ? 1 : 0;

            if (y != 0 && x != 0)
            {
                if (lastMovWasVert)
                    x = 0;
                else
                    y = 0;
            }

            if (x != 0)
                lastMovWasVert = false;
            else if (y != 0)
                lastMovWasVert = true;

            return new Point(x, y);
        }

        public abstract Point MovementAxix { get; }

        
        public PointF Speed { get; set; }
        
        private Mesh _myMesh;
        protected ShootCtrl _shootCtrl;

        public CharacterController(
            GameObject gameObject) : base(gameObject)
        {
        }

        public override void Start()
        {
            base.Start();
            _myMesh = gameObject.GetComponent<Mesh>();
            _shootCtrl = gameObject.GetComponent<ShootCtrl>();
        }

        public override void Update()
        {
            base.Update();

            var movement = MovementAxix;
            if (movement.X != 0 || movement.Y != 0)
                Move(movement);
        }

        public void Move(Point movement)
        {
            var delta = new PointF(
                movement.X * Speed.X * (float)Timer.DeltaTime,
                movement.Y * Speed.Y * (float)Timer.DeltaTime);

            if (movement.X > 0)
                MyTransform.rotation = 90;
            else if (movement.X < 0)
                MyTransform.rotation = -90;
            else if (movement.Y > 0)
                MyTransform.rotation = 180;
            else if (movement.Y < 0)
                MyTransform.rotation = 0;

            lastMovement = movement;
            MyTransform.Translate(delta);
        }
    }
}
