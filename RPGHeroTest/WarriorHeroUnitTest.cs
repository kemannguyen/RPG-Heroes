using RPG_Heroes.Classes;
using RPG_Heroes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Heroes.CustomExceptions;
using RPG_Heroes.Items;

namespace RPGHeroTest
{
    public class WarriorHeroUnitTest
    {
        [Fact]
        public void WarriorHero_NameAfterCreation_ShouldReturnName()
        {
            WarriorHero warrior = new WarriorHero("Garen");
            Assert.Equal("Garen", warrior.Name);
        }
        [Fact]
        public void WarriorHero_ClassAfterCreation_ShouldReturnRanger()
        {
            WarriorHero warrior = new WarriorHero("Garen");
            Assert.Equal("Warrior", warrior.heroClass);
        }
        [Fact]
        public void WarriorHero_LevelWhenCreated_ShouldReturnLevelOne()
        {
            WarriorHero warrior = new WarriorHero("Garen");
            Assert.Equal(1, warrior.Level);
        }
        [Fact]
        public void WarriorHero_LevelAttributes_ShouldReturnCorrectAttribute()
        {
            Hero warrior = new WarriorHero("Wizard of Oz");
            int[] expectedLvlAttr = new int[] { 3, 2, 1 };
            Assert.Equal(expectedLvlAttr, warrior.levelAttributes);
        }
        [Fact]
        public void WarriorHero_NoItemsWhenCreated_ShouldHaveNoEquipment()
        {
            WarriorHero warrior = new WarriorHero("Garen");
            Assert.Empty(warrior.equipments);
        }
        [Fact]
        public void WarriorHero_BaseStatsWhenCreated_ShouldHaveBaseStats()
        {
            WarriorHero warrior = new WarriorHero("Garen");
            HeroAttributes expectedStats = new HeroAttributes(5, 2, 1);
            Assert.True(
                expectedStats.strength == warrior.heroAttributes.strength &&
                expectedStats.dexterity == warrior.heroAttributes.dexterity &&
                expectedStats.intelligence == warrior.heroAttributes.intelligence);
        }
        [Fact]
        public void WarriorHero_LevelUp_ShouldBeLevelTwoWithCorrectStats()
        {
            WarriorHero warrior = new WarriorHero("Garen");
            warrior.LevelUp();
            HeroAttributes expectedStats = new HeroAttributes(8, 4, 2);
            Assert.True(warrior.Level == 2 &&
                expectedStats.strength == warrior.heroAttributes.strength &&
                expectedStats.dexterity == warrior.heroAttributes.dexterity &&
                expectedStats.intelligence == warrior.heroAttributes.intelligence);
        }

        //weapon
        [Fact]
        public void WarriorHero_EquipValidWeapon_ShouldBeHoldingWeaponInsideHero()
        {
            WarriorHero warrior = new WarriorHero("Garen");
            Weapon sword = new Weapon("bow", 1, 10, WeaponType.Sword);
            warrior.Equip(sword);
            Assert.Equal(warrior.equipments[Slot.Weapon], sword);
        }
        [Fact]
        public void WarriorHero_EquipInvalidWeaponType_ShouldReturnInvalidWeaponException()
        {
            WarriorHero warrior = new WarriorHero("Garen");
            Weapon staff = new Weapon("sword", 1, 10, WeaponType.Staff);
            var ex = Assert.Throws<InvalidWeaponException>(() => warrior.Equip(staff));
            Assert.Equal(ex.Message, $"InvalidWeaponException: {warrior.Name} Couldn't equip |{staff.ItemName}| Error: wrong type");
        }
        [Fact]
        public void WarriorHero_EquipTooHighLevelWeapon_ShouldReturnInvalidWeaponException()
        {
            WarriorHero warrior = new WarriorHero("Garen");
            Weapon sword = new Weapon("sword", 10, 10, WeaponType.Sword);

            var ex = Assert.Throws<InvalidWeaponException>(() => warrior.Equip(sword));
            Assert.Equal(ex.Message, $"InvalidWeaponException: {warrior.Name} Couldn't equip |{sword.ItemName}| Error: too low level");
        }

        //armor
        [Fact]
        public void WarriorHero_EquipValidArmor_ShouldBeHoldingItemInsideHero()
        {
            WarriorHero warrior = new WarriorHero("Garen");
            Armor mail = new Armor("mail", 1, Slot.Body, ArmorType.Mail, new HeroAttributes(1, 1, 1));
            warrior.Equip(mail);
            Assert.Equal(warrior.equipments[Slot.Body], mail);
        }

