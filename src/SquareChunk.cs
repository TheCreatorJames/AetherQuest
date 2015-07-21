using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{

    /* Written By: Jesse Mitchell
     * 
     * This is a chunk that is square and flat.
     * 
     */
     [Serializable()]
    class SquareChunk : Chunk
    {
        public SquareChunk(int chunkPos, int baseHeight) : base(chunkPos)
        {
            if (Dungeon.getDeserializing()) return;
            boundingBoxes.Add(new BoundingBox(baseHeight));
        }

        public override string ToString()
        {
            return "SC";
        }
    }
}
