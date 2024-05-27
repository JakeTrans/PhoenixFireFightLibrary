namespace FireFight.Functions
{
    public class DicerollFunctions
    {
        private readonly Random DiceRoll;

        /// <summary>
        /// Default contructor using GUID to enhance randomness
        /// </summary>
        public DicerollFunctions()
        {
            int Guid = Guidrandom();

            DiceRoll = new Random(Guid);
        }

        /// <summary>
        /// Gives access to dice rolling functions
        /// </summary>
        /// <param name="FakeRandom">
        /// set to true to use default seeded Random - false generates an GUID to enhance randomness
        /// </param>
        public DicerollFunctions(bool FakeRandom)
        {
            if (FakeRandom == true)
            {
                DiceRoll = new Random();
            }
            else
            {
                int Guid = Guidrandom();

                DiceRoll = new Random(Guid);
            }
        }

        public int D10()
        {
            return DiceRoll.Next(1, 10);
        }

        public int D100()
        {
            return Convert.ToInt32(D10().ToString() + D10().ToString());
        }

        public int D6()
        {
            return DiceRoll.Next(1, 7);
        }

        public int ThreeD6()
        {
            return D6() + D6() + D6();
        }

        public int D20()
        {
            return DiceRoll.Next(1, 20);
        }

        private Int16 Guidrandom()
        {
            byte[] gb = null;
            for (int i = 0; i < 20; i++)
            {
                Guid g = Guid.NewGuid();
                gb = g.ToByteArray();
            }
            return BitConverter.ToInt16(gb, 0);
        }
    }
}