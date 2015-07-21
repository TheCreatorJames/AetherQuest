using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    /* Written By : Jesse Mitchell
     * 
     * This is a game effect, it can change stats or nearly anything.
     * 
     */
    [Serializable()]
    public abstract class GameEffect
    {
        protected bool activated;

        public GameEffect()
        {
            activated = false;
        }

        abstract public void activate();
        abstract public void deactivate();

        public bool isActivated()
        {
            return activated;
        }
    }
}
