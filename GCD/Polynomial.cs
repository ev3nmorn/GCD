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
            terms.Add(new Term("0", 0));
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
                    i--;
                }
            }

            if (terms.Count == 0)
            {
                terms.Add(new Term("0", 0));
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

        public static Polynomial GCD(Polynomial p1, Polynomial p2)
        {
            List<Polynomial> row1 = new List<Polynomial>() { new Polynomial("1".Split('+')), new Polynomial(), p1 },
                row2 = new List<Polynomial>() { new Polynomial(), new Polynomial("1".Split('+')), p2 },
                temp;
            Polynomial q;

            while (row2[2].terms[0].Coefficient != "0")
            {
                temp = new List<Polynomial>() { new Polynomial(row2[0].terms), new Polynomial(row2[1].terms), new Polynomial(row2[2].terms) };

                q = row1[2] / row2[2];
                row2[0] = row1[0] - (row2[0] * q);
                row2[1] = row1[1] - (row2[1] * q);
                row2[2] = row1[2] - (row2[2] * q);
                row1 = new List<Polynomial>() { new Polynomial(temp[0].terms), new Polynomial(temp[1].terms), new Polynomial(temp[2].terms) };

            }

            return row1[2];
        }

        public static Polynomial MultiGCD(List<Polynomial> polinomials)
        {
            Polynomial result = new Polynomial();

            for (int i = 0; i < polinomials.Count - 1; i++)
            {
                result = (i == 0) ? GCD(polinomials[i], polinomials[i + 1]) : GCD(result, polinomials[i + 1]);
            }

            return result;
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
                if (p1.terms.Count == 1 && p1.terms[0].Coefficient == "0")
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
