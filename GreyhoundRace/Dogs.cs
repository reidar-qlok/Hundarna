using System;
using System.Windows.Forms;

namespace GreyhoundRace
{
    public class Dogs
    {
        // Sets the Dog name
        public string DogName { get; set; }

        // Where my picturebox starts
        public int StartingPosition { get; set; }
        public int RaceTrackLength { get; set; } // How long my racetrack is
        public PictureBox MyPictureBox { get; set; } = null; // The picturebox object
        public int Location { get; set; } = 0; // My location on the racetrack
        public Random Randomiser { get; set; } // An instance of random for the random generator

        public bool Run(PictureBox raceTrack)
        {
            // Move forward 1, 2, 3 or 4 spaces at random
            MyPictureBox.Left += Randomiser.Next(0, 4);

            // Return true if race is won
            if (MyPictureBox.Right > raceTrack.Right)
            {
                return true;
            }

            return false;
        }

    }
}
