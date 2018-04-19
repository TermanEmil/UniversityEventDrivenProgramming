using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tanks.BuisnessLogic
{
    public class Input
    {
        public static Input instance;

        private Dictionary<int, bool> _pressedControlls = new Dictionary<int, bool>();

        public static Dictionary<int, bool> PressedKeys
        {
            get
            {
                return instance._pressedControlls;
            }
        }

        public Input(Control keyboardCtrl)
        {
            instance = this;
            keyboardCtrl.KeyPress += _OnKeyPress;
            keyboardCtrl.KeyUp += _OnKeyUp;
        }

        private void _OnKeyPress(object sender, KeyPressEventArgs e)
        {
            var normVal = e.KeyChar.ToString().ToUpper()[0];
            if (!_pressedControlls.ContainsKey(normVal))
                _pressedControlls.Add(normVal, true);
            
            _pressedControlls[normVal] = true;
        }

        private void _OnKeyUp(object sender, KeyEventArgs e)
        {
            _pressedControlls[e.KeyValue] = false;
        }
    }
}
