using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * Buys a potion and places into player Inventory.
     * 
     */
    class BuyPotionButton : Button
    {
        private const byte PRICE = 12;
        /// <summary>
        /// Buys a potion and places it into player's inventory.
        /// </summary>
        internal class BuyPotionButtonAction : GuiAction
        {
            /// <summary>
            /// Buys the potion and places into player's inventory.
            /// </summary>
            public override void executeAction()
            {
                //checks to make sure if there is enough room.
                int max = InputManager.getInstance().getCurrentPlayer().getMaximumSize();
                int num = InputManager.getInstance().getCurrentPlayer().getInventoryItems().Length;

                if(num < max)
                {
                    //checks to see if there are enough credits, then adds the item to the inventory.
                    if (InputManager.getInstance().getCurrentPlayer().getCredits().getValue() >= PRICE)
                    {
                        InputManager.getInstance().getCurrentPlayer().getCredits().setValue(InputManager.getInstance().getCurrentPlayer().getCredits().getValue()- PRICE);
                        InputManager.getInstance().getCurrentPlayer().add(ItemFactoryManager.getInstance().generateHealthPotion());
                    }
                }
            }
        }

        public BuyPotionButton() : base(Vector2.Zero, "Retrieve Potion (" + PRICE + " Credits)", Color.White)
        {
            setAction(new BuyPotionButtonAction());
        }
    }
}
