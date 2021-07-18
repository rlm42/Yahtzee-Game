using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {
    [Serializable]
    class FixedScore : Combination {

        private ScoreType scoreType;

        public FixedScore(ScoreType scoreTotals, Label label) : base(label) {
            scoreType = scoreTotals;
        }

        public override void CalculateScore(int[] faceValues) {
            Sort(faceValues); // Sort the values into ascending order
            int score = 0;

            // Check for Full House (3 values the same and 2 values the same):
            if (((faceValues[0] == faceValues[1]) && (faceValues[1] == faceValues[2]) && (faceValues[3] == faceValues[4]))) {
                score = 25;
            }
            if (((faceValues[0] == faceValues[1]) && (faceValues[2] == faceValues[3]) && (faceValues[3] == faceValues[4]))) {
                score = 25;
            }
            // Check for Large Straight
            if (((faceValues[0] == 1) && (faceValues[1] == 2) && (faceValues[2] == 3) && (faceValues[3] == 4) && (faceValues[4] == 5)) || ((faceValues[0] == 2) && (faceValues[1] == 3) && (faceValues[2] == 4) && (faceValues[3] == 5) && (faceValues[4] == 6))) {
                score = 40;
            }
            // Check for Small Straight (6 different possible combinations)
            if ((((faceValues[0] == 1) && (faceValues[1] == 2) && (faceValues[2] == 3) && (faceValues[3] == 4)) || ((faceValues[1] == 1) && (faceValues[2] == 2) && (faceValues[3] == 3) && (faceValues[4] == 4)) || ((faceValues[0] == 2) && (faceValues[1] == 3) && (faceValues[2] == 4) && (faceValues[3] == 5)) || ((faceValues[1] == 2) && (faceValues[2] == 3) && (faceValues[3] == 4) && (faceValues[4] == 5)) || ((faceValues[0] == 3) && (faceValues[1] == 4) && (faceValues[2] == 5) && (faceValues[3] == 6)) || ((faceValues[1] == 3) && (faceValues[2] == 4) && (faceValues[3] == 5) && (faceValues[4] == 6)))) {
                score = 30;
            }
            // Check for Yahtzee (all the same)
            if (((faceValues[0] == faceValues[1]) && (faceValues[0] == faceValues[2]) && (faceValues[0] == faceValues[3]) && (faceValues[0] == faceValues[4]))) {
                score = 50;
            }

            // Save score into the point's property
            Points = score;
        }

    }

}
