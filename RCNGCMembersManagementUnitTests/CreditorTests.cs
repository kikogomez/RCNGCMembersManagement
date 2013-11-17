using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementAppLogic.Billing.DirectDebit;

namespace RCNGCMembersManagementUnitTests
{
    [TestClass]
    public class CreditorTests
    {
        [TestMethod]
        public void TheCreditorInfoIsCorrectlyCreated()
        {
            Creditor creditor = new Creditor("G35008770", "Real Club Náutico de Gran Canaria");
            Assert.AreEqual("G35008770", creditor.NIF);
            Assert.AreEqual("Real Club Náutico de Gran Canaria", creditor.Name);
        }

        [TestMethod]
        public void ANewCreditorAgentIsCorrectlyCreated()
        {
            BankCode bankCode = new BankCode("2038", "Bankia, S.A.", "CAHMESMMXXX");
            CreditorAgent creditAgent = new CreditorAgent(bankCode);
            Assert.AreEqual("2038",creditAgent.LocalBankCode);
            Assert.AreEqual("Bankia, S.A.", creditAgent.BankName);
            Assert.AreEqual("CAHMESMMXXX", creditAgent.BankBIC);
        }

        [TestMethod]
        public void ANewDirectDebitInitiationContractIsCorrectlyCreated()
        {
            Creditor creditor = new Creditor("G35008770", "Real Club Náutico de Gran Canaria");
            BankAccount creditorAccount = new BankAccount(new ClientAccountCodeCCC("20381111401111111111"));
            BankCode bankCode = new BankCode("2038", "Bankia, S.A.", "CAHMESMMXXX");
            CreditorAgent creditorAgent = new CreditorAgent(bankCode);
            DirectDebitInitiationContract directDebitInitiationContract = new DirectDebitInitiationContract(
                creditorAccount, creditor.NIF, "777", creditorAgent);
            Assert.AreEqual("20381111401111111111", directDebitInitiationContract.CreditorAcount.CCC.CCC);
            Assert.AreEqual("CAHMESMMXXX", directDebitInitiationContract.CreditorAgent.BankBIC);
            Assert.AreEqual("777", directDebitInitiationContract.CreditorBussinessCode);
        }

        [TestMethod]
        public void ICanAssignANewDirectDebitContractForTheCreditor()
        {
            /*
            Creditor creditor = new Creditor("G35008770", "Real Club Náutico de Gran Canaria");
            BankCode bankCode = new BankCode("2038", "Bankia, S.A.", "CAHMESMMXXX");
            CreditorAgent creditAgent = new CreditorAgent(bankCode);*/


            Assert.Inconclusive();
        }

        [TestMethod]
        public void ICanAssignMoreThanOneCreditorAgentToACreditor()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void ICanRegisterANewContractForMyCreditAgentDirectDebitInitiations()
        {
/*            BankCode bankCode = new BankCode("2038", "Bankia, S.A.", "CAHMESMMXXX");
            CreditorAgent creditorAgent = new CreditorAgent(bankCode);
            BankAccount creditorAgentAccount = new BankAccount(new InternationalAccountBankNumberIBAN("ES6812345678061234567890"));
            CreditorAgentDirectDebitInitiationContract directDebitContract = new CreditorAgentDirectDebitInitiationContract(creditorAgentAccount, "011");
            creditorAgent.AddDirectDebitInitiacionContract(directDebitContract);
            Assert.AreEqual("011", creditorAgent.DirectDebitInitiationContracts["011"].CreditorBussinessCode);
            Assert.AreEqual("ES6812345678061234567890", creditorAgent.DirectDebitInitiationContracts["011"].CreditorAcount.IBAN.IBAN);*/
        }
    }
}
