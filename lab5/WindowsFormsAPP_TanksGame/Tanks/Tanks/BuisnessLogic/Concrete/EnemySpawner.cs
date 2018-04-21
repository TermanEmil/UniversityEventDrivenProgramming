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
                if (_instance == null)
                    _instance = new EnemySpawner();
                return _instance;
            }
        }

        public int maxNumOfEnemies = 5;
        private Random getRand = new Random();

        public void Update()
        {
            Tank toBeSpawned;
            if (enemyTanks.Count < maxNumOfEnemies)
            {
                toBeSpawned = new Tank(isAI: true);
                toBeSpawned.GetComponent<Transform>().position = new Point(getRand.Next(20, 40), getRand.Next(0, 400));
                GameObject.Instantiate(toBeSpawned);
            }
        }

        public List<GameObject> enemyTanks = new List<GameObject>();
    }
}
