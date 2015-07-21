using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * This is the inventory menu that displays both the player inventory and the
     * loot inventory.
     * 
     */
    class InventoryMenu : FixedMenu
    {
        private FixedMenu playerItems, lootItems, itemStats, itemPicture;

        private FixedMenu playerStats;

        //private Player playerInventory;
        //private InventoryLink inventoryLink;
        //private ItemLink link;
        private Inventory otherInventory;

        private Item currentItem;

        private Button takeDropButton;
        private Button useItemButton;
        private Button equipItemButton;


        public InventoryMenu(int width, int height)
            : base(new Microsoft.Xna.Framework.Vector2(0, 0), width, height, Microsoft.Xna.Framework.Color.Black)
        {
            playerItems = new FixedMenu(vector, getWidth() / 4, (int)(getHeight() / 1.8), Microsoft.Xna.Framework.Color.LightSteelBlue, true);
            lootItems = new FixedMenu(new Vector2(0, (int)(getHeight() / 1.8) + 1), getWidth() / 4, getHeight() - (int)(getHeight() / 1.8), Microsoft.Xna.Framework.Color.LightSteelBlue, true);
            itemStats = new FixedMenu(new Vector2(getWidth() - getWidth() / 4, 0), getWidth() / 4, (int)(getHeight() / 1.8), Microsoft.Xna.Framework.Color.LightSteelBlue);
            itemPicture = new FixedMenu(new Vector2(getWidth() - getWidth() / 4, (int)(getHeight() / 1.8) + 1), getWidth() / 4, (int)(getHeight() / 1.8), Microsoft.Xna.Framework.Color.LightSteelBlue);

            playerStats = new FixedMenu(new Vector2(getWidth() / 4 + 1, getHeight() - getHeight() / 3), -(int)(playerItems.getVector().X + getWidth() / 4 - itemStats.getVector().X) - 2, getHeight() / 3, Color.LightSteelBlue);

            takeDropButton = new Button(new Vector2(lootItems.getVector().X + getWidth() / 4 - ResourceHandler.getInstance().getVerdana().MeasureString("Switch").X - 2, (int)(getHeight() / 1.8) + 1), "Switch", Color.White);
            takeDropButton.setBackground(Color.LightSteelBlue);
            takeDropButton.setAction(new SwitchInventory());
           
            useItemButton = new Button(new Vector2(itemStats.getVector().X, (int)(getHeight()/1.8)), "Use", Color.White);
            useItemButton.setBackground(Color.LightSteelBlue);
            useItemButton.setAction(new UseItemAction());

            equipItemButton = new Button(new Vector2(itemStats.getVector().X, (int)(getHeight() / 1.8) + useItemButton.getHeight()), "Equip", Color.White);
            equipItemButton.setBackground(Color.LightSteelBlue);
            equipItemButton.setAction(new EquipItemAction());

            setLayer(5);
            setEnabled(false);
            takeDropButton.setLayer(6);
            useItemButton.setLayer(6);
            equipItemButton.setLayer(6);
        }

        /// <summary>
        /// Sets the current inventory being read from.
        /// </summary>
        /// <param name="loot"></param>
        public void setLootInventory(Inventory loot)
        {
            otherInventory = loot;
            refreshInventories();
        }

        /// <summary>
        /// Refreshes the stats of the player.
        /// </summary>
        public void refreshPlayerStats()
        {
            playerStats.clear();
            playerStats.add(new Label(new Vector2(), InputManager.getInstance().getCurrentPlayer().getName(), Color.White));
            playerStats.add(new Label(new Vector2(), InputManager.getInstance().getCurrentPlayer().getHealth().ToString(), Color.White));
            playerStats.add(new Label(new Vector2(), InputManager.getInstance().getCurrentPlayer().getAmmo().ToString(), Color.White));
            playerStats.add(new Label(new Vector2(), InputManager.getInstance().getCurrentPlayer().getWisdom().ToString(), Color.White));
            playerStats.add(new Label(new Vector2(), InputManager.getInstance().getCurrentPlayer().getIntelligence().ToString(), Color.White));
            playerStats.add(new Label(new Vector2(), InputManager.getInstance().getCurrentPlayer().getStrength().ToString(), Color.White));
        }

        /// <summary>
        /// Refreshes the list of items.
        /// </summary>
        public void refreshInventories()
        {
            playerItems.clear();
            lootItems.clear();

            refreshPlayerStats();

            Item[] items = InputManager.getInstance().getCurrentPlayer().getInventoryItems();

            foreach(Item item in items)
            {
                playerItems.add(new ItemButton(item));
            }

            if(otherInventory == null)
            {
                playerItems.resize(playerItems.getWidth(), getHeight());
            }
            else
            {
                playerItems.resize(playerItems.getWidth(), (int)(getHeight() / 1.8));
                items = otherInventory.getInventoryItems();

                foreach (Item item in items)
                {
                    lootItems.add(new ItemButton(item));
                }
            }
            
        }

        /// <summary>
        /// This is used to clear the item stats from an external class.
        /// </summary>
        public void clearItemStats()
        {
            itemStats.clear();
        }
        
        public override void draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            if (shown)
            {
                if (ItemFactoryManager.getInstance().hasChanged())
                {
                    refreshInventories(); 
                }

                playerItems.draw(spriteBatch, graphicsDevice);
                itemStats.draw(spriteBatch, graphicsDevice);

                if(otherInventory != null)
                {
                    lootItems.draw(spriteBatch, graphicsDevice);
                    takeDropButton.draw(spriteBatch, graphicsDevice);
                }
                

                itemPicture.draw(spriteBatch, graphicsDevice);

                playerStats.draw(spriteBatch, graphicsDevice);
                
                if (currentItem != null && currentItem.isInInventory(InputManager.getInstance().getCurrentPlayer()))
                {
                    if (currentItem.isUsable())
                    {
                        useItemButton.setColor(Color.White);
                    }
                    else
                    {
                        useItemButton.setColor(Color.Red);
                    }

                    if(typeof(EquippableItem).IsAssignableFrom(currentItem.GetType()))
                    {
                        equipItemButton.setColor(Color.White);
                    }
                    else
                    {
                        equipItemButton.setColor(Color.Red);
                    }

                }
                else
                {
                    useItemButton.setColor(Color.Red);
                    equipItemButton.setColor(Color.Red);
                }


                useItemButton.draw(spriteBatch, graphicsDevice);
                equipItemButton.draw(spriteBatch, graphicsDevice);

            }

        }

        /// <summary>
        /// Sets the current item being displayed.
        /// </summary>
        /// <param name="item"></param>
        public void setCurrentItem(Item item)
        {
            this.currentItem = item;
        }

        /// <summary>
        /// Moves the current item to the targeted inventory.
        /// </summary>
        public void moveCurrentItem()
        {
            if (currentItem == null) return;

            if(currentItem.isInInventory(otherInventory))
            {
                InputManager.getInstance().getCurrentPlayer().add(currentItem);
            }
            else
            {
                //If it is an equippable item, check if it is equipped.
                if (typeof(EquippableItem).IsAssignableFrom(currentItem.GetType()))
                {
                    //if it isn't equipped, add it to the other inventory.
                    if (!InputManager.getInstance().getCurrentPlayer().isItemEquipped((EquippableItem)currentItem))
                    {
                        otherInventory.add(currentItem);
                    }
                } //otherwise, add it to the inventory
                else otherInventory.add(currentItem);
            }
        }
        
        /// <summary>
        /// Displays the stats of the current item.
        /// </summary>
        public void displayCurrentItemStats()
        {
            itemStats.clear();

            if (currentItem == null) return;

            Label nameLabel = new Label(new Vector2(0, 0), currentItem.getName(), Color.White);
            itemStats.add(nameLabel);

            if (typeof(EquippableItem).IsAssignableFrom(currentItem.GetType()))
            {
                if (InputManager.getInstance().getCurrentPlayer().isItemEquipped((EquippableItem)currentItem))
                {
                    itemStats.add(new Label(Vector2.Zero, "Equipped", Color.Red));
                }

                if (currentItem.GetType().Equals(typeof(Weapon)))
                {
                    Weapon weapon = (Weapon)currentItem;
                    itemStats.add(new Label(new Vector2(0, 0), weapon.getAttack().ToString(), Color.White));
                    itemStats.add(new Label(new Vector2(0, 0), weapon.getSpeed().ToString(), Color.White));
                }
            }

            Label durabilityLabel = new Label(new Vector2(0, 0), currentItem.getDurability().ToString(), Color.White);
            itemStats.add(durabilityLabel);

           
            
        }

        /// <summary>
        /// Uses the current item.
        /// </summary>
        public void useCurrentItem()
        {
            if (currentItem == null) return; 

            if(currentItem.isInInventory(InputManager.getInstance().getCurrentPlayer()))
            {
                currentItem.activateUsableEffects(true);
                displayCurrentItemStats();
            }
        }


        /// <summary>
        /// Sets if the inventory menu is visible.
        /// </summary>
        /// <param name="info"></param>
        public override void setEnabled(bool info)
        {
            if(!info)
            {
                setLootInventory(null);
            }
            refreshPlayerStats();
            base.setEnabled(info);
        }

        /// <summary>
        /// Equips the current item.
        /// </summary>
        public void equipCurrentItem()
        {
            if (currentItem == null) return;

            if (currentItem.isInInventory(InputManager.getInstance().getCurrentPlayer()))
            {
                InputManager.getInstance().getCurrentPlayer().equipItem((EquippableItem)currentItem);
                displayCurrentItemStats();
            }   
        }
    }
}
