using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * This is a class used to store the rooms, and draw them.
     * It collects rooms to render.
     * 
     */
    [Serializable()]
    class Dungeon
    {
        protected RoomContainer rooms;
        private MobContainer entities;
        protected Random random;
        protected Chunk starter;
        protected Chunk ender;
        private Chunk extraBeginningChunk;
        private static bool deserializing;
        private TorchChunkContainer cLights = new TorchChunkContainer();


        public static bool getDeserializing()
        {
            return deserializing;
        }
       
        //Creates a New Dungeon, Generates it if it is not Preset.
        public Dungeon()
        {
            if (!deserializing)
            {
                rooms = new RoomContainer();
                random = new Random();

                starter = new SquareChunk(-1, 10000);

                //Logger.getInstance().log(SerializeThing());

                //if not preset, make it.
                if (!GetType().Equals(typeof(PresetDungeon)))
                    generateDungeon();
            }
            else
            {
                entities = new MobContainer();
                //deserializing = false;
            }
        }
        /// <summary>
        /// Creates a dungeon that is linked to another dungeon through a portalChunk
        /// </summary>
        /// <param name="linkedDungeon">The Dungeon for the portal to link to</param>
        /// <param name="vector">The position to warp to into the other dungeon</param>
        public Dungeon(Dungeon linkedDungeon, Vector2 vector)
        {
            rooms = new RoomContainer();
            random = new Random();

            extraBeginningChunk = new SquareChunk(-2, 10000);

            starter = new PortalChunk(-1, 230, linkedDungeon, vector);
            if (!GetType().Equals(typeof(PresetDungeon)))
                generateDungeon();
        }

        /// <summary>
        /// Gets all of the Torch Chunks in the Dungeon to provide light.
        /// </summary>
        /// <returns>Torch Chunks in the Dungeon</returns>
        public TorchChunk[] getTorchChunks()
        {
            if (cLights.Count == 0)
            {
                foreach (Room room in rooms)
                {
                    TorchChunk[] chunks = room.getTorchChunks();
                    foreach (TorchChunk chunk in chunks)
                    {
                        cLights.Add(chunk);
                    }
                }
            }
            return cLights.ToArray();
        }

       

        /// <summary>
        /// Gets the first chunk of the dungeon.
        /// </summary>
        /// <returns>First Chunk of the Dungeon</returns>
        public Chunk getFirstChunk()
        {
            return starter;
        }

        /// <summary>
        /// Adds an entity into the dungeon to be rendered.
        /// </summary>
        /// <param name="entity">The entity to be added.</param>
        public void addEntity(Mob entity)
        {
            Logger.getInstance().log("Entity Created.");
            if (entities == null) entities = new MobContainer();

            entities.Add(entity);
        }

        /// <summary>
        /// Gets a room at the position
        /// </summary>
        /// <param name="roomNum">The position of the room.</param>
        /// <returns></returns>
        public Room getRoomAt(int roomNum)
        {
            return rooms.ElementAt(roomNum);
        }

        /// <summary>
        /// Gets the number of rooms
        /// </summary>
        /// <returns>number of rooms</returns>
        public int getNumberOfRooms()
        {
            return rooms.Count;
        }

        /// <summary>
        /// Gets A random room from all of the rooms
        /// </summary>
        /// <returns></returns>
        public Room getRandomRoom()
        {
            return rooms.ElementAt(random.Next(rooms.Count));
        }

        /// <summary>
        /// Removes an entity from the Dungeon.
        /// </summary>
        /// <param name="entity"></param>
        public void removeEntity(Mob entity)
        {
            if (entities == null) return;

            entities.Remove(entity);
        }

        /// <summary>
        /// Generates the Dungeon by creating a random number of rooms.
        /// </summary>
        private void generateDungeon()
        {
            Room room;
            int size = random.Next(4, 10);
            int height = 230;
            int pos = 0;
            for (int i = 0; i < size; i++)
            {
                room = new Room(height, pos, this);
                height = room.getChunk(room.getSize() - 1).getHeight(); //gets height of the last room.
                pos += room.getSize(); //increases the chunk position
                rooms.Add(room);
                if (i == 0) starter.setColor(room.getColor());
            }
            ender = new SquareChunk(rooms.ElementAt(rooms.Count - 1).getSize(), 10000);
            ender.setColor(rooms.ElementAt(rooms.Count - 1).getColor());

            DungeonManager.getInstance().setCurrentDungeon(this);


        }
        /// <summary>
        /// The dungeon method to save it.
        /// </summary>
        /// <param name="fileName">File name to save to.</param>
        public void saveDungeon(String fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create);
            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            SoapFormatter formatter = new SoapFormatter();
            try
            {
                formatter.Serialize(fs, this);
               // StreamWriter writer = new StreamWriter(fileName);
               // writer.Write(SerializeThing());
               // writer.Close();
                
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                Logger.getInstance().log(e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        /// <summary>
        /// Loads the Dungeon from the File name.
        /// </summary>
        /// <param name="dataFile">File to be read from</param>
        /// <returns></returns>
        public static Dungeon loadDungeon(String dataFile)
        {

           FileStream fs = null;

            try
            {
                fs = new FileStream(dataFile, FileMode.Open);
                SoapFormatter formatter = new SoapFormatter();

                // Deserialize the hashtable from the file and 
                // assign the reference to the local variable.
               return (Dungeon)formatter.Deserialize(fs);
/*
                String json = "";
                StreamReader reader = new StreamReader(dataFile);
                json = reader.ReadToEnd();
                //Console.WriteLine(json);
                reader.Close();


                Newtonsoft.Json.JsonSerializerSettings jss = new Newtonsoft.Json.JsonSerializerSettings();

                if (true)
                {
                    Newtonsoft.Json.Serialization.DefaultContractResolver dcr = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                    dcr.DefaultMembersSearchFlags |= System.Reflection.BindingFlags.NonPublic;
                    jss.ContractResolver = dcr;
                }

                //deserializing = true;

                return Newtonsoft.Json.JsonConvert.DeserializeObject<Dungeon>(json, jss);
                */
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                Logger.getInstance().log(e.Message);

            }
            finally
            {
                if (fs != null)
                   fs.Close();
            }
            return new Dungeon();
        }


        /// <summary>
        /// Gets the current chunk left of the player
        /// </summary>
        /// <returns>The chunk left to the player</returns>
        public Chunk getCurrentChunkLeft()
        {
            return getCurrentChunkLeft(InputManager.getInstance().getCurrentPlayer());
        }

        /// <summary>
        /// Gets the chunk right of the player.
        /// </summary>
        /// <returns></returns>
        public Chunk getCurrentChunkRight()
        {
            return getCurrentChunkRight(InputManager.getInstance().getCurrentPlayer());
        }

        /// <summary>
        /// Gets the chunk right of the entity
        /// </summary>
        /// <param name="creature"></param>
        /// <returns></returns>
        public Chunk getCurrentChunkRight(Mob creature)
        {
            int chunkNum = (creature.getX() + creature.getWidth()) / Chunk.CHUNKWIDTH; //InputManager.getInstance().getCurrentPlayer().getWidth();

            for (int i = 0; i < rooms.Count; i++)
            {
                if (rooms.ElementAt(i).getSize() <= chunkNum)
                {
                    chunkNum -= rooms.ElementAt(i).getSize();
                }
                else
                {
                    return rooms.ElementAt(i).getChunk(chunkNum);
                }
            }
            return new SquareChunk(-1, Int32.MaxValue);
        }

        /// <summary>
        /// Gets the chunk left of the entity.
        /// </summary>
        /// <param name="creature"></param>
        /// <returns></returns>
        public Chunk getCurrentChunkLeft(Mob creature)
        {
            if (creature.getX() < 0 && creature.getX() > -Chunk.CHUNKWIDTH) return starter;
            int chunkNum = (creature.getX()) / Chunk.CHUNKWIDTH; //InputManager.getInstance().getCurrentPlayer().getWidth();

            for (int i = 0; i < rooms.Count; i++)
            {
                if (rooms.ElementAt(i).getSize() <= chunkNum)
                {
                    chunkNum -= rooms.ElementAt(i).getSize();
                }
                else
                {
                    return rooms.ElementAt(i).getChunk(chunkNum);
                }
            }
            return null;
        }


        public void draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            starter.draw(spriteBatch, graphics);
            ender.draw(spriteBatch, graphics);
            if (extraBeginningChunk != null)
            {
                extraBeginningChunk.setColor(rooms.ElementAt(0).getColor());
                extraBeginningChunk.draw(spriteBatch, graphics);

            }




            foreach (Room room in rooms)
            {
                room.draw(spriteBatch, graphics);
            }



            if (!GetType().Equals(typeof(PresetDungeon)))
                if (entities == null && !deserializing)
                {
                    
                    for (byte i = 0; i < 26; i++)
                        getRandomRoom().addZombie();
                }

            Mob[] drawEntities = getEntities();

            if (entities != null)
                foreach (Mob entity in drawEntities)
                {
                    if (!getCurrentChunkLeft(entity).checkLeftTopCollision(entity) && !getCurrentChunkRight(entity).checkRightTopCollision(entity))
                    {
                        entity.gravityPull();
                    }
                    else entity.killAcceleration();

                    //If the entity is a follower, let it follow :P
                    if (typeof(Follower).IsAssignableFrom(entity.GetType()))
                    {
                        ((Follower)entity).followPlayer(this);
                    }

                    entity.draw(spriteBatch, graphics);
                }


        }

        public Mob[] getEntities()
        {
            if (entities != null)
                return entities.ToArray();

            return null;
        }
    }
}
