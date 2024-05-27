using FireFight.CharacterObjects;
using FireFight.Functions;
using System.Data;
using System.Diagnostics;
using static FireFight.Functions.GameFunctions;

namespace FireFight.Classes
{
    internal class Firing
    {
        public class FiringFunc
        {
            public DamageResult FireSingle(Character Shooter, int AimActions, int Modifiers, RangedWeapon WeaponUsed, uint MapScale)
            {
                // Real Target Version

                // get bonus from Aim Actions
                uint Range = GameFunctions.RangeFinder(Shooter, Shooter.CurrentTarget, MapScale);

                DBFunctions DBfunct = new DBFunctions();

                // go through each aim column looking for the result
                int AimMod = this.GetAimModifier(Shooter, DBfunct, AimActions);

                // range
                DataTable DTRange = DBfunct.RunSQLStatementDT(DBfunct.DataTableConnection, ("SELECT TOP 1 * FROM tblRange WHERE range <= " + Range + " ORDER BY Range DESC"));
                // stance target aim to form part of modifiers

                int Rangemodifier = Convert.ToInt32(DTRange.Rows[0]["ALM"].ToString());

                int FinalEAL = AimMod + Rangemodifier + Shooter.SkillAccuracyLevel;
                if (FinalEAL > 21)
                {
                    FinalEAL = 21;
                }

                DataTable OddstoHit = DBfunct.RunSQLStatementDT(DBfunct.DataTableConnection, ("SELECT * FROM tblOddsofHitting WHERE EAL = " + FinalEAL));

                int ToHit = Convert.ToInt32(OddstoHit.Rows[0]["Single Shot"].ToString());

                WeaponUsed.CurrentAmmo -= 1;

                DicerollFunctions dice = new DicerollFunctions();
                int res = dice.D100();

                if (res < ToHit)
                {
                    StandardRangebrackets Ranges = new StandardRangebrackets();
                    string rangefield = Ranges.GetStatsfield(Range, 1);
                    DataTable DTDamageStats = DBfunct.RunSQLStatementDT(DBfunct.ArmouryConnection, ("SELECT TOP 1 [" + rangefield + " Pen], [" + rangefield + " DC] " + "FROM " + WeaponUsed.GetStatTableName() + " WHERE ID = " + WeaponUsed.ID));
                    DamageResult damres = GetDamageresult(DBfunct, Shooter.CurrentTarget, false, Convert.ToInt32(DTDamageStats.Rows[0][0].ToString()), Convert.ToInt32(DTDamageStats.Rows[0][1].ToString()));

                    damres.DisplayDamage();

                    Shooter.CurrentTarget.KnockoutRoll();

                    return damres;
                }
                else
                {
                    Debug.Print("Missed");
                    return null;
                }
            }

            public int GetAimModifier(Character Shooter, DBFunctions DBFunct, int AimActions)
            {
                DataTable WeaponData = DBFunct.RunSQLStatementDT(DBFunct.ArmouryConnection, ("SELECT TOP 1 * FROM " + Shooter.GetEquippedWeapon().GetStatTableName() + " WHERE ID = " + Shooter.GetEquippedWeapon().ID));
                int CurrentAimColumn = 12;
                for (int AIM = CurrentAimColumn; AIM > 0; AIM--)
                {
                    // System.Diagnostics.Debug.Print(AIM.ToString());
                    if (WeaponData.Rows[0]["Aim Time Value " + AIM].ToString() != "")
                    {
                        if (Convert.ToInt32(WeaponData.Rows[0]["Aim Time Modifier " + AIM].ToString()) < AimActions)
                        {
                            return Convert.ToInt32(WeaponData.Rows[0]["Aim Time Value " + AIM].ToString());
                        }
                    }
                }
                return Convert.ToInt32(WeaponData.Rows[0]["Aim Time Value 1"].ToString());
            }

