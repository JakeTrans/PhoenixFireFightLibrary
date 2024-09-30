using FireFight.CharacterObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFightLibrary.Classes
{
    public class Impulse
    {
        public Character Character { get; set; }
        public Action Action { get; set; }

        public Impulse(Character character, Action action)
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
    }
}