        [Fact]
        public void WarriorHero_EquipInvalidArmorType_ShouldReturnError()
        {
            WarriorHero warrior = new WarriorHero("Garen");
            Armor cloth = new Armor("cloth", 1, Slot.Body, ArmorType.Cloth, new HeroAttributes(1, 1, 1));

            var ex = Assert.Throws<InvalidArmorException>(() => warrior.Equip(cloth));
            Assert.Equal(ex.Message, $"InvalidArmorException: {warrior.Name} Couldn't equip |{cloth.ItemName}| Error: wrong type");
        }

        [Fact]
        public void WarriorHero_EquipTooHighLevelArmor_ShouldReturnError()
        {
            WarriorHero warrior = new WarriorHero("Garen");
            Armor mail = new Armor("mail", 10, Slot.Body, ArmorType.Mail, new HeroAttributes(1, 1, 1));

            var ex = Assert.Throws<InvalidArmorException>(() => warrior.Equip(mail));
            Assert.Equal(ex.Message, $"InvalidArmorException: {warrior.Name} Couldn't equip |{mail.ItemName}| Error: too low level");
        }

        //totalAttributes
        [Fact]
        public void WarriorHero_TotalAttributes_ShouldReturnItemTotalStats()
        {
            WarriorHero warrior = new WarriorHero("Dumbeldore");
            Armor mail = new Armor("mail", 1, Slot.Body, ArmorType.Mail, new HeroAttributes(1, 1, 1));
            warrior.Equip(mail);

            var expectedItemStats = new HeroAttributes(6,3,2);
            Assert.True(
                expectedItemStats.strength == warrior.TotalAttributes().strength + warrior.heroAttributes.strength&&
                expectedItemStats.dexterity == warrior.TotalAttributes().dexterity + warrior.heroAttributes.dexterity &&
                expectedItemStats.intelligence == warrior.TotalAttributes().intelligence + warrior.heroAttributes.intelligence
                );
        }
        [Fact]
        public void WarriorHero_StatsWhenOneArmorEquipped_ShouldReturnCorrectStats()
        {
            var warrior = new WarriorHero("Wizard of Oz");
            var mail = new Armor("Pay To Win mail", 1, Slot.Body, ArmorType.Mail, new HeroAttributes(1, 1, 10));
            Hero hero = warrior;
            hero.Equip(mail);
            HeroAttributes expectedStats = new HeroAttributes(
                5 + mail.returnItemStats()[0],
                2 + mail.returnItemStats()[1],
                1 + mail.returnItemStats()[2]);

            var tempAttributes = hero.TotalAttributes();

            Assert.True(
                  expectedStats.strength == hero.heroAttributes.strength + tempAttributes.strength &&
                  expectedStats.dexterity == hero.heroAttributes.dexterity + tempAttributes.dexterity &&
                  expectedStats.intelligence == hero.heroAttributes.intelligence + tempAttributes.intelligence);
        }
        [Fact]
        public void WarriorHero_StatsWhenTwoArmorEquipped_ShouldReturnCorrectStats()
        {
            var warrior = new WarriorHero("Wizard of Oz");
            var mail = new Armor("Pay To Win mail", 1, Slot.Body, ArmorType.Mail, new HeroAttributes(1, 1, 10));
            var hat = new Armor("hat", 1, Slot.Head, ArmorType.Mail, new HeroAttributes(1, 1, 5));

            Hero hero = warrior;
            hero.Equip(mail);
            hero.Equip(hat);

            HeroAttributes expectedStats = new HeroAttributes(
                5 + mail.returnItemStats()[0] + hat.returnItemStats()[0],
                2 + mail.returnItemStats()[1] + hat.returnItemStats()[1],
                1 + mail.returnItemStats()[2] + hat.returnItemStats()[2]);

            var tempAttributes = hero.TotalAttributes();

            Assert.True(
                  expectedStats.strength == hero.heroAttributes.strength + tempAttributes.strength &&
                  expectedStats.dexterity == hero.heroAttributes.dexterity + tempAttributes.dexterity &&
                  expectedStats.intelligence == hero.heroAttributes.intelligence + tempAttributes.intelligence);
        }
        [Fact]
        public void WarriorHero_StatsWhenOneReplacedArmorEquipped_ShouldReturnCorrectStats()
        {
            var warrior = new WarriorHero("Wizard of Oz");
            var mail = new Armor("First mail", 1, Slot.Body, ArmorType.Mail, new HeroAttributes(1, 10, 10));
            var newMail = new Armor("Second mail", 1, Slot.Body, ArmorType.Mail, new HeroAttributes(1, 1, 1));
            warrior.Equip(mail);
            warrior.Equip(newMail);
            HeroAttributes expectedStats = new HeroAttributes(
                5 + newMail.returnItemStats()[0],
                2 + newMail.returnItemStats()[1],
                1 + newMail.returnItemStats()[2]);

            var tempAttributes = warrior.TotalAttributes();

            Assert.True(
                  expectedStats.strength == warrior.heroAttributes.strength + tempAttributes.strength &&
                  expectedStats.dexterity == warrior.heroAttributes.dexterity + tempAttributes.dexterity &&
                  expectedStats.intelligence == warrior.heroAttributes.intelligence + tempAttributes.intelligence);
        }

