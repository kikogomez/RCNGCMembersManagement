using System;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RCNGCMembersManagementSpecFlowBDD
{
    [Binding]
    public class ManageAccountNumbersSteps
    {
        [Given(@"This bank account ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)""")]
        public void GivenThisBankAccount(string bank, string  office, string controlDigit, string accountNumber)
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I process them")]
        public void WhenIProcessThem()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"It is considered valid")]
        public void ThenItIsConsideredValid()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"is stored")]
        public void ThenIsStored()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"The CCC ""(.*)"" is created")]
        public void ThenTheCCCIsCreated(string ccc)
        {
            Assert.AreEqual(true, false);
        }
        
        [Then(@"The spanish IBAN code ""(.*)"" is created")]
        public void ThenTheSpanishIBANCodeIsCreated(string iban)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
