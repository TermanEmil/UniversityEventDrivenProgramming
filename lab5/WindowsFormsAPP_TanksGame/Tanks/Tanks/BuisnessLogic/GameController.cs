using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tanks.BuisnessLogic
{
    class GameController
    {
        public static GameController instance;

        public static readonly string rootPath = "..\\..\\";

        public Graphics mainGraphics;
        public Control mainKeyboardCtrl;

        private Timer _timer;
        public List<GameObject> gameObjects = new List<GameObject>();

        public bool repaintRequired = true;
        private readonly Control _controlToRepaint;

        public GameController()
        {
            instance = this;
            _timer = new Timer();
        }

        public void Update()
        {
            _timer.Tick();
            gameObjects.ForEach(x => x.Update());
        }

        public void Paint()
        {
            gameObjects.ForEach(x => x.Paint());
        }
    }
}
