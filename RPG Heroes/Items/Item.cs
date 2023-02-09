using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RPG_Heroes.Items
{
    public abstract class Item
    {
        public string ItemName { get; }
        public int RequiredLevel { get; }
        public Slot slot { get; protected set; }

        public Item(string itemName, int requiredLevel)
        {
            ItemName = itemName;
            RequiredLevel = requiredLevel;
        }

        public abstract bool Equipped(Hero heroEquip);
        public abstract double[] returnItemStats();
    }
}
