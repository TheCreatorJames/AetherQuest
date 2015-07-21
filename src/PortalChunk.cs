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
     * This is a Portal Chunk, it is a door to another layer of the dungeon.
     * 
     */
    [Serializable()]
    class PortalChunk : Chunk
    {
        protected Dungeon dungeon;
        protected float x, y;
        
        public PortalChunk(int chunkPos, int chunkHeight) : base(chunkPos)
        {
            boundingBoxes.Add(new BoundingBox(chunkHeight));
            
            
        }
        /// <summary>
        /// Creates a portal chunk to a predefined location.
        /// </summary>
        /// <param name="chunkPos"></param>
        /// <param name="chunkHeight"></param>
        /// <param name="dungeon"></param>
        /// <param name="vector"></param>
        public PortalChunk(int chunkPos, int chunkHeight, Dungeon dungeon, Vector2 vector) : base(chunkPos)
        {
            boundingBoxes.Add(new BoundingBox(chunkHeight));
            this.dungeon = dungeon;
            x = vector.X;
            y = vector.Y;    
        }

        public override void draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            base.draw(spriteBatch, graphics);

            if(GetType().Equals(typeof(PortalChunk)))
            {
                Vector2 vector = new Vector2(chunkPos * CHUNKWIDTH- InputManager.getInstance().getCurrentPlayer().getX() + graphics.Viewport.Bounds.Center.X + ((CHUNKWIDTH - 32) / 2), graphics.Viewport.Bounds.Bottom - 64 - getHeight() - InputManager.getInstance().getCurrentPlayer().getY() + graphics.Viewport.Bounds.Center.Y);
                spriteBatch.Draw(ResourceHandler.getInstance().getDungeonDoorTexture(), vector, getColor());
            }
        }

        /// <summary>
        /// Gets the location to teleport to.
        /// </summary>
        /// <returns></returns>
        public Vector2 getTeleportVector()
        {
            return new Vector2(x, y);
        }

        /// <summary>
        /// Returns the dungeon to teleport to.
        /// </summary>
        /// <returns></returns>
        public Dungeon usePortal()
        {
            if (dungeon == null)
            {
                dungeon = new Dungeon(DungeonManager.getInstance().getCurrentDungeon(), InputManager.getInstance().getCurrentPlayer().getVector());
                x = -80;
                y = ResourceHandler.getInstance().getBottom() - dungeon.getFirstChunk().getHeight() - InputManager.getInstance().getCurrentPlayer().getHeight();
            }

            DungeonManager.getInstance().setCurrentDungeon(dungeon);

            return dungeon;
        }
    }
}
