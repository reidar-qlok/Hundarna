using System;
using System.Windows.Forms;

namespace GreyhoundRace
{
    public partial class Form1 : Form
    {
        // Initialise an array of punters
        Punter[] myPunters = new Punter[3];
        // Initialise an array of Dogs
        Dogs[] DogsArray = new Dogs[4];

        public Form1()
        {
            InitializeComponent();
            DogsRace();
            Punters();
            LabelsClear();
        }

        public void LabelsClear() // Clear all labels
        {
            lblEmely.Text = "";
            lblReidar.Text = "";
            lblTobias.Text = "";
        }

        public void Punters()
        {
            //create an array of punters and instantiate factory class
            for (int i = 0; i < 3; i++)
            {
                myPunters[i] = Factory.GetAPunter(i);
            }

            //set the labels to the classes and update
            myPunters[0].MyLabel = lblEmely;
            myPunters[0].MyRadioButton = rbEmely;
            myPunters[0].MyRadioButton.Text = myPunters[0].Name + " has $" + myPunters[0].Cash;
            myPunters[1].MyLabel = lblTobias;
            myPunters[1].MyRadioButton = rbTobias;
            myPunters[1].MyRadioButton.Text = myPunters[1].Name + " has $" + myPunters[1].Cash;
            myPunters[2].MyLabel = lblReidar;
            myPunters[2].MyRadioButton = rbReidar;
            myPunters[2].MyRadioButton.Text = myPunters[2].Name + " has $" + myPunters[2].Cash;

        }

        public void GameOverCheck() // Checks to see if the game is over and close game
        {
            if (myPunters[0].Cash <= 0 && myPunters[1].Cash <= 0 && myPunters[2].Cash <= 0)
            {
                MessageBox.Show("Congratulations, all of your bettors are broke! Try Again :D");
                LabelsClear();
                ResetRace();
                this.Close();
            }

        }

        public void BettorBroke() // Checks to see if anyone is broke and cannot continue and update label and blank out radio button
        {
            if (myPunters[0].Cash <= 0)//Joe
            {
                lblEmely.Text = "Joe is now broke and cannot continue betting";
                rbEmely.Enabled = false;
            }
            if (myPunters[1].Cash <= 0)//Tobias
            {
                lblTobias.Text = "Tobias is now broke and cannot continue betting";
                rbTobias.Enabled = false;
            }
            if (myPunters[2].Cash <= 0)//Reidar
            {
                lblReidar.Text = "Reidar is now broke and cannot continue betting";
                rbReidar.Enabled = false;
            }

        }

        public void ResetBetAmount() // Reset the bet amounts to zero if the punter is busted
        {
            if (myPunters[0].Cash == 0)
            {
                myPunters[0].myBet.Amount = 0;
            }
            if (myPunters[1].Cash == 0)
            {
                myPunters[1].myBet.Amount = 0;
            }
            if (myPunters[2].Cash == 0)
            {
                myPunters[2].myBet.Amount = 0;
            }
        }

        public void DogsRace() // Instantiate the Dogs
        {
            DogsArray[0] = new Dogs { MyPictureBox = pbDog1, StartingPosition = pbDog1.Left, DogName = "#1", RaceTrackLength = pbRaceTrack.Width - pbDog1.Width, Randomiser = new Random() };
            DogsArray[1] = new Dogs { MyPictureBox = pbDog2, StartingPosition = pbDog2.Left, DogName = "#2", RaceTrackLength = pbRaceTrack.Width - pbDog2.Width, Randomiser = DogsArray[0].Randomiser };
            DogsArray[2] = new Dogs { MyPictureBox = pbDog3, StartingPosition = pbDog3.Left, DogName = "#3", RaceTrackLength = pbRaceTrack.Width - pbDog3.Width, Randomiser = DogsArray[0].Randomiser };
            DogsArray[3] = new Dogs { MyPictureBox = pbDog4, StartingPosition = pbDog4.Left, DogName = "#4", RaceTrackLength = pbRaceTrack.Width - pbDog4.Width, Randomiser = DogsArray[0].Randomiser };
        }

        private void btnBet_Click(object sender, EventArgs e) // Place the selected bet
        {
            int punter = 0;

            if (rbEmely.Checked)
            {
                punter = 0;
            }
            else if (rbTobias.Checked)
            {
                punter = 1;
            }
            else if (rbReidar.Checked)
            {
                punter = 2;
            }

            myPunters[punter].PlaceBet((int)udBoxBet.Value, (int)udBoxDog.Value - 1); // Updates the bet amount and Dog # using the Placebet.Punter class with Form designer details

        }

