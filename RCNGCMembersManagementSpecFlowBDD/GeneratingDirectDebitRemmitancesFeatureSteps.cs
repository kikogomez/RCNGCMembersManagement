using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementAppLogic;
using RCNGCMembersManagementAppLogic.MembersManaging;
using RCNGCMembersManagementAppLogic.Billing;
using RCNGCMembersManagementAppLogic.Billing.DirectDebit;
using RCNGCMembersManagementMocks;

namespace RCNGCMembersManagementSpecFlowBDD
{
    [Binding, Scope(Feature = "Generating Direct Debit Remmitances")]
    public class GeneratingDirectDebitRemmitancesFeatureSteps
    {
        private readonly MembersManagementContextData membersManagementContextData;
        private readonly InvoiceContextData invoiceContextData;
        private InvoicesManager invoicesManager;

        public GeneratingDirectDebitRemmitancesFeatureSteps(
            MembersManagementContextData membersManagementContextData,
            InvoiceContextData invoiceContextData)
        {
            this.membersManagementContextData = membersManagementContextData;
            this.invoiceContextData = invoiceContextData;
            invoicesManager = new InvoicesManager();
        }

        [BeforeScenario]
        public void InitializeSequenceNumbersManagers()
        {
            BillingSequenceNumbersMock billingSequenceNumbersMock = new BillingSequenceNumbersMock();
            invoiceContextData.billDataManager.SetBillingSequenceNumberCollaborator(billingSequenceNumbersMock);
        }

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
            ScenarioContext.Current.Add("DirectDebitInitiationContract", directDebitContract);
        }
        
        [Given(@"These Club Members")]
        public void GivenTheseClubMembers(Table membersTable)
        {
            Dictionary<string, ClubMember> membersCollection = new Dictionary<string, ClubMember>();
            Dictionary<string, string> bICDictionary = new Dictionary<string, string>();
            foreach (var row in membersTable.Rows)
            {
                BankAccount memberAccount = new BankAccount(new ClientAccountCodeCCC((string)row["Account"]));
                bICDictionary.Add(memberAccount.BankAccountFieldCodes.BankCode, row["BIC"]);
                ClubMember clubMember = new ClubMember(
                    row["MemberID"],
                    row["Name"],
                    row["FirstSurname"],
                    row["SecondSurname"]);
                DateTime mandateCreationDate = new DateTime(2009, 10, 30);
                int directDebitReferenceNumber = int.Parse(row["Reference"]);
                DirectDebitMandate directDebitMandate = new DirectDebitMandate(
                    directDebitReferenceNumber,
                    mandateCreationDate,
                    memberAccount);
                clubMember.AddDirectDebitMandate(directDebitMandate);
                membersCollection.Add(clubMember.MemberID, clubMember);
            }
            ScenarioContext.Current.Add("BICDictionary", bICDictionary);
            ScenarioContext.Current.Add("Members", membersCollection);
        }
        
        [Given(@"These bills")]
        public void GivenTheseBills(Table billsTable)
        {
            invoiceContextData.billDataManager.InvoiceSequenceNumber = 5000;
            Dictionary<string, ClubMember> membersCollection = (Dictionary<string, ClubMember>)ScenarioContext.Current["Members"];
            foreach (var row in billsTable.Rows)
            {
                string description = row["TransactionConcept"];
                double amount = double.Parse(row["Amount"]);
                List<Transaction> transaction = new List<Transaction>()
                {
                    new Transaction(description,1,amount,new Tax("NoTAX",0),0)
                };
                ClubMember clubMember = membersCollection[row["MemberID"]];
                InvoiceCustomerData invoiceCustomerData = new InvoiceCustomerData(clubMember);
                Invoice invoice = new Invoice(invoiceCustomerData, transaction, new DateTime(2013, 11, 11));
                invoicesManager.AddInvoiceToClubMember(invoice, clubMember);
            }
        }
        
        [Given(@"I have a I have a direct debit initiation contract")]
        public void GivenIHaveAIHaveADirectDebitInitiationContract()
        {
            DirectDebitInitiationContract directDebitInitiationContract = (DirectDebitInitiationContract)ScenarioContext.Current["DirectDebitInitiationContract"];
            Assert.AreEqual("777", directDebitInitiationContract.CreditorBussinessCode);
            Assert.AreEqual("ES90777G35008770", directDebitInitiationContract.CreditorID);
            Assert.AreEqual("ES5621001111301111111111", directDebitInitiationContract.CreditorAcount.IBAN.IBAN);
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
