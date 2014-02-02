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
                return (SecretNumber)Session["secretNumber"];
            }
            set
            {
                if (value == null)
                {
                    throw new ApplicationException("Ett fel har inträffat!");
                }

                Session["secretNumber"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GuessButton_Click(object sender, EventArgs e)
        {
            if (IsValid)
	        {
		         var number = int.Parse(SecretNumberGuess.Text);
                if (SecretNumber == null)
                {
                    SecretNumber = new SecretNumber();
                }

                Outcome outcome = SecretNumber.MakeGuess(number);
	        }
        }
    }
}