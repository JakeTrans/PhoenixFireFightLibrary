using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFight.Classes
{
    public class TerrainObject : TrackableObject
    {
        public bool BlockMovement { get; set; }
        public bool BlockSight { get; set; }
        public uint MovePenalty { get; set; }

        public TerrainObject(uint xpos, uint ypos, string Assetname) : base(xpos, ypos, 0)
        {
        }
    }
}