using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * Creates a chunk of downward stairs.
     * 
     */
     [Serializable()]
    class ReverseStairChunk : Chunk
    {

        public ReverseStairChunk(int chunkPos, int baseHeight) : base(chunkPos)
        {
            boundingBoxes.Add(new BoundingBox(baseHeight));

            boundingBoxes.Add(new BoundingBox(baseHeight - 20));

            boundingBoxes.Add(new BoundingBox(baseHeight - 40));
        }

        public override string ToString()
        {
            return "RSTC";
        }
    }
}
