using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tanks.BuisnessLogic
{
    public class Property
    {
        public readonly GameObject gameObject;
        public Transform MyTransform
        {
            get { return gameObject.transform; }
            set { gameObject.transform = value; }
        }

        public Property(GameObject gmObj)
        {
            gameObject = gmObj;
            gameObject.AddProperty(this);
        }

        public virtual void Update()
        {
        }

        public virtual void Paint()
        {
        }
    }
}
