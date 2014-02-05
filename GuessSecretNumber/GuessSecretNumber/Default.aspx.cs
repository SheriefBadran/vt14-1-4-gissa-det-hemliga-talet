using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GuessSecretNumber.Model;

namespace GuessSecretNumber
{
    public partial class Default : System.Web.UI.Page
    {
        // Protected property
        protected SecretNumber SecretNumber
        {
            get
            {
                return Session["SecretNumber"] as SecretNumber ?? (SecretNumber)(Session["SecretNumber"] = new SecretNumber());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Empty
        }

        protected void GuessButton_Click(object sender, EventArgs e)
        {
            if (Session.IsNewSession)
            {
                ModelState.AddModelError(String.Empty, "Session har gått ut!");
                GuessButton.Visible = false;
                NewGameButton.Visible = true;
                SecretNumberGuess.Text = String.Empty;
                SecretNumberGuess.Enabled = false;
                SecretNumber.Initialize();
                return;
            }

            if (IsValid)
	        {

                try
                {
                    Outcome outcome = SecretNumber.MakeGuess(int.Parse(SecretNumberGuess.Text));
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(String.Empty, ex.Message);
                }

                var gameEnd = RenderGuessOutcome(SecretNumber);
                

                if (gameEnd)
                {
                    GuessButton.Visible = false;
                    NewGameButton.Visible = true;
                    SecretNumberGuess.Text = String.Empty;
                    SecretNumberGuess.Enabled = false;
                    SecretNumber.Initialize();
                }
	        }
        }

        protected bool RenderGuessOutcome(SecretNumber SecretNumber)
        {
            var response = String.Empty;
            switch (SecretNumber.Outcome)
            {
                case Outcome.High:
                    response = "För högt!";
                    break;
                case Outcome.Low:
                    response = "För lågt!";
                    break;
                case Outcome.Right:
                    response = "Grattis! Du gissade rätt tal!";
                    break;
                case Outcome.OldGuess:
                    response = "Du har redan gissat på det talet.";
                    break;
                case Outcome.NoMoreGuesses:
                    response = "Tyvärr! Du har slut på gissningar, bättre lycka nästa gång.";
                    break;
            }

            Guesses.Text = SecretNumber.Guess.ToString();
            Guesses.Text = String.Join(" ", SecretNumber.GuessedNumbers.Select(sn => sn.Number));
            GuessResponse.Text = response;
            Guesses.Visible = true;
            GuessResponse.Visible = true;

            return SecretNumber.Outcome == Outcome.NoMoreGuesses || SecretNumber.Outcome == Outcome.Right;

        }
    }
}