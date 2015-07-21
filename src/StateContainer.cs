using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AetherQuest
{
    [Serializable]
    class RoomContainer : List<Room>
    {
        public RoomContainer() : base() { }
        public RoomContainer(SerializationInfo info, StreamingContext context) : base() { }
    }
}
