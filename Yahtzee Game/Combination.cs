using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {
    [Serializable]
    abstract class Combination : Score {

        public Combination(Label label) : base(label) {  //edited out Label[] label: base (label) for now, it's explained in Part D

        }

        public abstract void CalculateScore(int[] faceValues);

        /// <summary>
        /// Sort an int array into ascending order
        /// </summary>
        /// <param name="faceValues"></param>
        public void Sort(int[] faceValues) {
            Array.Sort(faceValues);
        }

    }
}

