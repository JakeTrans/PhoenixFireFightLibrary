using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFight.CharacterObjects
{
    public class Armour
    {
        // Add Presets

        public Armour()
        {
            HelmPF = 2;
            VisorPF = 2;
            BodyPF = 2;
            LimbsPF = 2;
            HelmWeight = 0;
            VisorWeight = 0;
            BodyWeight = 0;
            LimbsWeight = 0;
        }

        public Armour(ushort helmPF, ushort visorPF, ushort bodyPF, ushort limbsPF, decimal helmWeight, decimal visorWeight, decimal bodyWeight, decimal limbsWeight)
        {
            HelmPF = helmPF;
            VisorPF = visorPF;
            BodyPF = bodyPF;
            LimbsPF = limbsPF;
            HelmWeight = helmWeight;
            VisorWeight = visorWeight;
            BodyWeight = bodyWeight;
            LimbsWeight = limbsWeight;
        }

        public UInt16 HelmPF { get; set; }
        public UInt16 VisorPF { get; set; }
        public UInt16 BodyPF { get; set; }
        public UInt16 LimbsPF { get; set; }

        public decimal HelmWeight { get; set; }
        public decimal VisorWeight { get; set; }
        public decimal BodyWeight { get; set; }
        public decimal LimbsWeight { get; set; }
    }
}