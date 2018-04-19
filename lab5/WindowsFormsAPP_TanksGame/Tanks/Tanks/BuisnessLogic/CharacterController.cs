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
                
                x += pressedControlls['A'] ? -1 : 0;
                x += pressedControlls['D'] ? 1 : 0;

                y += pressedControlls['W'] ? -1 : 0;
                y += pressedControlls['S'] ? 1 : 0;
                
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

        public PointF Speed { get; set; }
        
        public Dictionary<int, bool> pressedControlls = new Dictionary<int, bool>();
        private Mesh _myMesh;

        public CharacterController(
            GameObject gameObject,
            Control control = null) : base(gameObject)
        {
            if (control != null)
            {
                control.KeyUp += _OnKeyUp;
                control.KeyPress += _OnKeyPress;
            }

            pressedControlls.Add('A', false);
            pressedControlls.Add('W', false);
            pressedControlls.Add('D', false);
            pressedControlls.Add('S', false);

            _myMesh = gameObject.GetComponent<Mesh>();
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

        private void _OnKeyPress(object sender, KeyPressEventArgs e)
        {
            pressedControlls[e.KeyChar.ToString().ToUpper()[0]] = true;
        }
        
        private void _OnKeyUp(object sender, KeyEventArgs e)
        {
            pressedControlls[e.KeyValue] = false;
        }
    }
}
