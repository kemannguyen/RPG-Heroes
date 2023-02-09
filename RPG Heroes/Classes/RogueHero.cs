﻿using System;
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

        public override void Damage()
        {
            TotalAttributes();

            double totalAttackDealt = (attackDamage * (1 + (heroAttributes.dexterity + tempAttributes.dexterity) / 100));
            Console.Write(totalAttackDealt + " -> ");
            totalAttackDealt = Math.Round(totalAttackDealt);
            Console.WriteLine(totalAttackDealt);
            Console.Write($"*{Name} dealt {totalAttackDealt} ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("slash damage!");
            Console.ResetColor();
        }
    }
}