using RPG_Heroes.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Heroes.Classes
{
    public class MageHero : Hero
    {
        public MageHero(string name) : base(name)
        {

            heroClass = "Mage";
            heroAttributes = new HeroAttributes(1, 1, 8);
            levelAttributes = new int[] { 1, 1, 5 };
            ValidWeaponsType = new WeaponType[] { WeaponType.Staff, WeaponType.Wand };
            ValidArmorType = new ArmorType[] { ArmorType.Cloth };
        }

        //increase level and increase stats depending on the class
        public override void LevelUp()
        {
            Level++;
            heroAttributes.IncreaseStats(levelAttributes[0], levelAttributes[1], levelAttributes[2]);
        }

        //round the damage calculation
        public override void Damage()
        {
            TotalAttributes();
            
            double totalAttackDealt = (attackDamage * (1 + (heroAttributes.intelligence + tempAttributes.intelligence) / 100));
            Console.Write(totalAttackDealt + " -> ");
            totalAttackDealt = Math.Round(totalAttackDealt);
            Console.WriteLine(totalAttackDealt);
            Console.Write($"*{Name} dealt {totalAttackDealt} ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("magic damage!");
            Console.ResetColor();
        }

    }
}
