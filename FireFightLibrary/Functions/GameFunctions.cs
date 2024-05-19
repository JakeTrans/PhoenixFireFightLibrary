using FireFight.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFight.Functions
{
    public static class GameFunctions
    {
        public enum PenetrationType
        {
            NoPenetration,
            LowVelPenetration,
            HighVelPenetration
        }

        public static uint RangeFinder(TrackableObject FromObject, TrackableObject ToObject, uint mapScale)
        {
            uint verticalDistance;
            if (FromObject.Ypos > ToObject.Ypos)
            {
                verticalDistance = FromObject.Ypos - ToObject.Ypos;
            }
            else
            {
                verticalDistance = ToObject.Ypos - FromObject.Ypos;
            }
            uint HorizonalDistance;
            if (FromObject.Xpos > ToObject.Xpos)
            {
                HorizonalDistance = FromObject.Xpos - ToObject.Xpos;
            }
            else
            {
                HorizonalDistance = ToObject.Xpos - FromObject.Xpos;
            }

            MathFunc math = new MathFunc();
            double pytha = math.Pythagoras(HorizonalDistance, verticalDistance);

            double truerange = pytha / mapScale;

            //return RoundUp(truerange);
            return Convert.ToUInt32(Math.Floor(truerange));
        }

        /// <summary>
        /// Force round up to int - there may be a better way but I don;t care right now
        /// </summary>
        /// <param name="numbertoRound"></param>
        /// <returns></returns>
        public static uint RoundUp(double numbertoRound)
        {
            string[] splitnumber = numbertoRound.ToString().Split('.');
            if (splitnumber.Length == 1)
            {
                return Convert.ToUInt32(splitnumber);
            }
            else if (Convert.ToUInt32(splitnumber[1].Substring(0, 8)) > 0)
            {
                return Convert.ToUInt32(splitnumber[0]) + 1;
            }
            else
            {
                return Convert.ToUInt32(splitnumber[1]) + 1;
            }
        }
    }
}