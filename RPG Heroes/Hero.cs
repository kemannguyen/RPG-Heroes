using RPG_Heroes.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Heroes
{
    public abstract class Hero
    {
        public string heroClass { get; protected set; }
        public string Name { get; protected set; }
        public int Level { get; protected set; }
        protected int[] levelAttributes { get; set; }
        public Dictionary<Slot, Item> equipments { get; protected set; }
        public HeroAttributes heroAttributes { get; protected set; }

        public WeaponType[] ValidWeaponsType { get; protected set; }
        public ArmorType[] ValidArmorType { get; protected set; }

        public double attackDamage { get; protected set; }

        public Hero(string name)
        {
            Name = name;
            Level = 1;
            attackDamage = 1;
            //since we only have 4 Slot enums equipments dictionary can only hold 4 items
            equipments = new Dictionary<Slot, Item>();
        }

        //functional methods
        public abstract void LevelUp();
        public void Equip(Item equipment)
        {
            //try
            //{
                //items hold a user verification
                if (equipment.Equipped(this))
                {
                    PutEquipmentIntoInventory(equipment);
                }
            //}
            //catch(InvalidWeaponException e) 
            //{
            //    Console.WriteLine(e.Message);
            //    Console.ResetColor();
            //}
            //catch(InvalidArmorException e)
            //{
            //    Console.WriteLine(e.Message);
            //    Console.ResetColor();
            //}
        }

        //equip items depending on their slot
        protected void PutEquipmentIntoInventory(Item item)
        {
            //0: Weap, 1: Head, 2: Body, 3: Legs
            if (item.slot == Slot.Weapon)
            {
                InsertItem(Slot.Weapon, item);
            }
            else if (item.slot == Slot.Head)
            {
                InsertItem(Slot.Head, item);
            }
            else if (item.slot == Slot.Body)
            {
                InsertItem(Slot.Body, item);
            }
            else
            {
                InsertItem(Slot.Legs, item);
            }
        }

        //equip the items and show text based on if its replacing an old item or not
        private void InsertItem(Slot itemSlot, Item item)
        {
            if (equipments.ContainsKey(itemSlot))
            {
                Console.WriteLine($"  *replaced |{equipments[itemSlot].ItemName}| with |{item.ItemName}|");
                equipments[itemSlot] = item;
            }
            else
            {

                Console.WriteLine($"*{Name} equipped ¨{item.ItemName}¨");
                equipments[itemSlot] = item;
            }
        }
        public abstract double Damage();

        //use item stats do calculate the new stats in tempAttributes and attackDmg
        public HeroAttributes TotalAttributes()
        {
            var tempAttributes = new HeroAttributes(0, 0, 0);
            double[] tempStats = new double[3];
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

            //saves the equipment name into the right display string
            for (int i = 0; i < showItems.Length; i++)
            {
                if (equipments.ContainsKey(slots[i]))
                {
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
            displayString.AppendLine(CreateCharacterDisplay($"| Attack: {attackDamage}"));

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
