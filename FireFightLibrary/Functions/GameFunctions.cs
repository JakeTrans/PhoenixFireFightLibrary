using FireFight.Classes;

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
        /// Calculates the bearing between two points
        /// </summary>
        /// <param name="x1">Orginal point X coordinate</param>
        /// <param name="y1">Orginal point Y coordinate</param>
        /// <param name="x2">Target point X coordinate</param>
        /// <param name="y2">Target point X coordinate</param>
        /// <returns>Bearing in degrees</returns>
        public static double CalculateBearing(int x1, int y1, int x2, int y2)

        {
            // Calculate the difference in coordinates

            int deltaX = x2 - x1;

            int deltaY = y2 - y1;

            // Calculate the bearing in radians

            double bearingRad = Math.Atan2(deltaX, -deltaY); // Note the inversion of deltaY to adjust for screen coordinate system

            // Convert the bearing to degrees

            double bearingDeg = RadiansToDegrees(bearingRad);

            // Normalize the bearing to 0-360 degrees

            return (bearingDeg + 360) % 360;
        }

        private static double RadiansToDegrees(double radians)

        {
            return radians * 180 / Math.PI;
        }
    }

    // Example usage

    internal class Program

    {
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