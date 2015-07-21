using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * This is a dungeon manager, it keeps track of the current dungeon.
     * 
     * It uses a Singleton to make sure there is only one instance of it, and not waste resources.
     * 
     */
    class DungeonManager
    {
        private Dungeon currentDungeon;
        private static DungeonManager instance = new DungeonManager();

        private ItemFactoryManager itemFactoryManager;

        public DungeonManager()
        {
            if (instance == null)
            {
                itemFactoryManager = ItemFactoryManager.loadItemFactoryManager("SavedItems.dat");
            }
            else throw new Exception("Do not instansiate DungeonManager. Use getInstance()");
        }

        /// <summary>
        /// Sets the Dungeon that is currently being played through.
        /// </summary>
        /// <param name="dungeon"></param>
        public void setCurrentDungeon(Dungeon dungeon)
        {
            if(currentDungeon != null && !currentDungeon.GetType().Equals(typeof(PresetDungeon)))
            MenuManager.getInstance().getInventoryMenu().setCurrentItem(InputManager.getInstance().getCurrentPlayer().getInventoryItems()[0]);
        
            this.currentDungeon = dungeon;

        }

        /// <summary>
        /// Gets the instance of this class
        /// </summary>
        /// <returns></returns>
        public static DungeonManager getInstance()
        {
            return instance;
        }

        /// <summary>
        /// Gets the current dungeon.
        /// </summary>
        /// <returns></returns>
        public Dungeon getCurrentDungeon()
        {
            return this.currentDungeon;
        }

        /// <summary>
        /// Sets the current IFM.
        /// </summary>
        /// <param name="itemFactoryManager"></param>
        public void setItemFactoryManager(ItemFactoryManager itemFactoryManager)
        {
            this.itemFactoryManager = itemFactoryManager;
        }

        /// <summary>
        /// Gets the current IFM.
        /// </summary>
        /// <returns></returns>
        public ItemFactoryManager getItemFactoryManager()
        {
            return itemFactoryManager;
        }
    }
}
