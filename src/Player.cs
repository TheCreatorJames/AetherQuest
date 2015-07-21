using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * This class is used to keep track of the player, its stats, and inventory.
     * 
     */

    enum PlayerClass
    {
        ROGUE,
        WIZARD,
        WARRIOR
    }
    [Serializable()]
    class Player : Mob
    {
        //Statistics
        private CappedStatistic ammo;
        private CappedStatistic credits;
        private Statistic strength;
        private Statistic intelligence;
        private Statistic speed;
        // private Statistic stamina;
        private Statistic wisdom;
        //Equip Slots
        private String helm;
        private String chest;
        private String boots;
        private String legs;
        //Weapon Slot
        private String weapon;
        //Class of Player
        private PlayerClass playerClass;


        private byte killCredExtra;

        public Player(String name)
            : this(name, 180)
        {

        }

        public Player(String name, int health)
            : this(name, health, 0, 0)
        {

        }

        public Player(String name, int health, int x, int y)
            : base(name, health, x, y, true)
        {
            ammo = new CappedStatistic("ammo", 0);
            this.width = 32;
            this.height = 32;
            strength = new Statistic("strength", 0);
            intelligence = new Statistic("intelligence", 0);
            credits = new CappedStatistic("credits", 500);
            // stamina = new Statistic("stamina", 0);
            speed = new Statistic("speed", 0);
            wisdom = new Statistic("wisdom", 0);

            playerClass = PlayerClass.ROGUE;

            EquippableItem shirt = (EquippableItem)ItemFactoryManager.getInstance().createEquippableItem("Shirt.xml");
            EquippableItem pants = (EquippableItem)ItemFactoryManager.getInstance().createEquippableItem("Pants.xml");
            EquippableItem boots = (EquippableItem)ItemFactoryManager.getInstance().createEquippableItem("Boots.xml");
            EquippableItem helm = (EquippableItem)ItemFactoryManager.getInstance().createEquippableItem("Helm.xml");
            Weapon weapon = (Weapon)ItemFactoryManager.getInstance().createWeapon();

            this.chest = this.boots = this.helm = this.weapon = this.legs = "";

            weapon.addUsableEffect(new TimedEffectTest(weapon, 7 * 1000, 2 * 1000));

            getAmmo().setMaxValue(120);
            getAmmo().setValue(16);
            credits.setValue(0);
            setIntelligenceValue(5);
            setWisdomValue(7);
            setStrengthValue(8);

            this.add(shirt);
            this.add(pants);
            this.add(boots);
            this.add(helm);
            this.add(weapon);

            equipItem(shirt);
            equipItem(pants);
            equipItem(boots);
            equipItem(helm);
            equipItem(weapon);

        }

        public void savePlayer(String fileName)
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
                //throw;
            }
            finally
            {
                fs.Close();
            }
        }

        public static Player loadPlayer(String dataFile)
        {

            FileStream fs = null;

            try
            {
                fs = new FileStream(dataFile, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();

                // Deserialize the hashtable from the file and 
                // assign the reference to the local variable.
                Player result = (Player)formatter.Deserialize(fs);
                //Console.WriteLine(result.getBoots());
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                Logger.getInstance().log("Failed to deserialize. Reason: " + e.Message);
                //throw;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
            return new Player("Default Dan");
        }

        /// <summary>
        /// Awards credits to the player.
        /// </summary>
        /// <param name="credit"></param>
        public void addCredits(int credit)
        {
            while(killCredExtra >= 10)
            {
                killCredExtra -= 10;
                credit += 1;
            }
            credits.setValue(credits.getValue() + credit);
        }

        private static byte[] frames = { 1, 2, 3, 2, 1, 4 };
        private static byte count;
        private static byte frame;
        private static int lastX;

        public override void draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            checkDirection();
            SpriteEffects se = SpriteEffects.FlipHorizontally;

            if (direction) se = SpriteEffects.None;

            if(lastX != getX())
            {
                count++;
                lastX = getX();
            }

            if(count == 6)
            {
                count = 0;
                frame++;
            }

            byte cFrame = frames[frame % frames.Length];

            if(frame == 100)
            {
                frame = 0;
            }

            switch(cFrame)
            {
                case 1:
                    texture = ResourceHandler.getInstance().getPlayerTexture1();
                    break;
                case 2:
                    texture = ResourceHandler.getInstance().getPlayerTexture2();
                    break;
                case 3:
                    texture = ResourceHandler.getInstance().getPlayerTexture3();
                    break;
                case 4:
                    texture = ResourceHandler.getInstance().getPlayerTexture4();
                    break;
            }
        

            if(getHealth().getValue() > 0)
            {
                Texture2D health1 = ResourceHandler.getInstance().getBlankTexture(graphics, getHealth().getValue() * 2, 18);

                spriteBatch.Draw(health1, Vector2.Zero, Color.Red);
                spriteBatch.DrawString(ResourceHandler.getInstance().getVerdana(), getHealth().ToString(), Vector2.Zero, Color.White);

                Vector2 ammoVector = Vector2.Zero;
                ammoVector.Y += 20;

                if (getAmmo().getValue() > 0)
                {
                    Texture2D ammo = ResourceHandler.getInstance().getBlankTexture(graphics, getAmmo().getValue() * 2, 18);

                    spriteBatch.Draw(ammo, ammoVector, Color.Gold);
                    spriteBatch.DrawString(ResourceHandler.getInstance().getVerdana(), getAmmo().ToString(), ammoVector, Color.White);

                }
                else spriteBatch.DrawString(ResourceHandler.getInstance().getVerdana(), "No Ammo", ammoVector, Color.White);

                Vector2 creditVector = Vector2.Zero;
                creditVector.Y += 40;

                if (credits.getValue() > 0)
                {
                    Texture2D credit = ResourceHandler.getInstance().getBlankTexture(graphics, credits.getValue()/2, 18);

                    spriteBatch.Draw(credit, creditVector, Color.Cyan);
                    spriteBatch.DrawString(ResourceHandler.getInstance().getVerdana(), credits.ToString(), creditVector, Color.White);

                }
                else spriteBatch.DrawString(ResourceHandler.getInstance().getVerdana(), "No Credits", creditVector, Color.White);
                
            }
            else spriteBatch.DrawString(ResourceHandler.getInstance().getVerdana(), "DEAD\nLoad or Start a new Dungeon\nPress ESC", Vector2.Zero, Color.White);
                
            
            
            
            Vector2 center = new Vector2(graphics.Viewport.Bounds.Center.X, graphics.Viewport.Bounds.Center.Y);
            //spriteBatch.Draw(texture, new Rectangle((int)center.X, (int)center.Y - getHeight() + 10, getWidth() * 2, getHeight() * 2), Color.White);
            center.Y -= getHeight() - 10;
            spriteBatch.Draw(texture, center, null, Color.White, 0,
        Vector2.Zero, 1.0f, se, 0f);


        }

        public void shootBullet()
        {
            if(ammo.getValue() > 0)
            {
                Bullet x = new Bullet(direction);
                x.setVector(getVector());
                DungeonManager.getInstance().getCurrentDungeon().addEntity(x);
                ammo.setValue(ammo.getValue() - 1);
            }
           
        }

        /// <summary>
        /// Attacks anything within range.
        /// </summary>
        public void attack()
        {
            Mob[] entities = DungeonManager.getInstance().getCurrentDungeon().getEntities();

           
            if(entities != null)
            foreach(Mob ent in entities)
            {
                if(ent.getDistanceFrom(this) < 60)
                {

                    if(ent.getX() < this.getX() && !direction)
                    {
                        attack(ent);
                        killCredExtra += 1;

                        ent.moveY(-9);
                        ent.moveX(-16);
                        continue;
                    }

                    if (ent.getX() > this.getX() && direction)
                    {
                        attack(ent);
                        killCredExtra += 1;

                        ent.moveY(-9);
                        ent.moveX(16);
                        continue;
                    }
                }
            }


        }

        //gets the equipped weapon
        public Weapon getWeapon()
        {
            return (Weapon)ItemFactoryManager.getInstance().getItem(weapon);
        }

        //gets the equipped helm
        public EquippableItem getHelm()
        {
            return (EquippableItem)ItemFactoryManager.getInstance().getItem(helm);
        }

        //gets the equipped boots
        public EquippableItem getBoots()
        {
            return (EquippableItem)ItemFactoryManager.getInstance().getItem(boots);
        }

        //gets the equipped chest
        public EquippableItem getChest()
        {
            return (EquippableItem)ItemFactoryManager.getInstance().getItem(chest);
        }

        //gets the equipped legs
        public EquippableItem getLegs()
        {
            return (EquippableItem)ItemFactoryManager.getInstance().getItem(legs);
        }

        //gets mana statistic
        public CappedStatistic getAmmo()
        {
            return ammo;
        }

        //gets wisdom statistic
        public Statistic getWisdom()
        {
            return wisdom;
        }

        //gets intelligence statistic
        public Statistic getIntelligence()
        {
            return intelligence;
        }

        //returns the credits
        public CappedStatistic getCredits()
        {
            return credits;
        }

        //gets the strength statistic
        public Statistic getStrength()
        {
            return strength;
        }

        //gets the speed statistic
        public Statistic getSpeed()
        {
            return speed;
        }

        //gets the player class
        public PlayerClass getPlayerClass()
        {
            return playerClass;
        }

        //sets the wisdom value
        public void setWisdomValue(int p)
        {
            wisdom.setValue(p);
        }

        //sets the intelligence value
        public void setIntelligenceValue(int p)
        {
            intelligence.setValue(p);
        }

        //sets the speed value
        public void setSpeedValue(int p)
        {
            speed.setValue(p);
        }

        //sets the strength value
        public void setStrengthValue(int p)
        {
            strength.setValue(p);
        }

        //sets the player class
        public void setPlayerClass(PlayerClass playerClass)
        {
            this.playerClass = playerClass;
        }

        //This dequips the item and removes any modifications made to current stats
        public void dequipItem(EquippableItem p)
        {
            //add code to modify stats - DONE
            if (p != null)
                if (isItemEquipped(p))
                {
                    p.activateEquipEffects(false);
                }

        }


        public override void attack(Mob creature)
        {
            int damage = 0;
            if (weapon != null) damage = getWeapon().getAttack().getValue();
            damage += (this.getStrength().getValue() / 7) * 5;

            int c = (new Random()).Next(1, 100);
            if (c <= this.getStrength().getValue()) damage *= 2;

            creature.getHealth().setValue(creature.getHealth().getValue() - damage/3);
            //creature.setHealthValue(creature.getHealth().getValue() - damage);
        }

        public bool isItemEquipped(EquippableItem p)
        {
            if (p.GetType().Equals(typeof(Weapon)))
            {
                if (getWeapon().Equals(p))
                {
                    return true;
                }
            }
            switch (p.getEquipSlot())
            {
                case EquipSlot.BOOTS:
                    return (getBoots().Equals(p));
                case EquipSlot.CHEST:
                    return (getChest().Equals(p));
                case EquipSlot.HELM:
                    return (getHelm().Equals(p));
                case EquipSlot.LEGS:
                    return (getLegs().Equals(p));
                default:
                    return false;
            }
            return false;
        }

        //this equips the item and adds modifications to stats
        public void equipItem(EquippableItem p)
        {
            p.activateEquipEffects(true);
            //add code to modify stats when item is equipped, by looking at effect. - DONE
            if (p.GetType().Equals(typeof(Weapon))) //tests if this is a weapon
            {
                if (weapon.Length > 0 && getWeapon() != p)
                    dequipItem(getWeapon()); //dequips current item
                weapon = p.getHash(); //sets item equipped
                return;
            }


            switch (p.getEquipSlot())
            {
                case EquipSlot.BOOTS:
                    if (boots.Length > 0 && getBoots() != p)
                        dequipItem(getBoots()); //dequips current item
                    boots = p.getHash(); //sets item equipped
                    break;
                case EquipSlot.CHEST:
                    if (chest.Length > 0 && getChest() != p)
                        dequipItem(getChest());
                    chest = p.getHash(); //sets item equipped
                    break;
                case EquipSlot.HELM:
                    if (helm.Length > 0 && getHelm() != p)
                        dequipItem(getHelm()); //dequips current item
                    helm = p.getHash(); //sets item equipped
                    break;
                case EquipSlot.LEGS:
                    if (legs.Length > 0 && getLegs() != p)
                        dequipItem(getLegs()); //dequips current item
                    legs = p.getHash(); //sets item equipped
                    break;
                default:
                    break;
            }
        }
    }
}
