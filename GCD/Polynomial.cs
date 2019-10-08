using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GCD
{
    class Polynomial
    {
        /*List<Term> terms;
        Dictionary<string, List<int>> field_elements;

        public Polynomial(List<Term> terms, Dictionary<string, List<int>> field_elements)
        {
            this.terms = new List<Term>(terms);
            this.field_elements = new Dictionary<string, List<int>>(field_elements);
        }

        public Polynomial(string[] terms, Dictionary<string, List<int>> field_elements)
        {
            string coefficient, powerStr;
            this.terms = new List<Term>();
            this.field_elements = new Dictionary<string, List<int>>(field_elements);

            foreach (string term in terms)
            {
                if (term.IndexOf("x^") > -1)
                {
                    powerStr = term.Substring(term.IndexOf("x^") + 2, term.Length - term.IndexOf("x^") - 2);

                    if (term.Contains('*'))
                        coefficient = term.Substring(0, term.IndexOf("*x^"));
                    else
                        coefficient = term.Substring(0, term.IndexOf("x^"));


                    if (coefficient == String.Empty)
                        this.terms.Add(new Term("1", Int32.Parse(powerStr), field_elements));
                    else
                        this.terms.Add(new Term(coefficient, Int32.Parse(powerStr), field_elements));
                }
                else if (term.IndexOf("x") > -1)
                {
                    if (term.Contains('*'))
                        coefficient = term.Substring(0, term.IndexOf("*x"));
                    else
                        coefficient = term.Substring(0, term.IndexOf("x"));

                    if (coefficient == String.Empty)
                        this.terms.Add(new Term("1", 1, field_elements));
                    else
                        this.terms.Add(new Term(coefficient, 1, field_elements));
                }
                else
                    this.terms.Add(new Term(term, 0, field_elements));
            }
        }*/
    }
}
