using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementAppLogic.MembersManaging;
using RCNGCMembersManagementAppLogic.Billing.DirectDebit;

namespace RCNGCMembersManagementSpecFlowBDD
{
    [Binding, Scope(Feature = "Generating Direct Debit Remmitances")]
    public class GeneratingDirectDebitRemmitancesFeatureSteps
    {
        [Given(@"My Direct Debit Initiation Contract is")]
        public void GivenMyDirectDebitInitiationContractIs(Table contractTable)
        {
            Creditor creditor = new Creditor(contractTable.Rows[0]["NIF"], contractTable.Rows[0]["Name"]);
            BankCode creditorAgentBankCode = new BankCode(
                contractTable.Rows[0]["LocalBankCode"],
                contractTable.Rows[0]["CreditorAgentName"],
                contractTable.Rows[0]["BIC"]);
            CreditorAgent creditorAgent = new CreditorAgent(creditorAgentBankCode);
            BankAccount creditorAccount = new BankAccount(new InternationalAccountBankNumberIBAN(contractTable.Rows[0]["CreditorAccount"]));
            string creditorBussinessCode = contractTable.Rows[0]["CreditorBussinesCode"];
            DirectDebitInitiationContract directDebitContract = new DirectDebitInitiationContract(
                creditorAccount,
                creditor.NIF,
                contractTable.Rows[0]["CreditorBussinesCode"],
                creditorAgent);
            ScenarioContext.Current.Add("Creditor", creditor);
            ScenarioContext.Current.Add("CreditorAgent", creditorAgent);
            ScenarioContext.Current.Add("DirectDebitContract", directDebitContract);
        }
        
        [Given(@"These Club Members")]
        public void GivenTheseClubMembers(Table table)
        {
            string pepe = "";
        }
        
        [Given(@"These bills")]
        public void GivenTheseBills(Table table)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"I have a I have a direct debit initiation contract")]
        public void GivenIHaveAIHaveADirectDebitInitiationContract()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I generate a new direct debit remmitance")]
        public void WhenIGenerateANewDirectDebitRemmitance()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"An empty direct debit remmitance is created")]
        public void ThenAnEmptyDirectDebitRemmitanceIsCreated()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
