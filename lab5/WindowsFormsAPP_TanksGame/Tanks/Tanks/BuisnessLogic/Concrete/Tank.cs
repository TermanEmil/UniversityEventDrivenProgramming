using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tanks.BuisnessLogic.Concrete
{
    public class Tank : GameObject
    {
        public Tank(string spritePath, PointF speed, bool isAI = false)
        {
            var tankMesh = new Mesh(
                this,
                spritePath,
                GameController.instance.mainGraphics);

            var keyboardCtrl = isAI ? null : GameController.instance.mainKeyboardCtrl;

            var charCtrl = new CharacterController(this, keyboardCtrl)
            {
                Speed = speed
            };
        }
    }
}
