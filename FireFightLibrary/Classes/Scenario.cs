using FireFight.CharacterObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFight.Classes
{
    public class Scenario
    {
        public List<Character> Characters;

        public uint Scale { get; set; }

        public int IntScale
        {
            get { return Convert.ToInt32(Scale); }
        }

        /// <summary>
        /// Game Control Point
        /// </summary>
        /// <param name="characters">List of all character to spawn</param>
        public Scenario(List<Character> characters, uint scale)
        {
            Scale = scale;

            Characters = characters;

            characters[0].Selected = true;// select first character

            GetNextUnitWithAction();
        }

        public List<Character> RefreshNonFriendlylist()
        {
            List<Character> NonFriendly = Characters.Where(c => c.Sidereference != SelectedCharacter().Sidereference).ToList();
            return NonFriendly;
        }

        public Character SelectedCharacter()
        {
            if (Characters.Where(x => x.Selected == true).Count() == 0)
            {
                Characters[0].Selected = true;
            }
            Character selected = Characters.First(c => c.Selected == true);
            return selected;
        }

        public bool GetNextUnitWithAction()
        {
            SelectedCharacter().Selected = false;

            if (Characters.Where(x => x.Selected == false && x.turnTaken == false && x.KnockedOut == false).Count() == 0)
            {
                foreach (Character chara in Characters)
                {
                    chara.turnTaken = false;
                }
                return false;
            }

            Characters.First(c => c.Selected == false && c.turnTaken == false).Selected = true;

            if (DetectOneSideLeft())
            {
                //System.Windows.MessageBox.Show("Only your Side Left - you Win");
                Debug.Print("Only your Side Left - you Win");
            }
            return true;
        }

        public bool DetectOneSideLeft()
        {
            if (Characters.Where(x => x.Sidereference != SelectedCharacter().Sidereference && x.KnockedOut == false).Count() == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}