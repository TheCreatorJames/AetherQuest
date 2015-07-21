using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AetherQuest
{
    [Serializable()]
    class TorchChunkContainer : List<TorchChunk>
    {
        public TorchChunkContainer() : base() { }
        public TorchChunkContainer(SerializationInfo info, StreamingContext context) : base() { }
    }
}
