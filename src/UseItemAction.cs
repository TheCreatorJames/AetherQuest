using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    /* Written By : Jesse Mitchell
     * 
     * This is an action to use the effects of the item.
     * 
     */
    class UseItemAction : GuiAction
    {
        public UseItemAction()
        {

        }

        public override void executeAction()
        {
            MenuManager.getInstance().getInventoryMenu().useCurrentItem();
        }
    }
}
