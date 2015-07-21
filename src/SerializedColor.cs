using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * This is a class used to save colors, as not all XNA and MonoGame Implementations allow this. ):
     * 
     */
    [Serializable()]
    class SerializedColor
    {
        private int r, g, b;

       

        public SerializedColor(Color color) : this((int)color.R, (int)color.G, (int)color.B)
        {

        }

        /// <summary>
        /// Makes the Serializable Color by setting its variables
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public SerializedColor(int r, int g, int b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        /// <summary>
        /// Generates the color to be used.
        /// </summary>
        /// <returns></returns>
        public Color getColor()
        {
            return new Color(r, g, b);
        }
    }
}
