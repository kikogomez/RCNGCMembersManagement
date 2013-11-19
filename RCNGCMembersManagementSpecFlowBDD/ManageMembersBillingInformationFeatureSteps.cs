using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementAppLogic.MembersManaging;
using RCNGCMembersManagementAppLogic.Billing;
using RCNGCMembersManagementAppLogic.Billing.DirectDebit;
using RCNGCMembersManagementMocks;

namespace RCNGCMembersManagementSpecFlowBDD
{
    [Binding, Scope(Feature = "Manage Members Billing Information")]
    public class ManageMembersBillingInformationFeatureSteps
    {
        private readonly MembersManagementContextData membersManagementContextData;
        private readonly DirectDebitContextData directDebitContextData;

        public ManageMembersBillingInformationFeatureSteps(
            MembersManagementContextData membersManagementContextData, 
            DirectDebitContextData directDebitContextData)
        {
            this.membersManagementContextData = membersManagementContextData;
            this.directDebitContextData = directDebitContextData;
        }

        [BeforeScenario]
        public void InitializeScenario()
        {
            directDebitContextData.billDataManager = BillingDataManager.Instance;
            BillingSequenceNumbersMock billingSequenceNumbersMock = new BillingSequenceNumbersMock();
            directDebitContextData.billDataManager.SetBillingSequenceNumberCollaborator(billingSequenceNumbersMock);

        }

        [Given(@"A Club Member")]
        public void GivenAClubMember(Table clientsTable)
        {
            membersManagementContextData.clubMember = new ClubMember(clientsTable.Rows[0]["MemberID"], clientsTable.Rows[0]["Name"], clientsTable.Rows[0]["FirstSurname"], clientsTable.Rows[0]["SecondSurname"]);
        }
        
        [Given(@"These Direct Debit Mandates")]
        public void GivenTheseDirectDebitMandates(Table directDebits)
        {
            directDebitContextData.directDebitMandates = new Dictionary<string, DirectDebitMandate>();
            foreach (var row in directDebits.Rows)
            {
                string internalReferenceNumber = row["DirectDebitInternalReferenceNumber"];
                string iBAN = (string)row["IBAN"];
                DateTime creationDate = DateTime.Parse((string)row["RegisterDate"]).Date;
                BankAccount bankAccount = new BankAccount(new InternationalAccountBankNumberIBAN(iBAN));
                DirectDebitMandate directDebitmandate = new DirectDebitMandate(int.Parse(internalReferenceNumber),creationDate, bankAccount);
                directDebitContextData.directDebitMandates.Add(internalReferenceNumber, directDebitmandate);
            }
        }

        [Given(@"These Account Numbers")]
        public void GivenTheseAccountNumbers(Table accountNumbers)
        {
            directDebitContextData.bankAccounts = new Dictionary<string, BankAccount>();
            foreach (var row in accountNumbers.Rows)
            {;
                string iBAN = (string)row["IBAN"];
                BankAccount bankAccount = new BankAccount(new InternationalAccountBankNumberIBAN(iBAN));
                directDebitContextData.bankAccounts.Add(iBAN, bankAccount);
            }
        }

        
        [Given(@"I have a member")]
        public void GivenIHaveAMember()
        {
            ScenarioContext.Current.Add("Member1", membersManagementContextData.clubMember);
        }

        [Given(@"The member has associated cash as payment method")]
        public void GivenTheMemberHasAssociatedCashAsPaymentMethod()
        {
            ClubMember clubMember = (ClubMember)ScenarioContext.Current["Member1"];
            CashPaymentMethod cashPaymnentMethod = new CashPaymentMethod();
            clubMember.SetDefaultPaymentMethod(cashPaymnentMethod);
        }

        [When(@"I set direct debit as new payment method")]
        public void WhenISetDirectDebitAsNewPaymentMethod()
        {
            ClubMember clubMember = (ClubMember)ScenarioContext.Current["Member1"];
            DirectDebitMandate directDebitMandate = directDebitContextData.directDebitMandates["2345"];
            DirectDebitPaymentMethod directDebitPaymentMethod = new DirectDebitPaymentMethod(directDebitMandate, null);
            ScenarioContext.Current.Add("DirectDebitPaymentMethod", directDebitPaymentMethod);
            clubMember.SetDefaultPaymentMethod(directDebitPaymentMethod);
        }

        [Then(@"The new payment method is correctly updated")]
        public void ThenTheNewPaymentMethodIsCorrectlyUpdated()
        {
            ClubMember clubMember = (ClubMember)ScenarioContext.Current["Member1"];
            DirectDebitPaymentMethod directDebitPaymentMethod = (DirectDebitPaymentMethod)ScenarioContext.Current["DirectDebitPaymentMethod"];
            Assert.AreEqual(directDebitPaymentMethod, (DirectDebitPaymentMethod)clubMember.DefaultPaymentMethod);
        }

        [Then(@"The new direct debit reference sequence number is (.*)")]
        public void ThenTheNewDirectDebitReferenceSequenceNumberIs(int directDebitInternalSequenceNumber)
        {
            directDebitContextData.billDataManager.DirectDebitSequenceNumber = (uint)directDebitInternalSequenceNumber;
        }
        
        [When(@"I add a new direct debit mandate to the member")]
        public void WhenIAddANewDirectDebitMandateToTheMember()
        {
            ClubMember clubMember = (ClubMember)ScenarioContext.Current["Member1"];
            DirectDebitMandate directDebitMandate = directDebitContextData.directDebitMandates["2345"];
            clubMember.AddDirectDebitMandate(directDebitMandate);
        }
        


        [Given(@"The direct debit reference sequence number is (.*)")]
        public void GivenTheDirectDebitReferenceSequenceNumberIs(int directDebitSequenceNumber)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"The new direct debit mandate is correctly assigned")]
        public void ThenTheNewDirectDebitMandateIsCorrectlyAssigned()
        {
            ScenarioContext.Current.Pending();
        }



        [Given(@"I have a direct debit associated to the member")]
        public void GivenIHaveADirectDebitAssociatedToTheMember()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I change the account number of the direct debit")]
        public void WhenIChangeTheAccountNumberOfTheDirectDebit()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"The account number is correctly changed")]
        public void ThenTheAccountNumberIsCorrectlyChanged()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"The old account number is stored in the account numbers history")]
        public void ThenTheOldAccountNumberIsStoredInTheAccountNumbersHistory()
        {
            ScenarioContext.Current.Pending();
        }




    }
}
