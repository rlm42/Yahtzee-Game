using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;



namespace Yahtzee_Game {

    public enum ScoreType {
        Ones, Twos, Threes, Fours, Fives, Sixes,
        SubTotal, BonusFor63Plus, SectionATotal,
        ThreeOfAKind, FourOfAKind, FullHouse,
        SmallStraight, LargeStraight, Chance, Yahtzee,
        YahtzeeBonus, SectionBTotal, GrandTotal
    }

    [Serializable]
    class Game {

        private BindingList<Player> players = new BindingList<Player>();
        private int currentPlayerIndex = 0;
        private Player currentPlayer;
        private Die[] dice = new Die[5];
        private int playersFinished;
        private int numRolls = 0;
        [NonSerialized]
        private Form1 form;
        [NonSerialized]
        private Label[] dieLabels = new Label[5];
        public static string defaultPath = Environment.CurrentDirectory;
        private static string savedGameFile = defaultPath + "\\YahtzeeGame.dat";

        // Array of different message labels to cycle through
        private static string[] rollMessages = {"Roll 1",
                                                    "Roll 2 or choose a combination to score",
                                                    "Roll 3 or choose a combination to score",
                                                    "Choose a combination to score",
                                                    "Your turn has ended - Click OK"
                                                    };


        public Game(Form1 form) {

            this.form = form;


            players = new BindingList<Player>();

            for (int i = 1; i <= 2; i++) {
                players.Add(new Player("Player " + i, form.GetScoresTotals()));
            }

            currentPlayer = players[currentPlayerIndex];

            dieLabels = form.GetDice();

            for (int i = 0; i < 5; i++) {
                dice[i] = new Die(dieLabels[i]);
            }


            form.ShowPlayerName(currentPlayer.Name);
            form.ShowMessage(rollMessages[numRolls]);

        }


        /// <summary>
        /// Updates current player and currentPlayerIndex to be the next player's turn
        /// </summary>
        public void NextTurn() {

            form.DisableAllButtons(ScoreType.Ones);
            form.HideOKButton();
            form.DisableAndClearCheckBoxes();
            form.EnableRollButton();


            if (currentPlayerIndex == 1) {
                currentPlayerIndex = 0;
            } else {
                currentPlayerIndex = currentPlayerIndex + 1;
            }

            currentPlayer = players[currentPlayerIndex];
            form.ShowPlayerName(currentPlayer.Name);

            numRolls = 0;


            currentPlayer.ShowScores(); // After the current player is updated, show the score

        }

        /// <summary>
        /// Rolls all of the active dice
        /// </summary>
        public void RollDice() {

            // Count controlled loop
            for (int i = 0; i <= (int)ScoreType.Yahtzee; i++) {
                // Two way selection
                if (currentPlayer.IsAvailable((ScoreType)i)) {
                    form.EnableScoreButton((ScoreType)i);
                } else {
                    form.ShowMessage("Game has finished - Thanks for playing");
                } // Enables score buttons that haven't yet been used by player
            }

            if (numRolls < 3) {
                for (int i = 0; i < 5; i++) {
                    if (dice[i].Active == true) {
                        dice[i].Roll();

                    }
                }
                numRolls = numRolls + 1;
                form.ShowMessage(rollMessages[numRolls]);
            }
            if (numRolls == 3) {
                form.DisableRollButton();
                form.DisableAndClearCheckBoxes();
            }
        }

        /// <summary>
        /// Makes the die with the specified index (parameter) inactive
        /// </summary>
        /// <param name="number"></param>
        public void HoldDie(int number) {
            dice[number].Active = false;
        }

        /// <summary>
        /// Makes the die with the specified index (parameter) active
        /// </summary>
        /// <param name="number"></param>
        public void ReleaseDie(int number) {
            dice[number].Active = true;
        }

        /// <summary>
        /// Calculates the score for the specified scoring combination (the parameter) for current player
        /// </summary>
        /// <param name="score"></param>
        public void ScoreCombination(ScoreType score) {

            int[] diceValues = new int[dice.Length];
            for (int i = 0; i < dice.Length; i++) {
                diceValues[i] = dice[i].FaceValue;
            }
            currentPlayer.ScoreCombination(score, diceValues);
            form.ShowOKButton(); // Last action of this method, show OK button
        }

        /// <summary>
        /// Load a saved game from the default save game file
        /// </summary>
        /// <param name="form">The GUI form</param>
        /// <returns>The saved game</returns>
        public static Game Load(Form1 form) {
            Game game = null;
            if (File.Exists(savedGameFile)) {
                try {
                    Stream bStream = File.Open(savedGameFile, FileMode.Open);
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    game = (Game)bFormatter.Deserialize(bStream);
                    bStream.Close();
                    game.form = form;
                    game.ContinueGame();
                    return game;
                } catch {
                    MessageBox.Show("Error reading saved game file.\nCannot load saved game.");
                }
            } else {
                MessageBox.Show("No current saved game.");
            }
            return null;
        }


        /// <summary>
        /// Save the current game to the default save file
        /// </summary>
        public void Save() {
            try {
                Stream bStream = File.Open(savedGameFile, FileMode.Create);
                BinaryFormatter bFormatter = new BinaryFormatter();
                bFormatter.Serialize(bStream, this);
                bStream.Close();
                MessageBox.Show("Game saved");
            } catch (Exception e) {

                //   MessageBox.Show(e.ToString());
                MessageBox.Show("Error saving game.\nNo game saved.");
            }
        }


        /// <summary>
        /// Continue the game after loading a saved game
        /// 
        /// Assumes game was saved at the start of a player's turn before they had rolled dice.
        /// </summary>
        private void ContinueGame() {
            LoadLabels(form);
            for (int i = 0; i < dice.Length; i++) {

            }

            form.ShowPlayerName(currentPlayer.Name);
            form.EnableRollButton();
            form.EnableCheckBoxes();
            // Can replace string with whatever you used
            form.ShowMessage("Roll 1");
            currentPlayer.ShowScores();
        }//end ContinueGame

        /// <summary>
        /// Link the labels on the GUI form to the dice and players
        /// </summary>
        /// <param name="form"></param>
        private void LoadLabels(Form1 form) {
            Label[] diceLabels = form.GetDice();
            for (int i = 0; i < dice.Length; i++) {
                dice[i].Load(diceLabels[i]);
            }
            for (int i = 0; i < players.Count; i++) {
                players[i].Load(form.GetScoresTotals());
            }

        }

        /// <summary>
        /// Returns the current binding list of players
        /// </summary>
        public BindingList<Player> Players {
            get {
                return players;
            }
        }

    }

}
