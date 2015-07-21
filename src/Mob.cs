using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * This is a class that will be used to create the player class, enemy class, and NPC class
     * 
     * Stores simple stats
     */
    [Serializable()]
    class Mob : Inventory
    {
        private string name;

        [NonSerialized()]
        private Vector2 vector;

        [NonSerialized()]
        protected Texture2D texture;


        private float x;
        private float y;

        private float lastX;
        protected bool direction;

        protected int width, height;
        protected float acceleration;

        private CappedStatistic health;

        public Mob()
            : base()
        {

        }

        public Mob(String name)
            : this(name, 25)
        {

        }

        public Mob(String name, int health)
            : this(name, health, 0, 0)
        {

        }

        public Mob(String name, int health, int x, int y)
            : this(name, health, x, y, false)
        {

        }

        public Mob(String name, int health, int x, int y, bool player)
            : base(20, player)
        {
            this.x = x;
            this.y = y;
            this.name = name;
            this.width = 0;
            this.height = 0;

            this.health = new CappedStatistic("health", health);

        }

        //Getters 

        public int getX()
        {
            return (int)x;
        }

        public void checkDirection()
        {
            if (lastX < x)
            {
                direction = true;
            }
            else if (lastX > x)
            {
                direction = false;
            }

            lastX = x;
        }

        public int getY()
        {
            return (int)y;
        }

        public int getWidth()
        {
            return width;
        }

        public int getHeight()
        {
            return height;
        }

        public Vector2 getVector()
        {
            if (vector.X == 0) vector = new Vector2(x, y);
            return vector;
        }

        //returns the name
        public String getName()
        {
            return name;
        }

        /// <summary>
        /// gets the current chunk left of the entity
        /// </summary>
        /// <returns></returns>
        public Chunk getCurrentChunkLeft()
        {
            return DungeonManager.getInstance().getCurrentDungeon().getCurrentChunkLeft(this);
        }

        /// <summary>
        /// gets the current chunk right of the entity
        /// </summary>
        /// <returns></returns>
        public Chunk getCurrentChunkRight()
        {
            return DungeonManager.getInstance().getCurrentDungeon().getCurrentChunkRight(this);
        }

        public CappedStatistic getHealth()
        {
            return health;
        }

        //Setters 

        public void setX(int p)
        {
            this.vector.X = this.x = p;
        }

        public void setVector(Vector2 vector)
        {
            this.vector.X = x = vector.X;
            this.vector.Y = y = vector.Y;
        }

        public void setY(int p)
        {
            this.vector.Y = y = p;
        }

        public void setPos(int p1, int p2)
        {
            this.vector.X = x = p1;
            this.vector.Y = y = p2;
        }

        /// <summary>
        /// Kills the acceleration after gravity pulling.
        /// </summary>
        public void killAcceleration()
        {
            acceleration = 0;
        }

        /// <summary>
        /// pulls the Entity down and accelerates
        /// </summary>
        virtual public void gravityPull()
        {
            this.vector.Y = y += 1 + acceleration;
            acceleration += 0.15f;
        }

        public void setName(string p)
        {
            name = p;
        }

        public void setHealthModifier(int p)
        {
            health.setModifier(p);
        }


        /// <summary>
        /// Gets the distance from another entity.
        /// </summary>
        /// <param name="creature"></param>
        /// <returns></returns>
        public float getDistanceFrom(Mob creature)
        {
            return Vector2.Distance(getVector(), creature.getVector());
        }


        /// <summary>
        /// Checks entity collision.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool collidingWith(Mob entity)
        {
            if (getVector().X + getWidth() >= entity.getVector().X && getVector().X <= entity.getVector().X + entity.getWidth())
            {
                if (getVector().Y + getHeight() >= entity.getVector().Y && getVector().Y <= entity.getVector().Y + entity.getHeight())
                {
                    return true;
                }
            }

            return false;
        }


        virtual public void draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {

            

            checkDirection();
            if (texture == null)
            {
                texture = ResourceHandler.getInstance().getBlankTexture(graphics, getWidth(), getHeight());
            }

            int pX = InputManager.getInstance().getCurrentPlayer().getX();
            int pY = InputManager.getInstance().getCurrentPlayer().getY();

            SpriteEffects se = SpriteEffects.None;

            if (direction) se = SpriteEffects.FlipHorizontally;

            Vector2 modified = new Vector2(getVector().X - InputManager.getInstance().getCurrentPlayer().getX() + graphics.Viewport.Bounds.Center.X, graphics.Viewport.Bounds.Center.Y + getVector().Y - InputManager.getInstance().getCurrentPlayer().getY());

            Color mColor = Color.Green;
            mColor.R -= (byte)(getHealth().getMaxValue() - getHealth().getValue() * 2);

            
            
            spriteBatch.Draw(texture, modified, null, mColor, 0,
       Vector2.Zero, 1.0f, se, 0f);
            
            if (this.getHealth().getValue() <= 0)
            {
                InputManager.getInstance().getCurrentPlayer().addCredits(1);
                DungeonManager.getInstance().getCurrentDungeon().removeEntity(this);
            }
        }

        /// <summary>
        /// Moves the pos.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        public void movePos(int p1, int p2)
        {
            this.vector.X = x += p1;
            this.vector.Y = y += p2;
        }

        public void moveX(int p)
        {
            this.vector.X = x += p;
        }

        public void moveY(int p)
        {
            this.vector.Y = y += p;
        }

        virtual public void attack(Mob creature)
        {
            int damage = new Random().Next(5);
            creature.damageEntity(damage);
        }

        virtual public void damageEntity(int damage)
        {
            health.setValue(health.getValue() - damage);
        }
    }
}
