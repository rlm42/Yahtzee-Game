using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {

    public partial class Form1 : Form {

        public Form1() {

            // Initialise Game
            InitializeComponent();
            InitalizeLabelsAndButtons();
            DisableAndClearCheckBoxes();
            DisableAllButtons(ScoreType.Ones); // Cycles through disabling all buttons, starting from ScoreType.Ones

            Game game = new Game(this);
            playerBindingSource.DataSource = game.Players;

        }

        // Set up instance variables
        Label[] dice = new Label[5];
        Button[] scoreButtons = new Button[(int)ScoreType.Yahtzee + 1];
        Label[] scoreTotals = new Label[(int)ScoreType.GrandTotal + 1];
        CheckBox[] checkBoxes = new CheckBox[5];
        Game game;


        private void InitalizeLabelsAndButtons() {


            dice[0] = die1;
            dice[1] = die2;
            dice[2] = die3;
            dice[3] = die4;
            dice[4] = die5;

            scoreButtons[(int)ScoreType.Ones] = button1;
            scoreButtons[(int)ScoreType.Twos] = button2;
            scoreButtons[(int)ScoreType.Threes] = button3;
            scoreButtons[(int)ScoreType.Fours] = button4;
            scoreButtons[(int)ScoreType.Fives] = button5;
            scoreButtons[(int)ScoreType.Sixes] = button6;
            scoreButtons[(int)ScoreType.ThreeOfAKind] = button7;
            scoreButtons[(int)ScoreType.FourOfAKind] = button8;
            scoreButtons[(int)ScoreType.FullHouse] = button9;
            scoreButtons[(int)ScoreType.SmallStraight] = button10;
            scoreButtons[(int)ScoreType.LargeStraight] = button11;
            scoreButtons[(int)ScoreType.Chance] = button12;
            scoreButtons[(int)ScoreType.Yahtzee] = button13;


            scoreTotals[(int)ScoreType.Ones] = scoreLabel1;
            scoreTotals[(int)ScoreType.Twos] = scoreLabel2;
            scoreTotals[(int)ScoreType.Threes] = scoreLabel3;
            scoreTotals[(int)ScoreType.Fours] = scoreLabel4;
            scoreTotals[(int)ScoreType.Fives] = scoreLabel5;
            scoreTotals[(int)ScoreType.Sixes] = scoreLabel6;
            scoreTotals[(int)ScoreType.ThreeOfAKind] = scoreLabel7;
            scoreTotals[(int)ScoreType.FourOfAKind] = scoreLabel8;
            scoreTotals[(int)ScoreType.FullHouse] = scoreLabel9;
            scoreTotals[(int)ScoreType.SmallStraight] = scoreLabel10;
            scoreTotals[(int)ScoreType.LargeStraight] = scoreLabel11;
            scoreTotals[(int)ScoreType.Chance] = scoreLabel12;
            scoreTotals[(int)ScoreType.Yahtzee] = scoreLabel13;
            scoreTotals[(int)ScoreType.SubTotal] = subLabel;
            scoreTotals[(int)ScoreType.BonusFor63Plus] = bonusLabel;
            scoreTotals[(int)ScoreType.SectionATotal] = upperLabel;
            scoreTotals[(int)ScoreType.SectionBTotal] = lowerLabel;
            scoreTotals[(int)ScoreType.YahtzeeBonus] = yahtzeeLabel;
            scoreTotals[(int)ScoreType.GrandTotal] = grandTotalLabel2;


            checkBoxes[0] = checkBox1;
            checkBoxes[1] = checkBox2;
            checkBoxes[2] = checkBox3;
            checkBoxes[3] = checkBox4;
            checkBoxes[4] = checkBox5;

        }

        /// <summary>
        /// Return array representing the five dice
        /// </summary>
        /// <returns></returns>
        public Label[] GetDice() {
            return dice;

        }

        /// <summary>
        /// Return the array representing the Score Totals
        /// </summary>
        /// <returns></returns>
        public Label[] GetScoresTotals() {
            return scoreTotals;

        }

        /// <summary>
        /// Display the specified player's name on the form
        /// </summary>
        /// <param name="name"></param>
        public void ShowPlayerName(string name) {
            playerLabel.Text = name;
        }

        /// <summary>
        /// Enable roll dice button
        /// </summary>
        public void EnableRollButton() {
            buttonRollDice.Enabled = true;
        }

        /// <summary>
        /// Disable roll dice button
        /// </summary>
        public void DisableRollButton() {
            buttonRollDice.Enabled = false;
        }

        /// <summary>
        /// Allow the checkboxes to be checked
        /// </summary>
        public void EnableCheckBoxes() {
            for (int i = 0; i < 5; i++) {
                checkBoxes[i].Enabled = true;
            }
        }

        /// <summary>
        /// Clear any ticks and disable all of the checkboxes
        /// </summary>
        public void DisableAndClearCheckBoxes() {
            for (int i = 0; i < 5; i++) {
                checkBoxes[i].Checked = false;
                checkBoxes[i].Enabled = false;
            }
        }

        /// <summary>
        /// Enable the specified button corresponding to the ScoreType parameter
        /// </summary>
        /// <param name="combo"></param>
        public void EnableScoreButton(ScoreType combo) {
            if (scoreButtons[(int)combo] != null) {
                scoreButtons[(int)combo].Enabled = true;
            }
        }

        /// <summary>
        /// Disable the specified button corresponding to the ScoreType parameter
        /// </summary>
        /// <param name="combo"></param>
        public void DisableScoreButton(ScoreType combo) {
            if (scoreButtons[(int)combo] != null) {
                scoreButtons[(int)combo].Enabled = false;
            }
        }


        /// <summary>
        /// Tick the checkbox with the index in the array of checkboxes
        /// </summary>
        /// <param name="index"></param>
        public void CheckCheckBox(int index) {
            checkBoxes[index].Checked = true;
        }

        /// <summary>
        /// Display the supplied message on the message label
        /// </summary>
        /// <param name="message"></param>
        public void ShowMessage(string message) {
            messageLabel.Text = message;
        }

        /// <summary>
        /// Display the OK button and make it available to be clicked
        /// </summary>
        public void ShowOKButton() {
            okButton.Visible = true;
        }

        /// <summary>
        /// Hide the OK button
        /// </summary>
        public void HideOKButton() {
            okButton.Visible = false;
        }

        /// <summary>
        /// Instantiate a new Game object
        /// </summary>
        public void StartNewGame() {
            game = new Game(this);
            okButton.Visible = false;

            for (int i = 0; i < 5; i++) {
                dice[i].Text = "";
            }
        }


        /// <summary>
        /// Disable all the ScoreType buttons
        /// </summary>
        /// <param name="combo"></param>
        public void DisableAllButtons(ScoreType combo) {
            //count controlled loop
            for (combo = ScoreType.Ones; combo <= ScoreType.Yahtzee; combo++) {
                //two way selection
                if (scoreButtons[(int)combo] != null) {
                    scoreButtons[(int)combo].Enabled = false;
                }
            }
            buttonRollDice.Enabled = false;
        }


        /// <summary>
        /// Enable all the ScoreType buttons
        /// </summary>
        /// <param name="combo"></param>
        public void EnableAllButtons(ScoreType combo) {
            //count controlled loop
            for (combo = ScoreType.Ones; combo <= ScoreType.Yahtzee; combo++) {
                //two way selection
                if (scoreButtons[(int)combo] != null) {
                    scoreButtons[(int)combo].Enabled = true;
                }
            }
            buttonRollDice.Enabled = true;
        }


        // Button Event Handlers
        private void button1_Click(object sender, EventArgs e) {
            game.ScoreCombination(ScoreType.Ones);
            DisableAllButtons(ScoreType.Ones);
            okButton.Visible = true;
            messageLabel.Text = "Finished - check scores and click OK";
            UpdatePlayersDataGridView(); // Needs to be the last statement
        }

        private void button2_Click(object sender, EventArgs e) {
            game.ScoreCombination(ScoreType.Twos);
            DisableAllButtons(ScoreType.Ones);
            okButton.Visible = true;
            messageLabel.Text = "Finished - check scores and click OK";
            UpdatePlayersDataGridView(); // Needs to be the last statement

        }

        private void button3_Click(object sender, EventArgs e) {
            game.ScoreCombination(ScoreType.Threes);
            DisableAllButtons(ScoreType.Ones);
            okButton.Visible = true;
            messageLabel.Text = "Finished - check scores and click OK";
            UpdatePlayersDataGridView(); // Needs to be the last statement

        }


        private void button4_Click(object sender, EventArgs e) {
            game.ScoreCombination(ScoreType.Fours);
            DisableAllButtons(ScoreType.Ones);
            okButton.Visible = true;
            messageLabel.Text = "Finished - check scores and click OK";
            UpdatePlayersDataGridView(); // Needs to be the last statement

        }

        private void button5_Click(object sender, EventArgs e) {
            game.ScoreCombination(ScoreType.Fives);
            DisableAllButtons(ScoreType.Ones);
            okButton.Visible = true;
            messageLabel.Text = "Finished - check scores and click OK";
            UpdatePlayersDataGridView(); // Needs to be the last statement
        }

        private void button6_Click(object sender, EventArgs e) {
            game.ScoreCombination(ScoreType.Sixes);
            DisableAllButtons(ScoreType.Ones);
            okButton.Visible = true;
            messageLabel.Text = "Finished - check scores and click OK";
            UpdatePlayersDataGridView(); // Needs to be the last statement

        }

        private void button7_Click(object sender, EventArgs e) {
            game.ScoreCombination(ScoreType.ThreeOfAKind);
            DisableAllButtons(ScoreType.Ones);
            okButton.Visible = true;
            messageLabel.Text = "Finished - check scores and click OK";
            UpdatePlayersDataGridView(); // Needs to be the last statement

        }

        private void button8_Click(object sender, EventArgs e) {
            game.ScoreCombination(ScoreType.FourOfAKind);
            DisableAllButtons(ScoreType.Ones);
            okButton.Visible = true;
            messageLabel.Text = "Finished - check scores and click OK";
            UpdatePlayersDataGridView(); // Needs to be the last statement


        }

        private void button9_Click(object sender, EventArgs e) {
            game.ScoreCombination(ScoreType.FullHouse);
            DisableAllButtons(ScoreType.Ones);
            okButton.Visible = true;
            messageLabel.Text = "Finished - check scores and click OK";
            UpdatePlayersDataGridView(); // Needs to be the last statement

        }

        private void button10_Click(object sender, EventArgs e) {
            game.ScoreCombination(ScoreType.SmallStraight);
            DisableAllButtons(ScoreType.Ones);
            okButton.Visible = true;
            messageLabel.Text = "Finished - check scores and click OK";
            UpdatePlayersDataGridView(); // Needs to be the last statement

        }

        private void button11_Click(object sender, EventArgs e) {
            game.ScoreCombination(ScoreType.LargeStraight);
            DisableAllButtons(ScoreType.Ones);
            okButton.Visible = true;
            messageLabel.Text = "Finished - check scores and click OK";
            UpdatePlayersDataGridView(); // Needs to be the last statement
        }

        private void button12_Click(object sender, EventArgs e) {
            game.ScoreCombination(ScoreType.Chance);
            DisableAllButtons(ScoreType.Ones);
            okButton.Visible = true;
            messageLabel.Text = "Finished - check scores and click OK";
            UpdatePlayersDataGridView(); // Needs to be the last statement

        }


        private void button13_Click(object sender, EventArgs e) {
            game.ScoreCombination(ScoreType.Yahtzee);
            DisableAllButtons(ScoreType.Ones);
            okButton.Visible = true;
            messageLabel.Text = "Finished - check scores and click OK";
            UpdatePlayersDataGridView(); // Needs to be the last statement

        }

        private void subTotalLabel_Click(object sender, EventArgs e) {

        }

        private void die1_Click(object sender, EventArgs e) {

        }

        private void die2_Click(object sender, EventArgs e) {

        }


        private void scoreLabel9_Click(object sender, EventArgs e) {

        }

        private void scoreLabel12_Click(object sender, EventArgs e) {

        }

        private void subLabel_Click(object sender, EventArgs e) {

        }

        private void yahtzeeLabel_Click(object sender, EventArgs e) {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e) {

        }

        // New game Button event handler
        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            numericUpDown1.Enabled = true;
            StartNewGame();
            buttonRollDice.Enabled = true;
            //messageLabel.Text = "Roll 1";
            playerLabel.Text = "Player 1";

            for (int i = 0; i < 5; i++) {
                checkBoxes[i].Checked = false;
            }

            playerLabel.Text = "Player 1";
            scoreLabel1.Text = "";
            scoreLabel2.Text = "";
            scoreLabel3.Text = "";
            scoreLabel4.Text = "";
            scoreLabel5.Text = "";
            scoreLabel6.Text = "";
            scoreLabel7.Text = "";
            scoreLabel8.Text = "";
            scoreLabel9.Text = "";
            scoreLabel10.Text = "";
            scoreLabel11.Text = "";
            scoreLabel12.Text = "";
            scoreLabel13.Text = "";
            upperLabel.Text = "";
            lowerLabel.Text = "";
            subLabel.Text = "";
            bonusLabel.Text = "";
            yahtzeeLabel.Text = "";
            grandTotalLabel2.Text = "";

        }

        private void messageLabel_Click(object sender, EventArgs e) {

        }

        // Roll dice Button event handler
        private void buttonRollDice_Click(object sender, EventArgs e) {
            numericUpDown1.Enabled = false;
            EnableCheckBoxes();
            //EnableAllButtons(ScoreType.Ones);

            for (int i = 0; i < 5; i++) {
                if (checkBoxes[i].Checked == true) {
                    game.HoldDie(i);
                } else if (checkBoxes[i].Checked == false) {
                    game.ReleaseDie(i);
                }
            }


            game.RollDice();  // Lowercase 'g' refers to game object not game class
        }

        private void gameToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        // OK Button event handler
        private void okButton_Click(object sender, EventArgs e) {
            messageLabel.Text = "Roll 1";
            game.NextTurn();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) {

        }

        private void UpdatePlayersDataGridView() {
            game.Players.ResetBindings();
        }
    }

}

