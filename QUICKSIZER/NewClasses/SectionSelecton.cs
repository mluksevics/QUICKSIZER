using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;

namespace QUICKSIZER
{
    class SectionSelecton
    {
        //transforming Effective Length to one of those used in tables
        public static double RoundEffectiveLength(double inputLength)
        {
            double roundedLength = 0;
            if (inputLength <= 0.5)
            {
                roundedLength = 1;
            }

            else if (inputLength < 4)
            {
                double remainder = inputLength % 0.5;
                if (remainder != 0)
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

        public static bool CheckEffectiveLength(double inputLength)
        {
            bool check = true;
            if (inputLength > 14)
            {
                MessageBox.Show("Wow! Looks like your element is really long. Sorry, I can only deal with lenght up to 14m.");
                check = false;
            }
            else if (inputLength <= 0)
            {
                MessageBox.Show("Hey! You are trying to kill me! No negative values allowed for length input.");
                check = false;
            }
            return check;
        }

        public static List<string> EvaluateAxialForce(double AxialForce, double EffectiveLength, string xmlSectionData)
        {
            double EffectiveLengthRounded = RoundEffectiveLength(EffectiveLength);

            //defining a list with sections
            List<SectionData> sectionsList = new List<SectionData>();

            // loading the XML data
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlSectionData);

            // processing XML node-by-node
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                // reading from XML and assigning to variables
                string section = node.ChildNodes[0].InnerText;
                double weight = Convert.ToDouble(node.ChildNodes[1].InnerText);
                double Leff = Convert.ToDouble(node.ChildNodes[2].InnerText);
                double NRd = Convert.ToDouble(node.ChildNodes[3].InnerText);

                //if capacity refers to wrong effective length, ignore the row
                if (EffectiveLengthRounded != Leff)
                {
                    continue;
                }

                //calculate utilisation ratio and ignore all sections that are over 100%
                if (AxialForce / NRd > 1)
                {
                    continue;
                }

                // creating a new SectionData object and adding to the list if data contains relevant effective length;
                sectionsList.Add(new SectionData()
                {
                    Name = section,
                    Weight = weight,
                    EffectiveLength = Leff,
                    NRd = NRd,
                    N_utilisation = Math.Round(AxialForce / NRd, 2)
                });

            }

            //sort list of sections by utilisation (see class SectionData definition)
            sectionsList.Sort();

            // output information into ListBox
            List<string> listboxItems = new List<string>();

            foreach (SectionData section in sectionsList)
            {
                listboxItems.Add(section.AxialOutput());
            }

            return listboxItems;

        }

        public static List<string> EvaluateBending(double BendingMoment, double ShearForce, double UDL_SLS, double BeamSpan, double AllowableDeflection, string xmlSectionData)
        {
            // rounding effective length 
            double BeamSpanRounded = RoundEffectiveLength(BeamSpan);

            //defining a list with sections
            List<SectionData> sectionsList = new List<SectionData>();

            // loading the XML data
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlSectionData);

            // processing XML node-by-node
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                // reading from XML and assigning to variables
                string section = node.ChildNodes[0].InnerText;
                double weight = Convert.ToDouble(node.ChildNodes[1].InnerText);
                double Leff = Convert.ToDouble(node.ChildNodes[2].InnerText);
                double MRd = Convert.ToDouble(node.ChildNodes[4].InnerText);
                double VRd = Convert.ToDouble(node.ChildNodes[5].InnerText);
                double Inertia = Convert.ToDouble(node.ChildNodes[6].InnerText);

                // calculating deflection
                //UDL as kN/m, Inertia as cm4, results in milimeters
                double deflection = 1000 * (((5 / 384) * UDL_SLS * Math.Pow(BeamSpan,4)) / (2.1 * Inertia));

                // calculating utilisations
                double momentUtilisation = Math.Round(BendingMoment / MRd,2);
                double shearUtilisation = Math.Round(ShearForce / VRd, 2);
                double deflectionUtilisation = Math.Round(deflection / AllowableDeflection, 2);
                double totalUtilisation;
                string governingAction;

                //if capacity refers to wrong effective length, ignore the row
                if (BeamSpanRounded != Leff) continue;

                //calculate  utilisation ratios and ignore all sections that are over 100%
                if ( deflectionUtilisation > 1) continue;
                if ( momentUtilisation > 1)  continue;
                if (shearUtilisation > 1) continue;

                //determining governing effect and total utilisation
                if (deflectionUtilisation >= momentUtilisation && deflectionUtilisation >= shearUtilisation)
                {
                    totalUtilisation = deflectionUtilisation;
                    governingAction = "Defl.";
                }
                else if (momentUtilisation >= deflectionUtilisation && momentUtilisation >= shearUtilisation)
                {
                    totalUtilisation = momentUtilisation;
                    governingAction = "M.Ed";
                }
                else
                {
                    totalUtilisation = shearUtilisation;
                    governingAction = "V.Ed";
                }

                // creating a new SectionData object and adding to the list if data contains relevant effective length;
                sectionsList.Add(new SectionData()
                {
                    Name = section,
                    Weight = weight,
                    EffectiveLength = Leff,
                    MRd = MRd,
                    VRd = VRd,
                    uDeflection = deflection,
                    M_utilisation = momentUtilisation,
                    V_utilisation = shearUtilisation,
                    Total_utilisation = totalUtilisation,
                    Governing = governingAction
                });

            }

            //sort list of sections by utilisation (see class SectionData definition)
            sectionsList.Sort();

            // output information into ListBox
            List<string> listboxItems = new List<string>();

            foreach (SectionData section in sectionsList)
            {
                listboxItems.Add(section.BendingOutput());
            }

            return listboxItems;

        }

    }
}
