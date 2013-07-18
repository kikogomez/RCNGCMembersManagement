using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCNGCMembersManagementAppLogic.Billing.DirectBebit
{
    public class ClientAccountCodeCCC
    {
        string bank;
        string office;
        CCCCheckDigits checkDigits;
        string accountNumber;
        string ccc;

        public ClientAccountCodeCCC(string bank, string office, string checkDigits, string accountNumber)
        {
            if (IsValidCCC(bank, office, checkDigits, accountNumber))
            {
                this.bank = bank;
                this.office = office;
                this.checkDigits.bankOfficeCheckDigit = checkDigits[0].ToString();
                this.checkDigits.accountNumberCheckDigit = checkDigits[1].ToString();
                this.accountNumber = accountNumber;
                this.ccc = bank + office + checkDigits + accountNumber;
            }
        }

        public string CCC
        {
            get { return ccc; }
        }

        public string FormattedCCC
        {
            get
            {
                if (ccc != null) return (bank + " " + office + " " + checkDigits.bankOfficeCheckDigit + checkDigits.accountNumberCheckDigit + " " + accountNumber);
                return null;
            }
        }

        public CCCCheckDigits CCCCheck
        {
            get { return checkDigits; }
        }

        public static CCCCheckDigits CalculateCCCCheckDigits(string bank, string office, string accountNumber)
        {
            return new CCCCheckDigits { bankOfficeCheckDigit = BankOfficeCCCCheck(bank, office), accountNumberCheckDigit = AccountNumberCCCCheck(accountNumber) };
        }

        public static string CalculateCCC(string bank, string office, string accountNumber)
        {
            CCCCheckDigits checkDigits = CalculateCCCCheckDigits(bank, office, accountNumber);
            if (checkDigits.bankOfficeCheckDigit != null && checkDigits.accountNumberCheckDigit != null)
            {
                return bank + office + checkDigits.bankOfficeCheckDigit + checkDigits.accountNumberCheckDigit + accountNumber;
            }
            return null;
        }

        public static bool IsValidCCC(string bank, string office, string checkDigits, string accountNumber)
        {
            if (!CheckDigitsAreRightSize(checkDigits)) return false;
            CCCCheckDigits calculatedCCCCheckDigits = CalculateCCCCheckDigits(bank, office, accountNumber);
            return (
                calculatedCCCCheckDigits.bankOfficeCheckDigit == checkDigits[0].ToString() &&
                calculatedCCCCheckDigits.accountNumberCheckDigit == checkDigits[1].ToString());
        }

        public static bool IsValidCCC(string ccc)
        {
            if (!CCCIsRightSize(ccc)) return false;
            SortedList<string, string> splittedCCC = SplitCCC(ccc);
            return IsValidCCC(splittedCCC["bank"], splittedCCC["office"], splittedCCC["checkDigits"], splittedCCC["accountNumber"]);
        }

        private static string BankOfficeCCCCheck(string bank, string office)
        {
            return BankAccountChekNumbersCalculator.CalculateCCCCheckDigit("00" + bank + office);
        }

        private static string AccountNumberCCCCheck(string accountNumber)
        {
            return BankAccountChekNumbersCalculator.CalculateCCCCheckDigit(accountNumber);
        }

        private static bool CheckDigitsAreRightSize(string checkDigits)
        {
            return (checkDigits ?? "").Trim().Length == CCCFieldLenghts.CheckDigitsLenght;
        }

        private static bool CCCIsRightSize(string ccc)
        {
            return (ccc ?? "").Trim().Length == CCCFieldLenghts.CCCLength;
        }

        private static SortedList<string, string> SplitCCC(string ccc)
        {
            return new SortedList<string,string> {
                {"bank",ccc.Substring(0, CCCFieldLenghts.BankLength)},
                {"office",ccc.Substring(4, CCCFieldLenghts.OfficeLenght)},
                {"checkDigits", ccc.Substring(8,CCCFieldLenghts.CheckDigitsLenght)},
                {"accountNumber", ccc.Substring(10, CCCFieldLenghts.AccountNumberLenght)}};
        }

        public struct CCCFieldLenghts
        {
            public const int BankLength = 4;
            public const int OfficeLenght = 4;
            public const int CheckDigitsLenght = 2;
            public const int AccountNumberLenght = 10;
            public const int CCCLength = 20;
        }

        public struct CCCCheckDigits
        {
            public string bankOfficeCheckDigit;
            public string accountNumberCheckDigit;
        }


    }
}
