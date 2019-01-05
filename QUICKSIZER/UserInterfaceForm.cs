using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace QUICKSIZER
{
    public partial class UserInterfaceForm : Form
    {
        public UserInterfaceForm()
        {
            InitializeComponent();
        }

        private void AxialNed_TextChanged(object sender, EventArgs e)
        {

        }
        
        //transforming Effective Length to one of those used in tables
        double RoundEffectiveLength(double inputLength)
        {
            double roundedLength = 0;

            if (inputLength > 14)
            {
                MessageBox.Show("Wow! Looks like your columns is really long. Sorry, I can only deal with lenght up to 14m.");
            }
            else if (inputLength <= 0)
            {
                MessageBox.Show("Hey! You are trying to kill me! No negative values allowed for length input.");
            }
            else if (inputLength < 4)
            {
                double remainder = inputLength % 0.5;
                if (remainder != 0 )
                {
                    roundedLength = inputLength - remainder + 0.5;
                }
                else
                {
                    roundedLength = inputLength;
                }
            }
            else if (inputLength >= 4)
            {
                double remainder = inputLength % 1;
                if (remainder != 0)
                {
                    roundedLength = inputLength - remainder + 1;
                }
                else
                {
                    roundedLength = inputLength;
                }
            }
            return roundedLength;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //getting input parameters from the Form
            double AxialForceInput = 0;
            double EffectiveLengthInput = 0;
            try
            {
                AxialForceInput = Math.Abs(Convert.ToDouble(AxialNed.Text));
                EffectiveLengthInput = RoundEffectiveLength(Convert.ToDouble(AxialLeff.Text));
            }
            catch (Exception)
            {
                MessageBox.Show("Something weird is going on with your input data. Are you sure that you have entered numbers?");
            }


            string xmlData = Properties.Resources.HE_axial_compression;
            List<string> UBlistboxItemsOutput = new List<string>();
            UBlistboxItemsOutput = SectionSelecton.EvaluateSections(AxialForceInput, EffectiveLengthInput, xmlData);
            UBsectionsListBox.DataSource = UBlistboxItemsOutput;
        }


    }
}
