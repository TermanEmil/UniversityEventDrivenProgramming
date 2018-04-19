using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.BuisnessLogic.Concrete;

namespace Tanks.BuisnessLogic
{
    public class Projectile : GameObject
    {
        public float Dmg { get; }
        public PointF Velocity { get; }
        public float lifeTime = 2f;
        private long _spawnTimeTicks;

        public Projectile(
            string sprite,
            PointF velocity,
            float dmg)
        {
            Dmg = dmg;
            Velocity = velocity;
            new Mesh(this, sprite, GameController.instance.mainGraphics);
            _spawnTimeTicks = DateTime.Now.Ticks;

            var collider = new Collider(this)
            {
                isTrigger = true
            };
            collider.OnColliding += OnCollide;
        }

        public override void Update()
        {
            base.Update();
            transform.position.X += Velocity.X * (float)Timer.DeltaTime;
            transform.position.Y += Velocity.Y * (float)Timer.DeltaTime;

            if (DateTime.Now.Ticks > _spawnTimeTicks + (long)(lifeTime * 1000 * 10000))
                GameObject.Destroy(this);  
        }

        private void OnCollide(object sender, Collider other)
        {
            var effectPos = transform.position;
            effectPos.X -= Velocity.X * (float)Timer.DeltaTime;
            effectPos.Y -= Velocity.Y * (float)Timer.DeltaTime;

            GameObject.Instantiate(
                new HollywoodEffect(
                    effectPos,
                    effectTime: 0.4f,
                    scale: new PointF(0.2f, 0.2f)));
            GameObject.Destroy(this);
        }
    }
}
