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

        private float playerColliderWidth;
        private float playerColliderHeight;

        private Random getRandom = new Random();

  

        private Point lastMovement = new Point(0, 0);
        public long reactionTime = 7000000; // in ticks
        private long lastTimeReacted = 0;
        private bool ItsTimeToReact
        {
            get
            {
                if (DateTime.Now.Ticks - lastTimeReacted >= reactionTime)
                {
                    lastTimeReacted = DateTime.Now.Ticks;
                    return true;
                } else
                return false;
            }
        }
        
        private bool IsAllignedForShot
        {
           get
            {
                if (_thisTankTransform.rotation == 0.0f)
                    return
                        _playerPos.Y < _thisTankTransform.position.Y
                        &&
                        Math.Abs(_playerPos.X - _thisTankTransform.position.X) <= playerColliderWidth / 2;
                else if (_thisTankTransform.rotation == 90.0f)
                    return Math.Abs(_playerPos.Y - _thisTankTransform.position.Y) <= playerColliderHeight / 2
                        && _playerPos.X > _thisTankTransform.position.X;
                else if (_thisTankTransform.rotation == 180.0f)
                    return _playerPos.Y > _thisTankTransform.position.Y
                        && Math.Abs(_playerPos.X - _thisTankTransform.position.X) <= playerColliderWidth / 2;
                else if (_thisTankTransform.rotation == 270.0f)
                    return _playerPos.X < _thisTankTransform.position.X &&
                        Math.Abs(_playerPos.Y - _thisTankTransform.position.Y) <= playerColliderHeight;
                return false;
            }
        }

        public override Point MovementAxix
        {
            get
            {
                if (ItsTimeToReact)
                {
                    float verticalDiff = _thisTankTransform.position.Y - _playerPos.Y;
                    float horizontalDiff = _thisTankTransform.position.X - _playerPos.X;

                    int right = 0;
                    int up = 0;

                    if (getRandom.Next(1) == 1 ? Math.Abs(verticalDiff) < Math.Abs(horizontalDiff) : Math.Abs(verticalDiff) > Math.Abs(horizontalDiff))
                    {
                        up = verticalDiff > 0 ? 1 : -1;
                    }
                    else
                    {
                        right = horizontalDiff > 0 ? -1 : 1;
                    }
                    if (IsAllignedForShot)
                        return new Point(0, 0);
                    lastMovement = MovementAxis(right < 0, right > 0, up > 0, up < 0);
                    return lastMovement;
                }else return lastMovement;
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
            playerColliderWidth = GameController.instance.player.gameObject.GetComponent<Collider>().rectangle.Width;
            playerColliderHeight = GameController.instance.player.gameObject.GetComponent<Collider>().rectangle.Height;
        }

        
        public override void Update()
        {
            base.Update();
            if (IsAllignedForShot)
                _schut.Shoot(false);
        }
    }
}
