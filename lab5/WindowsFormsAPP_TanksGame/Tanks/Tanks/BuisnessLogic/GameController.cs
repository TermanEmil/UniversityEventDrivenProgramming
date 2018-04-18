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

        private Timer _timer;
        public List<GameObject> gameObjects = new List<GameObject>();

        public bool repaintRequired = true;
        private readonly Control _controlToRepaint;
        private readonly Graphics _controlToRepaintGraphics;

        public GameController(Control controlToRepaint)
        {
            instance = this;
            _controlToRepaint = controlToRepaint;
            _controlToRepaintGraphics = _controlToRepaint.CreateGraphics();
            _timer = new Timer();
        }

        public void Update()
        {
            _timer.Tick();
            gameObjects.ForEach(x => x.Update());

            if (repaintRequired)
            {
                repaintRequired = false;
                _controlToRepaintGraphics.Clear(Color.White);
                Paint();
            }
        }

        public void Paint()
        {
            gameObjects.ForEach(x => x.Paint());
        }
    }
}
