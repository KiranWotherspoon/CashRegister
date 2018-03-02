using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Media;

namespace CashRegister
{
    public partial class cashRegister : Form
    {
        //declare constants
        const double BURGERCOST = 2.49;
        const double FRIESCOST = 1.89;
        const double DRINKCOST = 0.99;
        const double TAX = 0.13;

        //declare global variables
        int burgerNumber, friesNumber, drinkNumber = 0;
        double orderCost, taxTotal, totalCost, amountTendered, change = 0;

        private void totalButton_Click(object sender, EventArgs e)
        {
            //setup graphics
            receiptLabel.Visible = false;
            Graphics receipt = this.CreateGraphics();
            SolidBrush receiptBrush = new SolidBrush(Color.White);
            SolidBrush wordBrush = new SolidBrush(Color.Black);
            Font wordFont = new Font("Arial", 10, FontStyle.Regular);
            receipt.FillRectangle(receiptBrush, 213, 39, 274, 277);
            
            try
            {
                //read number of orders from text boxes
                burgerNumber = Convert.ToInt16(burgerBox.Text);
                friesNumber = Convert.ToInt16(fryBox.Text);
                drinkNumber = Convert.ToInt16(drinkBox.Text);

                //calculate subtotal, tax and total
                orderCost = (burgerNumber * BURGERCOST) + (friesNumber * FRIESCOST) + (drinkNumber * DRINKCOST);
                taxTotal = orderCost * TAX;
                totalCost = orderCost + taxTotal;

                if (totalCost < 1)
                {
                    //check if customer is actually ordering something
                    receipt.DrawString("Please make sure you order something.", wordFont, wordBrush, 220, 50);
                    Thread.Sleep(2000);
                    receipt.FillRectangle(receiptBrush, 213, 39, 274, 277);
                }

                else
                {
                    //display numbers in respective labels
                    subTotalLabel.Text = orderCost.ToString("C");
                    taxLabel.Text = taxTotal.ToString("C");
                    totalLabel.Text = totalCost.ToString("C");
                }
            }
            catch
            {
                //if program doesn't work display error message
                receipt.FillRectangle(receiptBrush, 213, 39, 274, 277);
                receipt.DrawString("Please make sure you have only entered \nwhole numbers into the text boxes.", wordFont, wordBrush, 220, 50);
                Thread.Sleep(2000);
                receipt.FillRectangle(receiptBrush, 213, 39, 274, 277);
            }
        }

        private void changeButton_Click(object sender, EventArgs e)
        {
            //set up sound
            SoundPlayer caching = new SoundPlayer(Properties.Resources.caChingSound);
            
            //set up graphics
            Graphics receipt = this.CreateGraphics();
            SolidBrush receiptBrush = new SolidBrush(Color.White);
            SolidBrush wordBrush = new SolidBrush(Color.Black);
            Font wordFont = new Font("Arial", 10, FontStyle.Regular);
            receipt.FillRectangle(receiptBrush, 213, 39, 274, 277);
            try
            {
                //read number amount tendered and calculate change
                amountTendered = Convert.ToDouble(tenderedBox.Text);
                change = amountTendered - totalCost;

                if (change < 0)
                {
                    //check to see if customer has entered enough money
                    receipt.DrawString("This is not enough money to pay for your \nmeal.\nPlease add more if you still wish buy this.", wordFont, wordBrush, 220, 50);
                    Thread.Sleep(2000);
                    receipt.FillRectangle(receiptBrush, 213, 39, 274, 277);
                }
                else
                {
                    //play sound and display change
                    caching.Play();
                    Thread.Sleep(100);
                    changeLabel.Text = change.ToString("C");
                }

            }
            catch
            {
                //if program dosen't work display error message
                receipt.DrawString("Please make sure you have only entered \nvalid characters into the text box.", wordFont, wordBrush, 220, 50);
                Thread.Sleep(2000);
                receipt.FillRectangle(receiptBrush, 213, 39, 274, 277);
            }
        }

