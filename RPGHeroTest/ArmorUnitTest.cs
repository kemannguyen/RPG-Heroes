using RPG_Heroes.Items;
using RPG_Heroes;
using RPG_Heroes.CustomExceptions;
using RPG_Heroes.Classes;

namespace RPGHeroTest
{
    public class ArmorUnitTest
    {
        //Armor
        [Fact]
        public void Armor_SetWeaponNameWhenCreated_ShouldReturnSameName()
        {
            Armor plate = new Armor("plate", 1, Slot.Body, ArmorType.Plate, new HeroAttributes(1, 1, 1));
            Assert.Equal("plate", plate.ItemName);
        }
        [Fact]
        public void Armor_SetArmorRequiredLevelWhenCreate_ShouldReturnSameLevel()
        {
            Armor plate = new Armor("plate", 1, Slot.Body, ArmorType.Plate, new HeroAttributes(1, 1, 1));
            Assert.Equal(1, plate.RequiredLevel);
        }
        [Fact]
        public void Armor_SetArmorSlotWhenCreated_ShouldReturnSameSlot()
        {
            Armor plate = new Armor("plate", 1, Slot.Body, ArmorType.Plate, new HeroAttributes(1, 1, 1));
            Assert.Equal(Slot.Body, plate.slot);
        }
        [Fact]
        public void Armor_SetArmorTypeWhenCreated_ShouldReturnSameType()
        {
            Armor plate = new Armor("plate", 1, Slot.Body, ArmorType.Plate, new HeroAttributes(1, 1, 1));
            Assert.Equal(ArmorType.Plate, plate.armorType);
        }
        [Fact]
        public void Armor_SetArmorStatsWhenCreated_ShouldReturnSameStats()
        {
            Armor plate = new Armor("plate", 1, Slot.Body, ArmorType.Plate, new HeroAttributes(1, 1, 1));
            var expectedStats = new double[] { 1, 1, 1 };
            var actualStats = new double[] { plate.returnItemStats()[0], plate.returnItemStats()[1], plate.returnItemStats()[2] };
            Assert.Equal(expectedStats, actualStats);
        }
        [Fact]
        public void Armor_EquipArmorInWeaponSlot_ShouldReturnError()
        {
            var mage = new MageHero("bilbo");
            Armor plate = new Armor("plate", 1, Slot.Weapon, ArmorType.Plate, new HeroAttributes(1, 1, 1));
            var ex = Assert.Throws<InvalidArmorException>(() => mage.Equip(plate));
            Assert.Equal(ex.Message, $"InvalidWeaponException: {mage.Name} Couldn't equip |{plate.ItemName}| Error: can't equip armor in Weapon Slot");
        }
    }
}
