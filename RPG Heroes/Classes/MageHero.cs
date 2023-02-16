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
        public override double Damage()
        {
            var tempAttributes=TotalAttributes();
            
            double totalAttackDealt = (attackDamage * (1 + (heroAttributes.intelligence + tempAttributes.intelligence) / 100));
            Console.Write(totalAttackDealt + " -> ");
            totalAttackDealt = Math.Round(totalAttackDealt);
            Console.WriteLine(totalAttackDealt);
            Console.Write($"*{Name} dealt {totalAttackDealt} ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("magic damage!\n");
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
            dmgString.AppendLine("magic damage!\n");
            return dmgString.ToString();
        }

    }
}
