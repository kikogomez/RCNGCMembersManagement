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
    }
}
