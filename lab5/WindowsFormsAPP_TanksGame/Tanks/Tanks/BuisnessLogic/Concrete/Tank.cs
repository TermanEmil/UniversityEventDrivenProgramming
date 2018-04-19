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

            var collider = new Collider(this)
            {
                rectangle = new Rectangle(
                    Point.Round(transform.position),
                    new Size(tankMesh.Image.Width, tankMesh.Image.Height))
            };
            collider.OnColliding += OnColliding;
        }

        private void OnColliding(object sender, Collider other)
        {
            if (other.isTrigger || MyCollider.isTrigger)
                return;

            var charCtrl = GetComponent<CharacterController>();

            if (charCtrl == null)
                return;

            if (charCtrl.lastMovement.X != 0)
            {
                transform.position.X = GetCollisionPos(
                    other.MyTransform.position.X, other.rectangle.Size.Width * other.MyTransform.scale.X,
                    transform.position.X, MyCollider.rectangle.Width * transform.scale.X);
            }

            if (charCtrl.lastMovement.Y != 0)
            {
                transform.position.Y = GetCollisionPos(
                    other.MyTransform.position.Y, other.rectangle.Size.Height * other.MyTransform.scale.Y,
                    transform.position.Y, MyCollider.rectangle.Height * transform.scale.Y);
            }
        }

        /// <summary>
        /// Gets the new axis position of obj2 when it intersects obj1.
        /// </summary>
        private static float GetCollisionPos(
            float pos1,
            float len1,
            float pos2,
            float len2)
        {
            if (Math.Abs(pos1 - (pos2 + len2)) < Math.Abs(pos1 + len1 - pos2))
                return pos1 - len2;
            else
                return pos1 + len1;
        }
    }
}
