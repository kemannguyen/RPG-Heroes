
using RPG_Heroes;
using RPG_Heroes.Classes;
using RPG_Heroes.CustomExceptions;
using RPG_Heroes.Items;

Hero hero = new RangerHero("Legolas");
var hat = new Armor("helmet", 1, Slot.Head, ArmorType.Mail, new HeroAttributes(0, 3, 0));
var bow = new Weapon("Deaths dance", 1, 3, WeaponType.Bow);
hero.Equip(bow);
hero.Equip(hat);
hero.Display();
hero.Damage();
