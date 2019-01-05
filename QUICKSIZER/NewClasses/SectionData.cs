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
        public double Capacity { get; set; }
        public double Utilisation { get; set; }

        public override string ToString()
        {
            return Utilisation*100 +  "% " + Name + " | " + Weight + "kg L.eff=" + EffectiveLength + "m N.Rd=" +Capacity +"kN";

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
