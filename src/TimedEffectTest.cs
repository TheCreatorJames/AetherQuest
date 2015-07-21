using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * This is a testing of the timedEffect.
     * 
     */
    [Serializable()]
    class TimedEffectTest : TimedEffect
    {
        private Weapon weapon;
        public TimedEffectTest(Weapon x, int time, int recharge) : base(time, recharge)
        {
            weapon = x;
        }

        public override void activate()
        {
            if(isRecharged() && !isActivated())
            {
                Statistic x = weapon.getAttack();
                x.setModifier(x.getModifier() + 10);
                //MenuManager.getInstance().getInventoryMenu().refreshInventories();
                base.activate();
            }
            
        }

        public override void deactivate()
        {
            //Console.Write("Hello");
            if(isActivated())
            {
                Statistic x = weapon.getAttack();
                x.setModifier(x.getModifier() - 10);
                MenuManager.getInstance().getInventoryMenu().displayCurrentItemStats();
                base.deactivate();

            }
            
        }

    }
}
