using RPG_Heroes;
using RPG_Heroes.Classes;
using RPG_Heroes.CustomExceptions;
using RPG_Heroes.Items;
using System.Reflection.Emit;
using System.Text;
using System.Xml.Linq;

namespace RPGHeroTest
{
    public class HeroUnitTest
    {
        //Hero equip
        [Fact]
        public void Hero_EquipValidWeapon_ShouldBeHoldingWeaponInsideHero()
        {
            MageHero mage = new MageHero("Gandalf");
            Weapon staff = new Weapon("staff", 1, 10, WeaponType.Staff);
            MageHero hero = mage;
            hero.Equip(staff);
            Assert.Equal(hero.equipments[Slot.Weapon], staff);
        }

        [Fact]
        public void Hero_EquipInvalidWeapon_ShouldReturnInvalidWeaponException()
        {
            MageHero mage = new MageHero("Harry Potter");
            Weapon dagger = new Weapon("dagger", 1, 10, WeaponType.Dagger);
            MageHero hero = mage;

            Assert.Throws<InvalidWeaponException>(() => hero.Equip(dagger));
        }

        [Fact]
        public void Hero_EquipTooHighLevelWeapon_ShouldReturnInvalidWeaponException()
        {
            MageHero mage = new MageHero("Dumbeldore");
            Weapon stick = new Weapon("stick", 10, 10, WeaponType.Staff);
            MageHero hero = mage;

            Assert.Throws<InvalidWeaponException>(() => hero.Equip(stick));
        }
        [Fact]
        public void Hero_EquipValidArmor_ShouldBeHoldingItemInsideHero()
        {
            MageHero mage = new MageHero("Gandalf");
            Armor robe = new Armor("robe", 1, Slot.Body, ArmorType.Cloth, new HeroAttributes(1, 1, 1));
            MageHero hero = mage;
            hero.Equip(robe);
            Assert.Equal(hero.equipments[Slot.Body], robe);
        }

        [Fact]
        public void Hero_EquipInvalidArmor_ShouldReturnError()
        {
            MageHero mage = new MageHero("Harry Potter");
            Armor plate = new Armor("plate", 1, Slot.Body, ArmorType.Plate, new HeroAttributes(1, 1, 1));
            MageHero hero = mage;

            Assert.Throws<InvalidArmorException>(() => hero.Equip(plate));
        }

        [Fact]
        public void Hero_EquipTooHighLevelArmor_ShouldReturnError()
        {
            MageHero mage = new MageHero("Dumbeldore");
            Armor robe = new Armor("robe", 10, Slot.Body, ArmorType.Cloth, new HeroAttributes(1, 1, 1));
            MageHero hero = mage;

            Assert.Throws<InvalidArmorException>(() => hero.Equip(robe));
        }
        [Fact]
        public void Hero_ReplaceEquippedItem_ShouldReturnNewItem()
        {
            MageHero mage = new MageHero("Dumbeldore");
            Armor robe = new Armor("robe", 1, Slot.Body, ArmorType.Cloth, new HeroAttributes(1, 1, 1));
            Armor newRobe = new Armor("new Robe", 1, Slot.Body, ArmorType.Cloth, new HeroAttributes(1, 1, 1));
            MageHero hero = mage;
            hero.Equip(robe);
            hero.Equip(newRobe);

            Assert.Equal(newRobe, hero.equipments[Slot.Body]);
        }

