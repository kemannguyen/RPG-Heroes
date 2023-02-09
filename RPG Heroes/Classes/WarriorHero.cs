using RPG_Heroes.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Heroes.Classes
{
    public class WarriorHero : Hero
    {
        public WarriorHero(string name) : base(name)
        {
            heroClass = "¨Warrior";
            heroAttributes = new HeroAttributes(5, 2, 1);
            this.levelAttributes = new int[] { 3, 2, 1 };
            ValidWeaponsType = new WeaponType[] { WeaponType.Hammer, WeaponType.Axe, WeaponType.Sword };
            ValidArmorType = new ArmorType[] { ArmorType.Mail, ArmorType.Leather };
        }
        public override void LevelUp()
        {
            Level++;
            heroAttributes.IncreaseStats(levelAttributes[0], levelAttributes[1], levelAttributes[2]);
        }
        public override void Damage()
        {
            TotalAttributes();

            double totalAttackDealt = (attackDamage * (1 + (heroAttributes.dexterity + tempAttributes.dexterity) / 100));
            Console.Write(totalAttackDealt + " -> ");
            totalAttackDealt = Math.Round(totalAttackDealt);
            Console.WriteLine(totalAttackDealt);
            Console.Write($"*{Name} dealt {totalAttackDealt} ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("physical damage!");
            Console.ResetColor();
        }

    }
}
