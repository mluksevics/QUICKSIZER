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
            //defining input parameters
            double AxialForceInput = 0;
            double EffectiveLengthInput = 0;
            
            //getting input parameters from the form, doing some basic checks
            try
            {
                AxialForceInput = Math.Abs(Convert.ToDouble(AxialNed.Text));
                EffectiveLengthInput = RoundEffectiveLength(Convert.ToDouble(AxialLeff.Text));
            }
            catch (Exception)
            {
                MessageBox.Show("Something weird is going on with your input data. Are you sure that you have entered numbers?");
            }

            // getting XML data from resources;
            string xmlDataHEA = Properties.Resources.HEA_NRd;
            string xmlDataHEB = Properties.Resources.HEB_NRd;
            string xmlDataIPE = Properties.Resources.IPE_NRd;
            string xmlDataUPE = Properties.Resources.UPE_UPN_NRd;
            string xmlDataUB = Properties.Resources.UB_NRd;
            string xmlDataUC = Properties.Resources.UC_NRd;
            string xmlDataUBP = Properties.Resources.UBP_NRd;
            string xmlDataPFC = Properties.Resources.PFC_NRd;
            string xmlDataSHS = Properties.Resources.SHS_NRd;
            string xmlDataCHS = Properties.Resources.CHS_NRd;
            string xmlDataEHS = Properties.Resources.EHS_NRd;
            string xmlDataRHS = Properties.Resources.RHS_NRd;
            string xmlDataAngle = Properties.Resources.Lequal_NRd;

            //defining ouput variables;
            List<string> HEAlistboxItemsOutput = new List<string>();
            List<string> HEBlistboxItemsOutput = new List<string>();
            List<string> IPElistboxItemsOutput = new List<string>();
            List<string> UPElistboxItemsOutput = new List<string>();
            List<string> UBlistboxItemsOutput = new List<string>();
            List<string> UClistboxItemsOutput = new List<string>();
            List<string> UBPlistboxItemsOutput = new List<string>();
            List<string> PFClistboxItemsOutput = new List<string>();
            List<string> SHSlistboxItemsOutput = new List<string>();
            List<string> CHSlistboxItemsOutput = new List<string>();
            List<string> RHSlistboxItemsOutput = new List<string>();
            List<string> EHSlistboxItemsOutput = new List<string>();
            List<string> AngleslistboxItemsOutput = new List<string>();

            //running the section selection;
            HEAlistboxItemsOutput = SectionSelecton.EvaluateSections(AxialForceInput, EffectiveLengthInput, xmlDataHEA);
            HEBlistboxItemsOutput = SectionSelecton.EvaluateSections(AxialForceInput, EffectiveLengthInput, xmlDataHEB);
            IPElistboxItemsOutput = SectionSelecton.EvaluateSections(AxialForceInput, EffectiveLengthInput, xmlDataIPE);
            UPElistboxItemsOutput = SectionSelecton.EvaluateSections(AxialForceInput, EffectiveLengthInput, xmlDataUPE);
            UBlistboxItemsOutput = SectionSelecton.EvaluateSections(AxialForceInput, EffectiveLengthInput, xmlDataUB);
            UClistboxItemsOutput = SectionSelecton.EvaluateSections(AxialForceInput, EffectiveLengthInput, xmlDataUC);
            UBPlistboxItemsOutput = SectionSelecton.EvaluateSections(AxialForceInput, EffectiveLengthInput, xmlDataUBP);
            PFClistboxItemsOutput = SectionSelecton.EvaluateSections(AxialForceInput, EffectiveLengthInput, xmlDataPFC);
            SHSlistboxItemsOutput = SectionSelecton.EvaluateSections(AxialForceInput, EffectiveLengthInput, xmlDataSHS);
            CHSlistboxItemsOutput = SectionSelecton.EvaluateSections(AxialForceInput, EffectiveLengthInput, xmlDataCHS);
            RHSlistboxItemsOutput = SectionSelecton.EvaluateSections(AxialForceInput, EffectiveLengthInput, xmlDataRHS);
            EHSlistboxItemsOutput = SectionSelecton.EvaluateSections(AxialForceInput, EffectiveLengthInput, xmlDataEHS);
            AngleslistboxItemsOutput = SectionSelecton.EvaluateSections(AxialForceInput, EffectiveLengthInput, xmlDataAngle);

            //output results to listbox
            HEAsectionsListBox.DataSource = HEAlistboxItemsOutput;
            HEBsectionsListBox.DataSource = HEBlistboxItemsOutput;
            IPEsectionsListBox.DataSource = IPElistboxItemsOutput;
            UPEsectionsListBox.DataSource = UPElistboxItemsOutput;
            UBsectionsListBox.DataSource = UBlistboxItemsOutput;
            UCsectionsListBox.DataSource = UClistboxItemsOutput;
            UBPsectionsListBox.DataSource = UBPlistboxItemsOutput;
            PFCsectionsListBox.DataSource = PFClistboxItemsOutput;
            SHSsectionsListBox.DataSource = SHSlistboxItemsOutput;
            CHSsectionsListBox.DataSource = CHSlistboxItemsOutput;
            RHSsectionsListBox.DataSource = RHSlistboxItemsOutput;
            EHSsectionsListBox.DataSource = EHSlistboxItemsOutput;
            AnglesSectionsListBox.DataSource = AngleslistboxItemsOutput;
        }

    }
}