        //Stats
        [Fact]
        public void Hero_StatsWhenNoArmorEquipped_ShouldHaveBaseStats()
        {
            MageHero mage = new MageHero("Wizard of Oz");
            Hero hero = mage;
            HeroAttributes expectedStats = new HeroAttributes(1, 1, 8);
            Assert.True(
                  expectedStats.strength == hero.heroAttributes.strength &&
                  expectedStats.dexterity == hero.heroAttributes.dexterity &&
                  expectedStats.intelligence == hero.heroAttributes.intelligence);
        }
        [Fact]
        public void Hero_StatsWhenOneArmorEquipped_ShouldReturnCorrectStats()
        {
            var mage = new MageHero("Wizard of Oz");
            var clothRobe = new Armor("Pay To Win Cloth", 1, Slot.Body, ArmorType.Cloth, new HeroAttributes(1, 1, 10));
            Hero hero = mage;
            hero.Equip(clothRobe);
            HeroAttributes expectedStats = new HeroAttributes(
                1 + clothRobe.returnItemStats()[0],
                1 + clothRobe.returnItemStats()[1],
                8 + clothRobe.returnItemStats()[2]);

            var tempAttributes = hero.TotalAttributes();

            Assert.True(
                  expectedStats.strength == hero.heroAttributes.strength + tempAttributes.strength &&
                  expectedStats.dexterity == hero.heroAttributes.dexterity + tempAttributes.dexterity &&
                  expectedStats.intelligence == hero.heroAttributes.intelligence + tempAttributes.intelligence);
        }
        [Fact]
        public void Hero_StatsWhenTwoArmorEquipped_ShouldReturnCorrectStats()
        {
            var mage = new MageHero("Wizard of Oz");
            var clothRobe = new Armor("Pay To Win Cloth", 1, Slot.Body, ArmorType.Cloth, new HeroAttributes(1, 1, 10));
            var hat = new Armor("hat", 1, Slot.Head, ArmorType.Cloth, new HeroAttributes(1, 1, 5));

            Hero hero = mage;
            hero.Equip(clothRobe);
            hero.Equip(hat);

            HeroAttributes expectedStats = new HeroAttributes(
                1 + clothRobe.returnItemStats()[0] + hat.returnItemStats()[0],
                1 + clothRobe.returnItemStats()[1] + hat.returnItemStats()[1],
                8 + clothRobe.returnItemStats()[2] + hat.returnItemStats()[2]);

            var tempAttributes = hero.TotalAttributes();

            Assert.True(
                  expectedStats.strength == hero.heroAttributes.strength + tempAttributes.strength &&
                  expectedStats.dexterity == hero.heroAttributes.dexterity + tempAttributes.dexterity &&
                  expectedStats.intelligence == hero.heroAttributes.intelligence + tempAttributes.intelligence);
        }
        [Fact]
        public void Hero_StatsWhenOneReplacedArmorEquipped_ShouldReturnCorrectStats()
        {
            var mage = new MageHero("Wizard of Oz");
            var clothRobe = new Armor("First cloth", 1, Slot.Body, ArmorType.Cloth, new HeroAttributes(1, 1, 10));
            var newClothRobe = new Armor("Second cloth", 1, Slot.Body, ArmorType.Cloth, new HeroAttributes(1, 1, 1));
            Hero hero = mage;
            hero.Equip(clothRobe);
            hero.Equip(newClothRobe);
            HeroAttributes expectedStats = new HeroAttributes(
                1 + newClothRobe.returnItemStats()[0],
                1 + newClothRobe.returnItemStats()[1],
                8 + newClothRobe.returnItemStats()[2]);

            var tempAttributes = hero.TotalAttributes();

            Assert.True(
                  expectedStats.strength == hero.heroAttributes.strength + tempAttributes.strength &&
                  expectedStats.dexterity == hero.heroAttributes.dexterity + tempAttributes.dexterity &&
                  expectedStats.intelligence == hero.heroAttributes.intelligence + tempAttributes.intelligence);
        }

