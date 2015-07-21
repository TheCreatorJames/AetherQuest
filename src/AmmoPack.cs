using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    /* Written By : Jesse Mitchell
     * 
     * This is an Ammo Pack. It will give the player Ammo.
     * 
     */
    [Serializable()]
    class AmmoPack : Item
    {
        /// <summary>
        /// Gives the player Ammo
        /// </summary>
        [Serializable()]
        internal class AmmoPackEffect : GameEffect
        {
            private AmmoPack ammoPack;
            public AmmoPackEffect(AmmoPack ammo)
            {
                ammoPack = ammo;
            }
            /// <summary>
            /// Adds 6 bullets to the players ammo, then removes it from the game.
            /// </summary>
            public override void activate()
            {
                InputManager.getInstance().getCurrentPlayer().getAmmo().setValue(InputManager.getInstance().getCurrentPlayer().getAmmo().getValue() + 6);
                ItemFactoryManager.getInstance().removeItem(ammoPack);
                MenuManager.getInstance().getInventoryMenu().clearItemStats();
                MenuManager.getInstance().getInventoryMenu().setCurrentItem(null);
            }

            /// <summary>
            /// Does nothing right now.
            /// </summary>
            public override void deactivate()
            {
                
            }
        }

        public AmmoPack(String hash) : base(hash, "Ammo Pack", new CappedStatistic("Bullets: 6", 6))
        {
            addUsableEffect(new AmmoPackEffect(this));
        }
    }
}
