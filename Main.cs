//Programmer: Josh Thrash
//Date: 07/09/2021
//Purpose: This Windows Form Application is a game similar to Deal or No Deal.  Users will have 8 cases to select their case from. 
// The user will then begin selecting cases and accepting/rejecting offered an amount depending on the remaning values left on the board.

using System;
using System.Linq;
using System.Windows.Forms;

namespace DealOrNoDeal
{
    public partial class Main : Form
    {
        public int totalCasesValue = 1361111; //Total of all Cases
        public int totalNumberOfTurns = 0; //Turn Counter. Increases by one after each offer
        int offer = 0;
        int selectedCaseValue = 0;
        int[] caseValues = { 1, 10, 100, 1000, 10000, 100000, 250000, 1000000 }; // Values to be assigned to the cases randomly
        int finalCaseAmount = 0;
        string dealResults = ""; //added to the End Screen to show result of deal and determine if a good or bad deal was made by user

        public Main()
        {
            InitializeComponent();
            lblOutput.Text = "Select the Case you think has 1 Million Dollars!!";
            Button[] cases = { btnCase1, btnCase2, btnCase3, btnCase4, btnCase5, btnCase6, btnCase7, btnCase8 };//Label to set each Case to be selected

            //Calls Function to set New Game with different order of Money in Cases
            int n = caseValues.Length;
            randomize(caseValues, n);
            var caseAndValues = caseValues.Zip(cases, (n, w) => new { CaseValues = n, Cases = w });
        }      

