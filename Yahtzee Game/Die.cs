using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Yahtzee_Game {
    [Serializable]
    class Die {

        private int faceValue;
        private bool active = true;
        [NonSerialized]
        private Label label;
        private static Random random = new Random();
        //private StreamReader rollFile;
        private static string rollFileName = Game.defaultPath + "\\basictestrolls.txt";
        [NonSerialized]
        private static StreamReader rollFile = new StreamReader(rollFileName);
        private static bool DEBUG;


        public Die(Label label) {
            this.label = label;
            faceValue = 1;
        }

        public int FaceValue {
            get {
                return faceValue;
            }
        }

        public bool Active {
            get {
                return active;
            }

            set {
                active = value;
            }

        }

        public void Roll() {
            //faceValue = random.Next(6) + 1;
            //label.Text = faceValue.ToString();
            if (!DEBUG) {
                // your original code is here
                faceValue = random.Next(6) + 1;
                label.Text = faceValue.ToString();
            } else {
                faceValue = int.Parse(rollFile.ReadLine());
                faceValue = random.Next(6) + 1;
                label.Text = faceValue.ToString();
                label.Refresh();
            }
        }

        public void Load(Label label) {
            this.label = label;
            if (faceValue == 0) {
                label.Text = string.Empty;
            } else {
                label.Text = faceValue.ToString();
            }
        }

    }
}
