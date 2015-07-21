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
         * This is a label.
         */
   
    class Label : Clickable
    {
        private String text;
        [NonSerialized()]
        private Color color;

        public Label(Vector2 vector, String text, Color color) : base(vector, 0,0,Color.Black)
        {
            this.color = color;
            this.text = text;
        }

        public void setText(String text)
        {
            this.text = text;
        }

        public String getText()
        {
            return text;
        }
        public override int getHeight()
        {
            return (int)ResourceHandler.getInstance().getVerdana().MeasureString(text).Y;
        }

        public override void draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            spriteBatch.DrawString(ResourceHandler.getInstance().getVerdana(), text, vector, color);
        }
    }
}
