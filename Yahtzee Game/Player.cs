using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {
    [Serializable]
    class Player {

        private string name;
        private int combinationsToDo = 13;
        private Score[] scores = new Score[19];   // 16 different ScoreTypes
        private int grandTotal;


        public Player(string name, Label[] label) {
            this.name = name;

            ScoreType combo;

            for (combo = ScoreType.Ones; combo <= ScoreType.GrandTotal; combo++) {
                switch (combo) {
                    case ScoreType.Ones:
                    case ScoreType.Twos:
                    case ScoreType.Threes:
                    case ScoreType.Fours:
                    case ScoreType.Fives:
                    case ScoreType.Sixes:
                        scores[(int)combo] = new CountingCombination(combo, label[(int)combo]);
                        break;

                    case ScoreType.ThreeOfAKind:
                    case ScoreType.FourOfAKind:
                    case ScoreType.Chance:
                        scores[(int)combo] = new TotalOfDice(combo, label[(int)combo]);
                        break;

                    case ScoreType.SmallStraight:
                    case ScoreType.LargeStraight:
                    case ScoreType.FullHouse:
                    case ScoreType.Yahtzee:
                        scores[(int)combo] = new FixedScore(combo, label[(int)combo]);
                        break;

                    case ScoreType.SubTotal:
                    case ScoreType.BonusFor63Plus:
                    case ScoreType.SectionATotal:
                    case ScoreType.SectionBTotal:
                    case ScoreType.YahtzeeBonus:
                    case ScoreType.GrandTotal:
                        scores[(int)combo] = new BonusOrTotal(label[(int)combo]);
                        break;
                }
            }
        }

        /// <summary>
        /// Get the player's name
        /// </summary>
        public string Name {
            get {
                return name;
            }
            set {
                name = value;
            }
        }



        public void ScoreCombination(ScoreType score, int[] faceValues) {

            var scorecombination = (Combination)scores[(int)score];
            scorecombination.CalculateScore(faceValues);

            scores[(int)ScoreType.SubTotal].Points = scores[(int)ScoreType.Ones].Points + scores[(int)ScoreType.Twos].Points + scores[(int)ScoreType.Threes].Points + scores[(int)ScoreType.Fours].Points + scores[(int)ScoreType.Fives].Points + scores[(int)ScoreType.Sixes].Points;
            if (scores[(int)ScoreType.SubTotal].Points >= 63) {
                scores[(int)ScoreType.BonusFor63Plus].Points = 35;
            } else {
                scores[(int)ScoreType.BonusFor63Plus].Points = 0;
            }
            scores[(int)ScoreType.SectionATotal].Points = scores[(int)ScoreType.SubTotal].Points + scores[(int)ScoreType.BonusFor63Plus].Points;

            //scores[(int)ScoreType.YahtzeeBonus].Points = 

            scores[(int)ScoreType.SectionBTotal].Points = scores[(int)ScoreType.ThreeOfAKind].Points + scores[(int)ScoreType.FourOfAKind].Points + scores[(int)ScoreType.FullHouse].Points + scores[(int)ScoreType.SmallStraight].Points + scores[(int)ScoreType.LargeStraight].Points + scores[(int)ScoreType.Chance].Points + scores[(int)ScoreType.Yahtzee].Points + scores[(int)ScoreType.YahtzeeBonus].Points;

            scores[(int)ScoreType.GrandTotal].Points = scores[(int)ScoreType.SectionATotal].Points + scores[(int)ScoreType.SectionBTotal].Points;

        }

        /// <summary>
        /// Return grand total value
        /// </summary>
        public int GrandTotal {
            get {
                return grandTotal;
            }
        }

        /// <summary>
        /// Check if button has already been attempted
        /// </summary>
        /// <param name="score"></param>
        /// <returns>True of false if the particular ScoreType has been done</returns>
        public bool IsAvailable(ScoreType score) {

            return !scores[(int)score].Done;
        }

        /// <summary>
        /// Display all the scores for this player on associated ScoreTotals labels
        /// </summary>
        public void ShowScores() {
            for (int i = 0; i <= 18; i++) {
                scores[i].ShowScore();
            }
        }

        /// <summary>
        /// Checks if player has attempted all of the combinations
        /// </summary>
        /// <returns></returns>
        public bool IsFinished() {
            if (combinationsToDo == 0) {
                return true;
            } else {
                combinationsToDo = combinationsToDo - 1;
                return false;
            }
        }


        public void Load(Label[] scoreTotals) {
            for (int i = 0; i < scores.Length; i++) {
                scores[i].Load(scoreTotals[i]);
            }
        }

    }

}

