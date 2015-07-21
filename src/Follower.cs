using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * This is a test of the entities following players (:
     * 
     */
    [Serializable()]
    class Follower : Mob
    {
        public Follower()
            : base("Follower")
        {
            width = 30;
            height = 40;
            setX(InputManager.getInstance().getCurrentPlayer().getX());
        }


        virtual public void followPlayer(Dungeon dungeon)
        {
            if (!collidingWith(InputManager.getInstance().getCurrentPlayer()))
            {
                if (getDistanceFrom(InputManager.getInstance().getCurrentPlayer()) < 450)
                {
                    if (getVector().X < InputManager.getInstance().getCurrentPlayer().getX())
                    {
                        if (getVector().Y > InputManager.getInstance().getCurrentPlayer().getY())
                        {
                            if (dungeon.getCurrentChunkRight(this).checkRightSideCollision(this))
                            {

                                if (dungeon.getCurrentChunkLeft(this).checkRightTopCollision(this))
                                    moveY(-32);
                                else moveX(-2);
                            }
                            else moveX(2);
                        }
                        else moveX(2);

                    }
                    else
                    {
                        if (getVector().Y > InputManager.getInstance().getCurrentPlayer().getY())
                        {
                            if (dungeon.getCurrentChunkLeft(this).checkLeftSideCollision(this))
                            {
                                if (dungeon.getCurrentChunkRight(this).checkLeftTopCollision(this))
                                    moveY(-32);
                                else moveX(2);
                            }
                            else moveX(-2);
                        }
                        else moveX(-2);
                    }
                }
            }
            
        }
    }
}
