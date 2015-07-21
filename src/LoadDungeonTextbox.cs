using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    /* Written By : Jesse Mitchell
     * 
     * Allows the player to type in the name of the save, then loads it.
     *  
     */
    class LoadDungeonTextbox : Textbox
    {
        /// <summary>
        /// Loads the Level of the World Typed in.
        /// </summary>
        internal class LoadDungeonTextboxAction : GuiAction
        {
            private Textbox textbox;
            public LoadDungeonTextboxAction(Textbox textbox)
            {
                this.textbox = textbox;
            }

            /// <summary>
            /// Gets the name of the Dungeon, then loads it in.
            /// </summary>
            public override void executeAction()
            {
                String dir = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/AetherQuest/";
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                DungeonManager.getInstance().setItemFactoryManager(ItemFactoryManager.loadItemFactoryManager(dir + textbox.getText() + "_Items.dat"));
                InputManager.getInstance().setCurrentPlayer(Player.loadPlayer(dir + textbox.getText() + "_Player.dat"));
                DungeonManager.getInstance().setCurrentDungeon(Dungeon.loadDungeon(dir + textbox.getText()+"_Dungeon.dat"));
                
                textbox.setEnabled(false);
                MenuManager.getInstance().getMainMenu().setEnabled(false);
            }
        }

        public LoadDungeonTextbox() : base()
        {
            setAction(new LoadDungeonTextboxAction(this));
        }

        public override void draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            setBackground(Color.Blue);
            base.draw(spriteBatch, graphicsDevice);
        }
    }
}
