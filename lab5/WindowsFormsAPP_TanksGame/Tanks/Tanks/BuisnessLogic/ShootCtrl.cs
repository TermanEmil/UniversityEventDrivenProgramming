using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tanks.BuisnessLogic
{
    public class ShootCtrl : Property
    {
        public PointF ProjectileSpeed { get; set; } = new PointF(1, 1);
        public string ProjectileSprite { get; set; } = null;
        public float dmg = 0;

        public float coolDown = 1f;
        private long _lastTimeShot = -10000;
        private bool _isKeyboardControlled;

        private bool CanShoot
        {
            get
            {
                return (DateTime.Now.Ticks - _lastTimeShot) > coolDown * 10000f * 1000;
            }
        }

        public ShootCtrl(
            GameObject gmObj,
            bool isKeyboardControlled) : base(gmObj)
        {
            _isKeyboardControlled = isKeyboardControlled;
        }

        public override void Update()
        {
            base.Update();

            if (_isKeyboardControlled && Input.PressedKeys.Get(' '))
                Shoot(false);
        }

        public Projectile Shoot(bool forced = true)
        {
            if (!CanShoot)
                return null;

            var direction = new PointF(
                (float)Math.Sin(Math.PI * MyTransform.rotation / 180.0f),
                -(float)Math.Cos(Math.PI * MyTransform.rotation / 180.0f));

            var velocity = direction.Mult(ProjectileSpeed);

            var newProjectile = new Projectile(
                ProjectileSprite,
                velocity,
                0);
            
            newProjectile.transform.position = ShootPosition(direction);
            GameObject.Instantiate(newProjectile);


            _lastTimeShot = DateTime.Now.Ticks;
            return newProjectile;
        }

        private PointF ShootPosition(PointF direction)
        {
            var pos = new PointF();

            var size = gameObject.GetComponent<Mesh>().Image.Size;
            pos.X = MyTransform.position.X + size.Width / 2 + size.Width * 0.7f * direction.X;
            pos.Y = MyTransform.position.Y + size.Height / 2 + size.Height * 0.7f * direction.Y;
            return pos;
        }
    }
}
