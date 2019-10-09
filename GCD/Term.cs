using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCD
{
    class Term
    {
        int power;
        string coefficient;
        Dictionary<string, uint> fieldElements;

        public Term(string coefficient, int power, Dictionary<string, uint> fieldElements)
        {
            this.power = power;
            this.coefficient = coefficient;
            this.fieldElements = new Dictionary<string, uint>(fieldElements);
        }

        public int Power
        {
            get
            {
                return power;
            }
        }

        public string Coefficient
        {
            get
            {
                return coefficient;
            }
        }

        public Dictionary<string, uint> FieldElements
        {
            get
            {
                return fieldElements;
            }
        }

        public static Term operator *(Term t1, Term t2)
        {
            if (t1.Coefficient == "1")
                return new Term(t2.Coefficient, t1.Power + t2.Power, t2.fieldElements);

            if (t2.Coefficient == "1")
                return new Term(t1.Coefficient, t1.Power + t2.Power, t1.fieldElements);

            int indT1 = t1.Coefficient.IndexOf('a'),
                degreeT1 = Int32.Parse(t1.Coefficient.Substring(indT1 + 1, t1.Coefficient.Length - indT1 - 1)),
                indT2 = t2.Coefficient.IndexOf('a'),
                degreeT2 = Int32.Parse(t2.Coefficient.Substring(indT2 + 1, t2.Coefficient.Length - indT2 - 1)),
                newDegree = (degreeT1 + degreeT2) % (t1.fieldElements.Count - 1);

            if (newDegree == 0)
                return new Term("1", t1.Power + t2.Power, t1.fieldElements);
            else
                return new Term("a" + newDegree.ToString(), t1.Power + t2.Power, t1.fieldElements);

        }

        public static Term operator /(Term t1, Term t2)
        {
            if (t2.Coefficient == "1")
                return new Term(t1.Coefficient, t1.Power - t2.Power, t1.fieldElements);

            int indT1 = t1.Coefficient.IndexOf('a'),
                degreeT1 = Int32.Parse(t1.Coefficient.Substring(indT1 + 1, t1.Coefficient.Length - indT1 - 1)),
                indT2 = t2.Coefficient.IndexOf('a'),
                degreeT2 = Int32.Parse(t2.Coefficient.Substring(indT2 + 1, t2.Coefficient.Length - indT2 - 1)),
                newDegree = degreeT1 - degreeT2;

            if (newDegree < 0)
                newDegree = t1.fieldElements.Count - 1 + newDegree;

            if (newDegree == 0)
                return new Term("1", t1.Power - t2.Power, t1.fieldElements);
            else
                return new Term("a" + newDegree.ToString(), t1.Power - t2.Power, t1.fieldElements);
        }

        public static Term operator +(Term t1, Term t2)
        {
            uint newCoefficientValue = t1.FieldElements[t1.Coefficient] ^ t2.FieldElements[t2.Coefficient];
            string newCoefficient = String.Empty;

            foreach (KeyValuePair<string, uint> element in t1.FieldElements)
            {
                if (newCoefficientValue == element.Value)
                    newCoefficient = element.Key;
            }

            return new Term(newCoefficient, t1.Power, t1.FieldElements);
        }
    }
}
