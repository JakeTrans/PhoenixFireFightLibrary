using PhoenixFireFight.Functions;

namespace PhoenixFireFight.Classes
{
    public abstract class TrackableObject
    {
        public bool Selected { get; set; }

        public Actions ActionsForTurn;

        public TrackableObject(uint xpos, uint ypos, uint sidereference)
        {
            Xpos = xpos;
            Ypos = ypos;
            Sidereference = sidereference;
            Facing = GeneralFunctions.CardinalDirectionsSquare.East;

            Selected = false;
            ActionsForTurn = new Actions();
        }

        public uint Xpos { get; set; }
        public uint Ypos { get; set; }
        public uint Sidereference { get; set; }  // 0 is terrain if needed

        public GeneralFunctions.CardinalDirectionsSquare Facing { get; set; }

        public uint GetRotation() // System  to assume all character models face north and rotation is clockwise
        {
            switch (Facing)
            {
                case GeneralFunctions.CardinalDirectionsSquare.North:
                    return 0;

                case GeneralFunctions.CardinalDirectionsSquare.NorthEast:
                    return 45;

                case GeneralFunctions.CardinalDirectionsSquare.East:
                    return 90;

                case GeneralFunctions.CardinalDirectionsSquare.SouthEast:
                    return 135;

                case GeneralFunctions.CardinalDirectionsSquare.South:
                    return 180;

                case GeneralFunctions.CardinalDirectionsSquare.SouthWest:
                    return 225;

                case GeneralFunctions.CardinalDirectionsSquare.West:
                    return 270;

                case GeneralFunctions.CardinalDirectionsSquare.NorthWest:
                    return 315;

                default:
                    throw new NotImplementedException("Direction not recogised--" + Facing.ToString());
            }
        }

        public float GetRadianRotation()
        {
            MathFunc mathobj = new MathFunc();
            return mathobj.GetRadianFromDegrees(GetRotation());
        }
    }
}