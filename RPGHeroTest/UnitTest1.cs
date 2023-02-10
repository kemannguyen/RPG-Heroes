using RPG_Heroes;
using RPG_Heroes.Classes;
using RPG_Heroes.CustomExceptions;
using RPG_Heroes.Items;
using System.Reflection.Emit;
using System.Text;
using System.Xml.Linq;

namespace RPGHeroTest
{
    public class UnitTest1
    {
        [Fact]
        public void HeroAssign_ShouldReturnName()
        {
            MageHero mage = new MageHero("Wizard of Oz");
            Hero hero = mage;
            Assert.True(hero.Name == "Wizard of Oz");
        }
        [Fact]
        public void HeroLevelUp_ShouldBeLevelTwo()
        {
            MageHero mage = new MageHero("Wizard of Oz");
            Hero hero = mage;
            hero.LevelUp();
            HeroAttributes expectedStats = new HeroAttributes(2, 2, 13);
            Assert.True(hero.Level == 2 &&
                expectedStats.strength == hero.heroAttributes.strength &&
                expectedStats.dexterity == hero.heroAttributes.dexterity &&
                expectedStats.intelligence == hero.heroAttributes.intelligence);
        }
        [Fact]
        public void HeroEquip_EquipValidWeapom_ShouldBeHoldingWeaponInsideHero()
        {
            MageHero mage = new MageHero("Gandalf");
            Weapon staff = new Weapon("staff", 1, 10, WeaponType.Staff);
            MageHero hero = mage;
            hero.Equip(staff);
            Assert.Equal(hero.equipments[Slot.Weapon], staff);
        }

        [Fact]
        public void HeroEquip_EquipInvalidWeapon_ShouldReturnError()
        {
            MageHero mage = new MageHero("Harry Potter");
            Weapon dagger = new Weapon("dagger", 1, 10, WeaponType.Dagger);
            MageHero hero = mage;

            Assert.Throws<InvalidWeaponException>(() => hero.Equip(dagger));
        }

        [Fact]
        public void HeroEquip_EquipTooHighLevelWeapon_ShouldReturnError()
        {
            MageHero mage = new MageHero("Dumbeldore");
            Weapon stick = new Weapon("stick", 10, 10, WeaponType.Staff);
            MageHero hero = mage;

            Assert.Throws<InvalidWeaponException>(() => hero.Equip(stick));
        }
        [Fact]
        public void HeroEquip_EquipValidArmor_ShouldBeHoldingItemInsideHero()
        {
            MageHero mage = new MageHero("Gandalf");
            Armor robe = new Armor("robe", 1, Slot.Body, ArmorType.Cloth, new HeroAttributes(1, 1, 1));
            MageHero hero = mage;
            hero.Equip(robe);
            Assert.Equal(hero.equipments[Slot.Body], robe);
        }

        [Fact]
        public void HeroEquip_EquipInvalidArmor_ShouldReturnError()
        {
            MageHero mage = new MageHero("Harry Potter");
            Armor plate = new Armor("plate", 1, Slot.Body, ArmorType.Plate, new HeroAttributes(1, 1, 1));
            MageHero hero = mage;

            Assert.Throws<InvalidArmorException>(() => hero.Equip(plate));
        }

        [Fact]
        public void HeroEquip_EquipTooHighLevelArmor_ShouldReturnError()
        {
            MageHero mage = new MageHero("Dumbeldore");
            Armor robe = new Armor("robe", 10, Slot.Body, ArmorType.Cloth, new HeroAttributes(1, 1, 1));
            MageHero hero = mage;

            Assert.Throws<InvalidArmorException>(() => hero.Equip(robe));
        }

        [Fact]
        public void HeroDamage_ShouldReturnDamagedBasedOnStatedEquation()
        {
            var mage = new MageHero("Solomon");
            var staff = new Weapon("staff", 1, 10, WeaponType.Staff);
            Hero hero = mage;
            hero.Equip(staff);
            var tempAttributes = hero.TotalAttributes();
            double expectedDMG = Math.Round(hero.attackDamage * (1 + (hero.heroAttributes.intelligence + tempAttributes.intelligence) / 100));
            Assert.Equal(hero.Damage(), expectedDMG);
        }

        [Fact]
        public void HeroTotalAttributes_SumOfHeroStatAndItemStat_ShouldReturnHeroTotalStats()
        {
            MageHero mage = new MageHero("Dr strange");
            Armor smartWatch = new Armor("smart watch", 1, Slot.Body, ArmorType.Cloth, new HeroAttributes(0, 0, 10));
            MageHero hero = mage;
            hero.Equip(smartWatch);
            var tempAttributes = hero.TotalAttributes();
            HeroAttributes expectedStats = new HeroAttributes(1, 1, 18);
            HeroAttributes heroTotalStats = new HeroAttributes(hero.heroAttributes.strength + tempAttributes.strength, hero.heroAttributes.dexterity + tempAttributes.dexterity, hero.heroAttributes.intelligence + tempAttributes.intelligence);
            Assert.True(heroTotalStats.strength == expectedStats.strength && heroTotalStats.dexterity == expectedStats.dexterity && heroTotalStats.intelligence == expectedStats.intelligence);
        }

        [Fact]
        public void DisplayTest()
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

            displayString.AppendLine(hero.CreateCharacterDisplay($"|             Stats"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| STR: {hero.heroAttributes.strength + tempAttributes.strength} (+ {tempAttributes.strength})"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| DEX: {hero.heroAttributes.dexterity + tempAttributes.dexterity} (+ {tempAttributes.dexterity})"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| INT: {hero.heroAttributes.intelligence + tempAttributes.intelligence} (+ {tempAttributes.intelligence})"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"|                                "));

            displayString.AppendLine(hero.CreateCharacterDisplay($"|^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ "));
            displayString.AppendLine(hero.CreateCharacterDisplay($"|           Equipment            "));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Weapon: {showItems[0]}"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Head: {showItems[1]}"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Body: {showItems[2]}"));
            displayString.AppendLine(hero.CreateCharacterDisplay($"| Legs: {showItems[3]}"));
            displayString.AppendLine("----------------------------------------");
            string expectedString = displayString.ToString();
            string heroDisplay = hero.Display();
            Assert.True(heroDisplay == expectedString);
        }
    }
}