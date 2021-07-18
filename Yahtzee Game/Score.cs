using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {
    [Serializable]
    abstract class Score {

        private int points;
        [NonSerialized]
        private Label label;
        protected bool done = false;

        public Score(Label label) {
            this.label = label;
        }

        /// <summary>
        /// Save the combination's points
        /// </summary>
        public int Points {
            get {
                return points;
            }
            set {
                points = value;
                done = true;
                label.Text = points.ToString();
            }
        }

        /// <summary>
        /// Check if ScoreType has been done
        /// </summary>
        public bool Done {
            get {
                return done;
            }

            set {
                done = value;
            }
        }

        /// <summary>
        /// Display the number of points on the associated label on the GUI
        /// </summary>
        public void ShowScore() {
            if (done) {
                label.Text = points.ToString();
            } else {
                label.Text = "";
            }
        }

        public void Load(Label label) {
            this.label = label;
        }

    }
}
