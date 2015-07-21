using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * Written to add Stats like Attack and Speed to the item class.
     * Features to allow it to harm monsters will be added
     */
    [Serializable()]
    class Weapon : EquippableItem
    {
        private Statistic attack;
        private Statistic speed;

        public Weapon() : this("Default Weapon", "DefaultHash")
        {
          
        }

        public Weapon(String name, string hash) : this(name, hash, 1, 1, 1)
        {

        }

        public Weapon(String name, string hash, int durability, int attack, int speed) : base(name, hash, durability)
        {
            this.attack = new Statistic("attack", attack);
            this.speed = new Statistic("speed", speed);
        }

        public Weapon(String hash, string name, CappedStatistic durability, Statistic attack, Statistic speed) : base(hash,name,durability, EquipSlot.UNDEFINED)
        {
            this.attack = attack;
            this.speed = speed;
        }

        //setters
        public void setAttackValue(int atk)
        {
            attack.setValue(atk);
        }

        public void setSpeedValue(int spd)
        {
            speed.setValue(spd);
        }

        public void setSpeedModifier(int mod)
        {
            speed.setModifier(mod);
        }

        public void setAttackModifier(int mod)
        {
            attack.setModifier(mod);
        }

        //Getters
        public Statistic getSpeed()
        {
            return speed;
        }

        public Statistic getAttack()
        {
            return attack;
        }

        public override string ToString()
        {
            return base.ToString() + "::" + attack.ToString() + "::" + speed.ToString();
        }
    }
}
