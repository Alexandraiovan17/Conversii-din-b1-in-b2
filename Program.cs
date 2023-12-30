using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conversii
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Introduceti numarul: ");
            string inputNumber = Console.ReadLine();
            Console.WriteLine("Introduceti baza initiala b1: ");
            int b1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Introduceti baza finala b2: ");
            int b2 = int.Parse(Console.ReadLine());
            string rezultat = ConvertBase(inputNumber, b1, b2);
            Console.WriteLine($"Rezultatul conversiei: {rezultat}");
        }

        static string ConvertBase(string intnr, int firstb, int secondb)
        { 
            bool isneg = false;
            if (intnr[0] == '-')
            {
                isneg = true;
                intnr = intnr.Substring(1);
            }
            double DecVal = ConvertToDecimal(intnr, firstb);
            string result = ConvertFromDecimal(DecVal, secondb);

            if (isneg)
            {
                result = "-" + result;
            }

            return result;
        }

        static double ConvertToDecimal(string inputNumber, int sourceBase)
        {
            double result = 0;

            int integerPartEndIndex = inputNumber.IndexOf('.');
            if (integerPartEndIndex == -1)
            {
                integerPartEndIndex = inputNumber.Length;
            }
            for (int i = 0; i < integerPartEndIndex; i++)
            {
                char digit = inputNumber[i];
                int digitValue = CharToDigitValue(digit);

                result = result * sourceBase + digitValue;
            }

            if (integerPartEndIndex < inputNumber.Length - 1)
            {
                double fractionalMultiplier = 1.0 / sourceBase;

                for (int i = integerPartEndIndex + 1; i < inputNumber.Length; i++)
                {
                    char digit = inputNumber[i];
                    int digitValue = CharToDigitValue(digit);

                    result += digitValue * fractionalMultiplier;
                    fractionalMultiplier /= sourceBase;
                }
            }

            return result;
        }

        static string ConvertFromDecimal(double decimalValue, int targetBase)
        {
            string result = "";

            int integerPart = (int)decimalValue;
            double fractionalPart = decimalValue - integerPart;

            result = ConvertIntegerPart(integerPart, targetBase);

            if (fractionalPart > 0)
            {
                result += ".";
                result += ConvertFractionalPart(fractionalPart, targetBase);
            }

            return result == "" ? "0" : result;
        }

        static string ConvertIntegerPart(int integerPart, int targetBase)
        {
            if (integerPart == 0)
            {
                return "0";
            }

            string result = "";

            while (integerPart > 0)
            {
                int remainder = integerPart % targetBase;
                char digit = DigitValueToChar(remainder);
                result = digit + result;

                integerPart /= targetBase;
            }

            return result;
        }

        static string ConvertFractionalPart(double fractionalPart, int targetBase)
        {
            const int maxFractionalDigits = 8;
            string result = "";

            for (int i = 0; i < maxFractionalDigits; i++)
            {
                fractionalPart *= targetBase;
                int digit = (int)fractionalPart;
                fractionalPart -= digit;

                result += DigitValueToChar(digit);
            }

            return result;
        }

        static int CharToDigitValue(char digit)
        {
            if (Char.IsDigit(digit))
            {
                return int.Parse(digit.ToString());
            }
            else
            {
                return char.ToUpper(digit) - 'A' + 10;
            }
        }

        static char DigitValueToChar(int digitValue)
        {
            if (digitValue < 10)
            {
                return digitValue.ToString()[0];
            }
            else
            {
                return (char)('A' + digitValue - 10);
            }
        }
    }
}
