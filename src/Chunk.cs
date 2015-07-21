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
     * Contains the bounding boxes and checks for collisions. This is what powers the dungeon.
     */
     [Serializable()]
    class Chunk
    {
        public static int CHUNKWIDTH = 160;

        protected BoundingBoxContainer boundingBoxes;
        protected SerializedColor sColor;

        [NonSerialized()]
        protected Color color;
        protected int chunkPos;
       
        private float x, y;
        public Chunk(int chunkPos)
        {
            if (Dungeon.getDeserializing()) return;
            this.chunkPos = chunkPos;
            this.color = Color.Orange;
            this.sColor = new SerializedColor(color);
            boundingBoxes = new BoundingBoxContainer();
        }

         /// <summary>
         /// sets the color of the Chunk.
         /// </summary>
         /// <param name="col"></param>
        public void setColor(Color col)
        {
            color = col;
            sColor = new SerializedColor(col);
        }

         public void setXY(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

         public float getX()
         {

             return x;
         }

         public float getY()
         {
             return y;
         }

         /// <summary>
         /// checks if the player is inside of the chunk.
         /// </summary>
         /// <returns></returns>
        public bool isInside()
        {
            return isInside(InputManager.getInstance().getCurrentPlayer());
        }

         /// <summary>
         /// Checks if the entity is even inside of the chunk.
         /// </summary>
         /// <param name="creature"></param>
         /// <returns></returns>
        virtual public bool isInside(Mob creature)
        {
            int creatureX = creature.getX();

            if (creatureX < chunkPos * CHUNKWIDTH + CHUNKWIDTH)
            {
                if (creatureX > chunkPos * CHUNKWIDTH)
                {
                    return true;
                }
            }

            return false;
        }

         /// <summary>
         /// checks for top collision from the entity from the left. 
         /// </summary>
         /// <param name="creature"></param>
         /// <returns></returns>
        virtual public bool checkLeftTopCollision(Mob creature)
        {
            int boxNum = creature.getX() % CHUNKWIDTH / (CHUNKWIDTH / boundingBoxes.Count);

            if (creature.getX() - chunkPos * CHUNKWIDTH <= 0)
            {
                boxNum = 0;
            }

            if (boxNum >= boundingBoxes.Count) return false;

            return boundingBoxes.ElementAt(boxNum).checkCollision(creature);
        }

         /// <summary>
         /// Checks if the top is collided with from the left
         /// </summary>
         /// <returns></returns>
        public bool checkLeftTopCollision()
        {
            return checkLeftTopCollision(InputManager.getInstance().getCurrentPlayer());
        }

         /// <summary>
         /// Checks if there is top collision of Entity from the right.
         /// </summary>
         /// <param name="creature"></param>
         /// <returns></returns>
        public bool checkRightTopCollision(Mob creature)
        {
            int boxNum = (creature.getX() + creature.getWidth()) % CHUNKWIDTH / (CHUNKWIDTH / boundingBoxes.Count);

            if (creature.getX() - chunkPos * CHUNKWIDTH <= 0)
            {
                boxNum = 0;
            }

            if (boxNum >= boundingBoxes.Count) return false;

            return boundingBoxes.ElementAt(boxNum).checkCollision(creature);
        }


         /// <summary>
         /// Checks if there is top collision from the right.
         /// </summary>
         /// <returns></returns>
        public bool checkRightTopCollision()
        {
            return checkRightTopCollision(InputManager.getInstance().getCurrentPlayer());
        }


         /// <summary>
         /// Checks if there is collision of the entity from the left side.
         /// </summary>
         /// <param name="creature"></param>
         /// <returns></returns>
        public bool checkLeftSideCollision(Mob creature)
        {
            int boxNum = creature.getX() % CHUNKWIDTH / (CHUNKWIDTH / boundingBoxes.Count);

            if (creature.getX() - chunkPos * CHUNKWIDTH <= 0)
            {
                boxNum = 0;
            }

            if (boxNum >= boundingBoxes.Count) return false;

            return boundingBoxes.ElementAt(boxNum).checkSideCollision(creature);
        }

         /// <summary>
         /// Checks if there is Collision from the Left Side.
         /// </summary>
         /// <returns></returns>
        public bool checkLeftSideCollision()
        {
            return checkLeftSideCollision(InputManager.getInstance().getCurrentPlayer());
        }


         /// <summary>
         /// Gets the max height of the chunk.
         /// </summary>
         /// <returns></returns>
        public int getHeight()
        {
            return boundingBoxes.ElementAt(boundingBoxes.Count - 1).getHeight();
        }

         /// <summary>
         /// Checks for collision of entity from the right side of the chunk. 
         /// </summary>
         /// <param name="creature"></param>
         /// <returns></returns>
        public bool checkRightSideCollision(Mob creature)
        {
            int boxNum = (creature.getX() + creature.getWidth()) % CHUNKWIDTH / (CHUNKWIDTH / boundingBoxes.Count);

            if (creature.getX() + creature.getWidth() - chunkPos * CHUNKWIDTH <= 0)
            {
                boxNum = 0;
            }

            if (boxNum >= boundingBoxes.Count) return false;

            return boundingBoxes.ElementAt(boxNum).checkSideCollision(creature);
        }

         /// <summary>
         /// Checks for collision from the right side of the chunk.
         /// </summary>
         /// <returns></returns>
        public bool checkRightSideCollision()
        {
            return checkRightSideCollision(InputManager.getInstance().getCurrentPlayer());
        }


         /// <summary>
         /// Gets the vector of the chunk.
         /// </summary>
         /// <returns></returns>
        public Vector2 getVector()
        {
            return new Vector2(x, y);
        }
       

        virtual public void draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            int boundingBoxPos = 0;

            if (color.R == 0
                && color.G == 0
                && color.B == 0) color = sColor.getColor();

            foreach (BoundingBox box in boundingBoxes)
            {
                int extraHeight = 0;
                Vector2 vector = new Vector2(chunkPos * CHUNKWIDTH + boundingBoxPos++ * (CHUNKWIDTH / boundingBoxes.Count + 1) + graphics.Viewport.Bounds.Center.X, graphics.Viewport.Bounds.Bottom - box.getHeight() + graphics.Viewport.Bounds.Center.Y);
                
                if (boundingBoxPos == 1)
                {
                    x = vector.X;
                    y = vector.Y;
                }
                
                if(InputManager.getInstance().getCurrentPlayer().getY() + graphics.Viewport.Bounds.Center.Y > 500)
                {
                    extraHeight = 350;
                }

               Vector2 playerVector = InputManager.getInstance().getCurrentPlayer().getVector();
               spriteBatch.Draw(ResourceHandler.getInstance().getCobblestoneTexture(), new Vector2(vector.X, -5000), new Rectangle((int)vector.X, (int)0,  CHUNKWIDTH, 6440), color, 0, playerVector, 1, SpriteEffects.None, 1);

                if (vector.X >= -CHUNKWIDTH && vector.X - playerVector.X <= graphics.Viewport.Bounds.Right)
                    spriteBatch.Draw(ResourceHandler.getInstance().getGoldenBrickTexture(), vector, new Rectangle((int)vector.X, (int)vector.Y, CHUNKWIDTH / boundingBoxes.Count + 1, box.getHeight() + extraHeight), color, 0, playerVector, 1, SpriteEffects.None, 0);
                else break;
             }

        }

        public Color getColor()
        {
            return color;
        }
    }
}
