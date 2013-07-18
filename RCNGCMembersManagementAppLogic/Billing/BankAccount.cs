using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace RCNGCMembersManagementAppLogic.Billing
{
    public class BankAccount
    {
        string bank;
        string office;
        string checkDigits;
        string accountNumber;
        ClientAccountCodeCCC ccc;
        InternationalAccountBankNumberIBAN iban;
      
        public BankAccount(string bank, string office, string checkDigits, string accountNumber)
        {
            try
            {
                CheckBankAccountFieldsLength(bank,office,checkDigits,accountNumber);
            }
            catch (ArgumentException e)
            {
                throw e;
            }
                        
            this.bank = bank;
            this.office = office;
            this.checkDigits = checkDigits;
            this.accountNumber = accountNumber;

            ccc= new ClientAccountCodeCCC(bank, office, checkDigits, accountNumber);
            iban = new InternationalAccountBankNumberIBAN(ccc.CCC);
        }


        public string BankCode
        {
            get { return bank; }
        }

        public string OfficeCode
        {
            get { return office; }
        }

        public string CheckDigits
        {
            get { return checkDigits; }
        }

        public string AccountNumber
        {
            get { return accountNumber; }
        }

        public bool HasValidCCC
        {
            get { return (ccc.CCC != null); }
        }

        public ClientAccountCodeCCC CCC
        {
            get { return ccc;  }
        }

        public bool HasValidIBAN
        {
            get { return (iban.IBAN != null); }
        }

        public InternationalAccountBankNumberIBAN IBAN
        {
            get { return iban; }
        }

        public static ClientAccountCodeCCC.CCCCheckDigits CalculateCCCCheckDigits(string bank, string office, string accountNumber)
        {
            return ClientAccountCodeCCC.CalculateCCCCheckDigits(bank, office, accountNumber);
        }

        public static string CalculateCCC(string bank, string office, string accountNumber)
        {
            return ClientAccountCodeCCC.CalculateCCC(bank, office, accountNumber);
        }

        public static bool IsValidCCC(string bank, string office, string checkDigits, string accountNumber)
        {
            string ccc = bank + office + checkDigits + accountNumber;
            return IsValidCCC(ccc);
        }

        public static bool IsValidCCC(string ccc)
        {
            return ClientAccountCodeCCC.IsValidCCC(ccc);
        }

        public static string CalculateSpanishIBANCheck(string bank, string office, string cccCheckDigits, string accountNumber)
        {
            string ccc = bank + office + cccCheckDigits + accountNumber;
            return CalculateSpanishIBANCheck(ccc);
        }

        public static string CalculateSpanishIBANCheck(string ccc)
        {
            return InternationalAccountBankNumberIBAN.CalculateSpanishIBANCheck(ccc);
        }

        public static string CalculateSpanishIBAN(string bank, string office, string accountNumber)
        {
            ClientAccountCodeCCC.CCCCheckDigits cccCheckDigits = CalculateCCCCheckDigits(bank, office, accountNumber);
            string ccc = bank + office + cccCheckDigits.bankOfficeCheckDigit + cccCheckDigits.accountNumberCheckDigit + accountNumber;
            return CalculateSpanishIBAN(ccc);
        }

        public static string CalculateSpanishIBAN(string bank, string office, string checkDigits, string accountNumber)
        {
            string ccc = bank + office + checkDigits + accountNumber;
            return CalculateSpanishIBAN(ccc);
        }

        public static string CalculateSpanishIBAN(string ccc)
        {
            return InternationalAccountBankNumberIBAN.CalculateSpanishIBAN(ccc);
        }

        public static bool IsValidIBAN(string iban)
        {
            return InternationalAccountBankNumberIBAN.IsValidIBAN(iban);
        }

        private void CheckBankAccountFieldsLength(string bank, string office, string checkDigits, string accountNumber)
        {
            try
            {
                ThrowExceptionOnTooLongAccountDataString("banco", bank, ClientAccountCodeCCC.CCCFieldLenghts.BankLength);
                ThrowExceptionOnTooLongAccountDataString("sucursal", office, ClientAccountCodeCCC.CCCFieldLenghts.OfficeLenght);
                ThrowExceptionOnTooLongAccountDataString("dígito de control", checkDigits, ClientAccountCodeCCC.CCCFieldLenghts.CheckDigitsLenght);
                ThrowExceptionOnTooLongAccountDataString("número de cuenta", accountNumber, ClientAccountCodeCCC.CCCFieldLenghts.AccountNumberLenght);
            }
            catch (System.ArgumentException e)
            {
                throw e;
            }
            
        }
        
        private void ThrowExceptionOnTooLongAccountDataString(string fieldName, string fieldValue, int maxLenght)
        {
             if ((fieldValue ?? "").Length>maxLenght) throw new System.ArgumentException("El código de es demasiado largo", fieldName);
        }


        


    }
}
