namespace PhoenixFireFight.Classes
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