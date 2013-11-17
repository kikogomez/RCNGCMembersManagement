using System;
using TechTalk.SpecFlow;

namespace RCNGCMembersManagementSpecFlowBDD
{
    [Binding, Scope(Feature = "Generating Direct Debit Remmitances")]
    public class GeneratingDirectDebitRemmitancesFeatureSteps
    {
        [Given(@"I have a I have a direct debit initiation contract")]
        public void GivenIHaveAIHaveADirectDebitInitiationContract()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"I have direct debit remmitance")]
        public void GivenIHaveDirectDebitRemmitance()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"I have a bill with a direct debit mandate asssociated")]
        public void GivenIHaveABillWithADirectDebitMandateAsssociated()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I generate a new direct debit remmitance")]
        public void WhenIGenerateANewDirectDebitRemmitance()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I assign the bill to de direct debit remmitance")]
        public void WhenIAssignTheBillToDeDirectDebitRemmitance()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"An empty direct debit remmitance is created")]
        public void ThenAnEmptyDirectDebitRemmitanceIsCreated()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"The bill is marked as ""(.*)""")]
        public void ThenTheBillIsMarkedAs(string p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"The bill is added to the direct debit remmitance")]
        public void ThenTheBillIsAddedToTheDirectDebitRemmitance()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
