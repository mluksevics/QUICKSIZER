using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUICKSIZER
{
    // Simple business object. A PartId is used to identify the type of part 
    // but the part name can change. 
    public class SectionData : IEquatable<SectionData>, IComparable<SectionData>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public double EffectiveLength { get; set; }
        public double NRd { get; set; }
        public double MRd { get; set; }
        public double VRd { get; set; }
        public double uDeflection { get; set; }
        public double N_utilisation { get; set; }
        public double M_utilisation { get; set; }
        public double V_utilisation { get; set; }
        public double Total_utilisation { get; set; }
        public double Governing { get; set; }

        public override string ToString()
        {
            return N_utilisation*100 +  "% " + Name + " | " + Weight + "kg L.eff=" + EffectiveLength + "m N.Rd=" +NRd +"kN";

            //100% UB456x456x056 L.eff=5m N.Rd=34000kN
        }

        public string AxialOutput()
        {
            return N_utilisation * 100 + "% " + Name + " | " + Weight + "kg L.eff=" + EffectiveLength + "m N.Rd=" + NRd + "kN";

            //100% UB456x456x056 L.eff=5m N.Rd=34000kN
        }


        public string BendingOutput()
        {
            return Total_utilisation * 100 + "% (" + Governing + ") " + Name + " | " + Weight + "kg L.eff=" + EffectiveLength + "m u.defl=" + uDeflection + "mm M.Rd=" + MRd + "kNm V.Rd="+ VRd;

            //100% UB456x456x056 L.eff=5m N.Rd=34000kN
        }


        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            SectionData objAsPart = obj as SectionData;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }

        // Default comparer for Part type.
        public int CompareTo(SectionData comparePart)
        {
            // A null value means that this object is greater.
            if (comparePart == null)
                return 1;

            else
                return this.Weight.CompareTo(comparePart.Weight);
        }
        public override int GetHashCode()
        {
            return Id;
        }
        public bool Equals(SectionData other)
        {
            if (other == null) return false;
            return (this.Id.Equals(other.Id));
        }

    }
}
