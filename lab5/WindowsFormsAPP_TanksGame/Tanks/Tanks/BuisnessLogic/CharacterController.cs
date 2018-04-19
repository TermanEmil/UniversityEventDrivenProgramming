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
        private bool _lastMovWasVert = true;

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
                    if (_lastMovWasVert)
                        x = 0;
                    else
                        y = 0;
                }

                if (x != 0)
                    _lastMovWasVert = false;
                else if (y != 0)
                    _lastMovWasVert = true;

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

            var mvAxis = MovementAxix;
            var delta = new PointF(
                mvAxis.X * Speed.X * (float)Timer.DeltaTime,
                mvAxis.Y * Speed.Y * (float)Timer.DeltaTime);

            if (mvAxis.X > 0)
                MyTransform.rotation = 90;
            else if (mvAxis.X < 0)
                MyTransform.rotation = -90;
            else if (mvAxis.Y > 0)
                MyTransform.rotation = 180;
            else if (mvAxis.Y < 0)
                MyTransform.rotation = 0;

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
