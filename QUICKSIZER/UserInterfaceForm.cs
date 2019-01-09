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

            if (!SectionSelecton.CheckEffectiveLength(EffectiveLengthInput))
            {
                return;
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

        private void button3_Click(object sender, EventArgs e)
        {
            //defining input parameters
            double BendingMomentInput = 0;
            double ShearForceInput = 0;
            double UDL_SLSInput = 0;
            double SpanInput = 0;
            double AllowableDeflectionInput = 0;


            //getting input parameters from the form, doing some basic checks
            try
            {
                BendingMomentInput = Convert.ToDouble(BeamMEd.Text.Split(' ')[0]); //split field contents using space and separate units
                ShearForceInput = Convert.ToDouble(BeamVd.Text.Split(' ')[0]); //split field contents using space and separate units
                UDL_SLSInput = Convert.ToDouble(BeamUDLatSLS.Text);
                SpanInput = Convert.ToDouble(BeamSpan.Text);
                AllowableDeflectionInput = Convert.ToDouble(BeamDeflectionLimit.Text.Split(' ')[0]); //split field contents using space and separate units
            }
            catch (Exception)
            {
                MessageBox.Show("Something weird is going on with your input data. Are you sure that you have entered numbers?");
            }

            // getting XML data from resources;
            string xmlDataHEA = Properties.Resources.HEA_NRd1;

            //defining ouput variables;
            List<string> HEAlistboxItemsOutput = new List<string>();

            //running the section selection;
            HEAlistboxItemsOutput = SectionSelecton.EvaluateBending(BendingMomentInput, ShearForceInput, UDL_SLSInput, SpanInput, AllowableDeflectionInput, xmlDataHEA);

            //output results to listbox
            listBox4.DataSource = HEAlistboxItemsOutput;
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

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (BeamUDLatULS.Text == "")
                {
                    return;
                }

                double BendingUDLInput = Math.Abs(Convert.ToDouble(BeamUDLatULS.Text));
                double BendingLengthInput = Convert.ToDouble(BeamSpan.Text);

                BeamUDLatSLS.Text = Math.Round(BendingUDLInput / 1.35, 1).ToString(); // divide by 1.35 to get SLS UDL
                BeamMEd.Text = Math.Round((BendingUDLInput * BendingLengthInput * BendingLengthInput) / 8, 1).ToString() + " kNm";
                BeamVd.Text = Math.Round((BendingUDLInput * BendingLengthInput) / 2, 1).ToString() + " kN";
            }
            catch (Exception)
            {
                MessageBox.Show("Something weird is going on with your input data. Are you sure that you have entered numbers?");
            }



        }


        private void BeamUDLatSLS_TextChanged(object sender, EventArgs e)
        {


        }

        private void BeamDeflectionLimitRatio_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (BeamDeflectionLimitRatio.Text == "")
                {
                    return;
                }

                double BendingLengthInput = Convert.ToDouble(BeamSpan.Text);
                double BendingDeflectionLimitRatio = Convert.ToDouble(BeamDeflectionLimitRatio.Text);

                BeamDeflectionLimit.Text = Math.Round((BendingLengthInput / BendingDeflectionLimitRatio) * 1000, 0).ToString() + " mm";
            }
            catch (Exception)
            {
                MessageBox.Show("Something weird is going on with your input data. Are you sure that you have entered numbers?");
            }


        }

        private void BeamSpan_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (BeamSpan.Text == "") return;

                double BendingUDLInput = Math.Abs(Convert.ToDouble(BeamUDLatULS.Text));
                double BendingLengthInput = Math.Abs(Convert.ToDouble(BeamSpan.Text));
                double BendingDeflectionLimitRatio = Convert.ToDouble(BeamDeflectionLimitRatio.Text);

                if (BendingLengthInput > 14)
                {
                    MessageBox.Show("Wow! Looks like your beam is really long. Sorry, I can only deal with lenght up to 14m.");
                    return;
                };

                BeamMEd.Text = Math.Round((BendingUDLInput * BendingLengthInput * BendingLengthInput) / 8, 1).ToString() + " kNm";
                BeamVd.Text = Math.Round((BendingUDLInput * BendingLengthInput) / 2, 1).ToString() + " kN";
                BeamDeflectionLimit.Text = Math.Round((BendingLengthInput / BendingDeflectionLimitRatio) * 1000, 0).ToString() + " mm";

            }
            catch (Exception)
            {
                MessageBox.Show("Something weird is going on with your input data. Are you sure that you have entered numbers?");
            }


        }
    }
}
