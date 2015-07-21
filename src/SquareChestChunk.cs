using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * This is a chunk that contains a chest upon it. You will be 
     * able to loot items from this chest. 
     */
     [Serializable()]
    class SquareChestChunk : SquareChunk
    {
        private Inventory inventory;
        private Button chestButton;
        public SquareChestChunk(int chunkPos, int baseHeight) : base(chunkPos, baseHeight)
        {
            inventory = new Inventory();
            inventory.add(ItemFactoryManager.getInstance().createWeapon());
            inventory.add(ItemFactoryManager.getInstance().generateHealthPotion());
            chestButton = new ChestButton(this, Vector2.Zero, inventory);

            Random rand = new Random(new Guid().GetHashCode());

            int bonus = rand.Next(8);

            if (bonus == 6)
            {
                inventory.add(ItemFactoryManager.getInstance().generateAmmoPack());
            }
            else if (bonus == 3)
            {
                inventory.add(ItemFactoryManager.getInstance().generateHealthPotion());
            }
        }

        public SquareChestChunk(int chunkPos, int baseHeight, String inventory) : base(chunkPos, baseHeight)
        {
            this.inventory = new Inventory(10, inventory);
            chestButton = new ChestButton(this, Vector2.Zero, this.inventory);
        }

        public override void draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            base.draw(spriteBatch, graphics);
           
            //Move the Chest Button so it can be rendered in the correct place.
            Vector2 vector = new Vector2(chunkPos * CHUNKWIDTH- InputManager.getInstance().getCurrentPlayer().getX() + graphics.Viewport.Bounds.Center.X + ( (CHUNKWIDTH-chestButton.getWidth())/2 ), graphics.Viewport.Bounds.Bottom - chestButton.getHeight() - getHeight() - InputManager.getInstance().getCurrentPlayer().getY() + graphics.Viewport.Bounds.Center.Y);
            chestButton.setX((int)vector.X);
            chestButton.setY((int)vector.Y);

            chestButton.draw(spriteBatch, graphics);
        }

        public void useChest()
        {
            chestButton.executeAction();
        }

        public override string ToString()
        {
            return "SCC:" + inventory.getInventoryHash();
        }
    }
}
