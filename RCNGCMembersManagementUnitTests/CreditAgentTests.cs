using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementAppLogic.Billing.DirectDebit;

namespace RCNGCMembersManagementUnitTests
{
    [TestClass]
    public class CreditAgentTests
    {
        [TestMethod]
        public void ANewCreditAgentIsCorrectlyCreated()
        {
            BankCode bankCode = new BankCode("2038", "Bankia, S.A.", "CAHMESMMXXX");
            CreditAgent creditAgent = new CreditAgent(bankCode);
            Assert.AreEqual("2038","");
            Assert.AreEqual("Bankia, S.A.", "");
            Assert.AreEqual("CAHMESMMXXX", "");
        }

        [TestMethod]
        public void ICanRegisterANewContractForMyCreditAgentDirectDebitInitiations()
        {
            Assert.Inconclusive();
        }
    }
}
