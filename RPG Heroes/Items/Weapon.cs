using RPG_Heroes.CustomExceptions;
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

        //Verify if the hero trying to equip the item is able to do it or not
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
                //Console.ForegroundColor= ConsoleColor.DarkRed;
                throw new InvalidWeaponException($"InvalidWeaponException: {heroEquipping.Name} Couldn't equip |{ItemName}| Error: wrong type");
            }
            if (RequiredLevel > heroEquipping.Level)
            {
                //Console.ForegroundColor = ConsoleColor.DarkRed;
                throw new InvalidWeaponException($"InvalidWeaponException: {heroEquipping.Name} Couldn't equip |{ItemName}| Error: too low level");
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
