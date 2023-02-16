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
    public class RangerHeroUnitTest
    {
        [Fact]
        public void RangerHero_NameAfterCreation_ShouldReturnName()
        {
            Hero ranger = new RangerHero("Legolas");
            Assert.Equal("Legolas", ranger.Name);
        }
        [Fact]
        public void RangerHero_ClassAfterCreation_ShouldReturnRanger()
        {
            Hero ranger = new RangerHero("Legolas");
            Assert.Equal("Ranger", ranger.heroClass);
        }
        [Fact]
        public void RangerHero_LevelWhenCreated_ShouldReturnLevelOne()
        {
            RangerHero ranger = new RangerHero("Legolas");
            Assert.Equal(1, ranger.Level);
        }
        [Fact]
        public void RangeHero_LevelAttributes_ShouldReturnCorrectAttribute()
        {
            Hero ranger = new RangerHero("Wizard of Oz");
            int[] expectedLvlAttr = new int[] { 1, 5, 1 };
            Assert.Equal(expectedLvlAttr, ranger.levelAttributes);
        }
        [Fact]
        public void RangerHero_NoItemsWhenCreated_ShouldHaveNoEquipment()
        {
            Hero ranger = new RangerHero("Legolas");
            Assert.Empty(ranger.equipments);
        }
        [Fact]
        public void RangerHero_BaseStatsWhenCreated_ShouldHaveBaseStats()
        {
            Hero ranger = new RangerHero("Legolas");
            HeroAttributes expectedStats = new HeroAttributes(1, 7, 1);
            Assert.True(
                expectedStats.strength == ranger.heroAttributes.strength &&
                expectedStats.dexterity == ranger.heroAttributes.dexterity &&
                expectedStats.intelligence == ranger.heroAttributes.intelligence);
        }
        [Fact]
        public void RangerHero_LevelUp_ShouldBeLevelTwoWithCorrectStats()
        {
            Hero ranger = new RangerHero("Legolas");
            ranger.LevelUp();
            HeroAttributes expectedStats = new HeroAttributes(2, 12, 2);
            Assert.True(ranger.Level == 2 &&
                expectedStats.strength == ranger.heroAttributes.strength &&
                expectedStats.dexterity == ranger.heroAttributes.dexterity &&
                expectedStats.intelligence == ranger.heroAttributes.intelligence);
        }

        //weapon
        [Fact]
        public void RangerHero_EquipValidWeapon_ShouldBeHoldingWeaponInsideHero()
        {
            Hero ranger = new RangerHero("Gandalf");
            Weapon bow = new Weapon("bow", 1, 10, WeaponType.Bow);
            ranger.Equip(bow);
            Assert.Equal(ranger.equipments[Slot.Weapon], bow);
        }
        [Fact]
        public void RangerHero_EquipInvalidWeaponType_ShouldReturnInvalidWeaponException()
        {
            Hero ranger = new RangerHero("Harry Potter");
            Weapon staff = new Weapon("dagger", 1, 10, WeaponType.Staff);
            var ex = Assert.Throws<InvalidWeaponException>(() => ranger.Equip(staff));
            Assert.Equal(ex.Message, $"InvalidWeaponException: {ranger.Name} Couldn't equip |{staff.ItemName}| Error: wrong type");
        }
        [Fact]
        public void RangerHero_EquipTooHighLevelWeapon_ShouldReturnInvalidWeaponException()
        {
            Hero ranger = new RangerHero("Dumbeldore");
            Weapon bow = new Weapon("bow", 10, 10, WeaponType.Bow);

            var ex = Assert.Throws<InvalidWeaponException>(() => ranger.Equip(bow));
            Assert.Equal(ex.Message, $"InvalidWeaponException: {ranger.Name} Couldn't equip |{bow.ItemName}| Error: too low level");
        }

        //armor
        [Fact]
        public void RangerHero_EquipValidArmor_ShouldBeHoldingItemInsideHero()
        {
            Hero ranger = new RangerHero("Gandalf");
            Armor mail = new Armor("mail", 1, Slot.Body, ArmorType.Mail, new HeroAttributes(1, 1, 1));
            ranger.Equip(mail);
            Assert.Equal(ranger.equipments[Slot.Body], mail);
        }

        [Fact]
        public void RangerHero_EquipInvalidArmorType_ShouldReturnError()
        {
            Hero ranger = new RangerHero("Harry Potter");
            Armor plate = new Armor("plate", 1, Slot.Body, ArmorType.Plate, new HeroAttributes(1, 1, 1));

            var ex = Assert.Throws<InvalidArmorException>(() => ranger.Equip(plate));
            Assert.Equal(ex.Message, $"InvalidArmorException: {ranger.Name} Couldn't equip |{plate.ItemName}| Error: wrong type");
        }

        [Fact]
        public void RangerHero_EquipTooHighLevelArmor_ShouldReturnError()
        {
            Hero ranger = new RangerHero("Dumbeldore");
            Armor mail = new Armor("mail", 10, Slot.Body, ArmorType.Mail, new HeroAttributes(1, 1, 1));

            var ex = Assert.Throws<InvalidArmorException>(() => ranger.Equip(mail));
            Assert.Equal(ex.Message, $"InvalidArmorException: {ranger.Name} Couldn't equip |{mail.ItemName}| Error: too low level");
        }

        //total Attributes
        [Fact]
        public void RangerHero_TotalAttributes_ShouldReturnItemTotalStats()
        {
            Hero ranger = new RangerHero("Dumbeldore");
            Armor mail = new Armor("mail", 1, Slot.Body, ArmorType.Mail, new HeroAttributes(1, 1, 1));
            ranger.Equip(mail);

            var expectedItemStats = new HeroAttributes(2, 8, 2);
            Assert.True(
                expectedItemStats.strength == ranger.TotalAttributes().strength + ranger.heroAttributes.strength &&
                expectedItemStats.dexterity == ranger.TotalAttributes().dexterity + ranger.heroAttributes.dexterity &&
                expectedItemStats.intelligence == ranger.TotalAttributes().intelligence + ranger.heroAttributes.intelligence
                );
        }
        [Fact]
        public void RangerHero_StatsWhenOneArmorEquipped_ShouldReturnCorrectStats()
        {
            Hero ranger = new RangerHero("Wizard of Oz");
            var mail = new Armor("Pay To Win mail", 1, Slot.Body, ArmorType.Mail, new HeroAttributes(1, 1, 10));
            Hero hero = ranger;
            hero.Equip(mail);
            HeroAttributes expectedStats = new HeroAttributes(
                1 + mail.returnItemStats()[0],
                7 + mail.returnItemStats()[1],
                1 + mail.returnItemStats()[2]);

            var tempAttributes = hero.TotalAttributes();

            Assert.True(
                  expectedStats.strength == hero.heroAttributes.strength + tempAttributes.strength &&
                  expectedStats.dexterity == hero.heroAttributes.dexterity + tempAttributes.dexterity &&
                  expectedStats.intelligence == hero.heroAttributes.intelligence + tempAttributes.intelligence);
        }
        [Fact]
        public void RangerHero_StatsWhenTwoArmorEquipped_ShouldReturnCorrectStats()
        {
            Hero ranger = new RangerHero("Wizard of Oz");
            var mail = new Armor("Pay To Win mail", 1, Slot.Body, ArmorType.Mail, new HeroAttributes(1, 1, 10));
            var hat = new Armor("hat", 1, Slot.Head, ArmorType.Mail, new HeroAttributes(1, 1, 5));

            Hero hero = ranger;
            hero.Equip(mail);
            hero.Equip(hat);

            HeroAttributes expectedStats = new HeroAttributes(
                1 + mail.returnItemStats()[0] + hat.returnItemStats()[0],
                7 + mail.returnItemStats()[1] + hat.returnItemStats()[1],
                1 + mail.returnItemStats()[2] + hat.returnItemStats()[2]);

            var tempAttributes = hero.TotalAttributes();

            Assert.True(
                  expectedStats.strength == hero.heroAttributes.strength + tempAttributes.strength &&
                  expectedStats.dexterity == hero.heroAttributes.dexterity + tempAttributes.dexterity &&
                  expectedStats.intelligence == hero.heroAttributes.intelligence + tempAttributes.intelligence);
        }
        [Fact]
        public void RangerHero_StatsWhenOneReplacedArmorEquipped_ShouldReturnCorrectStats()
        {
            Hero ranger = new RangerHero("Wizard of Oz");
            var mail = new Armor("First mail", 1, Slot.Body, ArmorType.Mail, new HeroAttributes(1, 10, 10));
            var newMail = new Armor("Second mail", 1, Slot.Body, ArmorType.Mail, new HeroAttributes(1, 1, 1));
            ranger.Equip(mail);
            ranger.Equip(newMail);
            HeroAttributes expectedStats = new HeroAttributes(
                1 + newMail.returnItemStats()[0],
                7 + newMail.returnItemStats()[1],
                1 + newMail.returnItemStats()[2]);

            var tempAttributes = ranger.TotalAttributes();

            Assert.True(
                  expectedStats.strength == ranger.heroAttributes.strength + tempAttributes.strength &&
                  expectedStats.dexterity == ranger.heroAttributes.dexterity + tempAttributes.dexterity &&
                  expectedStats.intelligence == ranger.heroAttributes.intelligence + tempAttributes.intelligence);
        }

        //damage
        [Fact]
        public void RangerHero_DamageWhenNoWeaponEquipped_ShouldReturnCorrectDamage()
        {
            Hero ranger = new RangerHero("Keman");
            double expectedDMG = 1;
            Assert.Equal(expectedDMG, ranger.Damage());
        }
        [Fact]
        public void RangerHero_DamageWhenEquippedOnlyWeapon_ShouldReturnCorrectDamage()
        {
            Hero ranger = new RangerHero("Keman");
            var overPoweredBow = new Weapon("Pay To Win bow", 1, 100, WeaponType.Bow);
            ranger.Equip(overPoweredBow);
            double expectedDMG = 107;
            Assert.Equal(expectedDMG, ranger.Damage());
        }
        [Fact]
        public void RangerHero_DamageWhenEquippedArmorAndWeapon_ShouldReturnCorrectDamage()
        {
            Hero ranger = new RangerHero("Keman");
            var overPoweredWeapon = new Weapon("Pay To Win bow", 1, 100, WeaponType.Bow);
            var overPoweredArmor = new Armor("Pay To Win Cloth", 1, Slot.Body, ArmorType.Mail, new HeroAttributes(1, 1000, 1));
            ranger.Equip(overPoweredWeapon);
            ranger.Equip(overPoweredArmor);
            double expectedDMG = 1107;

            Assert.Equal(expectedDMG, ranger.Damage());
        }

        [Fact]
        public void RangerHero_DamageWhenEquippedOnlyReplacedWeapon_ShouldReturnCorrectDamage()
        {
            Hero ranger = new RangerHero("Keman");
            var overPoweredBow = new Weapon("Pay To Win bow", 1, 100, WeaponType.Bow);
            var newOPBOW = new Weapon("new Free To play Staff", 1, 1, WeaponType.Bow);
            ranger.Equip(overPoweredBow);
            ranger.Equip(newOPBOW);
            double expectedDMG = 1;
            Assert.Equal(expectedDMG, ranger.Damage());
        }

        //Fancy display creation
        [Fact]
        public void RangerHero_Display_ShouldPrintOutStatsAndEqupment()
        {
            Hero hero = new RangerHero("Keman");

            StringBuilder displayString = new StringBuilder();
            displayString.AppendLine("----------------------------------------");
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Hero: Keman"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Class: Ranger"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Level: 1"));
            displayString.AppendLine(hero.CreateCharacterDisplay("|--------------------------------------"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Attack: 1 "));

            displayString.AppendLine(hero.CreateCharacterDisplay($"|                Stats"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| STR: 1 (+ 0)"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| DEX: 7 (+ 0)"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| INT: 1 (+ 0)"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"|                                "));

            displayString.AppendLine(hero.CreateCharacterDisplay($"|^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ "));
            displayString.AppendLine(hero.CreateCharacterDisplay($"|              Equipment"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Weapon: "));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Head: "));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Body: "));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Legs: "));
            displayString.AppendLine("----------------------------------------");
            displayString.AppendLine("1.07 -> 1");
            displayString.AppendLine("*Keman deals 1 piercing damage!\n");
            string expectedString = displayString.ToString();
            string heroDisplay = hero.Display();

            Assert.Equal(heroDisplay, expectedString);
        }
    }
}
