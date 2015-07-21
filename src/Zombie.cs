using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    /* Written By : Jesse Mitchell
     * 
     * This is a zombie. It will be fast, and evil.
     * 
     */
    [Serializable()]
    class Zombie : Follower
    {
        private bool frame;
        private byte count;
        private float lastX;

        public Zombie()
        {
            height = 48;
        }
        /// <summary>
        /// Follows the player and attacks him when touching him.
        /// </summary>
        /// <param name="dungeon"></param>
        public override void followPlayer(Dungeon dungeon)
        {
            base.followPlayer(dungeon);
           
            if(collidingWith(InputManager.getInstance().getCurrentPlayer()))
            {
                Random rand = new Random();
                int lel = rand.Next(10);
                if (lel == 9)
                {
                    attack(InputManager.getInstance().getCurrentPlayer());
                }
            }
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.Graphics.GraphicsDevice graphics)
        {
            if(!(getX() == lastX))
            {
                count++;
            }

            if(count >= 10)
            {
                count = 0;
                frame = !frame;
            }


            if(frame)
            {
                texture = ResourceHandler.getInstance().getZombieTexture(); 
            }
            else
            {
                texture = ResourceHandler.getInstance().getZombie2Texture();
            }

            
            base.draw(spriteBatch, graphics);
            lastX = getX();
        }
    }
}
