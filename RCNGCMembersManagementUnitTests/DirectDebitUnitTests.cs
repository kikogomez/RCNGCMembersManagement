using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementAppLogic.Billing;
using RCNGCMembersManagementAppLogic.Billing.DirectDebit;

namespace RCNGCMembersManagementUnitTests
{
    [TestClass]
    public class DirectDebitUnitTests
    {
        [TestMethod]
        public void GivenAReferenceNumberAndABankAccountADirectDebitOrderIsCreatedAndReferenceNumberIsAccesible()
        {
            string referenceNumber = "000001102645";
            ClientAccountCodeCCC ccc = new ClientAccountCodeCCC("12345678061234567890");
            BankAccount bankAccount = new BankAccount(ccc);
            DirectDebitMandate directDebitMandate = new DirectDebitMandate(bankAccount, referenceNumber);
            Assert.AreEqual(referenceNumber, directDebitMandate.ReferenceNumber);
        }

        [TestMethod]
        public void GivenAReferenceNumberAndABankAccountADirectDebitOrderIsCreatedAndBankAccountIsAccesible()
        {
            string referenceNumber = "000001102645";
            ClientAccountCodeCCC ccc = new ClientAccountCodeCCC("12345678061234567890");
            BankAccount bankAccount = new BankAccount(ccc);
            DirectDebitMandate directDebitMandate = new DirectDebitMandate(bankAccount, referenceNumber);
            Assert.AreEqual("12345678061234567890", directDebitMandate.BankAcount.CCC.CCC);
        }

        [TestMethod]
        public void TheReferenceNumberOfADirectDebitOrderCanBeChanged()
        {
            string referenceNumber = "000001102645";
            ClientAccountCodeCCC ccc = new ClientAccountCodeCCC("12345678061234567890");
            BankAccount bankAccount = new BankAccount(ccc);
            DirectDebitMandate directDebitMandate = new DirectDebitMandate(bankAccount, referenceNumber);
            directDebitMandate.ReferenceNumber = "111111111111";
            Assert.AreEqual("111111111111", directDebitMandate.ReferenceNumber);
        }

        [TestMethod]
        public void TheBankAccountOfADirectDebitOrderCanBeChanged()
        {
            string referenceNumber = "000001102645";
            ClientAccountCodeCCC ccc = new ClientAccountCodeCCC("12345678061234567890");
            BankAccount bankAccount = new BankAccount(ccc);
            DirectDebitMandate directDebitMandate = new DirectDebitMandate(bankAccount, referenceNumber);
            BankAccountFields bankAccountFields = new BankAccountFields("0128", "0035", "69", "0987654321");
            BankAccount newBankAccount= new BankAccount(bankAccountFields);
            directDebitMandate.BankAcount = newBankAccount;
            Assert.AreEqual("01280035690987654321", directDebitMandate.BankAcount.CCC.CCC);
        }
    }
}
