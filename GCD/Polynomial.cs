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

        public Polynomial()
        {
            terms = new List<Term>();
        }

        public Polynomial(List<Term> terms)
        {
            this.terms = new List<Term>(terms);
            DeleteZeroElements();
            SortByPower();
        }

        public Polynomial(string[] terms)
        {
            string coefficient, powerStr;
            this.terms = new List<Term>();

            foreach (string term in terms)
            {
                if (term.IndexOf("x^") > -1)
                {
                    powerStr = term.Substring(term.IndexOf("x^") + 2, term.Length - term.IndexOf("x^") - 2);

                    if (term.Contains('*'))
                    {
                        coefficient = term.Substring(0, term.IndexOf("*x^"));
                    }
                    else
                    {
                        coefficient = term.Substring(0, term.IndexOf("x^"));
                    }


                    if (coefficient == String.Empty)
                    {
                        this.terms.Add(new Term("1", Int32.Parse(powerStr)));
                    }
                    else
                    {
                        this.terms.Add(new Term(coefficient, Int32.Parse(powerStr)));
                    }
                }
                else if (term.IndexOf("x") > -1)
                {
                    if (term.Contains('*'))
                    {
                        coefficient = term.Substring(0, term.IndexOf("*x"));
                    }
                    else
                    {
                        coefficient = term.Substring(0, term.IndexOf("x"));
                    }

                    if (coefficient == String.Empty)
                    {
                        this.terms.Add(new Term("1", 1));
                    }
                    else
                    {
                        this.terms.Add(new Term(coefficient, 1));
                    }
                }
                else
                {
                    this.terms.Add(new Term(term, 0));
                }
            }

            DeleteZeroElements();
            SortByPower();
        }

        private void SortByPower()
        {
            terms = terms.OrderBy(o => o.Power).ToList();
            terms.Reverse();
        }

        private void DeleteZeroElements()
        {
            for (int i = 0; i < terms.Count; i++)
            {
                if (terms[i].Coefficient == "0")
                {
                    terms.Remove(terms[i]);
                }
            }
        }

        private bool HasEqualPower(Term term)
        {
            foreach (Term t in terms)
            {
                if (t.Power == term.Power)
                {
                    return true;
                }
            }

            return false;
        }

        private void AddToEqualPower(Term term)
        {
            for (int i = 0; i < terms.Count; i++)
            {
                if (terms[i].Power == term.Power)
                {
                    terms[i] += term;
                }
            }
        }

        private void AddUnique(Term term)
        {
            if (HasEqualPower(term))
            {
                AddToEqualPower(term);
            }
            else
            {
                terms.Add(term);
            }
        }

        public static Polynomial operator *(Polynomial p1, Polynomial p2)
        {
            Polynomial result = new Polynomial();

            foreach (Term t1 in p1.terms)
            {
                foreach (Term t2 in p2.terms)
                {
                    result.AddUnique(t1 * t2);
                }
            }

            result.DeleteZeroElements();
            result.SortByPower();

            return result;
        }

        public static Polynomial operator /(Polynomial p1, Polynomial p2)
        {
            List<Term> result = new List<Term>();
            Term nextResultTerm;
            Polynomial temp;

            while (p1.terms[0].Power >= p2.terms[0].Power)
            {
                nextResultTerm = p1.terms[0] / p2.terms[0];
                result.Add(nextResultTerm);
                temp = new Polynomial(new List<Term>() { nextResultTerm }) * p2;

                p1 = p1 - temp;
                if (p1.terms.Count == 0)
                {
                    break;
                }
            }

            return new Polynomial(result);
        }

        public static Polynomial operator +(Polynomial p1, Polynomial p2)
        {
            Polynomial result = new Polynomial(p1.terms);

            foreach (Term term in p2.terms)
            {
                result.AddUnique(term);
            }

            result.DeleteZeroElements();
            result.SortByPower();

            return result;
        }

        public static Polynomial operator -(Polynomial p1, Polynomial p2)
        {
            return p1 + p2;
        }

        public override string ToString()
        {
            string result = String.Empty;

            foreach (Term term in terms)
            {
                result += term.ToString() + "+";
            }

            return result.Remove(result.Length - 1, 1);
        }
    }
}
