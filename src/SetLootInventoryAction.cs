using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * Used to set the loot inventory of the inventory menu.
     * 
     */
     [Serializable()]
    class SetLootInventoryAction : GuiAction
    {
        private Inventory inventory;
        private Chunk chunk;
       
        public SetLootInventoryAction(Inventory inventory, Chunk chunk)
        {
            this.inventory = inventory;
            this.chunk = chunk;

        }

        /// <summary>
        /// Opens the Chest Inventory.
        /// </summary>
        public override void executeAction()
        {
                
           
                if(chunk.isInside())
                {
                    MenuManager.getInstance().getInventoryMenu().setLootInventory(inventory);
                    MenuManager.getInstance().getInventoryMenu().setEnabled(true);
                }
           
        }
    }
}
