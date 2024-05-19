using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFight.Classes
{
    public enum ActionsPossible
    {
        MoveN,
        MoveNE,
        MoveE,
        MoveSE,
        MoveS,
        MoveSW,
        MoveW,
        MoveNW,
        RotateClockwise,
        RotateAntiClockWise,
        Aim,
        Reload,
        FireSingle,
        FireBurst,
    }

    public class Actions
    {
        // holder for Action charater took
        public List<ActionsPossible> ActionsTaken;

        public Actions()
        {
            ActionsTaken = new List<ActionsPossible>();
        }
    }
}