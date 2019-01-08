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



        private void button1_Click(object sender, EventArgs e)
        {
            //defining input parameters
            double AxialForceInput = 0;
            double EffectiveLengthInput = 0;
            
            //getting input parameters from the form, doing some basic checks
            try
            {
                AxialForceInput = Math.Abs(Convert.ToDouble(AxialNed.Text));
                EffectiveLengthInput = Convert.ToDouble(AxialLeff.Text);
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
            HEAlistboxItemsOutput = SectionSelecton.EvaluateAxialForce(AxialForceInput, EffectiveLengthInput, xmlDataHEA);
            HEBlistboxItemsOutput = SectionSelecton.EvaluateAxialForce(AxialForceInput, EffectiveLengthInput, xmlDataHEB);
            IPElistboxItemsOutput = SectionSelecton.EvaluateAxialForce(AxialForceInput, EffectiveLengthInput, xmlDataIPE);
            UPElistboxItemsOutput = SectionSelecton.EvaluateAxialForce(AxialForceInput, EffectiveLengthInput, xmlDataUPE);
            UBlistboxItemsOutput = SectionSelecton.EvaluateAxialForce(AxialForceInput, EffectiveLengthInput, xmlDataUB);
            UClistboxItemsOutput = SectionSelecton.EvaluateAxialForce(AxialForceInput, EffectiveLengthInput, xmlDataUC);
            UBPlistboxItemsOutput = SectionSelecton.EvaluateAxialForce(AxialForceInput, EffectiveLengthInput, xmlDataUBP);
            PFClistboxItemsOutput = SectionSelecton.EvaluateAxialForce(AxialForceInput, EffectiveLengthInput, xmlDataPFC);
            SHSlistboxItemsOutput = SectionSelecton.EvaluateAxialForce(AxialForceInput, EffectiveLengthInput, xmlDataSHS);
            CHSlistboxItemsOutput = SectionSelecton.EvaluateAxialForce(AxialForceInput, EffectiveLengthInput, xmlDataCHS);
            RHSlistboxItemsOutput = SectionSelecton.EvaluateAxialForce(AxialForceInput, EffectiveLengthInput, xmlDataRHS);
            EHSlistboxItemsOutput = SectionSelecton.EvaluateAxialForce(AxialForceInput, EffectiveLengthInput, xmlDataEHS);
            AngleslistboxItemsOutput = SectionSelecton.EvaluateAxialForce(AxialForceInput, EffectiveLengthInput, xmlDataAngle);

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

        private void button2_Click(object sender, EventArgs e)
        {
            AboutForm f2 = new AboutForm(); 
            f2.ShowDialog();
        }

        private void label48_Click(object sender, EventArgs e)
        {

        }

        private void label52_Click(object sender, EventArgs e)
        {

        }
    }
}
