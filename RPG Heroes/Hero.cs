using RPG_Heroes.CustomExceptions;
using RPG_Heroes.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Heroes
{
    public abstract class Hero
    {
        public string heroClass { get; protected set; } = "No Class";
        public string Name { get; protected set; }
        public int Level { get; protected set; } = 1;
        public int[] levelAttributes { get; protected set; } = new int[] { 0, 0, 0 };
        public Dictionary<Slot, Item> equipments { get; protected set; } = new Dictionary<Slot, Item>();
        public HeroAttributes heroAttributes { get; protected set; } = new HeroAttributes(1, 1, 1);

        public WeaponType[] ValidWeaponsType { get; protected set; } = new WeaponType[0];
        public ArmorType[] ValidArmorType { get; protected set; } = new ArmorType[0]; 

        public double attackDamage { get; protected set; } = 1;

        public Hero(string name)
        {
            Name = name;
        }

        //functional methods
        public abstract void LevelUp();
        public void Equip(Item equipment)
        {
            //items hold a user verification
            if (equipment.Equipped(this))
            {
                if (equipments.ContainsKey(equipment.slot))
                {
                    Console.WriteLine($"  *replaced |{equipments[equipment.slot].ItemName}| with |{equipment.ItemName}|");
                    equipments[equipment.slot] = equipment;
                }
                else
                {
                    Console.WriteLine($"*{Name} equipped ¨{equipment.ItemName}¨");
                    equipments[equipment.slot] = equipment;
                }
            }
        }

        public abstract double Damage();
        protected abstract string DamageString();

        //Calculates the extra stats from items apart from the heroes own stats so that 
        //we later can show how much stats we gain from the items at the display
        public HeroAttributes TotalAttributes()
        {
            var tempAttributes = new HeroAttributes(0, 0, 0);
            double[] tempStats = new double[3] {0,0,0};
            foreach (var (slot, item) in equipments)
            {
                if (item != null)
                {

                    double[] itemStats = item.returnItemStats();
                    if (slot != Slot.Weapon)
                    {
                        for (int i = 0; i < itemStats.Length; i++)
                        {
                            tempStats[i] += itemStats[i];
                        }
                    }
                    else
                    {
                        attackDamage = itemStats[0];
                    }
                }
            }
            return tempAttributes = new HeroAttributes(tempStats[0], tempStats[1], tempStats[2]);
        }

        //Print out information about the Hero
        public string Display()
        {
            var tempAttributes = TotalAttributes();
            string[] showItems = new string[4];
            Slot[] slots = new Slot[] { Slot.Weapon, Slot.Head, Slot.Body, Slot.Legs };
            string weapontype = "";
            //saves the equipment name into the right display string
            for (int i = 0; i < showItems.Length; i++)
            {
                if (equipments.ContainsKey(slots[i]))
                {
                    if (i == 0)
                    {
                        var tempWeapon = (Weapon)equipments[slots[i]];
                        weapontype = "("+tempWeapon.weaponType.ToString()+")";
                    }
                    //need to cover this branch
                    showItems[i] = equipments[slots[i]].ItemName;
                }
                if (showItems[i] == null)
                {
                    showItems[i] = "";
                }
            }

            StringBuilder displayString = new StringBuilder();
            //draw out stats box in console 
            displayString.AppendLine("----------------------------------------");
            displayString.AppendLine(CreateCharacterDisplay($"| Hero: {Name}"));
            displayString.AppendLine(CreateCharacterDisplay($"| Class: {heroClass}"));
            displayString.AppendLine(CreateCharacterDisplay($"| Level: {Level}"));
            displayString.AppendLine(CreateCharacterDisplay("|--------------------------------------"));
            displayString.AppendLine(CreateCharacterDisplay($"| Attack: {attackDamage} {weapontype}"));

            displayString.AppendLine(CreateCharacterDisplay($"|                Stats"));
            displayString.AppendLine(CreateCharacterDisplay($"| STR: {heroAttributes.strength + tempAttributes.strength} (+ {tempAttributes.strength})"));
            displayString.AppendLine(CreateCharacterDisplay($"| DEX: {heroAttributes.dexterity + tempAttributes.dexterity} (+ {tempAttributes.dexterity})"));
            displayString.AppendLine(CreateCharacterDisplay($"| INT: {heroAttributes.intelligence + tempAttributes.intelligence} (+ {tempAttributes.intelligence})"));
            displayString.AppendLine(CreateCharacterDisplay($"|  "));

            displayString.AppendLine(CreateCharacterDisplay($"|^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ "));
            displayString.AppendLine(CreateCharacterDisplay($"|              Equipment"));
            displayString.AppendLine(CreateCharacterDisplay($"| Weapon: {showItems[0]}"));
            displayString.AppendLine(CreateCharacterDisplay($"| Head: {showItems[1]}"));
            displayString.AppendLine(CreateCharacterDisplay($"| Body: {showItems[2]}"));
            displayString.AppendLine(CreateCharacterDisplay($"| Legs: {showItems[3]}"));
            displayString.AppendLine("----------------------------------------");
            displayString.Append(DamageString());
            Console.WriteLine(displayString);

            return displayString.ToString();

        }

        // closes the box correctly
        public string CreateCharacterDisplay(string input)
        {
            int borderWidth = 40;
            int offset = 0;
            StringBuilder resultString = new StringBuilder();
            resultString.Append(input);
            if (input.Length <= borderWidth)
            {
                offset = borderWidth - input.Length;
            }
            for (int i = 0; i < offset; i++)
            {
                if (i < offset - 1)
                {
                    resultString.Append(" ");
                }
                else
                {
                    resultString.Append("|");
                }
            }

            return resultString.ToString();
        }

    }
}
