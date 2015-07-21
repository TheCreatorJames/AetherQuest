using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * This button is used to generate a new dungeon for the player to play one.
     */
    class GenerateDungeonButton : Button
    {
        internal class GenerateDungeonButtonAction : GuiAction
        {
            /// <summary>
            /// Generates a new dungeon and sets it to be the current one.
            /// </summary>
            public override void executeAction()
            {
                DungeonManager.getInstance().setCurrentDungeon(new Dungeon());
                InputManager.getInstance().getCurrentPlayer().setVector(DungeonManager.getInstance().getCurrentDungeon().getFirstChunk().getVector());

                InputManager.getInstance().setCurrentPlayer(new Player("Default Bane", 180));


                while (!DungeonManager.getInstance().getCurrentDungeon().getCurrentChunkLeft().checkLeftTopCollision())
                {
                    InputManager.getInstance().getCurrentPlayer().gravityPull();
                }
            }
        }

        /// <summary>
        /// Makes a new Generate Button for the main menu.
        /// </summary>
        public GenerateDungeonButton() : base(Vector2.Zero, "Generate Dungeon", Color.White, Color.Red)
        {
            setAction(new GenerateDungeonButtonAction());
        }
    }
}
