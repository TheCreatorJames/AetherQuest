using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{ 
    /* Written By: Jesse Mitchell
     * 
     * This is a button that will load the dungeon after letting you type the name
     * of the dungeon in.
     * 
     */
    class LoadDungeonButton : Button
    {
        /// <summary>
        /// Shows the Dungeon Loading Dialog.
        /// </summary>
        internal class LoadDungeonButtonAction : GuiAction
        {
            /// <summary>
            /// Shows the dungeon loading dialog.
            /// </summary>
            public override void executeAction()
            {
                MenuManager.getInstance().getMainMenu().showLoadDungeon();
            }
        }

        /// <summary>
        /// Creates A Load Dungeon Button to Show Dialog.
        /// </summary>
        public LoadDungeonButton() : base(Vector2.Zero, "Load Dungeon", Color.White, Color.Red)
        {
            setAction(new LoadDungeonButtonAction());
        }
    }
}
