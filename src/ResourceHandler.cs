using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * This class handles all the textures and such, so that they're easily
     * accessible by the entire game.
     * 
     */
    class ResourceHandler
    {
        private SpriteFont verdana;
        private Texture2D cobblestone;
        private Texture2D goldenBrick;
        private Texture2D woodenFloor;
        private Texture2D playerTexture1;
        private Texture2D playerTexture2;
        private Texture2D playerTexture3;
        private Texture2D light;
        private Texture2D playerTexture4;
        private Texture2D teamBasyl;
        private Texture2D bitBlit;
        private Texture2D tavernDoor;
        private Texture2D chestTexture;
        private Texture2D zombie2Texture;
        private Texture2D dungeonDoor;
        private Texture2D zombieTexture;
        private Boolean initialized;
        private int bottom;

        private static ResourceHandler instance = new ResourceHandler();

        public ResourceHandler()
        {
            if(instance == null)
            {
                initialized = false;
                verdana = null;
            } else throw new Exception("NO!");
        }

        public int getBottom()
        {
            return bottom;
        }

        public void setBottom(int bot)
        {
            bottom = bot;
        }

       

        public void initialize(ContentManager content)
        {
            verdana = content.Load<SpriteFont>("Verdana");
            cobblestone = content.Load<Texture2D>("TestPicture");
            goldenBrick = content.Load<Texture2D>("GoldBrick");
            woodenFloor = content.Load<Texture2D>("WoodenFloor");
            chestTexture = content.Load<Texture2D>("Chest");
            bitBlit = content.Load<Texture2D>("BitBlit");
            playerTexture1 = content.Load<Texture2D>("Player1");
            playerTexture2 = content.Load<Texture2D>("Player2");
            playerTexture3 = content.Load<Texture2D>("Player3");
            playerTexture4 = content.Load<Texture2D>("Player4");
            teamBasyl = content.Load<Texture2D>("TeamBasyl");
            light = content.Load<Texture2D>("Light");
            zombieTexture = content.Load<Texture2D>("Zombie");
            zombie2Texture = content.Load<Texture2D>("Zombie2");
            dungeonDoor = content.Load<Texture2D>("DungeonDoor");
            tavernDoor = content.Load<Texture2D>("TavernDoor");
            initialized = true;
        }

        public Texture2D getLight()
        {
            return light;
        }

        public Texture2D getTeamBasyl()
        {
            return teamBasyl;
        }

        public Texture2D getPlayerTexture1()
        {
            return playerTexture1;
        }
        public Texture2D getPlayerTexture2()
        {
            return playerTexture2;
        }
        public Texture2D getPlayerTexture3()
        {
            return playerTexture3;
        }
        public Texture2D getPlayerTexture4()
        {
            return playerTexture4;
        }

        public Texture2D getChestTexture()
        {
            return chestTexture;
        }

        public Texture2D getZombieTexture()
        {
            return zombieTexture;
        }

        public Texture2D getCobblestoneTexture()
        {
            return cobblestone;
        }

        public Texture2D getGoldenBrickTexture()
        {
            return goldenBrick;
        }

        public Texture2D getWoodenFloorTexture()
        {
            return woodenFloor;
        }

        public Texture2D getDungeonDoorTexture()
        {
            return dungeonDoor;
        }

        

        public Texture2D getBlankTexture(GraphicsDevice gd, int width, int height)
        {
            Texture2D rectangleTexture = new Texture2D(gd, width, height, false, SurfaceFormat.Color);

            Color[] color = new Color[width * height];

            for (int i = 0; i < color.Length; i++)
            {
                color[i] = Color.White;
            }

            rectangleTexture.SetData(color);

            return rectangleTexture;
        }

        public Texture2D getTavernDoorTexture()
        {
            return tavernDoor;
        }
        
        public Texture2D getBitBlitTexture()
        {
            return bitBlit;
        }

        public SpriteFont getVerdana()
        {
            if (!initialized) throw new Exception("You need to intialize the ResourceHandler");
            return verdana;
        }

        public static ResourceHandler getInstance()
        {
            return instance;
        }

        internal Texture2D getZombie2Texture()
        {
            return zombie2Texture;
        }
    }
}
