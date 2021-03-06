﻿using System;
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

        public static T Instantiate<T>(T gameObject) where T : GameObject
        {
            GameController.instance.gameObjects.Add(gameObject);
            if (gameObject.GetComponent<Concrete.EnemyCharacterController>() != null)
                Concrete.EnemySpawner.Instance.enemyTanks.Add(gameObject);
            gameObject.Start();
            return gameObject;
        }

        public static void Destroy(GameObject gameObject)
        {
            var collider = gameObject.GetComponent<Collider>();
            if (collider != null)
                ColliderCtrl.Instance.colliders.Remove(collider);
            if (gameObject.GetComponent<Concrete.EnemyCharacterController>() != null)
                Concrete.EnemySpawner.Instance.enemyTanks.Remove(gameObject);
            GameController.instance.gameObjects.Remove(gameObject);
        }

        private Collider _myCollider = null;
        public Collider MyCollider
        {
            get
            {
                if (_myCollider == null)
                    _myCollider = GetComponent<Collider>();
                return _myCollider;
            }
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

        public virtual void Start()
        {
            properties.ForEach(x => x.Start());
        }

        public virtual void Update()
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
                if (typeof(T).IsAssignableFrom(property.GetType()))
                    return property as T;
            return null;
        }
    }
}
