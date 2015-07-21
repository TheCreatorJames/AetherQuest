using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * Buys an ammo pack and places into player Inventory.
     * 
     */
    class BuyAmmoButton : Button
    {
        private const byte PRICE = 19;
        /// <summary>
        /// Buys a ammo pack and places it into player's inventory.
        /// </summary>
        internal class BuyAmmoButtonAction : GuiAction
        {
            /// <summary>
            /// Buys the ammo pack and places into player's inventory.
            /// </summary>
            public override void executeAction()
            {
                //checks to make sure if there is enough room.
                int max = InputManager.getInstance().getCurrentPlayer().getMaximumSize();
                int num = InputManager.getInstance().getCurrentPlayer().getInventoryItems().Length;

                if (num < max)
                {
                    //checks to see if there are enough credits, then adds the item to the inventory.
                    if (InputManager.getInstance().getCurrentPlayer().getCredits().getValue() >= PRICE)
                    {
                        InputManager.getInstance().getCurrentPlayer().getCredits().setValue(InputManager.getInstance().getCurrentPlayer().getCredits().getValue() - PRICE);
                        InputManager.getInstance().getCurrentPlayer().add(ItemFactoryManager.getInstance().generateAmmoPack());
                    }
                }
            }
        }

        public BuyAmmoButton()
            : base(Vector2.Zero, "Retrieve Ammo (" + PRICE + " Credits)", Color.White)
        {
            setAction(new BuyAmmoButtonAction());
        }
    }
}
