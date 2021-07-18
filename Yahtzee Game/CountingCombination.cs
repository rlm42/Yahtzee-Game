using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {
    [Serializable]
    class CountingCombination : Combination {

        private int dieValue;

        public CountingCombination(ScoreType scoreTotals, Label label) : base(label) {

            dieValue = (int)scoreTotals + 1;
        }

        public override void CalculateScore(int[] faceValues) {

            int numMatches = 0;
            int score;

            for (int i = 0; i < 5; i++) {
                if (faceValues[i] == dieValue) {
                    numMatches = numMatches + 1;
                }
                score = numMatches * dieValue;
                Points = score;
            }
        }
    }
}
