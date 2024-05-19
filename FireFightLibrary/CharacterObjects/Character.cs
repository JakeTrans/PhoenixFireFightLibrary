using FireFight.Classes;
using FireFight.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFight.CharacterObjects
{
    public class Character : TrackableObject
    {
        public string Assettouse { get; set; }
        public string Name { get; set; }
        public int Agility { get; set; }
        public int BaseSpeed { get; set; }
        public int CombatAction { get; set; }
        public int GunCombatSkill { get; set; }
        public int Health { get; set; }
        public int Intelligence { get; set; }
        public int INTSkillFactor { get; set; }
        public int KnockoutValue { get; set; }
        public int MaxSpeed { get; set; }
        public int SkillAccuracyLevel { get; set; }
        public int Strength { get; set; }
        public int Will { get; set; }
        public int CurrentAimAmount { get; set; }
        public Character CurrentTarget { get; set; }
        public Armour ArmourWorn { get; set; }
        public uint PhysicalDamageTaken { get; set; }
        public bool KnockedOut { get; set; }

        public uint MapScale { get; set; }
        public bool turnTaken { get; set; }

        #region Constructors

        /// <summary>
        /// Constructor for new character generation
        /// </summary>
        /// <param name="guncombatskill"></param>
        public Character(int guncombatskill, uint MapScale) : base(0, 0, 0, "survivor-idle_rifle_0", 0)// since for generation scale does not matter
        {
            DicerollFunctions Dice = new DicerollFunctions();
            Strength = Dice.ThreeD6();
            Intelligence = Dice.ThreeD6();
            Will = Dice.ThreeD6();
            Health = Dice.ThreeD6();
            Agility = Dice.ThreeD6();
            GunCombatSkill = guncombatskill;
            ArmourWorn = new Armour();
            PhysicalDamageTaken = 0;
            KnockedOut = false;
            Recalulatestats();
            RangedWeapons = new List<RangedWeapon>();
        }

        /// <summary>
        /// Constructor for Fixed character
        /// </summary>
        /// <param name="StrengthValue">STR Stat</param>
        /// <param name="IntelligenceValue">INT Stat</param>
        /// <param name="WillValue">WIL Stat</param>
        /// <param name="HealthValue">HTL Stat</param>
        /// <param name="AgilityValue">AGL Stat</param>
        /// <param name="StartingXPosition">X Position</param>
        /// <param name="StartingYPosition">Y Position</param>
        /// <param name="sideReferenceNumber">Side Character is on</param>
        public Character(string name, int StrengthValue, int IntelligenceValue, int WillValue, int HealthValue, int AgilityValue, int GunCombatSkillValue, uint StartingXPosition, uint StartingYPosition, uint sideReferenceNumber, uint Scale) :
            base(StartingXPosition, StartingXPosition, sideReferenceNumber, "survivor-idle_rifle_0", Scale)
        {
            Name = name;
            DBFunctions DBfunct = new DBFunctions();
            Strength = StrengthValue;
            Intelligence = IntelligenceValue;
            Will = WillValue;
            Health = HealthValue;
            Agility = AgilityValue;
            PhysicalDamageTaken = 0;
            GunCombatSkill = GunCombatSkillValue;
            ArmourWorn = new Armour();
            KnockedOut = false;
            Recalulatestats();
            RangedWeapons = new List<RangedWeapon>();
            MapScale = Scale;
        }

        //public RangedWeapon PrimaryWeapon { get; set; }
        // list Modification

        public List<RangedWeapon> RangedWeapons;

        public RangedWeapon GetEquippedWeapon()
        {
            return RangedWeapons.Find(w => w.Equipped == true);
        }

        #endregion Constructors

        public uint Shockdamage { get; set; }

        private void Recalulatestats()
        {
            GetBaseSpeed();
            GetISF();
            GetMaxSpeed();
            GetSAL();
            GetKnockoutValue();
            GetCombatActions();
        }

        #region Calculated Fields

        private int GetEncumbrance()
        {
            int totalweight = 0;
            // add up all equipment
            return totalweight;
        }

        private void GetBaseSpeed()
        {
            // Approx encumbrance
            DBFunctions DBfunct = new DBFunctions();
            DataTable DT = DBfunct.RunSQLStatementDT(DBfunct.DataTableConnection, ("SELECT * From tblBaseSpeed where STR = " + Strength));

            string ColumnToUse = GetEncumbrance().ToString();

            ColumnToUse = DBfunct.GetNearestValue(DT, ColumnToUse);

            BaseSpeed = Convert.ToInt16(DT.Rows[0][(string)ColumnToUse]);
        }

        private void GetISF()
        {
            INTSkillFactor = Intelligence + SkillAccuracyLevel;
        }

        private void GetMaxSpeed()
        {
            DBFunctions DBfunct = new DBFunctions();
            DataTable DT = DBfunct.RunSQLStatementDT(DBfunct.DataTableConnection, ("SELECT [" + BaseSpeed + "] From tblMaximumSpeed where AGI = " + Agility));
            MaxSpeed = Convert.ToInt16(DT.Rows[0][0]);
        }

        private void GetSAL()
        {
            DBFunctions DBfunct = new DBFunctions();
            DataTable DT = DBfunct.RunSQLStatementDT(DBfunct.DataTableConnection, ("SELECT SAL From tblSkillAccuracy where [Skill Level] = " + GunCombatSkill));
            SkillAccuracyLevel = Convert.ToInt16(DT.Rows[0][0]);
        }

        private void GetKnockoutValue()
        {
            KnockoutValue = Convert.ToInt32(System.Math.Round((Will * 0.5) * GunCombatSkill));
        }

        private void GetCombatActions()
        {
            DBFunctions DBfunct = new DBFunctions();
            // issue here
            DataTable DT = DBfunct.RunSQLStatementDT(DBfunct.DataTableConnection, ("SELECT   * From tblCombatActions where MS = " + MaxSpeed));
            string ColumnToUse = INTSkillFactor.ToString();
            ColumnToUse = DBfunct.GetNearestValue(DT, ColumnToUse);
            CombatAction = Convert.ToInt16(DT.Rows[0][(string)ColumnToUse]);
        }

        #endregion Calculated Fields

        // Might move this to it's own class at some point
        public void Move(GeneralFunctions.CardinalDirectionsSquare DirectionToMove, uint MapScale)
        {
            switch (DirectionToMove)
            {
                case GeneralFunctions.CardinalDirectionsSquare.North:
                    // Movefunction(0, -10);
                    ActionsForTurn.ActionsTaken.Add(ActionsPossible.MoveN);
                    break;

                case GeneralFunctions.CardinalDirectionsSquare.NorthEast:
                    // Movefunction(10, -10);
                    ActionsForTurn.ActionsTaken.Add(ActionsPossible.MoveNE);
                    break;

                case GeneralFunctions.CardinalDirectionsSquare.East:
                    // Movefunction(10, 0);
                    ActionsForTurn.ActionsTaken.Add(ActionsPossible.MoveE);
                    break;

                case GeneralFunctions.CardinalDirectionsSquare.SouthEast:
                    // Movefunction(10, 10);
                    ActionsForTurn.ActionsTaken.Add(ActionsPossible.MoveSE);
                    break;

                case GeneralFunctions.CardinalDirectionsSquare.South:
                    // Movefunction(0, 10);
                    ActionsForTurn.ActionsTaken.Add(ActionsPossible.MoveS);
                    break;

                case GeneralFunctions.CardinalDirectionsSquare.SouthWest:
                    // Movefunction(-10, 10);
                    ActionsForTurn.ActionsTaken.Add(ActionsPossible.MoveSW);
                    break;

                case GeneralFunctions.CardinalDirectionsSquare.West:
                    // Movefunction(-10, 0);
                    ActionsForTurn.ActionsTaken.Add(ActionsPossible.MoveW);
                    break;

                case GeneralFunctions.CardinalDirectionsSquare.NorthWest:
                    // Movefunction(-10, -10);
                    ActionsForTurn.ActionsTaken.Add(ActionsPossible.MoveNW);
                    break;

                default:
                    throw new NotImplementedException();
            }

            CurrentAimAmount = 0;
        }

        public void Rotate(bool clockwork)
        {
            if (clockwork == true)
            {
                ActionsForTurn.ActionsTaken.Add(ActionsPossible.RotateClockwise);
            }
            else
            {
                ActionsForTurn.ActionsTaken.Add(ActionsPossible.RotateAntiClockWise);
            }
        }

        public void FireSingle()
        {
            ActionsForTurn.ActionsTaken.Add(ActionsPossible.FireSingle);
        }

        public void FireBurst()
        {
            ActionsForTurn.ActionsTaken.Add(ActionsPossible.FireBurst);
        }

        public void Aim()
        {
            ActionsForTurn.ActionsTaken.Add(ActionsPossible.Aim);
        }

        public void Reload()
        {
            ActionsForTurn.ActionsTaken.Add(ActionsPossible.Reload);
        }

        public void DoAllActions()
        {
            foreach (ActionsPossible act in ActionsForTurn.ActionsTaken)
            {
                DoAction(act);
            }
        }

        public void DoAction(ActionsPossible Actposs)
        {
            if (KnockedOut == true)
            {
                return;
            }

            switch (Actposs)
            {
                case ActionsPossible.MoveN:
                    Movefunction(0, -10);
                    break;

                case ActionsPossible.MoveNE:
                    Movefunction(10, -10);
                    break;

                case ActionsPossible.MoveE:
                    Movefunction(10, 0);
                    break;

                case ActionsPossible.MoveSE:
                    Movefunction(10, 10);
                    break;

                case ActionsPossible.MoveS:
                    Movefunction(0, 10);
                    break;

                case ActionsPossible.MoveSW:
                    Movefunction(-10, 10);
                    break;

                case ActionsPossible.MoveW:
                    Movefunction(-10, 0);
                    break;

                case ActionsPossible.MoveNW:
                    Movefunction(-10, -10);
                    break;

                case ActionsPossible.RotateClockwise:
                    Rotatefunction(true);
                    break;

                case ActionsPossible.RotateAntiClockWise:
                    Rotatefunction(false);
                    break;

                case ActionsPossible.Aim:
                    break;

                case ActionsPossible.Reload:
                    break;

                case ActionsPossible.FireSingle:
                    FireFunction();
                    break;
            }
        }

        private void Rotatefunction(bool Clockwise)
        {
            if (Clockwise == true)
            {
                if ((int)Facing == 8)
                {
                    Facing = (GeneralFunctions.CardinalDirectionsSquare)1;
                }
                else
                {
                    Facing = Facing + 1;
                }
            }
            else
            {
                if ((int)Facing == 1)
                {
                    Facing = (GeneralFunctions.CardinalDirectionsSquare)8;
                }
                else
                {
                    Facing = Facing - 1;
                }
            }
        }

        // Just to simplify code
        private void Movefunction(int ChangeinX, int ChangeinY)
        {
            Xpos = Convert.ToUInt32(Xpos + ChangeinX);
            Ypos = Convert.ToUInt32(Ypos + ChangeinY);
        }

        private void FireFunction()
        {
            FiringFunc firef = new FiringFunc();
            firef.FireSingle(this, CurrentAimAmount, 0, GetEquippedWeapon(), MapScale);
        }

        public bool KnockoutRoll()
        {
            DicerollFunctions Diceroll = new DicerollFunctions();
            int roll = Diceroll.D100();
            uint KnockOutDamage = Shockdamage + PhysicalDamageTaken;

            if (KnockOutDamage > (KnockoutValue * 3))
            {
                if (roll < 98)
                {
                    KnockedOut = true;
                }
            }
            else if (KnockOutDamage > (KnockoutValue * 2))
            {
                if (roll < 75)
                {
                    KnockedOut = true;
                }
            }
            else if (KnockOutDamage > (KnockoutValue))
            {
                if (roll < 25)
                {
                    KnockedOut = true;
                }
            }
            else if (KnockOutDamage > (KnockoutValue / 10))
            {
                if (roll < 10)
                {
                    KnockedOut = true;
                }
            }
            else
            {
                // no roll
            }

            Shockdamage = 0;
            return KnockedOut;
        }
    }
}