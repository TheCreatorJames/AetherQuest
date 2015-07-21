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
     * 
     * This is a fixed menu that will display clickables and information.
     * 
     */
    class FixedMenu : Clickable
    {
        private List<Clickable> items;

        private bool scrollable;

        private Button scrollButton;
        private int slide;

        public FixedMenu(Vector2 vector, int width, int height, Color color) : base(vector, width, height, color)
        {
            items = new List<Clickable>();
        }

        public FixedMenu(Vector2 vector, int width, int height, Color color, bool scroll)
            : base(vector, width, height, color)
        {
            items = new List<Clickable>();

            if(scroll)
            {
                scrollButton = new Button(Vector2.Zero, "Scroll >>>", Color.White);
                scrollButton.setAction(new ScrollButtonAction(this));
                add(scrollButton);
            }
            
            scrollable = scroll;
            
        }

        /// <summary>
        /// Adds clickable to the menu.
        /// </summary>
        /// <param name="clickable"></param>
        virtual public void add(Clickable clickable)
        {
            clickable.setLayer(getLayer() + 1);
            items.Add(clickable);
        }

        /// <summary>
        /// Adds clickable array to the menu.
        /// </summary>
        /// <param name="clickable"></param>
        virtual public void add(Clickable[] clickable)
        {
            foreach(Clickable click in clickable)
            {
                click.setLayer(getLayer() + 1);
                items.Add(click);
            }
        }


        /// <summary>
        /// Resizes the menu.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        virtual public void resize(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        //Scrolls up.
        public void scrollUp()
        {
            slide++;
        }

        //never sees that it is pressed.
        public override bool isPressed()
        {
            return false;
        }

        //removes all of the clickables from the list.
        virtual public void clear()
        {
            items.Clear();
            if (scrollable) add(scrollButton);
        }

        public override void draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            
            base.draw(spriteBatch, graphicsDevice);
            //spriteBatch.Draw(FontHandler.getInstance().getTexture(),  getVector(), new Rectangle((int)getVector().X, (int)getVector().Y, getWidth(), getHeight()), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            //spriteBatch.Draw(FontHandler.getInstance().getTexture(), new Rectangle((int)getVector().X, (int)getVector().Y, 64, 64), Color.White);


            int i = 0;
            int n = 0;

            if(scrollable)
            {
                scrollButton.setX((int)vector.X);
                scrollButton.setY((int)vector.Y + i * 2 + n);

                n += scrollButton.getHeight();

                if (n <= this.getHeight())
                {
                    scrollButton.setBackground(this.backgroundColor);
                    scrollButton.draw(spriteBatch, graphicsDevice);
                }
            }

            if (slide >= items.Count) slide = 0;

            foreach(Clickable clickable in items)
            {
                if(!scrollable || i != 0)
                if(!scrollable || i >= slide)
                {
                    clickable.setX((int)vector.X);
                    clickable.setY((int)vector.Y + i * 2 + n);

                    n += clickable.getHeight();

                    if(n <= this.getHeight())
                    {
                        clickable.setBackground(this.backgroundColor);
                        clickable.draw(spriteBatch, graphicsDevice);
                    }
                }
                i++;
            }

        }
    }
}
