using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    /* Written By : Jesse Mitchell
     * 
     * BoundingBox class
     * This class keeps track of where there are boxes to be drawn on the screen
     * and checks if the player is colliding with them.
     * Provides side and top collision.
     * 
     */
    [Serializable()]
    class BoundingBox
    {
        //height used for rendering
        private int height;

        //Constructor
        public BoundingBox(int height)
        {
            this.height = height;
        }

        /// <summary>
        /// Gets the height of the chunk
        /// </summary>
        /// <returns></returns>
        public int getHeight()
        {
            return height;
        }

        /// <summary>
        /// Sets the height of the chunk.
        /// </summary>
        /// <param name="height"></param>
        public void setHeight(int height)
        {
            this.height = height;
        }

        /// <summary>
        /// Checks collision of the entity on the BoundingBox.
        /// </summary>
        /// <param name="creature"></param>
        /// <returns></returns>
        public Boolean checkCollision(Mob creature)
        {
            if (creature.getY() + creature.getHeight() == ResourceHandler.getInstance().getBottom() - height) return true;
            else if (creature.getY() + creature.getHeight() > ResourceHandler.getInstance().getBottom() - height)
            {
                creature.setY(creature.getY() - 1);
                return true;
            }
            return false;
        }


        /// <summary>
        /// Checks collision on the side of the chunk.
        /// </summary>
        /// <param name="creature"></param>
        /// <returns></returns>
        public Boolean checkSideCollision(Mob creature)
        {
            //creature.moveY(-1);
            bool result = (creature.getY() - 2 + creature.getHeight() >= ResourceHandler.getInstance().getBottom() - height);
            //creature.moveY(1);
            return result;
        }
    }
}
