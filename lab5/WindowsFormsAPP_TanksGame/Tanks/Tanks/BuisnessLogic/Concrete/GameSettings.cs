using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.BuisnessLogic.Concrete
{
    public interface ITankSettings
    {
        string Sprite { get; set; }
        string ProjectileSprite { get; set; }
        PointF MovementSpeed { get; set; }
        PointF ProjectileSpeed { get; set; }
        float ShootCD { get; set; }
        float Dmg { get; set; }
        float HP { get; set; }
        float HPRegen { get; set; }
    }

    public class PlayerTankSettings : ITankSettings
    {
        public string Sprite { get; set; } = "imgs\\TankPlayer.png";
        public string ProjectileSprite { get; set; } = "imgs\\ProjectilePlayer.png";
        public PointF MovementSpeed { get; set; } = new PointF(15.0f, 15.0f);
        public PointF ProjectileSpeed { get; set; } = new PointF(60.0f, 60.0f);
        public float ShootCD { get; set; } = 0.6f;
        public float Dmg { get; set; } = 100;
        public float HP { get; set; } = 600;
        public float HPRegen { get; set; } = 2;
    }

    public class EnemyTankSettings : ITankSettings
    {
        public string Sprite { get; set; } = "imgs\\TankEnemy.png";
        public string ProjectileSprite { get; set; } = "imgs\\ProjectileEnemy.png";
        public PointF MovementSpeed { get; set; } = new PointF(13.0f, 13.0f);
        public PointF ProjectileSpeed { get; set; } = new PointF(50.0f, 50.0f);
        public float ShootCD { get; set; } = 2f;
        public float Dmg { get; set; } = 100;
        public float HP { get; set; } = 200;
        public float HPRegen { get; set; } = 0;
    }

    public static class GameSettings
    {
        public static readonly string rootPath = "..\\..\\";
        public static readonly string boomEffect = "imgs\\Boom.png";

        public static int enemyPoints = 100;
    }
}
