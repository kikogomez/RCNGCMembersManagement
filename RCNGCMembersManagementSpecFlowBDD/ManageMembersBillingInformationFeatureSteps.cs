using System;
using TechTalk.SpecFlow;
using RCNGCMembersManagementAppLogic.MembersManaging;

namespace RCNGCMembersManagementSpecFlowBDD
{
    [Binding, Scope(Feature = "Manage Members Billing Information")]
    public class ManageMembersBillingInformationFeatureSteps
    {
        private readonly MembersManagementContextData membersManagementContextData;

        public ManageMembersBillingInformationFeatureSteps(MembersManagementContextData membersManagementContextData)
        {
            this.membersManagementContextData = membersManagementContextData;
        }

        [BeforeScenario]
        public void InitializeScenario()
        {

        }

        [Given(@"A Club Member")]
        public void GivenAClubMember(Table clientsTable)
        {
            membersManagementContextData.clubMember = new ClubMember(clientsTable.Rows[0]["MemberID"], clientsTable.Rows[0]["Name"], clientsTable.Rows[0]["FirstSurname"], clientsTable.Rows[0]["SecondSurname"]);
        }
        
        [Given(@"These Direct Debit Mandates")]
        public void GivenTheseDirectDebitMandates(Table table)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"These Account Numbers")]
        public void GivenTheseAccountNumbers(Table table)
        {
            ScenarioContext.Current.Pending();
        }

        
        [Given(@"I have a member")]
        public void GivenIHaveAMember()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"The member has associated ""(.*)"" as payment method")]
        public void GivenTheMemberHasAssociatedAsPaymentMethod(string p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I set ""(.*)"" as new payment method")]
        public void WhenISetAsNewPaymentMethod(string p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I add a new direct debit mandate to the member")]
        public void WhenIAddANewDirectDebitMandateToTheMember()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"The new payment method is correctly updated")]
        public void ThenTheNewPaymentMethodIsCorrectlyUpdated()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"The direct debit reference sequence number is (.*)")]
        public void GivenTheDirectDebitReferenceSequenceNumberIs(int p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"The new direct debit mandate is correctly assigned")]
        public void ThenTheNewDirectDebitMandateIsCorrectlyAssigned()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"The new direct debit reference sequence number is (.*)")]
        public void ThenTheNewDirectDebitReferenceSequenceNumberIs(int p0)
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
