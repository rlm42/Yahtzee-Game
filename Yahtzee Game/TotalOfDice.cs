using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {
    [Serializable]
    class TotalOfDice : Combination {

        private int numberOfOneKind;

        public TotalOfDice(ScoreType scoreTotals, Label label) : base(label) {
            if (scoreTotals == ScoreType.Chance) {
                numberOfOneKind = 1;
            } else if (scoreTotals == ScoreType.ThreeOfAKind) {
                numberOfOneKind = 2;
            } else if (scoreTotals == ScoreType.FourOfAKind) {
                numberOfOneKind = 3;
            }

        }

        public override void CalculateScore(int[] faceValues) {
            Sort(faceValues); // Sort the values into ascending order
            int score = 0;
            // Check for 3 of a Kind (Either the first 3 numbers, middle 3, or last 3)
            if (((faceValues[0] == faceValues[1]) && (faceValues[0] == faceValues[2]))) {
                score = faceValues[0] * 3 + faceValues[3] + faceValues[4];
                Points = score;
            }
            if (((faceValues[2] == faceValues[3]) && (faceValues[2] == faceValues[4]))) {
                score = faceValues[2] * 3 + faceValues[0] + faceValues[1];
                Points = score;
            }
            if (((faceValues[1] == faceValues[2]) && (faceValues[2] == faceValues[3]))) {
                score = faceValues[1] * 3 + faceValues[0] + faceValues[4];
                Points = score;
            }
            // Check for 4 of a Kind (Either the first 4 numbers, or last 4)
            if (((faceValues[0] == faceValues[1]) && (faceValues[0] == faceValues[2]) && (faceValues[0] == faceValues[3]))) {
                score = faceValues[0] * 4 + faceValues[4];
                Points = score;
            }
            if (((faceValues[1] == faceValues[2]) && (faceValues[1] == faceValues[3]) && (faceValues[1] == faceValues[4]))) {
                score = faceValues[1] * 4 + faceValues[0];
                Points = score;
            }
            if (numberOfOneKind == 1) {
                score = faceValues[0] + faceValues[1] + faceValues[2] + faceValues[3] + faceValues[4];
                Points = score;
            }
            // Save the score into the point's property
            Points = score;
        }
    }
}
