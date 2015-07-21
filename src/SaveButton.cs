using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * A Save Button that will show the Save Dialog.
     */
    class SaveButton : Button
    {
        /// <summary>
        /// Creates a Button to open the Save Dialog.
        /// </summary>
        public SaveButton() : base()
        {
            setText("Save Game");
            setColor(Microsoft.Xna.Framework.Color.White);
            setAction(new SaveButtonAction());
        }

        
    }
}
