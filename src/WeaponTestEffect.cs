using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * This shows a test effect on the weapon, increases the attack.
     * 
     */
    [Serializable()]
    class WeaponTestEffect : GameEffect
    {
        private Weapon weapon;
        public WeaponTestEffect(Weapon weapon)
        {
            this.weapon = weapon;
        }

        public override void activate()
        {
            if(!isActivated())
            {
                activated = true;
                weapon.getAttack().setModifier(weapon.getAttack().getModifier() + 10);
            }
        }

        public override void deactivate()
        {
            if(isActivated())
            {
                activated = false;
                weapon.getAttack().setModifier(weapon.getAttack().getModifier() - 10);
            }
            
        }
    }
}
