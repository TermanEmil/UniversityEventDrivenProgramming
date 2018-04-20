using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.BuisnessLogic
{
    public class Life : Property
    {
        public double HP { get; private set; } = 0;
        public double maxHP;
        public double hpRegen = 0;

        public float NormHP { get { return (float)(HP / maxHP); } }

        public EventHandler onTakeDmg;
        public EventHandler onDie;

        public Life(GameObject gmObj) : base(gmObj)
        {
        }

        public void Init()
        {
            HP = maxHP;
        }

        public override void Update()
        {
            if (!((float)hpRegen).Aprox(0))
                TakeDmg(-hpRegen * Timer.DeltaTime); 
            base.Update();
        }

        public void TakeDmg(double dmg)
        {
            HP -= dmg;
            if (HP <= 0)
            {
                HP = 0;
                Die();
            }
            else if (HP >= maxHP)
                HP = maxHP;

            if (onTakeDmg != null)
                onTakeDmg.Invoke(this, null);
        }

        public void Die()
        {
            onDie.Invoke(this, null);
            GameObject.Destroy(gameObject);
        }
    }
}
