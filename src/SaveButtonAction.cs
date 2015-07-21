using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * A GuiAction to show the Save Dialog.
     * 
     */
    class SaveButtonAction : GuiAction
    {
        /// <summary>
        /// Shows the dialog for saving the dungeon
        /// </summary>
        public override void executeAction()
        {
            MenuManager.getInstance().getMainMenu().showSaveDungeon();
        }
    }
}
