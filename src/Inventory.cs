using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     *
     * This is used to keep track of items in locations. 
     * Used for chests, players, etc.
     * 
     */
     [Serializable()]
    public class Inventory
    {
        private String inventory;
        private int maximumSize;

        public Inventory() : this(10)
        {
            
        }

        public Inventory(int maximumSize) : this(maximumSize, false)
        {
            
        }
        
        public Inventory(int maximumSize, bool player)
        {
            inventory = ItemFactoryManager.getInstance().createInventory();
            this.maximumSize = maximumSize;
        }

        public Inventory(int maximumSize, String inventory)
        {
            this.inventory = inventory;
            this.maximumSize = maximumSize;
        }

        //gets the items from the inventory
        public Item[] getInventoryItems()
        {
            return ItemFactoryManager.getInstance().getContainerItems(inventory);
        }

        //gets the inventory hash
        public String getInventoryHash()
        {
            return inventory;
        }

        //sets maximum size of the inventory
        public void setMaximumSize(int p)
        {
            maximumSize = p;
        }

        //gets the maximum size of the inventory
        public int getMaximumSize()
        {
            return maximumSize;
        }

         //adds and item to the inventory.
        public void add(Item item)
        {
            ItemFactoryManager.getInstance().moveItem(item, this);
        }
    }
}
