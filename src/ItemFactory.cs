using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Text;
using System.IO;
/*
 * Team Basyl
 * Item Factory Class is used to generate the items from XML Files
 * Writer: Jesse Mitchell
 * */

namespace AetherQuest
{
    [Serializable()]
    public class ItemFactory
    {
        private Random random;

        public ItemFactory()
        {
            random = new Random();
        }
        
        public Item createItem(String hash)
        {
            //write code to pick a random item XML file

            String[] files = Directory.GetFiles("Items");

            String file = files[random.Next(files.Length)];
            file = file.Split(new string[] { "\\" }, StringSplitOptions.None).Last();
            //totally random
            return createItem(hash, file);
        }

        public Item createWeapon(String hash)
        {
            //write code to pick a random item XML file

            String[] files = Directory.GetFiles("Weapons");

            String file = files[random.Next(files.Length)];
            file = file.Split(new string[] { "\\" }, StringSplitOptions.None).Last();
            //totally random
            return createWeapon(hash, file);
        }

        public Item createEquippableItem(String hash)
        {
            //write code to pick a random item XML file

            String[] files = Directory.GetFiles("Equippables");

            String file = files[random.Next(files.Length)];
            file = file.Split(new string[] { "\\" }, StringSplitOptions.None).Last();
            //totally random
            return createEquippableItem(hash, file);
        }

        //generates values between the min and max of the variable in the XML file
        private int generateValue(XmlDocument doc, String variable)
        {
            int min = Int32.Parse(getValue(doc, variable + "/min"));
            int max = Int32.Parse(getValue(doc, variable + "/max"));

            return random.Next(min, max);
        }

        //gets a value from the XML Document
        private String getValue(XmlDocument doc, String variable)
        {
            return doc.DocumentElement.SelectSingleNode(variable).InnerText.ToString().Trim();
        }

        //Creates and item using the provided hash and the XML File
        public Item createItem(String hash, String XMLFile)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Items\\" + XMLFile);
            

            //This needs to be better
            String name = getValue(doc, "name");
            String[] meh = getValue(doc, "adjectives").Split(',');
            String adj = meh[random.Next(meh.Length)].Trim();

            name = adj + " " + name;


            return new Item(hash, name, generateValue(doc, "durability"));
        }

        public Item createEquippableItem(String hash, String XMLFile)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Equippables\\" + XMLFile);

            //This needs to be better
            String name = getValue(doc, "name");
            String[] meh = getValue(doc, "adjectives").Split(',');
            String adj = meh[random.Next(meh.Length)].Trim();

            EquipSlot slot;

            switch(getValue(doc, "EquipSlot"))
            {
                case "CHEST":
                    slot = EquipSlot.CHEST;
                    break;
                case "LEGS":
                    slot = EquipSlot.LEGS;
                    break;
                case "BOOTS":
                    slot = EquipSlot.BOOTS;
                    break;
                case "HELM":
                    slot = EquipSlot.HELM;
                    break;
                default:
                    slot = EquipSlot.UNDEFINED;
                    break;
            }

            name = adj + " " + name;

            return new EquippableItem(hash, name, generateValue(doc, "durability"), slot);
        }

        public Item createWeapon(String hash, String XMLFile)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Weapons\\" + XMLFile);

            String name = getValue(doc, "name");
            String[] meh = getValue(doc, "adjectives").Split(',');
            String adj = meh[random.Next(meh.Length)].Trim();

            name = adj + " " + name;

            return new Weapon(hash, name, generateValue(doc, "durability"), generateValue(doc, "attack"), generateValue(doc, "speed"));
        }

    }
}
