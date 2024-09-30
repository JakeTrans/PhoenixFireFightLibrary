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

        public static void SortImpulseListByINTSkill(List<Impulse> impulseList)
        {
            impulseList.Sort((x, y) => x.Character.INTSkillFactor.CompareTo(y.Character.INTSkillFactor));
        }

        public void AddActionsToImpulse(Character Char)
        {
            DBFunctions DBfunct = new DBFunctions();
            DataTable DT = DBfunct.RunSQLStatementDT(DBfunct.DataTableConnection, ("SELECT * From tblCombatActionsPerImpulse WHERE [Combat Actions] = " + Char.CombatAction));
            // work though the numbers
            for (int i = 0; i < Char.ActionsForTurn.ActionsTaken.Count(); i++)
            {
                if (i + 1 <= Convert.ToInt32(DT.Columns["1"].ToString()))
                {
                    ImpulseList1.Add(new Impulse(Char, Char.ActionsForTurn.ActionsTaken[i]));
                }
                else if ((i + 1) + Convert.ToInt32(DT.Columns["1"].ToString()) <= Convert.ToInt32(DT.Columns["2"].ToString()))
                {
                    ImpulseList2.Add(new Impulse(Char, Char.ActionsForTurn.ActionsTaken[i]));
                }
                else if ((i + 1) + Convert.ToInt32(DT.Columns["1"].ToString()) + Convert.ToInt32(DT.Columns["2"].ToString()) <= Convert.ToInt32(DT.Columns["3"].ToString()))
                {
                    ImpulseList3.Add(new Impulse(Char, Char.ActionsForTurn.ActionsTaken[i]));
                }
                else if ((i + 1) + Convert.ToInt32(DT.Columns["1"].ToString()) + Convert.ToInt32(DT.Columns["2"].ToString()) + Convert.ToInt32(DT.Columns["2"].ToString()) <= Convert.ToInt32(DT.Columns["4"].ToString()))
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