        //damage
        [Fact]
        public void WarriorHero_DamageWhenNoWeaponEquipped_ShouldReturnCorrectDamage()
        {
            var warrior = new WarriorHero("Keman");
            double expectedDMG = 1;
            Assert.Equal(expectedDMG, warrior.Damage());
        }
        [Fact]
        public void WarriorHero_DamageWhenEquippedOnlyWeapon_ShouldReturnCorrectDamage()
        {
            var warrior = new WarriorHero("Keman");
            var overPoweredSword = new Weapon("Pay To Win sword", 1, 100, WeaponType.Sword);
            warrior.Equip(overPoweredSword);
            double expectedDMG = 102;
            Assert.Equal(expectedDMG, warrior.Damage());
        }
        [Fact]
        public void WarriorHero_DamageWhenEquippedArmorAndWeapon_ShouldReturnCorrectDamage()
        {
            var warrior = new WarriorHero("Keman");
            var overPoweredWeapon = new Weapon("Pay To Win sword", 1, 100, WeaponType.Sword);
            var overPoweredArmor = new Armor("Pay To Win Cloth", 1, Slot.Body, ArmorType.Mail, new HeroAttributes(1, 1000, 1));
            warrior.Equip(overPoweredWeapon);
            warrior.Equip(overPoweredArmor);
            double expectedDMG = 1102;

            Assert.Equal(expectedDMG, warrior.Damage());
        }

        [Fact]
        public void WarriorHero_DamageWhenEquippedOnlyReplacedWeapon_ShouldReturnCorrectDamage()
        {
            var warrior = new WarriorHero("Keman");
            var overPoweredSword = new Weapon("Pay To Win sword", 1, 100, WeaponType.Sword);
            var newOPweapon = new Weapon("new Free To play weapon", 1, 1, WeaponType.Sword);
            warrior.Equip(overPoweredSword);
            warrior.Equip(newOPweapon);
            double expectedDMG = 1;
            Assert.Equal(expectedDMG, warrior.Damage());
        }

        //Fancy display creation
        
        [Fact]
        public void WarriorHero_Display_ShouldPrintOutStatsAndEqupment()
        {
            Hero hero = new WarriorHero("Keman");

            StringBuilder displayString = new StringBuilder();
            displayString.AppendLine("----------------------------------------");
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Hero: Keman"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Class: Warrior"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Level: 1"));
            displayString.AppendLine(hero.CreateCharacterDisplay("|--------------------------------------"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Attack: 1 "));

            displayString.AppendLine(hero.CreateCharacterDisplay($"|                Stats"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| STR: 5 (+ 0)"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| DEX: 2 (+ 0)"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| INT: 1 (+ 0)"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"|                                "));

            displayString.AppendLine(hero.CreateCharacterDisplay($"|^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ "));
            displayString.AppendLine(hero.CreateCharacterDisplay($"|              Equipment"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Weapon: "));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Head: "));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Body: "));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Legs: "));
            displayString.AppendLine("----------------------------------------");
            displayString.AppendLine("1.02 -> 1");
            displayString.AppendLine("*Keman deals 1 physical damage!\n");
            string expectedString = displayString.ToString();
            string heroDisplay = hero.Display();

            Assert.Equal(heroDisplay, expectedString);
        }
    }
}