        private void rbEmely_CheckedChanged(object sender, EventArgs e)
        {
            //Show that Joe is betting in the bettor label
            lblBettor.Text = myPunters[0].Name;
            // Sets the maximum bet based off cash
            udBoxBet.Maximum = myPunters[0].Cash;
        }

        private void rbTobias_CheckedChanged(object sender, EventArgs e)
        {
            //Show that Tobias is betting in the bettor label
            lblBettor.Text = myPunters[1].Name;
            // Sets the maximum bet based off cash
            udBoxBet.Maximum = myPunters[1].Cash;
        }

        private void rbReidar_CheckedChanged(object sender, EventArgs e)
        {
            //Show that Reidar is betting in the bettor label
            lblBettor.Text = myPunters[2].Name;
            // Sets the maximum bet based off cash
            udBoxBet.Maximum = myPunters[2].Cash;
        }

        public void ResetRace() // Reset Dog positions back to start
        {
            // Reset the label text
            myPunters[0].MyLabel.ResetText();
            myPunters[1].MyLabel.ResetText();
            myPunters[2].MyLabel.ResetText();
            //Reset the bet amounts to zero
            myPunters[0].myBet.Amount = 0;
            myPunters[1].myBet.Amount = 0;
            myPunters[2].myBet.Amount = 0;

            foreach (Dogs t in DogsArray)
            {
                t.MyPictureBox.Left = t.StartingPosition;
            }
        }

        private void btnRace_Click(object sender, EventArgs e)
        {
            //Check to see if the punters have enough money to proceed with the race and provide warning if not
            if (myPunters[0].Cash < udBoxBet.Value && rbEmely.Enabled)
            {
                MessageBox.Show("Sorry but Joe does not have enough cash to proceed.");
                timer1.Enabled = false;
            }
            if (myPunters[1].Cash < udBoxBet.Value && rbTobias.Enabled)
            {
                MessageBox.Show("Sorry but Tobias does not have enough cash to proceed.");
                timer1.Enabled = false;
            }
            if (myPunters[2].Cash < udBoxBet.Value && rbReidar.Enabled)
            {
                MessageBox.Show("Sorry but Reidar does not have enough cash to proceed.");
                timer1.Enabled = false;
            }
            else
            {
                //Reset starting positions
                foreach (Dogs t in DogsArray)
                {
                    t.MyPictureBox.Left = t.StartingPosition;
                }

                // Start the timer for the race
                timer1.Enabled = true;
                btnRace.Enabled = false; // Disable the race button
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        { // Run the timer event for the race and return the winner and bet results
            // If no bet is placed then return warning message and try again
            try
            {
                int winner;

                for (int i = 0; i < DogsArray.Length; i++)
                {
                    if (DogsArray[i].Run(pbRaceTrack)) // use Dog.Run class for race and if true return a winner and stop timer event
                    {
                        winner = i;
                        timer1.Enabled = false;
                        MessageBox.Show("Dog #" + (winner + 1) + " Wins!");

                        for (int j = 0; j < myPunters.Length; j++)
                        {
                            if (myPunters[j].myBet.PayOut(winner) != 0) // if punters payout is not 0
                                myPunters[j].Cash += myPunters[j].myBet.PayOut(winner); // Update punters cash with the bet payout amount
                            myPunters[j].MyRadioButton.Text = myPunters[j].Name + " has $" + myPunters[j].Cash; // Updates the radio button text with new cash value
                        }

                        ResetRace(); // Resets the starting positions, bet amounts, and labels
                        ResetBetAmount(); // Reset bet amounts if bettor is bust
                        BettorBroke(); // Checks to see if anyone is bust and blank them out
                        GameOverCheck(); // Checks to see if the game is over and close if true

                        break;

                    }

                }

            }

            catch
            {
                MessageBox.Show("A bet was not placed, you could have won some coin.");
            }

        }

        public void btnLockIn_Click(object sender, EventArgs e) // Unlock the Race Button and check bet amount is not more than cash
        {
            try
            {
                if (myPunters[0].Cash < myPunters[0].myBet.Amount)
                {
                    MessageBox.Show("Joe does not have enough to proceed");
                    btnRace.Enabled = false;
                }
                if (myPunters[1].Cash < myPunters[1].myBet.Amount)
                {
                    MessageBox.Show("Tobias does not have enough to proceed");
                    btnRace.Enabled = false;
                }
                if (myPunters[2].Cash < myPunters[2].myBet.Amount)
                {
                    MessageBox.Show("Reidar does not have enough to proceed");
                    btnRace.Enabled = false;
                }
                else
                {
                    btnRace.Enabled = true;
                }
            }

            catch
            {
                MessageBox.Show("Please place all bets");
            }

        }
    }

}
