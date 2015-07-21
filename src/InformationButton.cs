using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * This was written to give information to the player.
     * (:
     * 
     */
    class InformationButton : Button
    {

        /// <summary>
        /// Gives information to the Quest Menu.
        /// To be Displayed.
        /// </summary>
        internal class InformationButtonAction : GuiAction
        {
            private String information;
            public InformationButtonAction(String info)
            {
                information = info;
            }

            /// <summary>
            /// Sets the Information of the Quest Menu.
            /// </summary>
            public override void executeAction()
            {
                MenuManager.getInstance().getQuestMenu().setGivenInformation(information);
            }
       }

        public InformationButton(String name, String info) : base(Vector2.Zero, name, Color.White)
        {
            setAction(new InformationButtonAction(info));
        }
    }
}
