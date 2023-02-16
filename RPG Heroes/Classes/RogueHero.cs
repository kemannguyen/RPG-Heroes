using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Heroes.Classes
{
    public class RogueHero : Hero
    {
        public RogueHero(string name) : base(name)
        {
            heroClass = "Rogue";
            heroAttributes = new HeroAttributes(2, 6, 1);
            levelAttributes = new int[] { 1, 4, 1};
            ValidWeaponsType = new WeaponType[] { WeaponType.Dagger, WeaponType.Sword };
            ValidArmorType = new ArmorType[] { ArmorType.Leather, ArmorType.Mail};
        }

        public override void LevelUp()
        {
            Level++;
            heroAttributes.IncreaseStats(levelAttributes[0], levelAttributes[1], levelAttributes[2]);
        }

        public override double Damage()
        {
            var tempAttributes = TotalAttributes();

            double totalAttackDealt = (attackDamage * (1 + (heroAttributes.dexterity + tempAttributes.dexterity) / 100));
            Console.Write(totalAttackDealt + " -> ");
            totalAttackDealt = Math.Round(totalAttackDealt);
            Console.WriteLine(totalAttackDealt);
            Console.Write($"*{Name} dealt {totalAttackDealt} ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("slash damage!\n");
            Console.ResetColor();
            return totalAttackDealt;
        }

        protected override string DamageString()
        {
            var tempAttributes = TotalAttributes();

            double totalAttackDealt = (attackDamage * (1 + (heroAttributes.dexterity + tempAttributes.dexterity) / 100));

            StringBuilder dmgString= new StringBuilder();
            dmgString.Append(totalAttackDealt + " -> ");
            totalAttackDealt = Math.Round(totalAttackDealt);
            dmgString.AppendLine(totalAttackDealt.ToString());
            dmgString.Append($"*{Name} deals {totalAttackDealt} ");
            dmgString.AppendLine("slash damage!\n");
            return dmgString.ToString();
        }
    }
}