        //Fisher-Yates Shuffle Algorithim.  Randomizes the array of Case Values so they can be asigned to a Case Number
        static void randomize(int[] arr, int z)
        {
            Random random = new Random();

            for (int i = z - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }

        //Method that locks all cases on the board before an offer is made
        void CaseLock()
        {
            Button[] cases = { btnCase1, btnCase2, btnCase3, btnCase4, btnCase5, btnCase6, btnCase7, btnCase8 };

            foreach (var btnCase in cases)
            {
                btnCase.Enabled = false;
            }
        }

        //Method that unlocks the remianing cases after an offer to start another round
        void CaseUnlock()
        {
            Button[] cases = { btnCase1, btnCase2, btnCase3, btnCase4, btnCase5, btnCase6, btnCase7, btnCase8 };

            foreach (var btnCase in cases)
            {
                btnCase.Enabled = true;
            }
        }

        //Method that hides the value of each case when called
        void HideValue()
        {
            Label[] CaseAmountsArray = { lblCase1Amount, lblCase2Amount, lblCase3Amount, lblCase4Amount,
                 lblCase5Amount, lblCase6Amount, lblCase7Amount, lblCase8Amount };

            foreach (var caseAmount in CaseAmountsArray)
            {
                caseAmount.Visible = false;
            }
        }

        //Method that is called after each round, minus the first.  This method calculates the remaing values of each remaing cases
        //From the total amount and divides it by the remaining number of turns.  The Accept and Reject Offer Buttons are then prompted
        void Offer(int CaseValue)
        {
            int offerCount = 1;
            int averageAmount = 8;

            while(offerCount != totalNumberOfTurns)
            {
                offerCount++;
                averageAmount--;

                if (totalNumberOfTurns == offerCount)
                {
                    totalCasesValue = totalCasesValue - CaseValue;
                    offer = totalCasesValue / averageAmount;
                    lblOutput.Text = "Your offer from the bank is " + offer.ToString("C");
                    btnAccept.Show();
                    btnReject.Show();
                }
            }
        }

        //Method is called each new round for the first Case.  Does not show the cases value and moved the selected case 
        //number to the lower right portion till needed in the final round
        void FirstCaseTest(string caseNumDisplay, Button CaseNumber, int caseVal)
        {
            if (totalNumberOfTurns == 1)
            {
                btnSelectedCase.Text = caseNumDisplay;
                CaseNumber.Hide();
                btnSelectedCase.Show();
                lblSelectedCase.Visible = true;
                lblOutput.Text = "Please select another case";
                selectedCaseValue = caseVal;
            }           
        }

        //Mehtod use each turn after first turn when a Case is Selected. Locks cases so only one can be picked each turn 
        //while hiding the case selected and then removes the amount of that case from Remaining Values Board
        void CaseTest(Button CaseNumber, int Val)
        {
            int turnCount = 1;

            while(turnCount != totalNumberOfTurns)
            {
                ++turnCount;

                if (totalNumberOfTurns == turnCount)
                {
                    CaseLock();
                    CaseNumber.Visible = false;
                    HideAmount(Val);
                }
            }
        }

        //Method that appears when there is only one case on the board and the players selected case left to be picked.
        //Goes through all cases to determine the last case on the boards value
        void FinalTwoCases()
        {
            lblOutput.Text = "Would you like to swap or keep your case?";
            CaseLock();
            btnSwitch.Visible = true;
            btnKeep.Visible = true;

            if (btnCase1.Enabled == false  && btnCase1.Visible == true)
                finalCaseAmount = Convert.ToInt32(caseValues[0]);
            else if (btnCase2.Enabled == false && btnCase2.Visible == true)
                finalCaseAmount = Convert.ToInt32(caseValues[1]);
            else if (btnCase3.Enabled == false && btnCase3.Visible == true)
                finalCaseAmount = Convert.ToInt32(caseValues[2]);
            else if (btnCase4.Enabled == false && btnCase4.Visible == true)
                finalCaseAmount = Convert.ToInt32(caseValues[3]);
            else if (btnCase5.Enabled == false && btnCase5.Visible == true)
                finalCaseAmount = Convert.ToInt32(caseValues[4]);
            else if (btnCase6.Enabled == false && btnCase6.Visible == true)
                finalCaseAmount = Convert.ToInt32(caseValues[5]);
            else if (btnCase7.Enabled == false && btnCase7.Visible == true)
                finalCaseAmount = Convert.ToInt32(caseValues[6]);
            else if (btnCase8.Enabled == false && btnCase8.Visible == true)
                finalCaseAmount = Convert.ToInt32(caseValues[7]);
        }

        //Method that hides the value amount of case that is picked from the Remaining Values Board. Called W/ CaseTest Method
        void HideAmount(int Val)
        {
            if (Val == 1)
                lblOneDol.Visible = false;
            else if (Val == 10)
                lblTenDol.Visible = false;
            else if (Val == 100)
                lblHundredDol.Visible = false;
            else if (Val == 1000)
                lblThousandDol.Visible = false;
            else if (Val == 10000)
                lblTenThousandDol.Visible = false;
            else if (Val == 100000)
                lblHundredThousandDol.Visible = false;
            else if (Val == 250000)
                lblTwoFiftyThousandDol.Visible = false;
            else if (Val == 1000000)
                lblMillionDol.Visible = false;
        }

        void btnCase1_Click(object sender, EventArgs e)
        {
            int caseVal = caseValues[0]; //Randomly assigned value to the position of Case 1          
            totalNumberOfTurns++;  //Increases the number of Turns each case that is selected

            FirstCaseTest("1", btnCase1, caseVal); //First Round Select Player Case
            CaseTest(btnCase1, caseVal); //Calls CaseTest method whenever button is clicked if after first turn;
            Offer(caseVal); //Calls OfferMethod if button is clicked after first turn

            lblCase1Amount.Visible = true; //Makes the amount in the case show when selected
            if (totalNumberOfTurns == 1) //Does not allow the first case selected to show its value
                HideValue();
            lblCase1Amount.Text = caseVal.ToString("C");
        }

        void btnCase2_Click(object sender, EventArgs e)
        {          
            int caseVal = caseValues[1];
            totalNumberOfTurns++;

            FirstCaseTest("2", btnCase2, caseVal);
            CaseTest(btnCase2, caseVal);
            Offer(caseVal);

            lblCase2Amount.Visible = true;
            if (totalNumberOfTurns == 1)
                HideValue();
            lblCase2Amount.Text = caseVal.ToString("C");
        }
        void btnCase3_Click(object sender, EventArgs e)
        {
            int caseVal = caseValues[2];
            totalNumberOfTurns++;

            FirstCaseTest("3", btnCase3, caseVal);
            CaseTest(btnCase3, caseVal);
            Offer(caseVal);

            lblCase3Amount.Visible = true;
            if (totalNumberOfTurns == 1)
                HideValue();
            lblCase3Amount.Text = caseVal.ToString("C");
        }

        void btnCase4_Click(object sender, EventArgs e)
        {
            int caseVal = caseValues[3];
            totalNumberOfTurns++;

            FirstCaseTest("4", btnCase4, caseVal);
            CaseTest(btnCase4, caseVal);
            Offer(caseVal);

            lblCase4Amount.Visible = true;
            if (totalNumberOfTurns == 1)
                HideValue();
            lblCase4Amount.Text = caseVal.ToString("C");            
        }

        void btnCase5_Click(object sender, EventArgs e)
        {
            int caseVal = caseValues[4];
            totalNumberOfTurns++;

            FirstCaseTest("5", btnCase5, caseVal);
            CaseTest(btnCase5, caseVal);
            Offer(caseVal);

            lblCase5Amount.Visible = true;
            if (totalNumberOfTurns == 1)
                HideValue();
            lblCase5Amount.Text = caseVal.ToString("C");
        }

        void btnCase6_Click(object sender, EventArgs e)
        {
            int caseVal = caseValues[5];
            totalNumberOfTurns++;

            FirstCaseTest("6", btnCase6, caseVal);
            CaseTest(btnCase6, caseVal);
            Offer(caseVal);

            lblCase6Amount.Visible = true;
            if (totalNumberOfTurns == 1)
                HideValue();
            lblCase6Amount.Text = caseVal.ToString("C");
        }

        void btnCase7_Click(object sender, EventArgs e)
        {
            int caseVal = caseValues[6];
            totalNumberOfTurns++;

            FirstCaseTest("7", btnCase7, caseVal); 
            CaseTest(btnCase7, caseVal);
            Offer(caseVal);

            lblCase7Amount.Visible = true;
            if (totalNumberOfTurns == 1)
                HideValue();
            lblCase7Amount.Text = caseVal.ToString("C");
        }

        void btnCase8_Click(object sender, EventArgs e)
        {
            int caseVal = caseValues[7];
            totalNumberOfTurns++;

            FirstCaseTest("8", btnCase8, caseVal); 
            CaseTest(btnCase8, caseVal);
            Offer(caseVal);

            lblCase8Amount.Visible = true;
            if (totalNumberOfTurns == 1)
                HideValue();
            lblCase8Amount.Text = caseVal.ToString("C");
        }

        //Reject Offer Button.  Will reset remaining cases and ask for another Case to be selected
        void btnReject_Click(object sender, EventArgs e)
        {
            btnAccept.Hide();
            btnReject.Hide();
            CaseUnlock();
            HideValue();

            if (totalNumberOfTurns <= 6)
                lblOutput.Text = "Please select another case";
            if(totalNumberOfTurns == 7)
                FinalTwoCases();               
        }

        //Button to take Offer and show Deal Results
        private void btnAccept_Click(object sender, EventArgs e)
        {
            lblWin.Visible = true;

            if (selectedCaseValue < offer)
                dealResults = "You made a great deal!!";
            if (selectedCaseValue > offer)
                dealResults = "You made a bad deal!!";
            string output =  "You Won " + offer.ToString("C") + " !!!!\n";
            string output1 = "Your case contained " + selectedCaseValue.ToString("C") +"\n" + dealResults;

            lblWin.Text = output + output1;

            CaseLock();
            btnAccept.Hide();
            btnReject.Hide();
        }

        //Button to Keep Selected Case and show Deal Results
        private void btnKeep_Click(object sender, EventArgs e)
        {
            lblWin.Visible = true;
            if (selectedCaseValue > finalCaseAmount)
                dealResults = "You made a great deal!!";
            if (selectedCaseValue < finalCaseAmount)
                dealResults = "You made a bad deal!!";
            lblWin.Text = "Your case contained " + selectedCaseValue.ToString("C") +" !!!!\n" + dealResults;
            btnKeep.Hide();
            btnSwitch.Hide();      
        }

        //Button to Switch Selected Case with last remaing case and show Deal Results
        private void btnSwitch_Click(object sender, EventArgs e)
        {
            lblWin.Visible = true;
            if (selectedCaseValue < finalCaseAmount)
                dealResults = "You made a great deal!!";
            if (selectedCaseValue > finalCaseAmount)
                dealResults = "You made a bad deal!!";
            lblWin.Text = "Your switched cases for " + finalCaseAmount.ToString("C") + " !!!!\n" + dealResults;
            btnKeep.Hide();
            btnSwitch.Hide();
        }
    }
}
