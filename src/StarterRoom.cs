using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * This is the room you will start out in. Very basic
     * 
     */
    [Serializable()]
    class StarterRoom : Room
    {
        public StarterRoom()
        {
            sColor = new SerializedColor(Microsoft.Xna.Framework.Color.Red);

            color = sColor.getColor();
            chunks = new ChunkContainer();

            //chunks.Add(new SquareChunk(-2, 1000));
            for(byte i = 0; i < 5; i++)
            {
                chunks.Add(new TorchChunk(i, 100));
            }
            
        }
    }
}
