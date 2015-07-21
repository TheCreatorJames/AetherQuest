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
     * This is a room that keeps track of the chunks within.
     */
    [Serializable()]
    class Room
    {

        private int baseHeight;
        protected String name; //not implemented yet
        protected int chunkPos;
        private Random random;
        [NonSerialized()]
        protected Color color;
        protected SerializedColor sColor;
        protected ChunkContainer chunks;
        [NonSerialized()]
        private bool serialed;

        public Room()
        {
            if (Dungeon.getDeserializing())
            {
                //Console.Write(chunks);
               
                serialed = true;
            }
        }

        public Room(int height, int chunkPos, Dungeon dungeon)
        {
            if (Dungeon.getDeserializing()) return;
            baseHeight = height;
            random = new Random(this.GetHashCode());
            sColor = new SerializedColor(random.Next(256), random.Next(256), random.Next(256));
            color = sColor.getColor();
            chunks = new ChunkContainer();
            this.chunkPos = chunkPos;



            if (GetType().Equals(typeof(Room)))
                generateRoom();
        }


        /// <summary>
        /// Generates the Room by making random chunks.
        /// </summary>
        private void generateRoom()
        {
            
            int size = random.Next(10, 40);
            int height = baseHeight;
            int chestCount = 0;

            int dungeonDoorCount = 0;
            int tavernDoorCount = 0;

            for (int i = chunkPos; i < size + chunkPos; i++)
            {
                int choice = random.Next(1, 8);
                //Generates Chunks based on random number.
                switch (choice)
                {
                    case 1:
                        chunks.Add(new SquareChunk(i++, height));
                        chunks.Add(new TorchChunk(i, height));
                        break;
                    case 2:
                        if (dungeonDoorCount < 2)
                            chunks.Add(new PortalChunk(i, height));
                        else i--;
                        dungeonDoorCount++;
                        break;
                    case 3:
                        if (height - 40 >= 100)
                        {
                            chunks.Add(new ReverseStairChunk(i++, height));
                            height -= 40;
                            chunks.Add(new SquareChunk(i, height));
                        }
                        else i--;
                        break;
                    case 4:
                        chunks.Add(new StairChunk(i++, height));
                        height += 40;
                        chunks.Add(new SquareChunk(i, height));
                        break;
                    case 5:
                        if (chestCount < 3)
                        {
                            chunks.Add(new SquareChestChunk(i, height));
                            chestCount++;
                        }
                        else i--;
                        break;
                    case 6:
                        chunks.Add(new StairChunk(i++, height));
                        height += 40;
                        int added = random.Next(80, 600);
                        chunks.Add(new MovingChunk(i++, height, added));
                        height += added;
                        chunks.Add(new SquareChunk(i, height));
                        break;
                    case 7:
                        if (tavernDoorCount < 1)
                        {
                            chunks.Add(new TavernChunk(i, height));
                            tavernDoorCount++;
                        }
                        else i--;
                        break;

                }
            }

        }

        /// <summary>
        /// Gets all of the chunks that are Torch Chunks.
        /// </summary>
        /// <returns></returns>
        public TorchChunk[] getTorchChunks()
        {
            List<TorchChunk> tChunks = new List<TorchChunk>();
            foreach (Chunk chunk in chunks)
            {
                if (chunk.GetType().Equals(typeof(TorchChunk)))
                {
                    tChunks.Add((TorchChunk)chunk);
                }
            }

            return tChunks.ToArray();
        }



        /// <summary>
        /// Gets the chunk at this position.
        /// </summary>
        /// <param name="x">The Chunk Position</param>
        /// <returns></returns>
        public Chunk getChunk(int x)
        {
            if (x < 0)
            {
                return new SquareChunk(-1, Int32.MaxValue);
            }
            else if (x >= chunks.Count)
            {
                return new SquareChunk(-1, Int32.MaxValue);
            }
            return chunks.ElementAt(x);
        }

        /// <summary>
        /// Gets size of the room (number of chunks).
        /// </summary>
        /// <returns></returns>
        public int getSize()
        {
            return chunks.Count;
        }

        /// <summary>
        /// Returns the width of the dungeon.
        /// </summary>
        /// <returns></returns>
        public int getWidth()
        {
            return chunks.Count * Chunk.CHUNKWIDTH;
        }

        public void addZombie()
        {
            Vector2 xom = getChunk(random.Next(chunks.Count)).getVector();
            Zombie zombie = new Zombie();
            zombie.setVector(xom);
            zombie.moveX(-Chunk.CHUNKWIDTH * 4);
            zombie.moveY(-zombie.getHeight());

            DungeonManager.getInstance().getCurrentDungeon().addEntity(zombie);
        }

        public void draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {

       

            if (color.R == 0
                && color.G == 0
                && color.B == 0) color = sColor.getColor();

            int z = 0;
            foreach (Chunk chunk in chunks)
            {
                chunk.setColor(color);
                chunk.draw(spriteBatch, graphics);
                if(serialed)
                {
                    chunk.setXY(chunk.getX() + z, chunk.getY());
                    z += 1;
                }
            }
        }

        public Color getColor()
        {
            return color;
        }
    }

}
