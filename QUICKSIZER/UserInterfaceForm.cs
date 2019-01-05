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
            double AxialForce = 0;
            double EffectiveLength = 0;
            try
            {
                AxialForce = Math.Abs(Convert.ToDouble(AxialNed.Text));
                EffectiveLength = RoundEffectiveLength(Convert.ToDouble(AxialLeff.Text));
            }
            catch (Exception)
            {
                MessageBox.Show("Something weird is going on with your input data. Are you sure that you have entered numbers?");
            }


            //defining a list with sections
            List<SectionData> sectionsList = new List<SectionData>();

            // loading the XML document
            string xmlData = Properties.Resources.HE_axial_compression;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlData);

            // processing XML node-by-node
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                // reading from XML and assigning to variables
                string section = node.ChildNodes[0].InnerText;
                double weight = Convert.ToDouble( node.ChildNodes[1].InnerText);
                double Leff = Convert.ToDouble( node.ChildNodes[2].InnerText);
                double capacity = Convert.ToDouble( node.ChildNodes[3].InnerText);

                //if capacity refers to wrong effective length, ignore the row
                if (EffectiveLength != Leff)
                {
                    continue;
                }
                
                //calculate utilisation ratio and ignore all sections that are over 100%
                if (AxialForce/capacity > 1)
                {
                    continue;
                }

                // creating a new SectionData object and adding to the list if data contains relevant effective length;
                sectionsList.Add(new SectionData() {
                    Name = section,
                    Weight = weight,
                    EffectiveLength = Leff,
                    Capacity = capacity,
                Utilisation = Math.Round(AxialForce / capacity, 2)
                });

            }
            
            //sort list of sections by utilisation (see class SectionData definition)
            sectionsList.Sort();

            // output information into ListBox
            List<string> listboxItems = new List<string>();

            foreach (SectionData section in sectionsList)
            {
                listboxItems.Add(section.ToString());
            }
            /*
            for (int i = 0; i < 20; i++)
            {
                listboxItems.Add(sectionsList[i].ToString());
            }
            */
            UBsectionsListBox.DataSource = listboxItems;



        }


    }
}
