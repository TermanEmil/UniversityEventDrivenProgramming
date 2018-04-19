using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.BuisnessLogic
{
    public class Projectile : GameObject
    {
        public float Dmg { get; }
        public PointF Velocity { get; }

        public Projectile(
            string sprite,
            PointF velocity,
            float dmg)
        {
            Dmg = dmg;
            Velocity = velocity;
            new Mesh(this, sprite, GameController.instance.mainGraphics);
        }

        public override void Update()
        {
            transform.position.X += Velocity.X * (float)Timer.DeltaTime;
            transform.position.Y += Velocity.Y * (float)Timer.DeltaTime;
            base.Update();
        }
    }
}
