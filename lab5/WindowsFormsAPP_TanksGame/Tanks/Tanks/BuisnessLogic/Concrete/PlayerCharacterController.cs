using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.BuisnessLogic;
using System.Drawing;

namespace Tanks
{
    class PlayerCharacterController : CharacterController
    {

        public override Point MovementAxix
        {
            get
            {
                return MovementAxis(
                    Input.PressedKeys.Get('A'),
                    Input.PressedKeys.Get('D'),
                    Input.PressedKeys.Get('W'),
                    Input.PressedKeys.Get('S'));
            }
        }

        public override void Update()
        {
            base.Update();
            if (Input.PressedKeys.Get(' '))
                _shootCtrl.Shoot(false);
        }

        public PlayerCharacterController(GameObject gmObj) : base(gmObj)
        {

        }
    }
}
