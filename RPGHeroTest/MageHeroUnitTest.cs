using RPG_Heroes.Classes;
using RPG_Heroes;

namespace RPGHeroTest
{
    public class MageHeroUnitTest
    {
        [Fact]
        public void MageHero_NameAfterCreation_ShouldReturnName()
        {
            MageHero mage = new MageHero("Wizard of Oz");
            Assert.Equal("Wizard of Oz", mage.Name);
        }
        [Fact]
        public void MageHero_ClassAfterCreation_ShouldReturnMage()
        {
            MageHero mage = new MageHero("Wizard of Oz");
            Assert.Equal("Mage", mage.heroClass);
        }
        [Fact]
        public void MageHero_LevelWhenCreated_ShouldReturnLevelOne()
        {
            MageHero mage = new MageHero("Wizard of Oz");
            Assert.Equal(1, mage.Level);
        }
        [Fact]
        public void MageHero_NoItemsWhenCreated_ShouldHaveNoEquipment()
        {
            MageHero mage = new MageHero("Wizard of Oz");
            Assert.Empty(mage.equipments);
        }
        [Fact]
        public void MageHero_LevelUp_ShouldBeLevelTwoWithCorrectStats()
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
    }
}
