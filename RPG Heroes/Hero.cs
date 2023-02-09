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
        protected string heroClass { get; set; }
        public string Name { get; protected set; }
        public int Level { get; protected set; }
        protected int[] levelAttributes { get; set; }
        protected Dictionary<Slot, Item> equipments { get; set; }
        protected HeroAttributes heroAttributes { get; set; }
        protected HeroAttributes tempAttributes { get; set; }

        public WeaponType[] ValidWeaponsType { get; protected set; }
        public ArmorType[] ValidArmorType { get; protected set; }

        protected double attackDamage { get; set; }

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
            if (equipment.slot == Slot.Weapon)
            {
                if (equipment.Equipped(this))
                {
                    Console.WriteLine($"*{Name} equipped ¨{equipment.ItemName}¨");
                    PutEquipmentIntoInventory(equipment);
                }
            }
            else
            {
                if (equipment.Equipped(this))
                {
                    Console.WriteLine($"*{Name} equipped ¨{equipment.ItemName}¨");
                    PutEquipmentIntoInventory(equipment);
                }
            }
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

        //replace items with text if there already is an item
        private void InsertItem(Slot itemSlot, Item item)
        {
            if (equipments.ContainsKey(itemSlot))
            {
                Console.WriteLine($"  *replaced |{equipments[itemSlot].ItemName}| with |{item.ItemName}|");
                equipments[itemSlot] = item;
            }
            else
            {
                equipments[itemSlot] = item;
            }
        }
        public abstract void Damage();

        //use item stats do calculate the new stats in tempAttributes
        public void TotalAttributes()
        {
            tempAttributes = new HeroAttributes(0, 0, 0);
            double[] tempStats = new double[3];
            foreach (var (slot,item) in equipments)
            {
                if (item != null)
                {
                    double[] itemStats = item.returnItemStats();
                    if (slot != Slot.Weapon)
                    {
                        for(int i = 0; i<itemStats.Length; i++)
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
            tempAttributes = new HeroAttributes(tempStats[0], tempStats[1], tempStats[2]);
        }

        //Print out information about the Hero
        public void Display()
        {
            TotalAttributes();
            string[] showItems = new string[4];
            Slot[] slots= new Slot[]{ Slot.Weapon, Slot.Head, Slot.Body, Slot.Legs};
            
            //saves the equipment name into the right display string
            for (int i = 0; i < showItems.Length; i++)
            {
                if (equipments.ContainsKey(slots[i]))
                {
                    showItems[i] = equipments[slots[i]].ItemName;
                }
                if (showItems[i] == null)
                {
                    //place holder to show nothing in the display
                    showItems[i] = "";
                }
            }

            //draw out stats box in console 
            Console.WriteLine("----------------------------------------");
            CreateCharacterDisplay($"| Hero: {Name}");
            CreateCharacterDisplay($"| Class: {heroClass}");
            CreateCharacterDisplay($"| Level: {Level}");
            CreateCharacterDisplay("|--------------------------------------");
            CreateCharacterDisplay($"| Attack: {attackDamage}");

            CreateCharacterDisplay($"|             Stats");
            CreateCharacterDisplay($"| STR: {heroAttributes.strength + tempAttributes.strength} (+ {tempAttributes.strength})");
            CreateCharacterDisplay($"| DEX: {heroAttributes.dexterity + tempAttributes.dexterity} (+ {tempAttributes.dexterity})");
            CreateCharacterDisplay($"| INT: {heroAttributes.intelligence + tempAttributes.intelligence} (+ {tempAttributes.intelligence})");
            CreateCharacterDisplay($"|                                ");

            CreateCharacterDisplay($"|^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ^ ");
            CreateCharacterDisplay($"|           Equipment            ");
            CreateCharacterDisplay($"| Weapon: {showItems[0]}");
            CreateCharacterDisplay($"| Head: {showItems[1]}");
            CreateCharacterDisplay($"| Body: {showItems[2]}");
            CreateCharacterDisplay($"| Legs: {showItems[3]}");
            Console.WriteLine("----------------------------------------");
        }

        // closes the box correctly
        private void CreateCharacterDisplay(string input)
        {
            int borderWidth = 40;
            int offset = 0;
            StringBuilder border = new StringBuilder();
            border.Append(input);
            if (input.Length <= borderWidth)
            {
                offset = borderWidth - input.Length;
            }
            for (int i = 0; i < offset; i++)
            {
                if (i < offset - 1)
                {
                    border.Append(" ");
                }
                else
                {
                    border.Append("|");
                }

            }
            Console.WriteLine(border);
        }

    }
}
