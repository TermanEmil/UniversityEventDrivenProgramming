using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tanks.BuisnessLogic
{
    public class CharacterController : Property
    {
        public bool lastMovWasVert = true;
        public Point lastMovement = new Point();

        public Point MovementAxix
        {
            get
            {
                int x = 0;
                int y = 0;
                
                x += Input.PressedKeys.Get('A') ? -1 : 0;
                x += Input.PressedKeys.Get('D') ? 1 : 0;

                y += Input.PressedKeys.Get('W') ? -1 : 0;
                y += Input.PressedKeys.Get('S') ? 1 : 0;
                
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
        }

        public bool IsKeyboardControlled { get; set; }
        public PointF Speed { get; set; }
        
        private Mesh _myMesh;

        public CharacterController(
            GameObject gameObject,
            bool isKeyboardControlled = false) : base(gameObject)
        {
            IsKeyboardControlled = isKeyboardControlled;
            _myMesh = gameObject.GetComponent<Mesh>();
        }

        public override void Update()
        {
            base.Update();

            if (!IsKeyboardControlled)
                return;

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
