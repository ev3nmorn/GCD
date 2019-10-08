using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GCD
{
    class Program
    {
        static uint BinaryStrToUInt(string str)
        {
            uint result = 0,
                temp = 1;

            for (int i = str.Length; i > 0; i--)
            {
                if (str[i - 1] == '1')
                    result += temp;
                temp *= 2;
            }

            return result;
        }

        static bool CheckTerms(string[] terms, Dictionary<string, uint> elements)
        {
            Regex regex1 = new Regex(@".+(?=(\*x(\^[0-9]+)?))"),
                regex2 = new Regex(@"^x(\^[0-9]+)?");

            foreach (string term in terms)
            {
                if (regex1.IsMatch(term))
                {
                    if (!elements.ContainsKey(regex1.Match(term).ToString()))
                        return false;
                    continue;
                }

                if (regex2.IsMatch(term))
                    continue;

                if (!elements.ContainsKey(term))
                    return false;
            }

            return true;
        }

        static void Main(string[] args)
        {
            Console.Write("Enter irreducible polynomial: ");
            string irreduciblePolynomialStr = Console.ReadLine();
            if (!new Regex("^1[0,1]*1$").IsMatch(irreduciblePolynomialStr) ||
                irreduciblePolynomialStr.Length > 32)
            {
                Console.WriteLine("Invalid input");
                return;
            }

            GF2m field = new GF2m(BinaryStrToUInt(irreduciblePolynomialStr));
            field.PrintField();
                      
            Console.Write("Enter number of polynomials: ");
            string nStr = Console.ReadLine();
            int n;
            if (!Int32.TryParse(nStr, out n))
            {
                Console.WriteLine("Invalid input");
                return;
            }

            Console.WriteLine("Enter " + n + " polynomials:");
            string polynomial;
            string[] terms;
            List<Polynomial> polynomials = new List<Polynomial>();
            for (int i = 0; i < 1; ++i)
            {
                polynomial = Console.ReadLine();
                terms = polynomial.Split('+');
                if (!CheckTerms(terms, field.Elements))
                {
                    Console.WriteLine("Invalid input");
                    return;
                }
                polynomials.Add(new Polynomial(terms, field.Elements));
            }

            //Term t = (new Term("a2", 5, field.Elements)) / (new Term("a10", 3, field.Elements));
            //Console.WriteLine(t.Coefficient + " " + t.Power);

            Console.ReadLine();
        }
    }
}
