using System.Diagnostics;

namespace PhoenixFireFight.Classes
{
    public class DamageResult
    {
        private uint _DamageAmount;
        private string _HitLocation;
        private bool _Disabling;

        public bool Disabling
        {
            get { return _Disabling; }
            set { _Disabling = value; }
        }

        //
        public uint DamageAmount
        {
            get { return _DamageAmount; }
            set { _DamageAmount = value; }
        }

        public string HitLocation
        {
            get { return _HitLocation; }
            set { _HitLocation = value; }
        }

        public DamageResult(uint DamageAmount, string HitLocation, bool Disabling)
        {
            _DamageAmount = DamageAmount;
            _HitLocation = HitLocation;
            _Disabling = Disabling;
        }

        public DamageResult(uint DamageAmount, string HitLocation, string Disabling) // For DataTable
        {
            _DamageAmount = DamageAmount;
            _HitLocation = HitLocation;
            if (Disabling == "0")
            {
                _Disabling = false;
            }
            else
            {
                _Disabling = true;
            }
        }

        public Tuple<uint, string> DisplayDamage()
        {
            if (Disabling == true)
            {
                Debug.Print("Hit for " + DamageAmount + " in the " + HitLocation + "Hit was Disabling");
            }
            else
            {
                Debug.Print("Hit for " + DamageAmount + " in the " + HitLocation);
            }
            return Tuple.Create(DamageAmount, HitLocation);
        }
    }
}