using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * This equips the current item selected in the inventory.
     */
    class EquipItemAction : GuiAction
    {
        public override void executeAction()
        {
            MenuManager.getInstance().getInventoryMenu().equipCurrentItem();
        }
    }
}
