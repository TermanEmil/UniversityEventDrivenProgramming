using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.BuisnessLogic.Concrete
{
    class EnemySpawner
    {
        static EnemySpawner _instance;
        static public EnemySpawner Instance
        {
            get
            {
                return _instance;
            }
        }

        public int maxNumOfEnemies = 5;
        private Random _getRand = new Random();
        private Size _gameSize;

        public EnemySpawner(Size gameSize)
        {
            _instance = this;
            _gameSize = gameSize;
        }

        public void Update()
        {
            Tank toBeSpawned;
            if (enemyTanks.Count < maxNumOfEnemies)
            {
                toBeSpawned = new Tank(isAI: true);
                var pos = new Point(
                    _getRand.Next(0, _gameSize.Width),
                    _getRand.Next(0, _gameSize.Height));
                toBeSpawned.transform.position = pos;
                GameObject.Instantiate(toBeSpawned);
            }
        }

        public List<GameObject> enemyTanks = new List<GameObject>();
    }
}
