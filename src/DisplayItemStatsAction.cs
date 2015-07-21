using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * Displays the stats of the item.
     * 
     */
    class DisplayItemStatsAction : GuiAction
    {
        private Item item;
        public DisplayItemStatsAction(Item item)
        {
            this.item = item;
        }

        /// <summary>
        /// Sets the current item, and displays its stats
        /// </summary>
        public override void executeAction()
        {
            MenuManager.getInstance().getInventoryMenu().setCurrentItem(item);
            MenuManager.getInstance().getInventoryMenu().displayCurrentItemStats();
        }
    }
}
