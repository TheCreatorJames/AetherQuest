using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * This is a menu that resizes itself upon receiving items.
     * 
     */
    class ResizeableMenu : FixedMenu
    {
        public ResizeableMenu(Vector2 vector, Color backgroundColor) : base(vector, 0, 0, backgroundColor)
        {

        }

        /// <summary>
        /// Adds the clickable to the menu, and adds height to it.
        /// </summary>
        /// <param name="clickable"></param>
        override public void add(Clickable clickable)
        {
            height += clickable.getHeight() + 2;
           
            if (clickable.getWidth() > width) width = clickable.getWidth();
            base.add(clickable);
        }

        /// <summary>
        /// add an array of clickables to the menu, and adds height.
        /// </summary>
        /// <param name="clickable"></param>
        override public void add(Clickable[] clickable)
        {
            foreach (Clickable click in clickable)
            {
                height += click.getHeight() + 2;
               
                if (click.getWidth() > width) width = click.getWidth();
                base.add(clickable);
            }
        }


        /// <summary>
        /// Clears the menu and its height.
        /// </summary>
        public override void clear()
        {
            base.clear();
            height = 0;
            width = 0;
        }
        
    }
}
