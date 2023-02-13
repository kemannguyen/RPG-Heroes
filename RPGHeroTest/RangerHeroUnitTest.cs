using RPG_Heroes.Classes;
using RPG_Heroes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGHeroTest
{
    public class RangerHeroUnitTest
    {
        [Fact]
        public void RangerHero_NameAfterCreation_ShouldReturnName()
        {
            RangerHero ranger = new RangerHero("Legolas");
            Assert.Equal("Legolas", ranger.Name);
        }
        [Fact]
        public void RangerHero_ClassAfterCreation_ShouldReturnRanger()
        {
            RangerHero ranger = new RangerHero("Legolas");
            Assert.Equal("Ranger", ranger.heroClass);
        }
        [Fact]
        public void RangerHero_LevelWhenCreated_ShouldReturnLevelOne()
        {
            RangerHero ranger = new RangerHero("Legolas");
            Assert.Equal(1, ranger.Level);
        }
        [Fact]
        public void RangerHero_NoItemsWhenCreated_ShouldHaveNoEquipment()
        {
            RangerHero ranger = new RangerHero("Legolas");
            Assert.Empty(ranger.equipments);
        }
        [Fact]
        public void RangerHero_LevelUp_ShouldBeLevelTwoWithCorrectStats()
        {
            RangerHero ranger = new RangerHero("Legolas");
            ranger.LevelUp();
            HeroAttributes expectedStats = new HeroAttributes(2, 12, 2);
            Assert.True(ranger.Level == 2 &&
                expectedStats.strength == ranger.heroAttributes.strength &&
                expectedStats.dexterity == ranger.heroAttributes.dexterity &&
                expectedStats.intelligence == ranger.heroAttributes.intelligence);
        }
    }
}
