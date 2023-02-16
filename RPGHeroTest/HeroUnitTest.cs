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
        //DisplayLine
        [Fact]
        public void Hero_DisplayLine_ShouldReturnStringWithCorrectEndLenghtAndClosingBox()
        {
            var mage = new MageHero("testman");
            string expectedString = "|                                      |";
            Assert.Equal(expectedString, mage.CreateCharacterDisplay("|"));
        }

    }
}