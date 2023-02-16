using RPG_Heroes.Classes;
using RPG_Heroes;
using RPG_Heroes.CustomExceptions;
using RPG_Heroes.Items;
using System.Text;
using System.Xml.Linq;

namespace RPGHeroTest
{
    public class MageHeroUnitTest
    {
        [Fact]
        public void MageHero_NameAfterCreation_ShouldReturnName()
        {
            Hero mage = new MageHero("Wizard of Oz");
            Assert.Equal("Wizard of Oz", mage.Name);
        }
        [Fact]
        public void MageHero_ClassAfterCreation_ShouldReturnMage()
        {
            Hero mage = new MageHero("Wizard of Oz");
            Assert.Equal("Mage", mage.heroClass);
        }
        [Fact]
        public void MageHero_LevelWhenCreated_ShouldReturnLevelOne()
        {
            MageHero mage = new MageHero("Wizard of Oz");
            Assert.Equal(1, mage.Level);
        }
        [Fact]
        public void MageHero_LevelAttributes_ShouldReturnCorrectAttribute()
        {
            Hero mage = new MageHero("Wizard of Oz");
            int[] expectedLvlAttr = new int[] { 1, 1, 5 };
            Assert.Equal(expectedLvlAttr, mage.levelAttributes);
        }
        [Fact]
        public void MageHero_NoItemsWhenCreated_ShouldHaveNoEquipment()
        {
            Hero mage = new MageHero("Wizard of Oz");
            Assert.Empty(mage.equipments);
        }
        [Fact]
        public void MageHero_BaseStatsWhenCreated_ShouldHaveBaseStats()
        {
            Hero mage = new MageHero("Wizard of Oz");
            Hero hero = mage;
            HeroAttributes expectedStats = new HeroAttributes(1, 1, 8);
            Assert.True(
                expectedStats.strength == hero.heroAttributes.strength &&
                expectedStats.dexterity == hero.heroAttributes.dexterity &&
                expectedStats.intelligence == hero.heroAttributes.intelligence);
        }
        [Fact]
        public void MageHero_LevelUp_ShouldBeLevelTwoWithCorrectStats()
        {
            Hero mage = new MageHero("Wizard of Oz");
            Hero hero = mage;
            hero.LevelUp();
            HeroAttributes expectedStats = new HeroAttributes(2, 2, 13);
            Assert.True(hero.Level == 2 &&
                expectedStats.strength == hero.heroAttributes.strength &&
                expectedStats.dexterity == hero.heroAttributes.dexterity &&
                expectedStats.intelligence == hero.heroAttributes.intelligence);
        }
        
        //weapon
        [Fact]
        public void MageHero_EquipValidWeapon_ShouldBeHoldingWeaponInsideHero()
        {
            Hero mage = new MageHero("Gandalf");
            Weapon staff = new Weapon("staff", 1, 10, WeaponType.Staff);
            mage.Equip(staff);
            Assert.Equal(mage.equipments[Slot.Weapon], staff);
        }
        [Fact]
        public void MageHero_EquipInvalidWeaponType_ShouldReturnInvalidWeaponException()
        {
            Hero mage = new MageHero("Harry Potter");
            Weapon dagger = new Weapon("dagger", 1, 10, WeaponType.Dagger);
            var ex = Assert.Throws<InvalidWeaponException>(() => mage.Equip(dagger));
            Assert.Equal(ex.Message, $"InvalidWeaponException: Harry Potter Couldn't equip |dagger| Error: wrong type");
        }
        [Fact]
        public void MageHero_EquipTooHighLevelWeapon_ShouldReturnInvalidWeaponException()
        {
            Hero mage = new MageHero("Dumbeldore");
            Weapon stick = new Weapon("stick", 10, 10, WeaponType.Staff);

            var ex = Assert.Throws<InvalidWeaponException>(() => mage.Equip(stick));
            Assert.Equal(ex.Message, $"InvalidWeaponException: Dumbeldore Couldn't equip |stick| Error: too low level");
        }

        //armor
        [Fact]
        public void MageHero_EquipValidArmor_ShouldBeHoldingItemInsideHero()
        {
            MageHero mage = new MageHero("Gandalf");
            Armor robe = new Armor("robe", 1, Slot.Body, ArmorType.Cloth, new HeroAttributes(1, 1, 1));
            mage.Equip(robe);
            Assert.Equal(mage.equipments[Slot.Body], robe);
        }

        [Fact]
        public void MageHero_EquipInvalidArmorType_ShouldReturnError()
        {
            Hero mage = new MageHero("Harry Potter");
            Armor plate = new Armor("plate", 1, Slot.Body, ArmorType.Plate, new HeroAttributes(1, 1, 1));

            var ex = Assert.Throws<InvalidArmorException>(() => mage.Equip(plate));
            Assert.Equal(ex.Message, $"InvalidArmorException: Harry Potter Couldn't equip |plate| Error: wrong type");
        }

