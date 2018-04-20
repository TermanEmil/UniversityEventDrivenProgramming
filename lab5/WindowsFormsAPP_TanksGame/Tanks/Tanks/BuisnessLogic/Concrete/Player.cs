using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.BuisnessLogic.Concrete
{
    public class Player : Property
    {
        public int Points { get; private set; }
        private Form1 _mainForm;

        public Player(GameObject gmObj, Form1 mainForm) : base(gmObj)
        {
            var life = gameObject.GetComponent<Life>();
            life.onDie += OnPlayerDie;
            life.onTakeDmg += OnTakeDamage;

            _mainForm = mainForm;

            InitInterface();
        }

        public void ConfigTankEvents(Tank tank)
        {
            tank.GetComponent<Life>().onDie += OnEnemyDie;
        }

        public void OnEnemyDie(object sender, EventArgs e)
        {
            Points += GameSettings.enemyPoints;
            _mainForm.PlayerPoints.Text = Points.ToString();
        }

        public void OnPlayerDie(object sender, EventArgs e)
        {
            _mainForm.Text = "Player died";
        }

        public void OnTakeDamage(object sender, EventArgs e)
        {
            var value = gameObject.GetComponent<Life>().NormHP * _mainForm.HPBar.Maximum;
            _mainForm.HPBar.Value = (int)value;
        }

        private void InitInterface()
        {
            _mainForm.PlayerPoints.Text = "0";
            _mainForm.HPBar.Value = _mainForm.HPBar.Maximum;
        }
    }
}
