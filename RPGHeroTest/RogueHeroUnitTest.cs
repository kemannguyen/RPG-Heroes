using RPG_Heroes.Classes;
using RPG_Heroes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGHeroTest
{
    public class RogueHeroUnitTest
    {
        [Fact]
        public void RangerHero_NameAfterCreation_ShouldReturnName()
        {
            RogueHero ranger = new RogueHero("Legolas");
            Assert.Equal("Legolas", ranger.Name);
        }
        [Fact]
        public void RangerHero_ClassAfterCreation_ShouldReturnRanger()
        {
            RogueHero ranger = new RogueHero("Legolas");
            Assert.Equal("Ranger", ranger.heroClass);
        }
        [Fact]
        public void RangerHero_LevelWhenCreated_ShouldReturnLevelOne()
        {
            RogueHero ranger = new RogueHero("Legolas");
            Assert.Equal(1, ranger.Level);
        }
        [Fact]
        public void RangerHero_NoItemsWhenCreated_ShouldHaveNoEquipment()
        {
            RogueHero ranger = new RogueHero("Legolas");
            Assert.Empty(ranger.equipments);
        }
        [Fact]
        public void RangerHero_LevelUp_ShouldBeLevelTwoWithCorrectStats()
        {
            RogueHero ranger = new RogueHero("Legolas");
            ranger.LevelUp();
            HeroAttributes expectedStats = new HeroAttributes(3, 10, 2);
            Assert.True(ranger.Level == 2 &&
                expectedStats.strength == ranger.heroAttributes.strength &&
                expectedStats.dexterity == ranger.heroAttributes.dexterity &&
                expectedStats.intelligence == ranger.heroAttributes.intelligence);
        }
    }
}
