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
     * This is a chest button, when clicked on, it opens the inventory
     *
     */
     [Serializable()]
    class ChestButton : Button
    {
        private Chunk chunk;
        public ChestButton(Chunk chunk, Vector2 vector, Inventory inventory) : base(vector,"      ", chunk.getColor())
        {
            setAction(new SetLootInventoryAction(inventory, chunk));

            this.chunk = chunk;
        }

        public override void draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
         
            if(isPressed())
            {  
            }

            spriteBatch.Draw(ResourceHandler.getInstance().getChestTexture(), getVector(), chunk.getColor());
        }

        /// <summary>
        /// Gets the height, it is preset to make sure the chest is the right size.
        /// </summary>
        /// <returns></returns>
        public override int getHeight()
        {
            return 32;
        }

         /// <summary>
         /// Returns a preset width of the chest button.
         /// </summary>
         /// <returns></returns>
        public override int getWidth()
        {
            return 32;
        }
    }
}
