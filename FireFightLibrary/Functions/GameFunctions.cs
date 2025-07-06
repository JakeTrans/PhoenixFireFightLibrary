using PhoenixFireFight.Classes;

namespace PhoenixFireFight.Functions
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

        // Function to check if the target is within the arc

        public static bool IsTargetWithinArc(float sourceRotation, float targetBearing, float arcWidth)

        {
            // Normalize source rotation and target bearing to be within 0 to 360 degrees

            // sourceRotation = NormalizeAngle(sourceRotation);

            // targetBearing = NormalizeAngle(targetBearing);

            float perside = arcWidth / 2;

            //right-side

            float rightside = sourceRotation + perside;

            if (targetBearing >= sourceRotation && targetBearing <= rightside)
            {
                Console.WriteLine("Inbetween Right Side Arc 1");
                return true;
            }

            //left side plus
            if (rightside > 360)
            {
                float RightNormalized = rightside += 360;

                if (RightNormalized >= targetBearing)
                {
                    Console.WriteLine("Inbetween right Side Arc 2");
                    return true;
                }
            }

            //left-side
            float leftside = sourceRotation - perside;

            if (targetBearing <= sourceRotation && targetBearing >= leftside)
            {
                Console.WriteLine("Inbetween Left Side Arc 1");
                return true;
            }

            //left side minus
            if (leftside < 0)
            {
                float LeftNormalized = leftside += 360;

                if (LeftNormalized <= targetBearing)
                {
                    Console.WriteLine("Inbetween Left Side Arc 2");
                    return true;
                }
            }

            return false;
        }

        // Helper function to normalize angles to be within 0 to 360 degrees

        //private static float NormalizeAngle(float angle)

        //{
        //    angle = angle % 360;

        //    if (angle < 0)

        //    {
        //        angle += 360;
        //    }

        //    return angle;
        //}
    }
}