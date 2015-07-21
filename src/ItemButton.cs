using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * This is an item button, it displays the item's stats.
     * 
     */
    class ItemButton : Button
    {
        public ItemButton(Item x) : base(new Vector2(0,0), x.getName(), Color.White)
        {
            setAction(new DisplayItemStatsAction(x));
            
        }
    }
}
