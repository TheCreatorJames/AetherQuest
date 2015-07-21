using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{ 
    /* Written By: Jesse Mitchell
     * 
     * This is a class used to execute actions when the GUI is clicked or entered upon.
     */
     [Serializable()]
    abstract class GuiAction
    {
        abstract public void executeAction();
    }
}
