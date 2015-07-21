using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    /* Written By : Jesse Mitchell
     * 
     * This is a singleton menu manager. It keeps track of all the menus in the game.
     * 
     */
    class MenuManager
    {
        private static MenuManager instance = null;
        private InventoryMenu inventoryMenu;
        private JournalMenu journalMenu;
        private MainMenu mainMenu;

        public MenuManager(int width, int height)
        {
            if (instance == null)
            {
                inventoryMenu = new InventoryMenu(width, height);
                journalMenu = new JournalMenu(width, height);
                mainMenu = new MainMenu(width, height);

                instance = this;
            }
            else throw new Exception("Do not construct MenuManager more than once. Use getInstance()");
        }

        public static MenuManager getInstance()
        {
            return instance;
        }

        /// <summary>
        /// Toggles if the inventory menu is shown or not.
        /// </summary>
        public void toggleInventoryMenu()
        {
            mainMenu.setEnabled(false);
            journalMenu.setEnabled(false);
            inventoryMenu.setEnabled(!inventoryMenu.getEnabled());
        }

        /// <summary>
        /// Toggles if the Quest Menu is shown or not.
        /// </summary>
        public void toggleJournalMenu()
        {
            mainMenu.setEnabled(false);
            inventoryMenu.setEnabled(false);
            journalMenu.setEnabled(!journalMenu.getEnabled());
        }

        /// <summary>
        /// Toggles if the main menu is shown or not.
        /// </summary>
        public void toggleMainMenu()
        {
            inventoryMenu.setEnabled(false);
            journalMenu.setEnabled(false);
            mainMenu.setEnabled(!mainMenu.getEnabled());
        }

        public void draw(SpriteBatch sprite, GraphicsDevice graphics)
        {
            inventoryMenu.draw(sprite, graphics);
            journalMenu.draw(sprite, graphics);
            mainMenu.draw(sprite, graphics);
        }

        /// <summary>
        /// Gets the Quest Menu.
        /// </summary>
        /// <returns></returns>
        public JournalMenu getQuestMenu()
        {
            return journalMenu;
        }


        /// <summary>
        /// Gets the Main Menu.
        /// </summary>
        /// <returns></returns>
        public MainMenu getMainMenu()
        {
            return mainMenu;
        }

        /// <summary>
        /// Gets the Inventory Menu
        /// </summary>
        /// <returns></returns>
        public InventoryMenu getInventoryMenu()
        {
            return inventoryMenu;
        }
    }
}
