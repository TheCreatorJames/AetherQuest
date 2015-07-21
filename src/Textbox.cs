using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    /* Written By : Jesse Mitchell
     * 
     * This will allow you to type things in, if you click on it and hover over it.
     * 
     */
    class Textbox : Clickable
    {
        private String text = "";
        private int pos = 0;
        private int maxLength;

        private GuiAction action;

        public Textbox() : base(Vector2.Zero, 100, 20, Color.White)
        {
            setLayer(20);
            maxLength = -1;
        }

        public override void draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
           if(getEnabled())
           try
           {

               if (texture == null)
               {
                   Texture2D rectangleTexture = new Texture2D(graphicsDevice, getWidth(), getHeight(), false, SurfaceFormat.Color);

                   Color[] color = new Color[getWidth() * getHeight()];

                   for (int i = 0; i < color.Length; i++)
                   {
                       color[i] = backgroundColor;
                   }

                   rectangleTexture.SetData(color);
                   texture = rectangleTexture;
               }

               base.draw(spriteBatch, graphicsDevice);

               if (text.Length > 0)
               {
                   int width = (int)ResourceHandler.getInstance().getVerdana().MeasureString(text.Substring(pos)).X;
                   if (width > getWidth())
                   {
                       pos++;
                   }

                   spriteBatch.DrawString(ResourceHandler.getInstance().getVerdana(), text.Substring(pos), getVector(), Color.Red);
               }
           } catch(Exception ex)
           {
               removeText();
           }
        }
        
        public void setWidth(int x)
        {
            width = x;
            texture = null;
        }

        public void setAction(GuiAction action)
        {
            this.action = action;
        }

        public void addText(String x)
        {
            if(maxLength == -1 || text.Length + x.Length < maxLength)
            text += x;
        }

        public void setMaxLength(int max)
        {
            this.maxLength = max;
        }

        public void executeAction()
        {
            if (action != null) action.executeAction();
        }

        public void removeText()
        {
            if(pos > 0)
            pos--;
            if (text.Length > 0) 
            text = text.Substring(0, text.Length - 1);
        }


        public string getText()
        {
            return text;
        }
    }
}
