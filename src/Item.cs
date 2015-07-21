using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    /*
     * Written By: Jesse Mitchell
     * 
     * This is the item class that all types of items are derived from.
     * Features durability and maxDurabilty as stats
     * Features a name and a hash as special variables for it
     */

    [Serializable()]
    public class Item
    {
        private string hash, name;
        
        private CappedStatistic durability;
        
        private List<GameEffect> inventoryEffects;
        private List<GameEffect> usableEffects;

        //The Constructors build up to the largest one to lessen code
        public Item() : this("DefaultHash")
        {
            
        }

        public Item(String hash) : this(hash, "Default Item", 0)
        {

        }

        //The statistics are set.
        public Item(string hash, string name, int maxDurability)
        {
            this.hash = hash;
            this.name = name;

            durability = new CappedStatistic("durability", maxDurability);
            
        }

        public Item(string hash, string name, CappedStatistic durability)
        {
            this.hash = hash;
            this.name = name;
            this.durability = durability;
        }

        //Getters and Setters
        public CappedStatistic getDurability()
        {
            return durability;
        }
        
        public void setName(string nme)
        {
            name = nme;
        }

        public bool isInInventory(Inventory inventory)
        {
            return ItemFactoryManager.getInstance().getItemInventoryHash(this).Equals(inventory.getInventoryHash());
        }

        //adds an effect to the usable list
        public void addUsableEffect(GameEffect effect)
        {
            if (usableEffects == null) usableEffects = new List<GameEffect>(); //instansiates the list because it is now necessary
            usableEffects.Add(effect);
        }

        //tells the game if you can use the item
        public bool isUsable()
        {
            return (usableEffects != null);
        }

        //activates the effects that are usable
        public void activateUsableEffects(bool info)
        {
            if (usableEffects != null)
                foreach (GameEffect effect in usableEffects)
                {
                    if (info)
                    {
                        effect.activate();
                    }
                    else
                    {
                        effect.deactivate();
                    }
                }
        }

        //adds an effect to be cast when entered in an inventory
        public void addEffect(GameEffect effect)
        {
            if (inventoryEffects == null) inventoryEffects = new List<GameEffect>(); //instansiates the list because it is now necessary
            inventoryEffects.Add(effect);
        }

        //activates the effects from being in the inventory
        public void activateEffects(bool info)
        {
            if (inventoryEffects != null)
                foreach (GameEffect effect in inventoryEffects)
                {
                    if (info)
                    {
                        effect.activate();
                    }
                    else
                    {
                        effect.deactivate();
                    }
                }
        }

        //if the durability is less than the max, SET IT
        public void setDurabilityValue(int value)
        {
            durability.setValue(value);
        }

        public void setMaxDurability(int maxDurability)
        {
            durability.setMaxValue(maxDurability);
        }

        public void setDurabilityModifier(int modifier)
        {
            durability.setModifier(modifier);
        }

        public string getHash()
        {
            return hash;
        }

        public string getName()
        {
            return name;
        }

        override public String ToString()
        {
            return name + "::" + durability.ToString() + "::" + hash;
            //need to add effects in
        }

        
    }
}
