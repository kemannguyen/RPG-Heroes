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
    public class RogueHeroUnitTest
    {
        [Fact]
        public void RogueHero_NameAfterCreation_ShouldReturnName()
        {
            RogueHero rogue = new RogueHero("Legolas");
            Assert.Equal("Legolas", rogue.Name);
        }
        [Fact]
        public void RogueHero_ClassAfterCreation_ShouldReturnRanger()
        {
            RogueHero rogue = new RogueHero("Legolas");
            Assert.Equal("Rogue", rogue.heroClass);
        }
        [Fact]
        public void RogueHero_LevelWhenCreated_ShouldReturnLevelOne()
        {
            RogueHero rogue = new RogueHero("Legolas");
            Assert.Equal(1, rogue.Level);
        }
        [Fact]
        public void RogueHero_LevelAttributes_ShouldReturnCorrectAttribute()
        {
            Hero rogue = new RogueHero("Wizard of Oz");
            int[] expectedLvlAttr = new int[] { 1, 4, 1 };
            Assert.Equal(expectedLvlAttr, rogue.levelAttributes);
        }
        [Fact]
        public void RogueHero_NoItemsWhenCreated_ShouldHaveNoEquipment()
        {
            RogueHero rogue = new RogueHero("Legolas");
            Assert.Empty(rogue.equipments);
        }
        [Fact]
        public void RogueHero_BaseStatsWhenCreated_ShouldHaveBaseStats()
        {
            RogueHero rogue = new RogueHero("Legolas");
            HeroAttributes expectedStats = new HeroAttributes(2, 6, 1);
            Assert.True(
                expectedStats.strength == rogue.heroAttributes.strength &&
                expectedStats.dexterity == rogue.heroAttributes.dexterity &&
                expectedStats.intelligence == rogue.heroAttributes.intelligence);
        }
        [Fact]
        public void RogueHero_LevelUp_ShouldBeLevelTwoWithCorrectStats()
        {
            RogueHero rogue = new RogueHero("Legolas");
            rogue.LevelUp();
            HeroAttributes expectedStats = new HeroAttributes(3, 10, 2);
            Assert.True(rogue.Level == 2 &&
                expectedStats.strength == rogue.heroAttributes.strength &&
                expectedStats.dexterity == rogue.heroAttributes.dexterity &&
                expectedStats.intelligence == rogue.heroAttributes.intelligence);
        }

        //weapon
        [Fact]
        public void RogueHero_EquipValidWeapon_ShouldBeHoldingWeaponInsideHero()
        {
            RogueHero rogue = new RogueHero("Gandalf");
            Weapon dagger = new Weapon("dagger", 1, 10, WeaponType.Dagger);
            rogue.Equip(dagger);
            Assert.Equal(rogue.equipments[Slot.Weapon], dagger);
        }
        [Fact]
        public void RogueHero_EquipInvalidWeaponType_ShouldReturnInvalidWeaponException()
        {
            RogueHero rogue = new RogueHero("Harry Potter");
            Weapon staff = new Weapon("staff", 1, 10, WeaponType.Staff);
            var ex = Assert.Throws<InvalidWeaponException>(() => rogue.Equip(staff));
            Assert.Equal(ex.Message, $"InvalidWeaponException: {rogue.Name} Couldn't equip |{staff.ItemName}| Error: wrong type");
        }
        [Fact]
        public void RogueHero_EquipTooHighLevelWeapon_ShouldReturnInvalidWeaponException()
        {
            RogueHero rogue = new RogueHero("Dumbeldore");
            Weapon sword = new Weapon("dagger", 10, 10, WeaponType.Sword);

            var ex = Assert.Throws<InvalidWeaponException>(() => rogue.Equip(sword));
            Assert.Equal(ex.Message, $"InvalidWeaponException: {rogue.Name} Couldn't equip |{sword.ItemName}| Error: too low level");
        }

        //armor
        [Fact]
        public void RogueHero_EquipValidArmor_ShouldBeHoldingItemInsideHero()
        {
            RogueHero rogue = new RogueHero("Gandalf");
            Armor mail = new Armor("robe", 1, Slot.Body, ArmorType.Mail, new HeroAttributes(1, 1, 1));
            rogue.Equip(mail);
            Assert.Equal(rogue.equipments[Slot.Body], mail);
        }

        [Fact]
        public void RogueHero_EquipInvalidArmorType_ShouldReturnError()
        {
            RogueHero rogue = new RogueHero("Harry Potter");
            Armor plate = new Armor("plate", 1, Slot.Body, ArmorType.Plate, new HeroAttributes(1, 1, 1));

            var ex = Assert.Throws<InvalidArmorException>(() => rogue.Equip(plate));
            Assert.Equal(ex.Message, $"InvalidArmorException: {rogue.Name} Couldn't equip |{plate.ItemName}| Error: wrong type");
        }

        [Fact]
        public void RogueHero_EquipTooHighLevelArmor_ShouldReturnError()
        {
            RogueHero rogue = new RogueHero("Dumbeldore");
            Armor mail = new Armor("robe", 10, Slot.Body, ArmorType.Mail, new HeroAttributes(1, 1, 1));

            var ex = Assert.Throws<InvalidArmorException>(() => rogue.Equip(mail));
            Assert.Equal(ex.Message, $"InvalidArmorException: {rogue.Name} Couldn't equip |{mail.ItemName}| Error: too low level");
        }

        //totalAttributes
        [Fact]
        public void RogueHero_TotalAttributes_ShouldReturnItemTotalStats()
        {
            RogueHero rogue = new RogueHero("Dumbeldore");
            Armor mail = new Armor("mail", 1, Slot.Body, ArmorType.Mail, new HeroAttributes(1, 1, 1));
            rogue.Equip(mail);

            var expectedItemStats = new HeroAttributes(3,7,2);
            Assert.True(
                expectedItemStats.strength == rogue.TotalAttributes().strength + rogue.heroAttributes.strength && 
                expectedItemStats.dexterity == rogue.TotalAttributes().dexterity + rogue.heroAttributes.dexterity &&
                expectedItemStats.intelligence == rogue.TotalAttributes().intelligence + rogue.heroAttributes.intelligence
                );
        }
        [Fact]
        public void RogueHero_StatsWhenOneArmorEquipped_ShouldReturnCorrectStats()
        {
            var rogue = new RogueHero("Wizard of Oz");
            var mail = new Armor("Pay To Win mail", 1, Slot.Body, ArmorType.Mail, new HeroAttributes(1, 1, 10));
            Hero hero = rogue;
            hero.Equip(mail);
            HeroAttributes expectedStats = new HeroAttributes(
                2 + mail.returnItemStats()[0],
                6 + mail.returnItemStats()[1],
                1 + mail.returnItemStats()[2]);

            var tempAttributes = hero.TotalAttributes();

            Assert.True(
                  expectedStats.strength == hero.heroAttributes.strength + tempAttributes.strength &&
                  expectedStats.dexterity == hero.heroAttributes.dexterity + tempAttributes.dexterity &&
                  expectedStats.intelligence == hero.heroAttributes.intelligence + tempAttributes.intelligence);
        }
        [Fact]
        public void RogueHero_StatsWhenTwoArmorEquipped_ShouldReturnCorrectStats()
        {
            var rogue = new RogueHero("Wizard of Oz");
            var mail = new Armor("Pay To Win mail", 1, Slot.Body, ArmorType.Mail, new HeroAttributes(1, 1, 10));
            var hat = new Armor("hat", 1, Slot.Head, ArmorType.Mail, new HeroAttributes(1, 1, 5));

            Hero hero = rogue;
            hero.Equip(mail);
            hero.Equip(hat);

            HeroAttributes expectedStats = new HeroAttributes(
                2 + mail.returnItemStats()[0] + hat.returnItemStats()[0],
                6 + mail.returnItemStats()[1] + hat.returnItemStats()[1],
                1 + mail.returnItemStats()[2] + hat.returnItemStats()[2]);

            var tempAttributes = hero.TotalAttributes();

            Assert.True(
                  expectedStats.strength == hero.heroAttributes.strength + tempAttributes.strength &&
                  expectedStats.dexterity == hero.heroAttributes.dexterity + tempAttributes.dexterity &&
                  expectedStats.intelligence == hero.heroAttributes.intelligence + tempAttributes.intelligence);
        }
        [Fact]
        public void RogueHero_StatsWhenOneReplacedArmorEquipped_ShouldReturnCorrectStats()
        {
            var rogue = new RogueHero("Wizard of Oz");
            var mail = new Armor("First mail", 1, Slot.Body, ArmorType.Mail, new HeroAttributes(1, 10, 10));
            var newMail = new Armor("Second mail", 1, Slot.Body, ArmorType.Mail, new HeroAttributes(1, 1, 1));
            rogue.Equip(mail);
            rogue.Equip(newMail);
            HeroAttributes expectedStats = new HeroAttributes(
                2 + newMail.returnItemStats()[0],
                6 + newMail.returnItemStats()[1],
                1 + newMail.returnItemStats()[2]);

            var tempAttributes = rogue.TotalAttributes();

            Assert.True(
                  expectedStats.strength == rogue.heroAttributes.strength + tempAttributes.strength &&
                  expectedStats.dexterity == rogue.heroAttributes.dexterity + tempAttributes.dexterity &&
                  expectedStats.intelligence == rogue.heroAttributes.intelligence + tempAttributes.intelligence);
        }

        //damage
        [Fact]
        public void RogueHero_DamageWhenNoWeaponEquipped_ShouldReturnCorrectDamage()
        {
            var rogue = new RogueHero("Keman");
            var tempAttributes = rogue.TotalAttributes();
            double expectedDMG = Math.Round(rogue.attackDamage * (1 + (rogue.heroAttributes.dexterity + tempAttributes.dexterity) / 100));
            Assert.Equal(expectedDMG, rogue.Damage());
        }
        [Fact]
        public void RogueHero_DamageWhenEquippedOnlyWeapon_ShouldReturnCorrectDamage()
        {
            var rogue = new RogueHero("Keman");
            var overPoweredDagger = new Weapon("Pay To Win dagger", 1, 100, WeaponType.Dagger);
            rogue.Equip(overPoweredDagger);
            var tempAttributes = rogue.TotalAttributes();
            double expectedDMG = 106;
            Assert.Equal(expectedDMG, rogue.Damage());
        }
        [Fact]
        public void RogueHero_DamageWhenEquippedArmorAndWeapon_ShouldReturnCorrectDamage()
        {
            var rogue = new RogueHero("Keman");
            var overPoweredWeapon = new Weapon("Pay To Win dagger", 1, 100, WeaponType.Dagger);
            var overPoweredArmor = new Armor("Pay To Win Cloth", 1, Slot.Body, ArmorType.Mail, new HeroAttributes(1, 1000, 1));
            rogue.Equip(overPoweredWeapon);
            rogue.Equip(overPoweredArmor);
            double expectedDMG = 1106;

            Assert.Equal(expectedDMG, rogue.Damage());
        }

        [Fact]
        public void RogueHero_DamageWhenEquippedOnlyReplacedWeapon_ShouldReturnCorrectDamage()
        {
            var rogue = new RogueHero("Keman");
            var overPoweredDagger = new Weapon("Pay To Win dagger", 1, 100, WeaponType.Dagger);
            var newOPweapon = new Weapon("new Free To play weapon", 1, 1, WeaponType.Dagger);
            rogue.Equip(overPoweredDagger);
            rogue.Equip(newOPweapon);
            double expectedDMG =1;
            Assert.Equal(expectedDMG, rogue.Damage());
        }

        //Fancy display creation
        [Fact]
        public void RogueHero_Display_ShouldPrintOutStatsAndEqupment()
        {
            Hero hero = new RogueHero("Keman");

            StringBuilder displayString = new StringBuilder();
            displayString.AppendLine("----------------------------------------");
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Hero: Keman"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Class: Rogue"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Level: 1"));
            displayString.AppendLine(hero.CreateCharacterDisplay("|--------------------------------------"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Attack: 1 "));

            displayString.AppendLine(hero.CreateCharacterDisplay($"|                Stats"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| STR: 2 (+ 0)"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| DEX: 6 (+ 0)"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| INT: 1 (+ 0)"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"|                                "));

            displayString.AppendLine(hero.CreateCharacterDisplay($"|^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ "));
            displayString.AppendLine(hero.CreateCharacterDisplay($"|              Equipment"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Weapon: "));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Head: "));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Body: "));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Legs: "));
            displayString.AppendLine("----------------------------------------");
            displayString.AppendLine("1.06 -> 1");
            displayString.AppendLine("*Keman deals 1 slash damage!\n");
            string expectedString = displayString.ToString();
            string heroDisplay = hero.Display();

            Assert.Equal(heroDisplay, expectedString);
        }
    }
}