        private void billButton_Click(object sender, EventArgs e)
        {
            //create some random numbers
            Random rng = new Random();
            int orderNumber = rng.Next(99,1000);
            int day = rng.Next(1,31);
            int year = rng.Next(1960, 2019);

            //set up sound
            SoundPlayer print = new SoundPlayer(Properties.Resources.printSound);
            print.Play();

            //setup graphics
            Graphics receipt = this.CreateGraphics();
            SolidBrush receiptBrush = new SolidBrush(Color.White);
            SolidBrush wordBrush = new SolidBrush(Color.Black);
            Font titleFont = new Font("Verdana", 14, FontStyle.Bold);
            Font wordFont = new Font("Consolas", 10, FontStyle.Regular);

            //draw receipt
            receipt.FillRectangle(receiptBrush, 213, 39, 274, 277);
            receipt.DrawString("Macks Donald's", titleFont, wordBrush, 270, 40);
            Thread.Sleep(200);
            receipt.DrawString("Order #" + orderNumber, wordFont, wordBrush, 220, 80);
            Thread.Sleep(100);
            receipt.DrawString("\nMarch " + day + ", " + year, wordFont, wordBrush, 220, 80);
            Thread.Sleep(100);
            receipt.DrawString("\n\n\nHamburgers x" + burgerNumber, wordFont, wordBrush, 220, 80);
            receipt.DrawString("\n\n\n@" + BURGERCOST, wordFont, wordBrush, 430, 80);
            Thread.Sleep(100);
            receipt.DrawString("\n\n\n\nFries x" + friesNumber, wordFont, wordBrush, 220, 80);
            receipt.DrawString("\n\n\n\n@" + FRIESCOST, wordFont, wordBrush, 430, 80);
            Thread.Sleep(100);
            receipt.DrawString("\n\n\n\n\nDrinks x" + drinkNumber, wordFont, wordBrush, 220, 80);
            receipt.DrawString("\n\n\n\n\n@" + DRINKCOST, wordFont, wordBrush, 430, 80);
            Thread.Sleep(100);
            receipt.DrawString("\n\n\n\n\n\n\nSubtotal", wordFont, wordBrush, 220, 80);
            receipt.DrawString("\n\n\n\n\n\n\n" + orderCost.ToString("C"), wordFont, wordBrush, 430, 80);
            Thread.Sleep(100);
            print.Play();
            receipt.DrawString("\n\n\n\n\n\n\n\nTax", wordFont, wordBrush, 220, 80);
            receipt.DrawString("\n\n\n\n\n\n\n\n" + taxTotal.ToString("C"), wordFont, wordBrush, 430, 80);
            Thread.Sleep(100);
            receipt.DrawString("\n\n\n\n\n\n\n\n\nTotal", wordFont, wordBrush, 220, 80);
            receipt.DrawString("\n\n\n\n\n\n\n\n\n" + totalCost.ToString("C"), wordFont, wordBrush, 430, 80);
            Thread.Sleep(100);
            receipt.DrawString("\n\n\n\n\n\n\n\n\n\n\nTendered", wordFont, wordBrush, 220, 80);
            receipt.DrawString("\n\n\n\n\n\n\n\n\n\n\n" + amountTendered.ToString("C"), wordFont, wordBrush, 430, 80);
            Thread.Sleep(100);
            receipt.DrawString("\n\n\n\n\n\n\n\n\n\n\n\nChange", wordFont, wordBrush, 220, 80);
            receipt.DrawString("\n\n\n\n\n\n\n\n\n\n\n\n" + change.ToString("C"), wordFont, wordBrush, 430, 80);
            Thread.Sleep(100);
            receipt.DrawString("\n\n\n\n\n\n\n\n\n\n\n\n\n\nHave a Nice Day!!", wordFont, wordBrush, 220, 80);
        }

        private void newOrderButton_Click(object sender, EventArgs e)
        {
            //set up sound
            SoundPlayer receiptRip = new SoundPlayer(Properties.Resources.paperRip);
            receiptRip.Play();
            
            //reset everything
            burgerNumber = 0;
            friesNumber = 0;
            drinkNumber = 0;
            orderCost = 0;
            taxTotal = 0;
            totalCost = 0;
            amountTendered = 0;
            change = 0;
            subTotalLabel.Text = "";
            taxLabel.Text = "";
            totalLabel.Text = "";
            changeLabel.Text = "";
            burgerBox.Text = "";
            fryBox.Text = "";
            drinkBox.Text = "";
            tenderedBox.Text = "";
            Graphics receipt = this.CreateGraphics();
            SolidBrush receiptBrush = new SolidBrush(Color.White);
            receipt.FillRectangle(receiptBrush, 213, 39, 274, 277);
        }

        public cashRegister()
        {
            InitializeComponent();
        }
    }
}
