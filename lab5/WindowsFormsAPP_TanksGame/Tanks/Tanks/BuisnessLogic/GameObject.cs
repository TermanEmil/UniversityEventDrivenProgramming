using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Tanks.BuisnessLogic
{
    public class GameObject
    {
        public Transform transform;
        public List<Property> properties = new List<Property>();

        public static void Instantiate(GameObject gameObject)
        {
            GameController.instance.gameObjects.Add(gameObject);
        }

        public static void Destroy(GameObject gameObject)
        {
            GameController.instance.gameObjects.Remove(gameObject);
        }
        
        public GameObject()
        {
            transform = new Transform(this);
            AddProperty(transform);
        }

        public void AddProperty(Property newProperty)
        {
            properties.Add(newProperty);
        }

        public void Update()
        {
            properties.ForEach(x => x.Update());
        }

        public void Paint()
        {
            properties.ForEach(x => x.Paint());
        }
    }
}
