using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * This statistic is modified so that it will only go up to the cap.
     * See?
    */
    [Serializable()]
    public class CappedStatistic : Statistic
    {
        private int cappedValue; 

        public CappedStatistic(String name, int value) : base(name, value)
        {
            cappedValue = value;
        }

        
        //when you get the value, it is now the value that is capped
        override public int getValue()
        {
            return cappedValue;
        }

        //same as getModifiedValue, but changed for clarity
        public int getMaxValue()
        {
            return getModifiedValue();
        }

        public void setMaxValue(int maxValue)
        {
            base.setValue(maxValue);
        }

        //this sets the value that will be capped by the max value
        override public void setValue(int cValue)
        {
            if(cValue <= getModifiedValue())
            cappedValue = cValue;
        }

        /// <summary>
        /// Sets the modifier of the stat.
        /// </summary>
        /// <param name="mod"></param>
        override public void setModifier(int mod)
        {
            base.setModifier(mod);
        }

        public override string ToString()
        {
            return getName() + ": " + getValue() + " / " + getMaxValue();
        }
        
    }
}
