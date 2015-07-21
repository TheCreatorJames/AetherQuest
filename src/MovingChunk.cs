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
     * This is a moving chunk, it is basically an elevator.
     * 
     */
    [Serializable()]
    class MovingChunk : Chunk
    {
        private int slide;
        private int originalHeight;
        private bool lifted;
        public MovingChunk(int chunkPos, int height, int slideModifier) : base(chunkPos)
        {
            lifted = false;
            boundingBoxes.Add(new BoundingBox(height));
            originalHeight = height;
            slide = slideModifier;
        }

        /// <summary>
        /// Sets if the chunk should be lifted or not.
        /// </summary>
        /// <param name="lifted"></param>
        public void setLifted(bool lifted)
        {
            this.lifted = lifted;
        }

        /// <summary>
        /// Gets if the Moving Chunk has already been lifted.
        /// </summary>
        /// <returns></returns>
        public bool getLifted()
        {
            return lifted;
        }

        /// <summary>
        /// Raises the chunk if the lift is activated.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="graphics"></param>
        public override void draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            //setColor(Microsoft.Xna.Framework.Color.Aqua);
            base.draw(spriteBatch, graphics);

            Vector2 vector = getVector();
            vector.Y += 30;
            vector.X += 32;
            spriteBatch.Draw(ResourceHandler.getInstance().getWoodenFloorTexture(), vector, new Rectangle((int)vector.X, (int)vector.Y + 10, CHUNKWIDTH- 64, getHeight()), color, 0, InputManager.getInstance().getCurrentPlayer().getVector(), 1, SpriteEffects.None, 0);
                

            if(getLifted())
            {
                if(boundingBoxes.ElementAt(0).getHeight() != originalHeight + slide)
                {
                    int addBy = 1;
                    if (slide < 0) addBy = -1;

                    foreach(BoundingBox box in boundingBoxes)
                    {
                        box.setHeight(box.getHeight() + addBy);
                    }
                }
            } else
            {
                if (boundingBoxes.ElementAt(0).getHeight() != originalHeight)
                {
                    int addBy = -1;
                    if (slide < 0) addBy = 1;

                    foreach (BoundingBox box in boundingBoxes)
                    {
                        box.setHeight(box.getHeight() + addBy);
                    }
                }
            }
        }
    }
}
