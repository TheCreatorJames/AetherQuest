using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Xml;
using System.IO;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * This is the Input Manager, it detects all key presses
     * and interacts with the GUI system in order to
     * make sure it is reliable and functional.
     * 
     */

    /* Keys:
     * Left
     * Right
     * Jump
     * 
     * Inventory
     * Journal
     * 1 - 9 (9 Keys)
     * 0
     * 
     * Escape
     */
    class InputManager
    {
        private static InputManager instance = new InputManager();
        private Keys[] gameKeys;
        private bool[] pressedGameKeys;
        private Keys[] allKeys;
        private Clickable clicked;
        private int laps;

        private Player currentPlayer;

        public InputManager()
        {
            if (instance == null)
            {
                loadKeyConfig();
                laps = 0;
                clicked = null;
            }
            else throw new Exception("Do not construct InputManager. Use getInstance()");
        }

        /// <summary>
        /// Gets the character from the XML Document.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="variable"></param>
        /// <returns></returns>
        private Char getValue(XmlDocument doc, String variable)
        {
            String var = doc.DocumentElement.SelectSingleNode(variable).InnerText.ToString().Trim();
            if (var.Length == 0) return ' ';
            return var.ElementAt(0);
        }

        /// <summary>
        /// Loads the configuration of keys.
        /// </summary>
        private void loadKeyConfig()
        {
            XmlDocument doc = new XmlDocument();
            //This detects if the keys.xml doesn't exist, then recreates it.
            if(!File.Exists("Options\\Keys.xml"))
            {
                Logger.getInstance().log("No Keys.XML file detected. Writing from default configuration.");
                StreamWriter write = File.CreateText("Options\\Keys.xml");
                write.Write("<?xml version=\"1.0\" encoding=\"utf-8\" ?><Keys><Left>A</Left><Right>D</Right><Jump> </Jump><Inventory>I</Inventory><Journal>J</Journal></Keys>");
                write.Flush();
                write.Close();
            }
            doc.Load("Options\\" + "Keys.xml");

            gameKeys = new Keys[16];
            //add code to read from XML.
            //Left Right
            gameKeys[0] = (Keys)((int)getValue(doc, "Left"));
            gameKeys[1] = (Keys)((int)getValue(doc, "Right"));

            //Jump
            gameKeys[2] = (Keys)((int)getValue(doc, "Jump"));

            //Inventory
            gameKeys[3] = (Keys)((int)getValue(doc, "Inventory"));
            //Journal
            gameKeys[4] = (Keys)((int)getValue(doc, "Journal"));

            //1-9
            gameKeys[5] = Keys.D1;
            gameKeys[6] = Keys.D2;
            gameKeys[7] = Keys.D3;
            gameKeys[8] = Keys.D4;
            gameKeys[9] = Keys.D5;
            gameKeys[10] = Keys.D6;
            gameKeys[11] = Keys.D7;
            gameKeys[12] = Keys.D8;
            gameKeys[13] = Keys.D9;
            //0
            gameKeys[14] = Keys.D0;

            //Escape
            gameKeys[15] = Keys.Escape;

        }

        /// <summary>
        /// Gets the current Input Manager
        /// </summary>
        /// <returns></returns>
        public static InputManager getInstance()
        {
            return instance;
        }

        /// <summary>
        /// Checks if the clicked is null.
        /// </summary>
        /// <returns></returns>
        public bool isReleased()
        {
            return (clicked == null);
        }


        /// <summary>
        /// Gets all keys that have been released.
        /// </summary>
        /// <returns></returns>
        public Keys[] getReleasedKeys()
        {
            Keys[] oldKeys = allKeys;
            List<Keys> keys = new List<Keys>();
            Keys[] newKeys = getKeys();

            
            if (oldKeys != null)
                foreach (Keys key in oldKeys)
                {      
                    if (newKeys.Contains(key))
                    {

                    }
                    else
                    {
                        keys.Add(key);
                        if (textBoxFocused())
                        {

                            if ((char)key >= 'A' && (char)key <= 'Z' || (char)key == ' ')
                            {
                                String uCase = "" + (char)key;

                                if (!oldKeys.Contains(Keys.LeftShift))
                                    uCase = uCase.ToLower();

                                ((Textbox)clicked).addText(uCase);
                                continue;
                            }
                            if(key == Keys.Back)
                            ((Textbox)clicked).removeText();
                            else if (key == Keys.Enter)
                            {
                                ((Textbox)clicked).executeAction();
                            }
                           
                           
                        }
                    }
                }

            return keys.ToArray();

        }

        /// <summary>
        /// Gets all keys being pressed.
        /// </summary>
        /// <returns></returns>
        public Keys[] getKeys()
        {
            return allKeys = Keyboard.GetState().GetPressedKeys();
        }

        /// <summary>
        /// Gets the keys that are game keys and says if they are true of false.
        /// </summary>
        /// <returns></returns>
        public bool[] getGameReleasedKeys()
        {
            bool[] tPressedGameKeys = pressedGameKeys;
            bool[] released = getGamePressedKeys();
            int i = 0;
            foreach (bool x in released)
            {

                if (tPressedGameKeys != null && tPressedGameKeys[i])
                {
                    if (!x)
                    {
                        released[i] = true;
                    }
                    else released[i] = false;
                }
                else released[i] = false;

                i++;
            }
            return released;
        }

        /// <summary>
        /// Gets the keys that are currently pressed that belong to the game. 
        /// True or false.
        /// </summary>
        /// <returns></returns>
        public bool[] getGamePressedKeys()
        {
            Keys[] pressed = getKeys();
            bool[] keysPressed = new bool[gameKeys.Length];
            for (int i = 0; i < gameKeys.Length; i++)
            {
                if(i >= 5 && i <= 13)
                {
                    keysPressed[i] = pressed.Contains((Keys)(96 + i - 4));
                   
                    if (keysPressed[i])
                    {
                        continue;
                    }
                }
                keysPressed[i] = pressed.Contains(gameKeys[i]);
            }

            return pressedGameKeys = keysPressed;
        }

        /// <summary>
        /// Checks if the clickable is being pressed.
        /// </summary>
        /// <param name="clickable"></param>
        /// <returns></returns>
        public bool isPressing(Clickable clickable)
        {
            bool set = (clicked == clickable);
            if (clicked != null)
            {
                if (clicked.getLayer() < clickable.getLayer()) return false;

                if (laps++ > 25)
                {
                    if (!set)
                        if (!isPressing(clicked))
                        {
                            clicked.tellPressed(false);
                            clicked = null;
                        }
                    laps = 0;
                }

            }

            bool pressed = Mouse.GetState().LeftButton.Equals(ButtonState.Pressed);


            if (clicked == null || set)
                if (pressed)
                {
                    if (set)
                    {
                        return true;
                    }

                    if (Mouse.GetState().X >= clickable.getVector().X && Mouse.GetState().X <= clickable.getVector().X + clickable.getWidth())
                    {
                        if (Mouse.GetState().Y >= clickable.getVector().Y && Mouse.GetState().Y <= clickable.getVector().Y + clickable.getHeight())
                        {
                            clicked = clickable;
                            clicked.tellPressed(true);
                            return true;
                        }
                    }
                }

            //temp fix
            if (set && textBoxFocused())
            {
                if (Mouse.GetState().X >= clickable.getVector().X && Mouse.GetState().X <= clickable.getVector().X + clickable.getWidth())
                {
                    if (Mouse.GetState().Y >= clickable.getVector().Y && Mouse.GetState().Y <= clickable.getVector().Y + clickable.getHeight())
                    {
                        clicked = clickable;
                        clicked.tellPressed(true);
                        return true;
                    }
                }
            }

            clickable.tellPressed(false);
            return false;
        }

        /// <summary>
        /// Checks if a textbox is being focused on.
        /// </summary>
        /// <returns></returns>
        public bool textBoxFocused()
        {
            if (clicked != null)
                if(clicked.getEnabled())
                return typeof(Textbox).IsAssignableFrom(clicked.GetType());

            return false;
        }

        /// <summary>
        /// Sets the Current Player of the Game.
        /// </summary>
        /// <param name="player"></param>
        public void setCurrentPlayer(Player player)
        {
            this.currentPlayer = player;
        }

        /// <summary>
        /// Gets the current player of the game.
        /// </summary>
        /// <returns></returns>
        public Player getCurrentPlayer()
        {
            return currentPlayer;
        }

        /// <summary>
        /// Checks if the right mouse button is pressing the clickable.
        /// </summary>
        /// <param name="clickable"></param>
        /// <returns></returns>
        public bool isRightPressing(Clickable clickable)
        {
            if (Mouse.GetState().RightButton.Equals(ButtonState.Pressed))
            {
                if (Mouse.GetState().X >= clickable.getVector().X && Mouse.GetState().X <= clickable.getVector().X + clickable.getWidth())
                {
                    if (Mouse.GetState().Y >= clickable.getVector().Y && Mouse.GetState().Y <= clickable.getVector().Y + clickable.getHeight())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Gets the mouse position.
        /// </summary>
        /// <returns></returns>
        public Vector2 getMousePos()
        {
            return new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
        }


    }


}
