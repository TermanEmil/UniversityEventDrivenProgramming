using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tanks.BuisnessLogic.Concrete;

namespace Tanks.BuisnessLogic
{
    class GameController
    {
        public static GameController instance;

        public List<Tank> enemyTanks = new List<Tank>();
        public Player player;

        public Graphics mainGraphics;
        
        private Timer _timer;
        public List<GameObject> gameObjects = new List<GameObject>();

        public bool repaintRequired = true;

        public GameController(Control keyboardCtrl)
        {
            instance = this;
            _timer = new Timer();

            new Input(keyboardCtrl);
        }

        public void Start()
        {
            gameObjects.ForEach(x => x.Start());
        }

        public void Update()
        {
            _timer.Tick();
            gameObjects.ToList().ForEach(x => x.Update());
            Concrete.EnemySpawner.Instance.Update();
        }

        public void Paint()
        {
            gameObjects.ForEach(x => x.Paint());
        }
    }
}
