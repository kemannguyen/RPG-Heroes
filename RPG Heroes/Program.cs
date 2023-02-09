
using RPG_Heroes;
using RPG_Heroes.Classes;
using RPG_Heroes.Items;

MageHero myHero = new MageHero("Keman");
RangerHero legolas = new RangerHero("Legolas");
WarriorHero mukmuk = new WarriorHero("MukMuk the great");
RogueHero thief = new RogueHero("thief");

Weapon staffOfWisdom = new Weapon("Staff Of Wisdom", 1, 30, WeaponType.Staff);
Weapon wandOfPuberty = new Weapon("Wand of Puberty", 3, 20, WeaponType.Wand);
Weapon shadowBow = new Weapon("ShadowBow", 1, 10, WeaponType.Bow);
Weapon bananaDagger = new Weapon("Banana dagger", 2, 5, WeaponType.Dagger);
Weapon commonAxe = new Weapon("Common Axe", 1, 2, WeaponType.Axe);

Armor silkyRobe = new Armor("Spider Queen's silky robe", 1, Slot.Body, ArmorType.Cloth, new HeroAttributes(1, 0, 8));
Armor ragRobe = new Armor("rag rope", 1, Slot.Body, ArmorType.Cloth, new HeroAttributes(0, 0, 0));
Armor hood = new Armor("common hood", 1, Slot.Head, ArmorType.Cloth, new HeroAttributes(0, 0, 1));
Armor pants = new Armor("pants", 1, Slot.Legs, ArmorType.Cloth, new HeroAttributes(1, 1, 1));

List<Hero> heroes = new List<Hero>();

heroes.Add(myHero);
heroes.Add(legolas);
heroes.Add(mukmuk);
heroes.Add(thief);

for(int i = 0; i < 10; i++)
{
    mukmuk.LevelUp();
}

foreach(Hero hero in heroes)
{
    hero.Equip(staffOfWisdom);
    hero.Equip(wandOfPuberty);
    hero.Display();
    hero.Damage();
}