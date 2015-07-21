using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * This creates a type of item that can be inserted into a specific inventory slot.
     * Like armour.
     * 
     */
    enum EquipSlot : byte
    {
        UNDEFINED,
        HELM,
        CHEST,
        LEGS,
        BOOTS
    }

    [Serializable()]
    class EquippableItem : Item
    {
        private EquipSlot equipSlot;

        private List<GameEffect> equipEffects;

        public EquippableItem(String hash) : this(hash, "Default Name", 0)
        {

        }

        public EquippableItem(String hash, String name, int maxDurability) : this(hash, name, maxDurability, EquipSlot.UNDEFINED)
        {
            
        }

        public EquippableItem(string hash, string name, int maxDurability, EquipSlot equipSlot) : base(hash, name, maxDurability)
        {
            this.equipSlot = equipSlot; //sets what slot it will equip to
           
        }

        public EquippableItem(String hash, string name, CappedStatistic durability, EquipSlot es) : base(hash, name, durability)
        {
            this.equipSlot = es;
        }

        //adds an equip effect to the item
        public void addEquipEffect(GameEffect effect)
        {
            if (equipEffects == null) equipEffects = new List<GameEffect>(); //instansiates a new effect list since it is needed here
            equipEffects.Add(effect);
        }

        //activates all the effects stored in the equipEffects List when the item is equipped
        public void activateEquipEffects(bool info)
        {
            if (equipEffects != null) //makes sure there are effects to use
                foreach (GameEffect effect in equipEffects)
                {
                    if (info)
                    {
                        effect.activate();
                    }
                    else
                    {
                        effect.deactivate();
                    }
                }
        }

        //gets what slot it will equip to
        public EquipSlot getEquipSlot()
        {
            return equipSlot;
        }

        //sets what equip slot it will equip to
        public void setEquipSlot(EquipSlot equipSlot)
        {
            this.equipSlot = equipSlot;
        }

        public override string ToString()
        {
            return base.ToString() + "::" + equipSlot.ToString();
        }

        
    }
}
