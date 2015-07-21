using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{

    /* Written By: Jesse Mitchell
     * 
     * This is a button in the main menu to exit the game.
     * 
     */
    class ExitButton : Button
    {
        /// <summary>
        /// This is the action executed to close the program.
        /// </summary>
        internal class ExitButtonAction : GuiAction
        {
            /// <summary>
            /// Kills the program.
            /// </summary>
            public override void executeAction()
            {
                Environment.Exit(0);
            }
        }

        public ExitButton() : base(Vector2.Zero, "Exit", Color.White)
        {
            setAction(new ExitButtonAction());
        }
    }
}
