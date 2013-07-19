using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCNGCMembersManagementAppLogic.Billing.DirectBebit
{
    static class BankAccountChekNumbersCalculator
    {
        public static string CalculateCCCCheckDigit(string tenDigitsLongInt)
        {
            int[] pesos = { 1, 2, 4, 8, 5, 10, 9, 7, 3, 6 };
            int calculocheck = 0;

            if (!IsATenDigitsLongInt(tenDigitsLongInt)) return null;

            for (int i = 0; i <= 9; i++)
            {
                calculocheck += int.Parse(tenDigitsLongInt[i].ToString()) * pesos[i];
            }
            calculocheck = calculocheck % 11;
            if (calculocheck > 1) calculocheck = 11 - calculocheck;
            return calculocheck.ToString();
        }

        public static string CalculateSpanishIBANCheckDigits(string ccc)
        {
            string longNumber = ccc + "142800";
            ulong modulus = CalculateLongNumberModulus(longNumber, 97);

            return ((98 - modulus).ToString());
        }

        private static ulong CalculateLongNumberModulus(string longNumber, ulong baseNumber)
        {
            const int ulongMaxLenght = 19;
            string leftPart;
            string rightPart;
            ulong leftPartModulus;
                        
            while (longNumber.Length > 19)
            {
                leftPart = longNumber.Substring(0, ulongMaxLenght);
                rightPart = longNumber.Substring(ulongMaxLenght);
                leftPartModulus = ulong.Parse(leftPart) % baseNumber;
                longNumber = leftPartModulus.ToString() + rightPart;
            }
            return ulong.Parse(longNumber) % baseNumber;
        }
 
        private static bool ItParsesLongInt(string stringToCheck)
        {
            long longResult;
            return long.TryParse(stringToCheck, out longResult);
        }

        private static bool IsTenCharactersStringWithoutSpaces(string stringToCheck)
        {
            return (stringToCheck ?? "").Replace(" ","").Length == 10;
        }

        private static bool IsATenDigitsLongInt(string stringToCheck)
        {
            return (IsTenCharactersStringWithoutSpaces(stringToCheck) && ItParsesLongInt(stringToCheck));
        }
    }
}
