using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.BuisnessLogic
{
    public class ColliderCtrl
    {
        private static ColliderCtrl _instance = null;
        public static ColliderCtrl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ColliderCtrl();
                return _instance;
            }
        }

        public List<Collider> colliders = new List<Collider>();
    }
}
