using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * This is used to scroll the menu up and show the items and quests available.
     * 
     */
    class ScrollButtonAction : GuiAction
    {
        private FixedMenu fixedMenu;
        public ScrollButtonAction(FixedMenu fixedMenu)
        {
            this.fixedMenu = fixedMenu;
        }

        /// <summary>
        /// Scroll the Menu Upward.
        /// </summary>
        public override void executeAction()
        {
            fixedMenu.scrollUp();
        }
    }
}
