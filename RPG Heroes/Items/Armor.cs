using RPG_Heroes.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Heroes.Items
{
    public class Armor : Item
    {
        public ArmorType armorType { get; private set; }
        private HeroAttributes armorAttributes;

        public Armor(string itemName, int requiredLevel, Slot slot, ArmorType armorType, HeroAttributes armorAttributes) : base(itemName, requiredLevel)
        {
            this.slot = slot;
            this.armorType = armorType;
            this.armorAttributes = armorAttributes;
        }

        //Verify if the hero trying to equip the item is able to do it or not
        public override bool Equipped(Hero heroEquipping)
        {
            bool success = false;
            for (int i = 0; i < heroEquipping.ValidArmorType.Length; i++)
            {
                if (armorType == heroEquipping.ValidArmorType[i])
                {
                    success = true;
                }
            }
            if (!success)
            {
                //Console.ForegroundColor = ConsoleColor.DarkRed;
                throw new InvalidArmorException($"InvalidArmorException: {heroEquipping.Name} Couldn't equip |{ItemName}| Error: wrong type");
            }
            if (RequiredLevel > heroEquipping.Level)
            {
                //Console.ForegroundColor = ConsoleColor.DarkRed;
                throw new InvalidArmorException($"InvalidArmorException: {heroEquipping.Name} Couldn't equip |{ItemName}| Error: wrong type");
                
            }
            return success;
        }

        //return the items stats
        public override double[] returnItemStats()
        {
            var itemStat = new double[] { armorAttributes.strength, armorAttributes.dexterity, armorAttributes.intelligence };
            return itemStat;
        }
    }
}
