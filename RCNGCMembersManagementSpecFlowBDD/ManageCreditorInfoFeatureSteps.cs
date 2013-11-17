using System;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementAppLogic.Billing.DirectDebit;

namespace RCNGCMembersManagementSpecFlowBDD
{
    [Binding, Scope(Feature = "Manage Creditor Info")]
    public class ManageCreditorInfoFeatureSteps
    {
        [Given(@"My creditor info is")]
        public void GivenMyCreditorInfoIs(Table creditorsTable)
        {
            Creditor creditor = new Creditor(creditorsTable.Rows[0]["NIF"], creditorsTable.Rows[0]["Name"]);
            ScenarioContext.Current.Add("Creditor", creditor);
        }

        [Given(@"I have a bank")]
        public void GivenIHaveABank()
        {
            BankCode bankCode = new BankCode("2038", "Bankia, S.A.", "CAHMESMMXXX");
            ScenarioContext.Current.Add("BankCode", bankCode);
        }
        
        [Given(@"I have a creditor agent")]
        public void GivenIHaveACreditorAgent()
        {
            BankCode bankCode = new BankCode("2038", "Bankia, S.A.", "CAHMESMMXXX");
            CreditorAgent creditorAgent = new CreditorAgent(bankCode);
            ScenarioContext.Current.Add("CreditorAgent", creditorAgent);    
        }
        
        [Given(@"I have a direct debit initiation contract registered")]
        public void GivenIHaveADirectDebitInitiationContractRegistered()
        {
            Creditor creditor = (Creditor)ScenarioContext.Current["Creditor"];
            BankCode bankCode = new BankCode("2100", "CaixaBank, S.A.", "CAIXESBBXXX");
            CreditorAgent creditorAgent = new CreditorAgent(bankCode);
            BankAccount creditorAccount = new BankAccount(new ClientAccountCodeCCC("21001111301111111111"));
            DirectDebitInitiationContract directDebitInitiationContract = new DirectDebitInitiationContract(
                creditorAccount, creditor.NIF, "333", creditorAgent);
            creditor.AddDirectDebitInitiacionContract(directDebitInitiationContract);
        }
        
        [Given(@"I have a direct debit initiation contract")]
        public void GivenIHaveADirectDebitInitiationContract()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I register the bank as a creditor agent")]
        public void WhenIRegisterTheBankAsMyCreditorAgent()
        {
            BankCode bankCode = (BankCode)ScenarioContext.Current["BankCode"];
            CreditorAgent creditorAgent = new CreditorAgent(bankCode);
            ScenarioContext.Current.Add("CreditorAgent", creditorAgent);           
        }
        
        [When(@"I register a contract data")]
        public void WhenIRegisterAContractData()
        {
            Creditor creditor = (Creditor)ScenarioContext.Current["Creditor"];
            BankAccount creditorAccount = new BankAccount(new ClientAccountCodeCCC("20381111401111111111"));
            CreditorAgent creditorAgent = (CreditorAgent)ScenarioContext.Current["CreditorAgent"];
            DirectDebitInitiationContract direcDebitinitiationContract = new DirectDebitInitiationContract(
                creditorAccount, creditor.NIF, "777", creditorAgent);
            creditor.AddDirectDebitInitiacionContract(direcDebitinitiationContract);
        }

        [When(@"I register a second contract data")]
        public void WhenIRegisterASecondContractData()
        {
            Creditor creditor = (Creditor)ScenarioContext.Current["Creditor"];
            BankCode bankCode = new BankCode("2038", "Bankia, S.A.", "CAHMESMMXXX");
            CreditorAgent creditorAgent = new CreditorAgent(bankCode);
            BankAccount creditorAccount = new BankAccount(new ClientAccountCodeCCC("20381111401111111111"));
            DirectDebitInitiationContract directDebitInitiationContract = new DirectDebitInitiationContract(
                creditorAccount, creditor.NIF, "777", creditorAgent);
            creditor.AddDirectDebitInitiacionContract(directDebitInitiationContract);
        }

        
        [When(@"I change the creditor account")]
        public void WhenIChangeTheCreditorAccount()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I change the creditor bussines code")]
        public void WhenIChangeTheCreditorBussinesCode()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"The creditor agent is correctly created")]
        public void ThenTheCreditorAgentIsCorrectlyRegistered()
        {
            BankCode bankCode= (BankCode)ScenarioContext.Current["BankCode"];
            CreditorAgent creditorAgent = (CreditorAgent)ScenarioContext.Current["CreditorAgent"];
            Assert.AreEqual(bankCode.BankBIC, creditorAgent.BankBIC);
            Assert.AreEqual(bankCode.BankName, creditorAgent.BankName);
            Assert.AreEqual(bankCode.LocalBankCode, creditorAgent.LocalBankCode);
        }
        
        [Then(@"The contract is correctly registered")]
        public void ThenTheContractIsCorrectlyRegistered()
        {
            Creditor creditor = (Creditor)ScenarioContext.Current["Creditor"];
            Assert.AreEqual("20381111401111111111", creditor.DirectDebitInitiationContracts["777"].CreditorAcount.CCC.CCC);
            Assert.AreEqual("CAHMESMMXXX", creditor.DirectDebitInitiationContracts["777"].CreditorAgent.BankBIC);
            Assert.AreEqual("777", creditor.DirectDebitInitiationContracts["777"].CreditorBussinessCode);
            Assert.AreEqual("ES90777G35008770", creditor.DirectDebitInitiationContracts["777"].CreditorID);
        }
        
        [Then(@"The contract is correctly updated")]
        public void ThenTheContractIsCorrectlyUpdated()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
