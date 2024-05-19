using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFight.Functions
{
    public static class GeneralFunctions
    {
        public static bool IsNumeric(string ToCheck)
        {
            if (int.TryParse(ToCheck, out int myNum))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public enum CardinalDirectionsSquare
        {
            North = 1,
            NorthEast = 2,
            East = 3,
            SouthEast = 4,
            South = 5,
            SouthWest = 6,
            West = 7,
            NorthWest = 8
        }
    }
}