        //Damage
        [Fact]
        public void Hero_DamageWhenNoWeaponEquipped_ShouldReturnCorrectDamage()
        {
            var testMage = new MageHero("Keman");
            var hero = testMage;
            var tempAttributes = hero.TotalAttributes();
            double expectedDMG = Math.Round(hero.attackDamage * (1 + (hero.heroAttributes.intelligence + tempAttributes.intelligence) / 100));
            Assert.Equal(expectedDMG, hero.Damage());
        }
        [Fact]
        public void Hero_DamageWhenEquippedOnlyWeapon_ShouldReturnCorrectDamage()
        {
            var testMage = new MageHero("Keman");
            var hero = testMage;
            var overPoweredStaff = new Weapon("Pay To Win Staff", 1, 100, WeaponType.Staff);
            hero.Equip(overPoweredStaff);
            var tempAttributes = hero.TotalAttributes();
            double expectedDMG = Math.Round(hero.attackDamage * (1 + (hero.heroAttributes.intelligence + tempAttributes.intelligence) / 100));
            Assert.Equal(expectedDMG, hero.Damage());
        }
        [Fact]
        public void Hero_DamageWhenEquippedOnlyReplacedWeapon_ShouldReturnCorrectDamage()
        {
            var testMage = new MageHero("Keman");
            var hero = testMage;
            var overPoweredStaff = new Weapon("Pay To Win Staff", 1, 100, WeaponType.Staff);
            var newWeakStaff = new Weapon("new Free To play Staff", 1, 1, WeaponType.Staff);
            hero.Equip(overPoweredStaff);
            hero.Equip(newWeakStaff);
            var tempAttributes = hero.TotalAttributes();
            double expectedDMG = Math.Round(hero.attackDamage * (1 + (hero.heroAttributes.intelligence + tempAttributes.intelligence) / 100));
            Assert.Equal(expectedDMG, hero.Damage());
        }
        [Fact]
        public void Hero_DamageWhenEquippedArmorAndWeapon_ShouldReturnCorrectDamage()
        {
            var testMage = new MageHero("Keman");
            var hero = testMage;
            var overPoweredStaff = new Weapon("Pay To Win Staff", 1, 100, WeaponType.Staff);
            var overPoweredArmor = new Armor("Pay To Win Cloth", 1, Slot.Body, ArmorType.Cloth, new HeroAttributes(1, 1, 1000));
            hero.Equip(overPoweredStaff);
            hero.Equip(overPoweredArmor);
            var tempAttributes = hero.TotalAttributes();
            double expectedDMG = Math.Round(hero.attackDamage * (1 + (hero.heroAttributes.intelligence + tempAttributes.intelligence) / 100));
            Assert.Equal(expectedDMG, hero.Damage());
        }

        [Fact]
        //Fancy display creation
        public void Hero_Display_ShouldPrintOutStatsAndEqupment()
        {
            MageHero testMage = new MageHero("Keman");
            Hero hero = testMage;

            var tempAttributes = hero.TotalAttributes();
            string[] showItems = new string[4];
            Slot[] slots = new Slot[] { Slot.Weapon, Slot.Head, Slot.Body, Slot.Legs };

            //saves the equipment name into the right display string
            for (int i = 0; i < showItems.Length; i++)
            {
                if (hero.equipments.ContainsKey(slots[i]))
                {
                    showItems[i] = hero.equipments[slots[i]].ItemName;
                }
                if (showItems[i] == null)
                {
                    showItems[i] = "";
                }
            }

            StringBuilder displayString = new StringBuilder();
            displayString.AppendLine("----------------------------------------");
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Hero: {hero.Name}"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Class: {hero.heroClass}"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Level: {hero.Level}"));
            displayString.AppendLine(hero.CreateCharacterDisplay("|--------------------------------------"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Attack: {hero.attackDamage}"));

            displayString.AppendLine(hero.CreateCharacterDisplay($"|                Stats"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| STR: {hero.heroAttributes.strength + tempAttributes.strength} (+ {tempAttributes.strength})"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| DEX: {hero.heroAttributes.dexterity + tempAttributes.dexterity} (+ {tempAttributes.dexterity})"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| INT: {hero.heroAttributes.intelligence + tempAttributes.intelligence} (+ {tempAttributes.intelligence})"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"|                                "));

            displayString.AppendLine(hero.CreateCharacterDisplay($"|^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ "));
            displayString.AppendLine(hero.CreateCharacterDisplay($"|              Equipment"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Weapon: {showItems[0]}"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Head: {showItems[1]}"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Body: {showItems[2]}"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Legs: {showItems[3]}"));
            displayString.AppendLine("----------------------------------------");
            string expectedString = displayString.ToString();
            string heroDisplay = hero.Display();

            Assert.Equal(heroDisplay, expectedString);
        }

    }
}