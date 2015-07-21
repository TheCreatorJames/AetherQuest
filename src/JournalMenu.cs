using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * Shows the list of quests to do.
     * 
     * Currently functions as a journal to teach the player.
     * 
     */
    class JournalMenu : FixedMenu
    {
        private FixedMenu questList;
        private FixedMenu completedQuestList;
        private FixedMenu questInformation;
        private FixedMenu questRewards;

        private FixedMenu playerStats;

        public JournalMenu(int width, int height) :
             base(new Microsoft.Xna.Framework.Vector2(0, 0), width, height, Microsoft.Xna.Framework.Color.Black)
        {
            questList = new FixedMenu(vector, getWidth() / 4, (int)(getHeight() / 1.8), Microsoft.Xna.Framework.Color.LightSteelBlue, true);
            completedQuestList = new FixedMenu(new Vector2(0, (int)(getHeight() / 1.8) + 1), getWidth() / 4, getHeight() - (int)(getHeight() / 1.8), Microsoft.Xna.Framework.Color.LightSteelBlue, false);
            questInformation = new FixedMenu(new Vector2(getWidth() - getWidth() / 4, 0), getWidth() / 4, (int)(getHeight() / 1.8), Microsoft.Xna.Framework.Color.LightSteelBlue);
            questRewards = new FixedMenu(new Vector2(getWidth() - getWidth() / 4, (int)(getHeight() / 1.8) + 1), getWidth() / 4, (int)(getHeight() / 1.8), Microsoft.Xna.Framework.Color.LightSteelBlue);

            playerStats = new FixedMenu(new Vector2(getWidth() / 4 + 1, getHeight() - getHeight() / 3), -(int)(questList.getVector().X + getWidth() / 4 - questInformation.getVector().X) - 2, getHeight() / 3, Color.LightSteelBlue);
            setEnabled(false);

            addInformation("Dear Diary", "First of all, let me get this straight This is a journal, not a diary So don't expect sissy entries in here. K? I might lose my mind in this dungeon It seems to go on forever But I don't mind. I'm losing my mind.");
            addInformation("Zombies", "The slimy green guys? The things that look like some pixel artist couldn't make art? Those are legitimate real zombies. I guarantee it.");
            addInformation("Zombies II", "The only way to attack them is to use your BRAINNNNS! Nah, just kidding, use your melee and ranged attacks The 2 and 3 buttons on the  keyboard should work. Quickly press the 2 button to melee effectively. This is not an easy life to live.");
            addInformation("Zombies III", "It's kill or be killed in this Dungeon. The faster you kill them, the longer you'll live. Funny how that works right?");
            addInformation("Taverns", "Apparently, it was planned that these Taverns Would have shops and such. I guess they packed up and left And couldn't finish");
            addInformation("Me", "MEEEEEEEEEEEEEEEEEE! I was bored, okay? Having amnesia isn't fun. And I know I'll plunge deeper   What was I writing about?");
            addInformation("Sorry", "That last journal entry was not important. I apologize.");
            addInformation("Sorry II", "Neither was that last one, nor this one");
            addInformation("Elevators", "There are elevators in this dungeon. I could not figure it out at first, but those chunks with the extra line down it? Tap the activate button to use them. Watch out for Zombies though. Freaking Creepy man.");
            addInformation("Doors", "I must have really lost my mind. I seem to have forgotten DOORS. DOORS DUDE. Tap the 1 button to go through the door.  Seriously. Who forgets how to use a door?");
            addInformation("Chests", "Chests are a little less obvious. Press on them, or, even better, hit the Inventory key when next to it.");
            addInformation("Rewards", "When we kill these monsters, I earn credit that allows me to remember my past. I've hidden supplies in this dungeon before, so if you earn me some credit, I can remember where some of those supplies are and retrieve it.");
            addInformation("Credits", "These names come to mind for some weird reason. Jesse Mitchell, Soren Swanson, Alex Gonzales, and Tavian Floyd.\nWhy do I know these names??");
            addInformation("Credits II", "I feel like if I could talk to them, they'd want to say a few things. First, a huge thank you to the CSSA for allowing them to begin this project.  And Something about how Jesse was Project Lead and other ambiguously defined roles");
            addInformation("Credits III", "They'd also probably like to thank the Business Professionals of America for allowing them to display their skills in national competition.");
            addInformation("Credits IIII", "But most importantly, They'd probably like to thank Susan Sevier: The greatest mentor and teacher we've ever had.\n\nThank you Mrs. Sevier. All of us loved working with you throughout our high school years, and without you we would not have gotten nearly as far as we have.");

            completedQuestList.add(new Label(Vector2.Zero, "- Rewards -", Color.White));
            completedQuestList.add(new BuyAmmoButton());
            completedQuestList.add(new BuyPotionButton());
            
        }

        /// <summary>
        /// Adds A button to open a journal entry to inform the player of content.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="information"></param>
        public void addInformation(String title, String information)
        {

            //parses the data, allowing it to be spaced properly and wrapped
            String testing = " ";
            while(ResourceHandler.getInstance().getVerdana().MeasureString(testing).X < questList.getWidth())
            {
                testing += " ";
            }
            
            int num = testing.Length - 21;
            //wraps the text by replacing spaces with \n's 
            for (int i = 0; i < information.Length; i += num)
            {
                if (information.IndexOf(' ', i) != -1)
                information = information.Substring(0, information.IndexOf(' ', i)) + "\n" + information.Substring(information.IndexOf(' ', i));
            }



            questList.add(new InformationButton(title, " - " + title + " - " + information));
        }

        /// <summary>
        /// Presents the information from a Journal Entry.
        /// </summary>
        /// <param name="info"></param>
        public void setGivenInformation(String info)
        {
            questInformation.clear();
            questInformation.add(new Label(Vector2.Zero, info, Color.White));
        }

        /// <summary>
        /// Draws the Menu upon the screen
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="graphicsDevice"></param>
        public override void draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            if(shown)
            {
                questList.draw(spriteBatch, graphicsDevice);
                completedQuestList.draw(spriteBatch, graphicsDevice);
                questInformation.draw(spriteBatch, graphicsDevice);
                questRewards.draw(spriteBatch, graphicsDevice);

                playerStats.draw(spriteBatch, graphicsDevice);
            }
        }

    }
}
