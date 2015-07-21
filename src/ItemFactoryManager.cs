using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace AetherQuest
{
    [Serializable()]
    public class ItemFactoryManager
    {
        /* ItemFactoryManager - This is used to manage all the items and inventories.
         * Written By: Jesse Mitchell
         * 
         * All items are kept track of here, this is to keep saving easy without weird iteration.
         * 
         * A createInventory Method is used to make a String, that will link an inventory to this.
         * 
         * When an inventory needs to get the items within, it retrieves it from here, rather than in the container, it makes the
         * design a lot simpler
         * */

        //private static ItemFactoryManager INSTANCE = loadItemFactoryManager("SavedItems.dat");

        //Lists of All the Items
        //gets item's container
        private Dictionary<Item, String> itemToInventory;
        //every container in the game 
        private Dictionary<String, List<Item>> inventoryToItems;
        //every item in the game
        private Dictionary<String, Item> items;
        //keeps track if inventories that are players
        //private List<String> playerInventories;

        private bool changed;




        //Used to Generate Random Hashes
        private Random randomNumberGenerator = new Random();

        //Used to generate the Items
        private ItemFactory itemFactory;

        //Only Needs a Default Constructor, This will be a Singleton
        public ItemFactoryManager()
        {
            itemFactory = new ItemFactory();

            items = new Dictionary<string, Item>();
            itemToInventory = new Dictionary<Item, string>();
            inventoryToItems = new Dictionary<string, List<Item>>();
            //playerInventories = new List<string>();
            changed = false;

            inventoryToItems["trash"] = new List<Item>();
        }

        public static ItemFactoryManager getInstance()
        {
            return DungeonManager.getInstance().getItemFactoryManager();
        }

        //Generates the Random Hash :P
        private String generateRandomHash()
        {
            String result = "";

            for (int i = 0; i < 12; i += 1)
            {
                int num = randomNumberGenerator.Next('0', 'Z');
                //int num2 = randomNumberGenerator.Next('a', 'z');
                result += (char)num;
            }

            return result;
        }

        //returns the item inventory hash
        public String getItemInventoryHash(Item item)
        {
            return itemToInventory[item];
        }

        //gets a value from the XML Document
        private String getValue(XmlDocument doc, String variable)
        {
            return doc.DocumentElement.SelectSingleNode(variable).InnerText.ToString().Trim();
        }

        /// <summary>
        /// Generates a health potion
        /// </summary>
        /// <returns></returns>
        public Item generateHealthPotion()
        {
            String itemHash = generateRandomHash();

            //Create the Item
            items[itemHash] = new HealthPotion(itemHash);
            
            //Place thhe item in the Location
            itemToInventory[items[itemHash]] = "trash";
            inventoryToItems["trash"].Add(items[itemHash]);

            return items[itemHash];

       }

        /// <summary>
        /// Generates an ammo pack
        /// </summary>
        /// <returns></returns>
        public Item generateAmmoPack()
        {
            String itemHash = generateRandomHash();

            //Create the Item
            items[itemHash] = new AmmoPack(itemHash);

            //Place thhe item in the Location
            itemToInventory[items[itemHash]] = "trash";
            inventoryToItems["trash"].Add(items[itemHash]);

            return items[itemHash];

        }
        
        public void saveItems()
        {
            saveItems("SavedItems.dat");
        }


        public void saveItems(String fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create);
            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, this);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);

                Logger.getInstance().log("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        public static ItemFactoryManager loadItemFactoryManager(String dataFile)
        {

            FileStream fs = null;

            try
            {
                fs = new FileStream(dataFile, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();

                // Deserialize the hashtable from the file and 
                // assign the reference to the local variable.
                return (ItemFactoryManager)formatter.Deserialize(fs);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                //throw;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
            return new ItemFactoryManager();
        }


        //Creates an Item with no parameters, selects a random XML and generates stats.
        public Item createItem()
        {
            String itemHash = generateRandomHash();

            //Create the Item
            items[itemHash] = itemFactory.createItem(itemHash);

            //Place thhe item in the Location
            itemToInventory[items[itemHash]] = "trash";
            inventoryToItems["trash"].Add(items[itemHash]);

            return items[itemHash];
        }

        //returns the item from its hash. Used for Loading or Special Occasions
        public Item getItem(String hash)
        {
            return items[hash];
        }

        //Gets all the Items in a Container
        public Item[] getContainerItems(String inventory)
        {
            return (inventoryToItems[inventory]).ToArray();
        }

        //Moves the item from current container to another
        private bool moveItem(Item item, String inventory)
        {
            if (inventoryToItems[inventory] != null)
            {
                if (item != null)
                {
                    changed = true;
                    
                    //remove from the old Container
                    if (itemToInventory.ContainsKey(item))
                        inventoryToItems[itemToInventory[item]].Remove(item);

                    //move to the new container
                    itemToInventory[item] = inventory;
                    inventoryToItems[inventory].Add(item);

                    
                    return true;
                }
            }
            return false;
        }
        //moves the item to the inventory
        public bool moveItem(Item item, Inventory inventory)
        {
            item.activateEffects(false);
            item.activateEffects(true);
            int itemCount = inventoryToItems[inventory.getInventoryHash()].Count;
            if (itemCount < inventory.getMaximumSize())
                return moveItem(item, inventory.getInventoryHash());

            return false;
        }

        public bool removeItem(Item item)
        {
            // will clean itself up later on
            //temporary
            return moveItem(item, "trash");
        }

        //creates the item using the XMLFile
        public Item createItem(string XMLFile)
        {
            String itemHash = generateRandomHash();

            //create the item
            items[itemHash] = itemFactory.createItem(itemHash, XMLFile);

            //place item in the correct location
            itemToInventory[items[itemHash]] = "trash";
            inventoryToItems["trash"].Add(items[itemHash]);


            return items[itemHash];
        }

        public Item createEquippableItem(String XMLFile)
        {
            String itemHash = generateRandomHash();

            //create the item
            items[itemHash] = itemFactory.createEquippableItem(itemHash, XMLFile);

            //place item in the correct location
            itemToInventory[items[itemHash]] = "trash";
            inventoryToItems["trash"].Add(items[itemHash]);


            return items[itemHash];
        }

        //creates the item using the XML then puts it in the correct container
        public Item createItem(string XMLFile, string inventory)
        {
            Item result = createItem(XMLFile);
            moveItem(result, inventory);
            return result;
        }

        //creates the item, puts it into the inventory
        public Item createItem(String XMLFile, Inventory inventory)
        {
            return createItem(XMLFile, inventory.getInventoryHash());
        }

        //A method to check if the item is a certain type
        public bool checkType(Item item, Type type)
        {
            return (item.GetType().Equals(type));
        }

        public bool hasChanged()
        {
            if (changed)
            {
                changed = false;
                return true;
            }
            return false;
        }

        //Creates A Weapon, Just like an Item, but extra attributes
        public Item createWeapon(string XMLFile)
        {
            String itemHash = generateRandomHash();

            //create the weapon
            items[itemHash] = itemFactory.createWeapon(itemHash, XMLFile);

            //set the location
            itemToInventory[items[itemHash]] = "trash";
            inventoryToItems["trash"].Add(items[itemHash]);


            return items[itemHash];
        }

        //creates a random weapon
        public Item createWeapon()
        {
            String itemHash = generateRandomHash();

            //create the weapon
            items[itemHash] = itemFactory.createWeapon(itemHash);

            //set the location
            itemToInventory[items[itemHash]] = "trash";
            inventoryToItems["trash"].Add(items[itemHash]);


            return items[itemHash];
        }

        //creates the random weapon and puts into inventory
        public Item createWeapon(Inventory inventory)
        {
            String itemHash = generateRandomHash();

            //create the weapon
            items[itemHash] = itemFactory.createWeapon(itemHash);

            inventory.add(items[itemHash]);

            return items[itemHash];
        }

        //creates weapon, and puts it into the right container
        public Item createWeapon(string XMLFile, string inventory)
        {
            Item result = createWeapon(XMLFile);
            moveItem(result, inventory);
            return result;
        }

        //creates weapon, puts it into the inventory
        public Item createWeapon(String XMLFile, Inventory inventory)
        {
            Item result = createWeapon(XMLFile);
            moveItem(result, inventory);
            return result;
        }

        private String createInventory(String inventory)
        {
            inventoryToItems[inventory] = new List<Item>();
            return inventory;
        }

        //creates an inventory string. This is used to keep things organized and centralized
        public String createInventory()
        {
            String inventory = generateRandomHash();
            inventoryToItems[inventory] = new List<Item>();
            return inventory;
        }
    }
}
