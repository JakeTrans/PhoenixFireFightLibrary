using FireFight.CharacterObjects;
using FireFight.Classes;

namespace FirefightUnitTests
{
    [TestClass]
    public class CharacterTests
    {
        [TestMethod]
        public void BasicCreateCharacterTest()
        {
            try
            {
                Character Char1 = new Character(7, 0);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void CreateCharacterTestWithWeapons()
        {
            try
            {
                Character Char2 = new Character(7, 0);
                Char2.RangedWeapons.Add(new RangedWeapon(1, WeaponType.AssaultRifles));
                Assert.AreEqual(Char2.RangedWeapons[0].Name, "AKM-47");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}