using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AetherQuest
{
    /* Written By : Jesse Mitchell
     * 
     * This will switch the inventory the item is in.
     * 
     */
    class SwitchInventory : GuiAction
    {
       
        public SwitchInventory()
        {
        }

        /// <summary>
        /// Moves the item into or out of the inventory.
        /// </summary>
        public override void executeAction()
        {
            MenuManager.getInstance().getInventoryMenu().moveCurrentItem();
        }
    }
}
