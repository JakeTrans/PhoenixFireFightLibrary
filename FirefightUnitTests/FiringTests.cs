using FireFight.CharacterObjects;
using FireFight.Classes;
using static Azure.Core.HttpHeader;

namespace FirefightUnitTests
{
    [TestClass]
    public class FiringTests
    {
        [TestMethod]
        public void BasicFiringtest()
        {
            try
            {
                //Generate two characters Equal on opposite sides
                Character Char1 = new Character(7, 0);
                Char1.RangedWeapons.Add(new RangedWeapon(1, WeaponType.AssaultRifles));
                Assert.AreEqual(Char1.RangedWeapons[0].Name, "AKM-47");
                Char1.RangedWeapons[0].Equipped = true;
                Char1.MapScale = 1;

                Char1.Xpos = 0;
                Char1.Ypos = 0;

                Character Char2 = new Character(7, 0);
                Char2.RangedWeapons.Add(new RangedWeapon(1, WeaponType.AssaultRifles));
                Assert.AreEqual(Char2.RangedWeapons[0].Name, "AKM-47");
                Char2.RangedWeapons[0].Equipped = true;
                Char2.MapScale = 1;

                Char2.Xpos = 10;
                Char2.Ypos = 0;

                Char1.CurrentTarget = Char2;

                Char1.DoAction(ActionsPossible.FireSingle);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void BasicFiringHitTest()
        {
            try
            {
                //Generate two characters Equal on opposite sides
                Character Char1 = new Character(20, 0);
                Char1.RangedWeapons.Add(new RangedWeapon(1, WeaponType.AssaultRifles));
                Assert.AreEqual(Char1.RangedWeapons[0].Name, "AKM-47");
                Char1.RangedWeapons[0].Equipped = true;
                Char1.MapScale = 1;

                Char1.Xpos = 0;
                Char1.Ypos = 0;
                Char1.CurrentAimAmount = 20;

                Character Char2 = new Character(7, 0);
                Char2.RangedWeapons.Add(new RangedWeapon(1, WeaponType.AssaultRifles));
                Assert.AreEqual(Char2.RangedWeapons[0].Name, "AKM-47");
                Char2.RangedWeapons[0].Equipped = true;
                Char2.MapScale = 1;

                Char2.Xpos = 1;
                Char2.Ypos = 0;

                Char1.CurrentTarget = Char2;
                DamageResult damres;
                do
                {
                    damres = Char1.FireFunction();
                } while (damres == null);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void BasicFiringMissTest()
        {
            try
            {
                //Generate two characters Equal on opposite sides
                Character Char1 = new Character(1, 0);
                Char1.RangedWeapons.Add(new RangedWeapon(1, WeaponType.AssaultRifles));
                Assert.AreEqual(Char1.RangedWeapons[0].Name, "AKM-47");
                Char1.RangedWeapons[0].Equipped = true;
                Char1.MapScale = 1;

                Char1.Xpos = 0;
                Char1.Ypos = 0;

                Character Char2 = new Character(7, 0);
                Char2.RangedWeapons.Add(new RangedWeapon(1, WeaponType.AssaultRifles));
                Assert.AreEqual(Char2.RangedWeapons[0].Name, "AKM-47");
                Char2.RangedWeapons[0].Equipped = true;
                Char2.MapScale = 1;

                Char2.Xpos = 20;
                Char2.Ypos = 0;

                Char1.CurrentTarget = Char2;
                DamageResult damres;
                do
                {
                    damres = Char1.FireFunction();
                } while (damres != null);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}