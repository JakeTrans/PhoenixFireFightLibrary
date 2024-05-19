using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFight.Classes
{
    public class StandardRangebrackets
    {
        private readonly uint[] ranges = { 10, 20, 40, 70, 100, 200, 300, 400 };

        public string GetStatsfield(uint rangetotarget, uint AmmoType)
        {
            foreach (uint range in ranges)
            {
                if (rangetotarget < range)
                {
                    return "Ammo " + AmmoType.ToString() + " Range " + range;
                }
            }

            throw new NotImplementedException("Range " + ranges + "Not Found");
        }
    }
}