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

        public static GameObject Instantiate(GameObject gameObject)
        {
            GameController.instance.gameObjects.Add(gameObject);
            return gameObject;
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

        public T GetComponent<T>() where T : Property
        {
            foreach (var property in properties)
                if (property.GetType() == typeof(T))
                    return property as T;
            return null;
        }
    }
}
