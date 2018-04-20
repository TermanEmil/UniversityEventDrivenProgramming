using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.BuisnessLogic.Concrete
{
    public class HollywoodEffect : GameObject
    {
        private float lifeTime;
        private long _spawnTimeTicks;
        private Mesh mesh;

        public HollywoodEffect(PointF pos, float effectTime, PointF scale)
        {
            lifeTime = effectTime;
            _spawnTimeTicks = DateTime.Now.Ticks;
            
            transform.scale = scale;
            
            mesh = new Mesh(
                this,
                GameSettings.rootPath + GameSettings.boomEffect,
                GameController.instance.mainGraphics);

            transform.position.X = pos.X - mesh.Image.Width / 2 * transform.scale.X;
            transform.position.Y = pos.Y - mesh.Image.Height / 2 * transform.scale.Y;
        }

        public override void Update()
        {
            if (DateTime.Now.Ticks > _spawnTimeTicks + (long)(lifeTime * 1000 * 10000))
                GameObject.Destroy(this);
            base.Update();
        }
    }
}
