using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCD
{
    public static class GF2m
    {
        static Dictionary<string, uint> elements;
        static uint fieldMod, bitsPerElement, highBitPoly; 

        private static uint GetHighBit(uint value)
        {
            uint result = 2147483648;

            while (value < result)
            {
                result >>= 1;
            }

            return result;
        }

        private static uint ElementsMult(uint op1, uint op2)
        {
            uint result = 0;
            uint hiBitSet;

            for (int i = 0; i < bitsPerElement; i++)
            {
                if ((op2 & 1) != 0)
                {
                    result ^= op1;
                }
                hiBitSet = op1 & (highBitPoly >> 1);
                op1 <<= 1;
                if (hiBitSet != 0)
                {
                    op1 ^= fieldMod;
                }
                op2 >>= 1;
            }
            return result & (highBitPoly - 1);
        }

        private static string ElementToBitStr(uint value)
        {
            string result = String.Empty;
            uint mask = highBitPoly >> 1;

            for (int i = 0; i < bitsPerElement; i++)
            {
                result += ((mask & value) != 0) ? "1" : "0";
                mask >>= 1;
            }

            return result;
        }
        
        public static void Build(uint irreduciblePolynomial)
        {
            uint primitiveElement = 2,
                fieldElement;

            highBitPoly = GetHighBit(irreduciblePolynomial);
            bitsPerElement = (uint)Math.Log(highBitPoly, 2);
            fieldMod = irreduciblePolynomial ^ highBitPoly;
            elements = new Dictionary<string, uint>();

            while (elements.Count != highBitPoly)
            {
                elements.Clear();
                elements.Add("0", 0);
                elements.Add("1", 1);

                fieldElement = primitiveElement;
                for (int i = 0; i < highBitPoly - 2; i++)
                {
                    if (elements.ContainsValue(fieldElement))
                    {
                        break;
                    }
                    elements.Add("a" + (i + 1).ToString(), fieldElement);
                    fieldElement = ElementsMult(fieldElement, primitiveElement);
                }

                primitiveElement++;
            }
        }

        public static void Print()
        {
            string result = "GF(2^" + bitsPerElement + ") is:\n";

            foreach (KeyValuePair<string, uint> element in elements)
            {
                result += element.Key + " " + ElementToBitStr(element.Value) + "\n";
            }

            Console.WriteLine(result.Remove(result.Length - 1, 1));
        }

        public static uint ElementsCount
        {
            get
            {
                return highBitPoly;
            }
        }

        public static Dictionary<string, uint> Elements
        {
            get
            {
                return elements;
            }
        }
    }
}
