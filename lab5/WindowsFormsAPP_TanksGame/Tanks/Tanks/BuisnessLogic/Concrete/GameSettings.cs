using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.BuisnessLogic.Concrete
{
    public static class GameSettings
    {
        public static readonly string rootPath = "..\\..\\";
        public static readonly string playerSprite = "imgs\\TankPlayer.png";
        public static readonly string enemySprite = "imgs\\TankEnemy.png";

        public static readonly string playerProjectile = "imgs\\ProjectilePlayer.png";
        public static readonly string enemyProjectile = "imgs\\ProjectileEnemy.png";

        public static readonly string boomEffect = "imgs\\Boom.png";

        public static PointF enemySpeed = new PointF(13.0f, 13.0f);
        public static PointF playerSpeed = new PointF(15.0f, 15.0f);

        public static PointF enemyProjectileSpeed = new PointF(50.0f, 50.0f);
        public static PointF playerProjectileSpeed = new PointF(60.0f, 60.0f);

        public static float playerShootCD = 0.6f;
        public static float enemyShootCD = 0.8f;

        public static float enemyDmg = 100;
        public static float playerDmg = 100;

        public static string GetPath(string partialPath)
        {
            return rootPath + partialPath;
        }
    }
}
