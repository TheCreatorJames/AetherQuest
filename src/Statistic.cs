using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * This is used to keep track of stats on players and items in the game
     * 
     * Stats like Wisdom, Vitality, Health, Durability, ETC
     */
    [Serializable()]
    public class Statistic
    {
        private String name;
        private int value;
        private int modifier;

        public Statistic(String name, int value) : this(name, value, 0)
        {
        }


        public Statistic(String nme, int val, int mod)
        {
            name = nme;
            value = val;
            modifier = mod;
        }

        //Setters
        public void setName(String nme)
        {
            name = nme;
        }

        virtual public void setValue(int val)
        {
            value = val;
        }

        virtual public void setModifier(int mod)
        {
            modifier = mod;
        }

        //Getters
        public String getName()
        {
            return name;
        }

        virtual public int getValue()
        {
            return value;
        }

        public int getModifiedValue()
        {
            return modifier + value;
        }


        public int getModifier()
        {
            return modifier;
        }

        override public String ToString()
        {
            return name + ": " + getModifiedValue();
        }

    }
}
