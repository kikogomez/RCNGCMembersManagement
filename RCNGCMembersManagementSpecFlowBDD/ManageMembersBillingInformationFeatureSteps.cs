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
        
        [Then(@"The new direct debit mandate is correctly")]
        public void ThenTheNewDirectDebitMandateIsCorrectly()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
