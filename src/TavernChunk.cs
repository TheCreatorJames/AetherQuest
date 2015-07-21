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
     * This is a tavern chunk, it is a PortalChunk.
     * 
     */
    [Serializable()]
    class TavernChunk : PortalChunk
    {
        public TavernChunk(int chunkPos, int height) : base(chunkPos, height)
        {
            x = 0;
            y = ResourceHandler.getInstance().getBottom() - 100 - InputManager.getInstance().getCurrentPlayer().getHeight();


        }

        public override void draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            if(dungeon == null)
            {
                dungeon = new PresetDungeon(new TavernRoom(new Vector2(chunkPos * CHUNKWIDTH+ 80, ResourceHandler.getInstance().getBottom() - 64 - getHeight())));
            }
            base.draw(spriteBatch, graphics);
            Vector2 vector = new Vector2(chunkPos * CHUNKWIDTH - InputManager.getInstance().getCurrentPlayer().getX() + graphics.Viewport.Bounds.Center.X + ((CHUNKWIDTH - 32) / 2), graphics.Viewport.Bounds.Bottom - 64 - getHeight() - InputManager.getInstance().getCurrentPlayer().getY() + graphics.Viewport.Bounds.Center.Y);
            spriteBatch.Draw(ResourceHandler.getInstance().getTavernDoorTexture(), vector, getColor());
        }
    }
}
