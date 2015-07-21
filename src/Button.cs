using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * When clicked on, this will execute an action.
     * 
     */
     [Serializable()]
    class Button : Clickable
    {
        private String text;
         [NonSerialized()]
        private Color textColor;

        private GuiAction action;

        public Button() : base()
        {

        }

        public Button(Vector2 vector, String text, Color color) : this(vector, text, color, Color.Blue)
        {
            
        }

        public Button(Vector2 vector, String text, Color color, Color backgroundColor) : base(vector, 0, 0, backgroundColor)
        {
            this.text = text;
            this.textColor = color;
        }
        
         /// <summary>
         /// Sets the text of the button.
         /// </summary>
         /// <param name="text"></param>
        public void setText(String text)
        {
            this.text = text;
        }

         /// <summary>
         /// Sets the action to be executed when button is clicked
         /// </summary>
         /// <param name="action"></param>
        public void setAction(GuiAction action)
        {
            this.action = action;
        }

         /// <summary>
         /// Draws the Button
         /// </summary>
         /// <param name="spriteBatch"></param>
         /// <param name="graphicsDevice"></param>
        public override void draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            if (shown)
            {
                base.draw(spriteBatch, graphicsDevice);
                spriteBatch.DrawString(ResourceHandler.getInstance().getVerdana(), getText(), new Vector2(getVector().X + 1, getVector().Y + 1), this.textColor);
            }
          }

         /// <summary>
         /// Gets the action of the button.
         /// </summary>
         /// <returns></returns>
        public GuiAction getAction()
        {
            return action;
        }

         /// <summary>
         /// Allows the input manager to tell the button if it is pressed.
         /// </summary>
         /// <param name="pressed"></param>
        public override void tellPressed(bool pressed)
        {
            base.tellPressed(pressed);
            if(action != null) if (pressed) action.executeAction();
        }

         /// <summary>
         /// Executes the action.
         /// </summary>
        public void executeAction()
        {
            if (action != null) action.executeAction();
        }

         /// <summary>
         /// Gets the width of the button.
         /// </summary>
         /// <returns></returns>
        public override int getWidth()
        {
            return (int)ResourceHandler.getInstance().getVerdana().MeasureString(getText()).X + 2;
        }

         /// <summary>
         /// gets the height of the button.
         /// </summary>
         /// <returns></returns>
        public override int getHeight()
        {
            return (int)ResourceHandler.getInstance().getVerdana().MeasureString("Hello").Y + 2;
        }

         /// <summary>
         /// gets the text inside the button.
         /// </summary>
         /// <returns></returns>
        virtual public String getText()
        {
            return text;  
        }

         /// <summary>
         /// Checks if it is pressed.
         /// </summary>
         /// <returns></returns>
        public override bool isPressed()
        {
            return base.isPressed();
        }

         /// <summary>
         /// Sets the color of the button.
         /// </summary>
         /// <param name="color"></param>
        internal void setColor(Color color)
        {
            this.textColor = color;
        }
    }
}
