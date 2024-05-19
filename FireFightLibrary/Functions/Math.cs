using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFight.Functions
{
    public class MathFunc
    {
        public double Pythagoras(double x, double y)
        {
            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }

        public float GetRadianFromDegrees(uint Degrees)
        {
            return (float)((Math.PI / 180) * Degrees);
        }
    }
}