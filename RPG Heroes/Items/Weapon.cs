using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Heroes.Items
{

    public class Weapon : Item
    {

        private WeaponType weaponType;
        private int weaponDamage;

        public Weapon(string itemName, int requiredLevel, int weaponDamage, WeaponType weaponType) : base(itemName, requiredLevel)
        {
            this.weaponType = weaponType;
            this.weaponDamage = weaponDamage;
            slot = Slot.Weapon;
        }

        //See if the hero trying to equip the item is able to do it or not
        public override bool Equipped(Hero heroEquipping)
        {
            bool success = false;
            for (int i = 0; i < heroEquipping.ValidWeaponsType.Length; i++)
            {
                if (weaponType == heroEquipping.ValidWeaponsType[i])
                {
                    success = true;
                }
            }
            if (!success)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"{heroEquipping.Name} Couldn't equip |{ItemName}| Error: wrong type");
                Console.ResetColor();
                return success;
            }
            if (RequiredLevel > heroEquipping.Level)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"{heroEquipping.Name} Couldn't equip |{ItemName}| Error: too low level");
                Console.ResetColor();
                success = false;
            }
            return success;
        }

        //return the items stats 
        public override double[] returnItemStats()
        {
            var itemStat = new double[] { weaponDamage };
            return itemStat;
        }
    }
}
