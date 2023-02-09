using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Heroes
{
    public class HeroAttributes
    {
        public double strength { get; set; }
        public double dexterity { get; set; }   
        public double intelligence { get; set; }    
        public HeroAttributes(double strenght, double dexterity, double intelligence)
        {
            this.strength = strenght;
            this.dexterity = dexterity;
            this.intelligence = intelligence;
        }

        //making sure the increases aren't decimals
        public void IncreaseStats(int str, int dex, int intelligence)
        {
            strength += str;
            dexterity+= dex;
            this.intelligence += intelligence;
        }
    }
}
