using FireFight.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFight.Classes
{
    public enum WeaponType
    {
        AssaultRifles
    };

    public class RangedWeapon
    {
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set
            {
                _ID = value;

                if (value != ID)
                {
                    ID = value;
                }
            }
        }

        public string Name { get; set; }

        public decimal Weight { get; set; }

        public string Length { get; set; }

        public int CilpCapacity { get; set; }

        public int CurrentAmmo { get; set; }

        public bool Equipped { get; set; }

        public WeaponType Catagory { get; set; }

        public RangedWeapon(int WeaponID, WeaponType RangedWeaponType)
        {
            ID = WeaponID;

            DBFunctions DBfunct = new DBFunctions();
            DataTable DT = DBfunct.RunSQLStatementDT(DBfunct.ArmouryConnection, ("SELECT * From " + GetStatTableName() + " where ID = " + ID));

            CilpCapacity = Convert.ToInt32(DT.Rows[0]["Ammo Capacity"].ToString());
            CurrentAmmo = CilpCapacity;
            Name = DT.Rows[0]["Name"].ToString();
            Weight = Convert.ToInt32(DT.Rows[0]["Weight"].ToString());
            Length = DT.Rows[0]["Length"].ToString();
        }

        public string GetStatTableName()
        {
            switch (Catagory)
            {
                case WeaponType.AssaultRifles:
                    return "vw_GetARWeaponData";

                default:
                    throw new NotSupportedException();
            }
        }
    }
}