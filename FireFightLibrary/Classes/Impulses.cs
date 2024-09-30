using FireFight.CharacterObjects;
using FireFight.Classes;
using FireFight.Functions;
using System.Data;

namespace FireFightLibrary.Classes
{
    public class Impulse
    {
        public Character Character { get; set; }
        public ActionsPossible Action { get; set; }

        public Impulse(Character character, ActionsPossible action)
        {
            if (character == null)
            {
                throw new ArgumentNullException(nameof(character));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Character = character;
            Action = action;
        }
    }

    public class Impulses
    {
        public List<Impulse> ImpulseList1 { get; set; }
        public List<Impulse> ImpulseList2 { get; set; }
        public List<Impulse> ImpulseList3 { get; set; }
        public List<Impulse> ImpulseList4 { get; set; }

        public Impulses()
        {
            ImpulseList1 = new List<Impulse>();
            ImpulseList2 = new List<Impulse>();
            ImpulseList3 = new List<Impulse>();
            ImpulseList4 = new List<Impulse>();
        }

        public void SortImpulseListByINTSkill(List<Impulse> impulseList)
        {
            impulseList.Sort((x, y) => x.Character.INTSkillFactor.CompareTo(y.Character.INTSkillFactor));
        }

        public void SortAllImpulseListByINTSkill()
        {
            SortImpulseListByINTSkill(ImpulseList1);
            SortImpulseListByINTSkill(ImpulseList2);
            SortImpulseListByINTSkill(ImpulseList3);
            SortImpulseListByINTSkill(ImpulseList4);
        }

        public void AddActionsToImpulse(Character Char)
        {
            DBFunctions DBfunct = new DBFunctions();
            DataTable DT = DBfunct.RunSQLStatementDT(DBfunct.DataTableConnection, ("SELECT * From tblCombatActionsPerImpulse WHERE [Combat Actions] = " + Char.ActionsForTurn.ActionsTaken.Count()));
            // work though the numbers
            for (int i = 0; i < Char.ActionsForTurn.ActionsTaken.Count(); i++)
            {
                if (ImpulseList1.Count() < Convert.ToInt32(DT.Rows[0]["1"].ToString()))
                {
                    ImpulseList1.Add(new Impulse(Char, Char.ActionsForTurn.ActionsTaken[i]));
                }
                else if (ImpulseList2.Count() < Convert.ToInt32(DT.Rows[0]["2"].ToString()))
                {
                    ImpulseList2.Add(new Impulse(Char, Char.ActionsForTurn.ActionsTaken[i]));
                }
                else if (ImpulseList3.Count() < Convert.ToInt32(DT.Rows[0]["3"].ToString()))
                {
                    ImpulseList3.Add(new Impulse(Char, Char.ActionsForTurn.ActionsTaken[i]));
                }
                else if (ImpulseList4.Count() < Convert.ToInt32(DT.Rows[0]["4"].ToString()))
                {
                    ImpulseList4.Add(new Impulse(Char, Char.ActionsForTurn.ActionsTaken[i]));
                }
                else
                {
                    throw new Exception("Not enough Impulse Actions");
                }
            }
        }
    }
}