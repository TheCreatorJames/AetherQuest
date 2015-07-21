using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * Heals the player when consumed.
     * 
     */
    [Serializable()]
    class HealthPotion : Item
    {
        /// <summary>
        /// This heals the player five points.
        /// </summary>
        [Serializable()]
        internal class HealPlayer : GameEffect
        {
            private HealthPotion healthPotion;
            public HealPlayer(HealthPotion hp)
            {
                healthPotion = hp;
            }

            /// <summary>
            /// Heals the player once activated, disappears from inventory.
            /// </summary>
            public override void activate()
            {
                InputManager.getInstance().getCurrentPlayer().getHealth().setValue(InputManager.getInstance().getCurrentPlayer().getHealth().getValue() + 5);
                ItemFactoryManager.getInstance().removeItem(healthPotion);
                MenuManager.getInstance().getInventoryMenu().clearItemStats();
                MenuManager.getInstance().getInventoryMenu().setCurrentItem(null);
            }

            public override void deactivate()
            {
                
            }
        }

        /// <summary>
        /// Creates HealthPotion
        /// </summary>
        /// <param name="hash"></param>
        public HealthPotion(String hash) : base(hash, "Health Potion", new CappedStatistic("Heal Uses", 1))
        {
            addUsableEffect(new HealPlayer(this));  
        }
    }
}
