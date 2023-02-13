using RPG_Heroes.Items;
using RPG_Heroes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGHeroTest
{
    public class WeaponUnitTest
    {
        //Weapon
        [Fact]
        public void Weapon_SetWeaponNameWhenCreated_ShouldReturnSameName()
        {
            Weapon staff = new Weapon("staff", 1, 10, WeaponType.Staff);
            Assert.Equal("staff", staff.ItemName);
        }
        [Fact]
        public void Weapon_SetWeaponRequiredLevelWhenCreated_ShouldReturnSameLevel()
        {
            Weapon staff = new Weapon("staff", 1, 10, WeaponType.Staff);
            Assert.Equal(1, staff.RequiredLevel);
        }
        [Fact]
        public void Weapon_SetWeaponDamageWhenCreated_ShouldReturnSameDamage()
        {
            Weapon staff = new Weapon("staff", 1, 10, WeaponType.Staff);
            Assert.Equal(10, staff.returnItemStats()[0]);
        }
        [Fact]
        public void Weapon_WeaponSlotWhenCreated_ShouldReturnWeaponSlot()
        {
            Weapon staff = new Weapon("staff", 1, 10, WeaponType.Staff);
            Assert.Equal(Slot.Weapon, staff.slot);
        }
        [Fact]
        public void Weapon_SetWeaponTypeWhenCreated_ShouldReturnSameType()
        {
            Weapon staff = new Weapon("staff", 1, 10, WeaponType.Staff);
            Assert.Equal(WeaponType.Staff, staff.weaponType);
        }
    }
}
