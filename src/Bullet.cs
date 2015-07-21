using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    [Serializable()]
    class Bullet : Mob
    {
        private bool direction;

        public Bullet(bool direction) : base("Bullet", 1, 99, 99, false)
        {
            this.direction = !direction;
            this.height = 16;
            this.width = 16;
        }

        public override void gravityPull()
        {
            //no gravity pulling
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.Graphics.GraphicsDevice graphics)
        {
            base.draw(spriteBatch, graphics);
            if (direction)
            {
                moveX(-6);
            }
            else moveX(6);

            if (getCurrentChunkLeft().checkLeftSideCollision()) getHealth().setValue(-1);
            if (getCurrentChunkRight().checkRightSideCollision()) getHealth().setValue(-1);

            Mob[] entities = DungeonManager.getInstance().getCurrentDungeon().getEntities();
            foreach(Mob entity in entities)
            {
                if(!entity.Equals(this))
                if(collidingWith(entity))
                {
                    entity.damageEntity(17);
                    getHealth().setValue(-1);
                    break;
                }
            }

        }


    }
}
