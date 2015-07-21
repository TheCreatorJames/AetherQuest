using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * This is a tavern room, it will host shops and such.
     * 
     */
    [Serializable()]
    class TavernRoom : Room
    {
        public TavernRoom(Vector2 vector)
        {
            sColor = new SerializedColor(Color.DarkGoldenrod);
            color = sColor.getColor();

            chunks = new ChunkContainer();
            int i = 0;
            chunks.Add(new PortalChunk(i++, 100, DungeonManager.getInstance().getCurrentDungeon(), vector));
            for (int j = 0; i < 5; i++ )
                chunks.Add(new SquareChunk(i, 100));

            chunks.Add(new StairChunk(i++, 100));
            int max = i + 5;
            for (int j = 0; i < max; i++)
                chunks.Add(new SquareChunk(i, 140));
        }
    }
}
