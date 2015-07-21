using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AetherQuest
{
    /* Written By : Jesse Mitchell
     * 
     * This is a clickable, it will interact with the input manager to detect if it has been
     * clicked upon.
     * 
     */
    [Serializable()]
    class Clickable
    {
        [NonSerialized()]
        protected Vector2 vector;
        [NonSerialized()]
        protected Color backgroundColor;
        protected int width;
        protected int height;
        private int layer;

        [NonSerialized()]
        protected Texture2D texture;

        private Boolean pressed;
        protected Boolean shown;

        //Constructors
        public Clickable() : this(0,0)
        {

        }

        public Clickable(int x, int y) : this(new Vector2(x,y))
        {

        }

        public Clickable(Vector2 vector) : this(vector, 0, 0)
        {
            
        }

        public Clickable(Vector2 vector, int width, int height) : this(vector,width, height, Color.Gold)
        {
        }

        public Clickable(Vector2 vector, int width, int height, Color backgroundColor)
        {
            this.vector = vector;
            this.width = width;
            this.height = height;
            this.shown = true;
            this.layer = 0;
           
            this.backgroundColor = backgroundColor;

            this.texture = ResourceHandler.getInstance().getCobblestoneTexture();
        }

        /// <summary>
        /// Sets if the Clickable is renderable.
        /// </summary>
        /// <param name="info"></param>
        virtual public void setEnabled(bool info)
        {
            shown = info;
        }

        /// <summary>
        /// Get if the clickable is enabled.
        /// </summary>
        /// <returns></returns>
        public bool getEnabled()
        {
            return shown;
        }

        /// <summary>
        /// Tells the clickable if it has been pressed.
        /// </summary>
        /// <param name="pressed"></param>
        virtual public void tellPressed(bool pressed)
        {
            this.pressed = pressed; 
        }

        /// <summary>
        /// Checks if the clickable has been pressed.
        /// </summary>
        /// <returns></returns>
        virtual public Boolean isPressed()
        {
            if (pressed)
            {
                return true;
            }
            else
            {
                InputManager.getInstance().isPressing(this); //sends request to the InputManager
                return false;
            }
        }

        /// <summary>
        /// Checks if the right mouse button is clicking it.
        /// </summary>
        /// <returns></returns>
        public Boolean isRightPressed()
        {
            return InputManager.getInstance().isRightPressing(this);
        }

        /// <summary>
        /// Sets the color of the background
        /// </summary>
        /// <param name="color"></param>
        public void setBackground(Color color)
        {
            backgroundColor = color;
        }

        /// <summary>
        /// sets the importance of the clickable.
        /// </summary>
        /// <param name="z"></param>
        public void setLayer(int z)
        {
            layer = z;
        }

        /// <summary>
        /// Gets the importance.
        /// </summary>
        /// <returns></returns>
        public int getLayer()
        {
            return layer;
        }

        virtual public void draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            if (shown)
            {
                if(isPressed())
                {
                    Color darkColor = new Color(backgroundColor.R + 80, backgroundColor.G + 80, backgroundColor.B + 80);
                    spriteBatch.Draw(texture, getVector(), new Rectangle((int)getVector().X, (int)getVector().Y, getWidth(), getHeight()), darkColor, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                }
                else
                spriteBatch.Draw(texture, getVector(), new Rectangle((int)getVector().X, (int)getVector().Y, getWidth(), getHeight()), backgroundColor, 0, Vector2.Zero, 1, SpriteEffects.None, 0);


                }
        }

        //getters
        virtual public int getWidth()
        {
            return width;
        }

        virtual public int getHeight()
        {
            return height;
        }

        //setters
        public void setX(int x)
        {
            vector = new Vector2(x, vector.Y);
        }

        public void setY(int y)
        {
            vector = new Vector2(vector.X, y);
        }

        public Vector2 getVector()
        {
            return vector;
        }

    }
}
