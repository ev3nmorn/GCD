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
        Dictionary<string, uint> field_elements;

        public Term(string coefficient, int power, Dictionary<string, uint> field_elements)
        {
            this.power = power;
            this.coefficient = coefficient;
            this.field_elements = new Dictionary<string, uint>(field_elements);
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
                return field_elements;
            }
        }

        public static Term operator *(Term t1, Term t2)
        {
            if (t1.Coefficient == "1")
                return new Term(t2.Coefficient, t1.Power + t2.Power, t2.field_elements);

            if (t2.Coefficient == "1")
                return new Term(t1.Coefficient, t1.Power + t2.Power, t1.field_elements);

            int ind_t1 = t1.Coefficient.IndexOf('a'),
                degree_t1 = Int32.Parse(t1.Coefficient.Substring(ind_t1 + 1, t1.Coefficient.Length - ind_t1 - 1)),
                ind_t2 = t2.Coefficient.IndexOf('a'),
                degree_t2 = Int32.Parse(t2.Coefficient.Substring(ind_t2 + 1, t2.Coefficient.Length - ind_t2 - 1)),
                new_degree = (degree_t1 + degree_t2) % (t1.field_elements.Count - 1);

            if (new_degree == 0)
                return new Term("1", t1.Power + t2.Power, t1.field_elements);
            else
                return new Term("a" + new_degree.ToString(), t1.Power + t2.Power, t1.field_elements);

        }

        public static Term operator /(Term t1, Term t2)
        {
            if (t2.Coefficient == "1")
                return new Term(t1.Coefficient, t1.Power - t2.Power, t1.field_elements);

            int ind_t1 = t1.Coefficient.IndexOf('a'),
                degree_t1 = Int32.Parse(t1.Coefficient.Substring(ind_t1 + 1, t1.Coefficient.Length - ind_t1 - 1)),
                ind_t2 = t2.Coefficient.IndexOf('a'),
                degree_t2 = Int32.Parse(t2.Coefficient.Substring(ind_t2 + 1, t2.Coefficient.Length - ind_t2 - 1)),
                new_degree = degree_t1 - degree_t2;

            if (new_degree < 0)
                new_degree = t1.field_elements.Count - 1 + new_degree;

            if (new_degree == 0)
                return new Term("1", t1.Power - t2.Power, t1.field_elements);
            else
                return new Term("a" + new_degree.ToString(), t1.Power - t2.Power, t1.field_elements);

        }
    }
}
