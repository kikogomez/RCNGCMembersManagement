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
            CreditorAgent creditAgent = new CreditorAgent(bankCode);
            Assert.AreEqual("2038",creditAgent.LocalBankCode);
            Assert.AreEqual("Bankia, S.A.", creditAgent.BankName);
            Assert.AreEqual("CAHMESMMXXX", creditAgent.BankBIC);
        }

        [TestMethod]
        public void ICanRegisterANewContractForMyCreditAgentDirectDebitInitiations()
        {
            BankCode bankCode = new BankCode("2038", "Bankia, S.A.", "CAHMESMMXXX");
            CreditorAgent creditorAgent = new CreditorAgent(bankCode);
            BankAccount creditorAgentAccount = new BankAccount(new InternationalAccountBankNumberIBAN("ES6812345678061234567890"));
            CreditorAgentDirectDebitInitiationContract directDebitContract = new CreditorAgentDirectDebitInitiationContract(creditorAgentAccount, "011");
            creditorAgent.AddDirectDebitInitiacionContract(directDebitContract);
            Assert.AreEqual("011", creditorAgent.DirectDebitInitiationContracts["011"].CreditorBussinessCode);
            Assert.AreEqual("ES6812345678061234567890", creditorAgent.DirectDebitInitiationContracts["011"].CreditorAgentAcount.IBAN.IBAN);
        }
    }
}
