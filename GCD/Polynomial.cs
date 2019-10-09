using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCD
{
    class Polynomial
    {
        List<Term> terms;
        Dictionary<string, uint> fieldElements;

        public Polynomial(List<Term> terms, Dictionary<string, uint> fieldElements)
        {
            this.terms = new List<Term>(terms);
            this.fieldElements = new Dictionary<string, uint>(fieldElements);
        }

        public Polynomial(string[] terms, Dictionary<string, uint> fieldElements)
        {
            string coefficient, powerStr;
            this.terms = new List<Term>();
            this.fieldElements = new Dictionary<string, uint>(fieldElements);

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
                        this.terms.Add(new Term("1", Int32.Parse(powerStr), fieldElements));
                    else
                        this.terms.Add(new Term(coefficient, Int32.Parse(powerStr), fieldElements));
                }
                else if (term.IndexOf("x") > -1)
                {
                    if (term.Contains('*'))
                        coefficient = term.Substring(0, term.IndexOf("*x"));
                    else
                        coefficient = term.Substring(0, term.IndexOf("x"));

                    if (coefficient == String.Empty)
                        this.terms.Add(new Term("1", 1, fieldElements));
                    else
                        this.terms.Add(new Term(coefficient, 1, fieldElements));
                }
                else
                    this.terms.Add(new Term(term, 0, fieldElements));
            }
        }
    }
}
