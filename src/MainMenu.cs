using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    /* Written By : Jesse Mitchell
     * 
     * This is the main menu of the game.
     * 
     */
    class MainMenu : FixedMenu
    {
        private FixedMenu mainList;
        private FixedMenu extraInput;
        private FixedMenu questRewards;

        private FixedMenu tutorials;

        public MainMenu(int width, int height) :
            base(new Microsoft.Xna.Framework.Vector2(0, 0), width, height, Microsoft.Xna.Framework.Color.Black)
        {
            mainList = new FixedMenu(vector, getWidth() / 4, (int)getHeight(), Microsoft.Xna.Framework.Color.LightSteelBlue, false);
            extraInput = new FixedMenu(new Vector2(getWidth() - getWidth() / 4, 0), getWidth() / 4, (int)(getHeight() / 1.8), Microsoft.Xna.Framework.Color.LightSteelBlue);
            questRewards = new FixedMenu(new Vector2(getWidth() - getWidth() / 4, (int)(getHeight() / 1.8) + 1), getWidth() / 4, (int)(getHeight() / 1.8), Microsoft.Xna.Framework.Color.LightSteelBlue);

            tutorials = new FixedMenu(new Vector2(getWidth() / 4 + 1, getHeight() - getHeight() / 3), -(int)(mainList.getVector().X + getWidth() / 4 - extraInput.getVector().X) - 2, getHeight() / 3, Color.LightSteelBlue);
            setEnabled(false);
            mainList.add(new Label(Vector2.Zero, "- Main Menu -", Color.White));
            mainList.add(new Label(Vector2.Zero, "", Color.White));

            mainList.add(new SaveButton());
            mainList.add(new LoadDungeonButton());

            mainList.add(new Label(Vector2.Zero, "", Color.White));

            mainList.add(new GenerateDungeonButton());
            mainList.add(new ExitButton());

            addTutorial("AetherQuest Version 1.0.0 Alpha");
            addTutorial("Generate a new Dungeon in the Top Left!");
            addTutorial("Defaults:");

            addTutorial("A -            Left");
            addTutorial("D -            Right");
            addTutorial("Space -      Jump");
            addTutorial("1 -            Activate");
            addTutorial("2 -            Melee Attack");
            addTutorial("3 -            Shoot Ammo");
        }

        /// <summary>
        /// Adds a tutorial to the center of the menu.
        /// </summary>
        /// <param name="line"></param>
        private void addTutorial(String line)
        {
            tutorials.add(new Label(Vector2.Zero, line, Color.White));
        }

        public override void draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            if (shown)
            {
                mainList.draw(spriteBatch, graphicsDevice);
                extraInput.draw(spriteBatch, graphicsDevice);
                questRewards.draw(spriteBatch, graphicsDevice);

                tutorials.draw(spriteBatch, graphicsDevice);
            }
        }

        /// <summary>
        /// Sets if the main menu is visible or not.
        /// </summary>
        /// <param name="info"></param>
        public override void setEnabled(bool info)
        {
            extraInput.clear();
            base.setEnabled(info);
        }


        /// <summary>
        /// Shows the loading dialog.
        /// </summary>
        public void showLoadDungeon()
        {
            extraInput.clear();
            extraInput.add(new Label(Vector2.Zero, "Load Dungeon:", Color.OrangeRed));
            extraInput.add(new LoadDungeonTextbox());
        }

        /// <summary>
        /// Shows the saving dialog
        /// </summary>
        internal void showSaveDungeon()
        {
            extraInput.clear();
            extraInput.add(new Label(Vector2.Zero, "Save Dungeon:", Color.OrangeRed));
            extraInput.add(new SaveDungeonTextbox());
        }
    }
}

