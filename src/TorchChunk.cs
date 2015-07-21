using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * This is a chunk that will display lights.
     * 
     */
    [Serializable()]
    class TorchChunk : SquareChunk
    {
        private SerializedColor sColor;
        private Vector2 vector;

        public TorchChunk(int chunkPos, int height)
            : base(chunkPos, height)
        {
            sColor = new SerializedColor(Color.Wheat);
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.Graphics.GraphicsDevice graphics)
        {
            base.draw(spriteBatch, graphics);

            vector = new Vector2(chunkPos * CHUNKWIDTH - InputManager.getInstance().getCurrentPlayer().getX() + graphics.Viewport.Bounds.Center.X + ((CHUNKWIDTH - 32) / 2), graphics.Viewport.Bounds.Bottom - 64 - getHeight() - InputManager.getInstance().getCurrentPlayer().getY() + graphics.Viewport.Bounds.Center.Y);
            spriteBatch.Draw(ResourceHandler.getInstance().getLight(), vector, Color.White);
        }

        public Light getLight()
        {
            Light result = new Light();

            result.setColor( Color.LightGoldenrodYellow);
            result.setPower(210);
            result.setLightRadius(280);

            result.setVector(new Vector3(vector.X, vector.Y, .56f));

            return result;
        }


    }
}
