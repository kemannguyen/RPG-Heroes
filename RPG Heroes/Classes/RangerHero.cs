using RPG_Heroes.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RPG_Heroes.Classes
{
    public class RangerHero:Hero
    {
        public RangerHero(string name) : base(name)
        {
            heroClass = "Ranger";
            heroAttributes = new HeroAttributes(1, 7, 1);
            levelAttributes = new int[] { 1, 5, 1};
            ValidWeaponsType = new WeaponType[] { WeaponType.Bow};
            ValidArmorType = new ArmorType[] { ArmorType.Leather, ArmorType.Mail };
        }

        //increase level and increase stats depending on the class
        public override void LevelUp()
        {
            Level++;
            heroAttributes.IncreaseStats(levelAttributes[0], levelAttributes[1], levelAttributes[2]);
        }
        //round the damage and print it out
        public override double Damage()
        {
            var tempAttributes = TotalAttributes();

            double totalAttackDealt = (attackDamage * (1 + (heroAttributes.dexterity + tempAttributes.dexterity) / 100));
            Console.Write(totalAttackDealt + " -> ");
            totalAttackDealt = Math.Round(totalAttackDealt);
            Console.WriteLine(totalAttackDealt);
            Console.Write($"*{Name} dealt {totalAttackDealt} ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("piercing damage!\n");
            Console.ResetColor();
            return totalAttackDealt;
        }
        protected override string DamageString()
        {
            var tempAttributes = TotalAttributes();

            double totalAttackDealt = (attackDamage * (1 + (heroAttributes.dexterity + tempAttributes.dexterity) / 100));

            StringBuilder dmgString = new StringBuilder();
            dmgString.Append(totalAttackDealt + " -> ");
            totalAttackDealt = Math.Round(totalAttackDealt);
            dmgString.AppendLine(totalAttackDealt.ToString());
            dmgString.Append($"*{Name} deals {totalAttackDealt} ");
            dmgString.AppendLine("piercing damage!\n");
            return dmgString.ToString();
        }
    }
}