            public DamageResult GetDamageresult(DBFunctions DBFunct, Character Target, bool Cover, int WeaponPen, int DC)
            {
                DicerollFunctions dice = new DicerollFunctions();
                int Hitlocation = dice.D100();
                DataTable DamageLocation;

                // get location column
                if (Cover == true)
                {
                    DamageLocation = DBFunct.RunSQLStatementDT(DBFunct.DataTableConnection, ("SELECT * FROM tblHitLocationandDamageinCover WHERE [Location Number Cover] = " + Hitlocation));
                }
                else
                {
                    DamageLocation = DBFunct.RunSQLStatementDT(DBFunct.DataTableConnection, ("SELECT * FROM tblHitLocationandDamageinOpen WHERE [Location Number Open] = " + Hitlocation));
                }

                // get Armour -- possibly farm out to function or enum
                int ArmourValue;
                switch (DamageLocation.Rows[0]["ArmourLocation"].ToString())
                {
                    case "Helm":
                        ArmourValue = Target.ArmourWorn.HelmPF;
                        break;

                    case "Visor":
                        ArmourValue = Target.ArmourWorn.VisorPF;
                        break;

                    case "Body":
                        ArmourValue = Target.ArmourWorn.BodyPF;
                        break;

                    case "Limbs":
                        ArmourValue = Target.ArmourWorn.LimbsPF;
                        break;

                    case "Weapon":
                        // do weapon hits later
                        ArmourValue = Target.ArmourWorn.BodyPF;
                        break;

                    default:
                        throw new NotImplementedException("Hot Location " + DamageLocation.Rows[0]["ArmourLocation"].ToString() + " Not Found");
                }

                // get weapon stats
                string Hitloc = DamageLocation.Rows[0]["Hit location"].ToString();
                int Epen = GetEpen(DBFunct, ArmourValue, WeaponPen);
                int EAPF = GetEAPF(DBFunct, ArmourValue);
                PenetrationType pentype = GetPenType(Epen, EAPF);

                DamageResult Result;
                switch (pentype)
                {
                    case PenetrationType.NoPenetration:
                        // No Pen mean no Damage
                        Result = new DamageResult(0, Hitloc, false);
                        break;

                    case PenetrationType.LowVelPenetration:
                        // damage with DC 1
                        Result = this.GetDamagevalue(1, Epen, DamageLocation);
                        break;

                    case PenetrationType.HighVelPenetration:
                        // damage with full DC
                        Result = this.GetDamagevalue(DC, Epen, DamageLocation);
                        break;

                    default:
                        throw new NotImplementedException();
                }

                // add damage to target
                Target.PhysicalDamageTaken += Result.DamageAmount;
                if (Result.Disabling == true)
                {
                    // shock damage
                    if (Result.HitLocation.Contains("Neck") == true)
                    {
                        Target.Shockdamage += 400;
                    }
                    else if (Result.HitLocation.Contains("Shoulder") == true)
                    {
                        Target.Shockdamage += 10;
                    }
                    else if (Result.HitLocation.Contains("Arm") == true)
                    {
                        Target.Shockdamage += 20;
                    }
                    else if (Result.HitLocation.Contains("Hand") == true)
                    {
                        Target.Shockdamage += 10;
                    }
                    else if (Result.HitLocation.Contains("Spine") == true)
                    {
                        Target.Shockdamage += 400;
                    }
                    else if (Result.HitLocation.Contains("Thigh") == true)
                    {
                        Target.Shockdamage += 80;
                    }
                    else if (Result.HitLocation.Contains("Knee") == true)
                    {
                        Target.Shockdamage += 50;
                    }
                    else if (Result.HitLocation.Contains("Ankle") == true)
                    {
                        Target.Shockdamage += 20;
                    }
                }

                return Result;
            }

            public int GetEpen(DBFunctions DBFunct, int ArmorPF, int WeaponPen)
            {
                int EAPF = GetEAPF(DBFunct, ArmorPF);

                int EPEN = WeaponPen - EAPF;

                return EPEN;
            }

            private static int GetEAPF(DBFunctions DBFunct, int ArmorPF)
            {
                DicerollFunctions diceroll = new DicerollFunctions();

                int GlancingRoll = diceroll.D10();
                if (GlancingRoll == 10) // Correcting as the lowest on this is zero not
                {
                    GlancingRoll = 0;
                }

                DataTable EffectiveArmorPF = DBFunct.RunSQLStatementDT(DBFunct.DataTableConnection, ("SELECT TOP 1 [Glancing " + GlancingRoll + "] FROM tblGlancingTable WHERE [Armour PF] = " + ArmorPF));

                int EAPF = Convert.ToInt32(EffectiveArmorPF.Rows[0][0].ToString());
                return EAPF;
            }

            public DamageResult GetDamagevalue(int DC, int Epen, DataTable Hitrow)
            {
                // get columns
                List<string> EPENS = (from dc in Hitrow.Columns.Cast<DataColumn>()
                                      where dc.ColumnName.Contains("DC " + DC + " ") == true & dc.ColumnName.Contains("Disabling") == false
                                      select dc.ColumnName).ToList();

                // reverse to highest first
                EPENS.Reverse();

                string EPENtouse = "0";
                foreach (string EPENcolumn in EPENS)
                {
                    decimal Epenvalue = Convert.ToDecimal(EPENcolumn.Replace("DC " + DC + " EPEN ", "").Replace("_", "."));

                    if (Epenvalue <= Epen)
                    {
                        EPENtouse = EPENcolumn;
                        break;
                    }
                }

                DamageResult damres = new DamageResult(Convert.ToUInt32(Hitrow.Rows[0][EPENtouse].ToString()), Hitrow.Rows[0]["Hit location"].ToString(), Hitrow.Rows[0][EPENtouse + " Disabling"].ToString());

                return damres;
            }

            public PenetrationType GetPenType(int EPEN, int EAPF)
            {
                if (EPEN <= 0)
                {
                    return PenetrationType.NoPenetration;
                }
                else if (EPEN < EAPF)
                {
                    return PenetrationType.LowVelPenetration;
                }
                else
                {
                    return PenetrationType.HighVelPenetration;
                }
            }
        }
    }
}