        [Fact]
        public void MageHero_EquipTooHighLevelArmor_ShouldReturnError()
        {
            Hero mage = new MageHero("Dumbeldore");
            Armor robe = new Armor("robe", 10, Slot.Body, ArmorType.Cloth, new HeroAttributes(1, 1, 1));

            var ex = Assert.Throws<InvalidArmorException>(() => mage.Equip(robe));
            Assert.Equal(ex.Message, $"InvalidArmorException: Dumbeldore Couldn't equip |robe| Error: too low level");
        }
        //Total Attributes
        [Fact]
        public void MageHero_TotalAttributes_ShouldReturnItemTotalStats()
        {
            Hero mage = new MageHero("Dumbeldore");
            Armor robe = new Armor("robe", 1, Slot.Body, ArmorType.Cloth, new HeroAttributes(1, 1, 1));
            mage.Equip(robe);

            var expectedItemStats = new HeroAttributes(2, 2, 9);
            Assert.True(
                expectedItemStats.strength == mage.TotalAttributes().strength + mage.heroAttributes.strength&&
                expectedItemStats.dexterity == mage.TotalAttributes().dexterity + mage.heroAttributes.dexterity && 
                expectedItemStats.intelligence == mage.TotalAttributes().intelligence + mage.heroAttributes.intelligence
                );
        }
        [Fact]
        public void MageHero_StatsWhenOneArmorEquipped_ShouldReturnCorrectStats()
        {
            Hero mage = new MageHero("Wizard of Oz");
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
        public void MageHero_StatsWhenTwoArmorEquipped_ShouldReturnCorrectStats()
        {
            Hero mage = new MageHero("Wizard of Oz");
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
        public void MageHero_StatsWhenOneReplacedArmorEquipped_ShouldReturnCorrectStats()
        {
            Hero mage = new MageHero("Wizard of Oz");
            var clothRobe = new Armor("First cloth", 1, Slot.Body, ArmorType.Cloth, new HeroAttributes(1, 1, 10));
            var newClothRobe = new Armor("Second cloth", 1, Slot.Body, ArmorType.Cloth, new HeroAttributes(1, 1, 1));
            mage.Equip(clothRobe);
            mage.Equip(newClothRobe);
            HeroAttributes expectedStats = new HeroAttributes(
                1 + newClothRobe.returnItemStats()[0],
                1 + newClothRobe.returnItemStats()[1],
                8 + newClothRobe.returnItemStats()[2]);

            var tempAttributes = mage.TotalAttributes();

            Assert.True(
                  expectedStats.strength == mage.heroAttributes.strength + tempAttributes.strength &&
                  expectedStats.dexterity == mage.heroAttributes.dexterity + tempAttributes.dexterity &&
                  expectedStats.intelligence == mage.heroAttributes.intelligence + tempAttributes.intelligence);
        }

        //damage
        [Fact]
        public void MageHero_DamageWhenNoWeaponEquipped_ShouldReturnCorrectDamage()
        {
            Hero mage = new MageHero("Keman");
            double expectedDMG = 1;
            Assert.Equal(expectedDMG, mage.Damage());
        }
        [Fact]
        public void MageHero_DamageWhenEquippedOnlyWeapon_ShouldReturnCorrectDamage()
        {
            Hero mage = new MageHero("Keman");
            var overPoweredStaff = new Weapon("Pay To Win Staff", 1, 100, WeaponType.Staff);
            mage.Equip(overPoweredStaff);
            double expectedDMG = 108;
            Assert.Equal(expectedDMG, mage.Damage());
        }
        [Fact]
        public void MageHero_DamageWhenEquippedOnlyReplacedWeapon_ShouldReturnCorrectDamage()
        {
            Hero mage = new MageHero("Keman");
            var overPoweredStaff = new Weapon("Pay To Win Staff", 1, 100, WeaponType.Staff);
            var newWeakStaff = new Weapon("new Free To play Staff", 1, 1, WeaponType.Staff);
            mage.Equip(overPoweredStaff);
            mage.Equip(newWeakStaff);
            double expectedDMG = 1;
            Assert.Equal(expectedDMG, mage.Damage());
        }
        [Fact]
        public void MageHero_DamageWhenEquippedArmorAndWeapon_ShouldReturnCorrectDamage()
        {
            Hero mage = new MageHero("Keman");
            var overPoweredStaff = new Weapon("Pay To Win Staff", 1, 100, WeaponType.Staff);
            var overPoweredArmor = new Armor("Pay To Win Cloth", 1, Slot.Body, ArmorType.Cloth, new HeroAttributes(1, 1, 1000));
            mage.Equip(overPoweredStaff);
            mage.Equip(overPoweredArmor);
            double expectedDMG = 1108;
            Assert.Equal(expectedDMG, mage.Damage());
        }

        //Fancy display creation
        [Fact]
        public void MageHero_Display_ShouldPrintOutStatsAndEqupment()
        {
            Hero hero = new MageHero("Keman");
            var overPoweredStaff = new Weapon("Pay To Win Staff", 1, 100, WeaponType.Staff);
            hero.Equip(overPoweredStaff);
            StringBuilder displayString = new StringBuilder();
            displayString.AppendLine("----------------------------------------");
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Hero: Keman"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Class: Mage"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Level: 1"));
            displayString.AppendLine(hero.CreateCharacterDisplay("|--------------------------------------"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Attack: 100 (Staff)"));

            displayString.AppendLine(hero.CreateCharacterDisplay($"|                Stats"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| STR: 1 (+ 0)"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| DEX: 1 (+ 0)"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| INT: 8 (+ 0)"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"|                                "));

            displayString.AppendLine(hero.CreateCharacterDisplay($"|^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ "));
            displayString.AppendLine(hero.CreateCharacterDisplay($"|              Equipment"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Weapon: Pay To Win Staff"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Head: "));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Body: "));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Legs: "));
            displayString.AppendLine("----------------------------------------");
            displayString.AppendLine("101 -> 101");
            displayString.AppendLine("*Keman deals 101 magic damage!\n");
            string expectedString = displayString.ToString();
            string heroDisplay = hero.Display();

            Assert.Equal(heroDisplay, expectedString);
        }
    }
}
