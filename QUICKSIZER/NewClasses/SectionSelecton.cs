using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace QUICKSIZER
{
    class SectionSelecton
    {
        public static List<string> EvaluateSections(double AxialForce, double EffectiveLength, string xmlSectionData)
        {

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
                double capacity = Convert.ToDouble(node.ChildNodes[3].InnerText);

                //if capacity refers to wrong effective length, ignore the row
                if (EffectiveLength != Leff)
                {
                    continue;
                }

                //calculate utilisation ratio and ignore all sections that are over 100%
                if (AxialForce / capacity > 1)
                {
                    continue;
                }

                // creating a new SectionData object and adding to the list if data contains relevant effective length;
                sectionsList.Add(new SectionData()
                {
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
            return listboxItems;

        }
    }
}
