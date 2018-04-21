using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Tanks.BuisnessLogic.Concrete
{
    class EnemyCharacterController : CharacterController
    {
        private Transform _playerTransform;
        private PointF _playerPos
        {
            get { return _playerTransform.position; }
        }
        private ShootCtrl _schut;
        private Transform _thisTankTransform;

        private float victimColliderWidth;
        private float victimColliderHeight;


        private bool AllignedForShot(Transform shooter, PointF victim)
        {
            if (shooter.rotation == 0.0f)
                return
                    victim.Y < shooter.position.Y
                    &&
                    Math.Abs(victim.X - shooter.position.X) <= victimColliderWidth / 2;
            else if (shooter.rotation == 90.0f)
                return Math.Abs(victim.Y - shooter.position.Y) <= victimColliderHeight / 2
                    && victim.X > shooter.position.X;
            else if (shooter.rotation == 180.0f)
                return victim.Y > shooter.position.Y
                    && Math.Abs(victim.X - shooter.position.X) <= victimColliderWidth / 2;
            else if (shooter.rotation == 270.0f)
                return victim.X < shooter.position.X &&
                    Math.Abs(victim.Y - shooter.position.Y) <= victimColliderHeight;
            return false;
        }

        public override Point MovementAxix
        {
            get
            {
                float verticalDiff = _thisTankTransform.position.Y - _playerPos.Y;
                float horizontalDiff = _thisTankTransform.position.X - _playerPos.X;

                int right = 0;
                int up = 0;

                if (Math.Abs(verticalDiff) > Math.Abs(horizontalDiff))
                {
                    up = verticalDiff > 0 ? 1 : -1;
                } else
                {
                    right = horizontalDiff > 0 ? -1 : 1;
                }
                if (AllignedForShot(_thisTankTransform, _playerPos))
                    return new Point(0, 0);
                return MovementAxis(right < 0, right > 0, up > 0, up < 0);
            }
        }



        public EnemyCharacterController(GameObject gmObj) : base(gmObj)
        {
            
        }
        public override void Start()
        {
            _playerTransform = GameController.instance.player.gameObject.GetComponent<Transform>();
            _schut = gameObject.GetComponent<ShootCtrl>();
            _thisTankTransform = gameObject.GetComponent<Transform>();
            victimColliderWidth = GameController.instance.player.gameObject.GetComponent<Collider>().rectangle.Width;
            victimColliderHeight = GameController.instance.player.gameObject.GetComponent<Collider>().rectangle.Height;
        }

        
        public override void Update()
        {
            base.Update();
            if (AllignedForShot(_thisTankTransform, _playerPos))
                _schut.Shoot(false);
        }
    }
}
