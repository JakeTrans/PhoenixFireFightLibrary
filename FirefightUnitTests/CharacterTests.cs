using FireFight.CharacterObjects;
using FireFight.Classes;
using FireFightLibrary.Classes;
using System.Diagnostics;

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
                Character Char = new Character(7, 0);
                Char.RangedWeapons.Add(new RangedWeapon(1, WeaponType.AssaultRifles));
                Assert.AreEqual(Char.RangedWeapons[0].Name, "AKM-47");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void ImpulseBasicTest()
        {
            try
            {
                Character Char = new Character(7, 0);

                Impulses impulsedata = new Impulses();

                Char.ActionsForTurn.ActionsTaken.Add(ActionsPossible.Aim);

                impulsedata.AddActionsToImpulse(Char);

                Debug.Print("List 1 Count: " + impulsedata.ImpulseList1.Count.ToString());
                Debug.Print("List 2 Count: " + impulsedata.ImpulseList2.Count.ToString());
                Debug.Print("List 3 Count: " + impulsedata.ImpulseList3.Count.ToString());
                Debug.Print("List 4 Count: " + impulsedata.ImpulseList4.Count.ToString());

                Assert.AreEqual(impulsedata.ImpulseList1.Count, 1);
                Assert.AreEqual(impulsedata.ImpulseList2.Count, 0);
                Assert.AreEqual(impulsedata.ImpulseList3.Count, 0);
                Assert.AreEqual(impulsedata.ImpulseList4.Count, 0);

                impulsedata.SortAllImpulseListByINTSkill();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void ImpulseAllOnesTest()
        {
            try
            {
                Character Char = new Character(7, 0);

                Impulses impulsedata = new Impulses();

                Char.ActionsForTurn.ActionsTaken.Add(ActionsPossible.Aim);
                Char.ActionsForTurn.ActionsTaken.Add(ActionsPossible.Aim);
                Char.ActionsForTurn.ActionsTaken.Add(ActionsPossible.Aim);
                Char.ActionsForTurn.ActionsTaken.Add(ActionsPossible.Aim);

                impulsedata.AddActionsToImpulse(Char);

                Debug.Print("List 1 Count: " + impulsedata.ImpulseList1.Count.ToString());
                Debug.Print("List 2 Count: " + impulsedata.ImpulseList2.Count.ToString());
                Debug.Print("List 3 Count: " + impulsedata.ImpulseList3.Count.ToString());
                Debug.Print("List 4 Count: " + impulsedata.ImpulseList4.Count.ToString());

                Assert.AreEqual(impulsedata.ImpulseList1.Count, 1);
                Assert.AreEqual(impulsedata.ImpulseList2.Count, 1);
                Assert.AreEqual(impulsedata.ImpulseList3.Count, 1);
                Assert.AreEqual(impulsedata.ImpulseList4.Count, 1);

                impulsedata.SortAllImpulseListByINTSkill();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void ImpulseMaxTest()
        {
            try
            {
                Character Char = new Character(7, 0);

                Impulses impulsedata = new Impulses();

                for (int i = 0; i < 21; i++)
                {
                    Char.ActionsForTurn.ActionsTaken.Add(ActionsPossible.Aim);
                }

                impulsedata.AddActionsToImpulse(Char);

                Debug.Print("List 1 Count: " + impulsedata.ImpulseList1.Count.ToString());
                Debug.Print("List 2 Count: " + impulsedata.ImpulseList2.Count.ToString());
                Debug.Print("List 3 Count: " + impulsedata.ImpulseList3.Count.ToString());
                Debug.Print("List 4 Count: " + impulsedata.ImpulseList4.Count.ToString());

                Assert.AreEqual(impulsedata.ImpulseList1.Count, 6);
                Assert.AreEqual(impulsedata.ImpulseList2.Count, 5);
                Assert.AreEqual(impulsedata.ImpulseList3.Count, 5);
                Assert.AreEqual(impulsedata.ImpulseList4.Count, 5);

                impulsedata.SortAllImpulseListByINTSkill();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}