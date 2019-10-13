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

        public Term(string coefficient, int power)
        {
            this.power = power;
            this.coefficient = coefficient;
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

        public static Term operator *(Term t1, Term t2)
        {
            if (t1.coefficient == "0" || t2.coefficient == "0")
            {
                return new Term("0", 0);
            }

            if (t1.coefficient == "1")
            {
                return new Term(t2.coefficient, t1.Power + t2.Power);
            }

            if (t2.coefficient == "1")
            {
                return new Term(t1.coefficient, t1.power + t2.power);
            }

            int indT1 = t1.coefficient.IndexOf('a'),
                degreeT1 = Int32.Parse(t1.coefficient.Substring(indT1 + 1, t1.coefficient.Length - indT1 - 1)),
                indT2 = t2.coefficient.IndexOf('a'),
                degreeT2 = Int32.Parse(t2.coefficient.Substring(indT2 + 1, t2.coefficient.Length - indT2 - 1)),
                newDegree = (degreeT1 + degreeT2) % (int)(GF2m.ElementsCount - 1);

            if (newDegree == 0)
            {
                return new Term("1", t1.Power + t2.Power);
            }
            else
            {
                return new Term("a" + newDegree.ToString(), t1.Power + t2.Power);
            }

        }

        public static Term operator /(Term t1, Term t2)
        {
            if (t1.coefficient == "0")
            {
                return new Term("0", 0);
            }

            if (t2.coefficient == "0")
            {
                throw new ArithmeticException("divide by zero element");
            }

            if (t2.coefficient == "1")
            {
                return new Term(t1.coefficient, t1.power - t2.power);
            }

            if (t1.coefficient == "1")
            {
                int ind = t2.coefficient.IndexOf('a'),
                    degree = Int32.Parse(t2.coefficient.Substring(ind + 1, t2.coefficient.Length - ind - 1));
                return new Term("a" + (GF2m.ElementsCount - degree - 1).ToString(), t1.power - t2.power);
            }

            

            int indT1 = t1.coefficient.IndexOf('a'),
                degreeT1 = Int32.Parse(t1.coefficient.Substring(indT1 + 1, t1.coefficient.Length - indT1 - 1)),
                indT2 = t2.coefficient.IndexOf('a'),
                degreeT2 = Int32.Parse(t2.coefficient.Substring(indT2 + 1, t2.coefficient.Length - indT2 - 1)),
                newDegree = degreeT1 - degreeT2;

            if (newDegree < 0)
            {
                newDegree = (int)GF2m.ElementsCount - 1 + newDegree;
            }

            if (newDegree == 0)
            {
                return new Term("1", t1.Power - t2.Power);
            }
            else
            {
                return new Term("a" + newDegree.ToString(), t1.Power - t2.Power);
            }
        }

        public static Term operator +(Term t1, Term t2)
        {
            uint newCoefficientValue = GF2m.Elements[t1.coefficient] ^ GF2m.Elements[t2.coefficient];
            string newCoefficient = String.Empty;

            foreach (KeyValuePair<string, uint> element in GF2m.Elements)
            {
                if (newCoefficientValue == element.Value)
                {
                    newCoefficient = element.Key;
                    break;
                }
            }

            return new Term(newCoefficient, t1.Power);
        }

        public static Term operator -(Term t1, Term t2)
        {
            return t1 + t2;
        }

        public override string ToString()
        {
            string result = String.Empty;

            if (coefficient == "1")
            {
                if (power == 0)
                {
                    result = coefficient;
                }
                else if (power == 1)
                {
                    result = "x";
                }
                else
                {
                    result = "x^" + power.ToString();
                }
            }
            else
            {
                if (power == 0)
                {
                    result = coefficient;
                }
                else if (power == 1)
                {
                    result = coefficient + "*x";
                }
                else
                {
                    result = coefficient + "*x^" + power.ToString();
                }
            }

            return result;
        }
    }
}
