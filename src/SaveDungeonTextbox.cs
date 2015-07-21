using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * A textbox that will allow you to type in the name of the dungeon
     * and save it to that filename.
     * 
     */

    class SaveDungeonTextbox : Textbox
    {
        /// <summary>
        /// Action to Save the Dungeon.
        /// </summary>
        internal class SaveDungeonTextboxAction : GuiAction
        {
            private Textbox textbox;
            public SaveDungeonTextboxAction(Textbox textbox)
            {
                this.textbox = textbox;
            }

            /// <summary>
            /// Saves the dungeon.
            /// </summary>
            public override void executeAction()
            {
                String dir = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/AetherQuest/";
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);


                InputManager.getInstance().getCurrentPlayer().savePlayer(dir + textbox.getText() + "_Player.dat");
                ItemFactoryManager.getInstance().saveItems(dir + textbox.getText() + "_Items.dat");
                DungeonManager.getInstance().getCurrentDungeon().saveDungeon(dir + textbox.getText() + "_Dungeon.dat");
                textbox.setEnabled(false);
                MenuManager.getInstance().getMainMenu().setEnabled(false);
            }
        }


        public SaveDungeonTextbox()
        {
            setAction(new SaveDungeonTextboxAction(this));
        }

        public override void draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            setBackground(Color.Blue);
            base.draw(spriteBatch, graphicsDevice);
        }

    }
}
