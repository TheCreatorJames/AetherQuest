using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * This is used to render rooms that are already premade. Like the starter room,
     * boss rooms, and the taverns.
     */
    [Serializable()]
    class PresetDungeon : Dungeon
    {
        public PresetDungeon(Room room) : base()
        {
            rooms.Add(room);
            
            ender = new SquareChunk(room.getSize(), 1000);
            
            starter.setColor(room.getColor());
            ender.setColor(room.getColor());
        }
    }
}
