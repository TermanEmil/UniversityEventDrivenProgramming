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
        public PointF Speed { get; set; }

        public CharacterController(
            GameObject gameObject,
            Control control) : base(gameObject)
        {
            control.KeyDown += OnKeyDown;
            control.KeyUp += OnKeyUp;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            var delta = new PointF();
            switch (e.KeyValue)
            {
                case 'W':
                    delta.Y = (float)Timer.DeltaTime * -Speed.Y;
                    break;
                case 'A':
                    delta.X = (float)Timer.DeltaTime * -Speed.X;
                    break;
                case 'S':
                    delta.Y = (float)Timer.DeltaTime * Speed.Y;
                    break;
                case 'D':
                    delta.X = (float)Timer.DeltaTime * Speed.X;
                    break;
                default:
                    return;
            }
            MyTransform.Translate(delta);
            GameController.instance.repaintRequired = true;
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {

        }
    }
}
