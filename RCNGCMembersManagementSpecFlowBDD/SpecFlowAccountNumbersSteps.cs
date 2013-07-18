using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementAppLogic.Billing.DirectBebit;

namespace RCNGCMembersManagementSpecFlowBDD
{
    [Binding]
    public class ManageAccountNumbersSteps
    {
        [Given(@"This bank account ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)""")]
        public void GivenThisBankAccount(string bank, string  office, string checkDigits, string accountNumber)
        {
            ScenarioContext.Current.Add("Bank", bank);
            ScenarioContext.Current.Add("office", office);
            ScenarioContext.Current.Add("checkDigits", checkDigits);
            ScenarioContext.Current.Add("accountNumber", accountNumber);
        }
        
        [When(@"I process it")]
        public void WhenIProcessit()
        {
            BankAccount bankAccount = new BankAccount(
                ScenarioContext.Current["Bank"].ToString(),
                ScenarioContext.Current["office"].ToString(),
                ScenarioContext.Current["checkDigits"].ToString(),
                ScenarioContext.Current["accountNumber"].ToString());
            ScenarioContext.Current.Add("Bank_Account", bankAccount);
        }
        
        [Then(@"It is considered valid")]
        public void ThenItIsConsideredValid()
        {
            string builtCCC= 
                ScenarioContext.Current["Bank"].ToString() +
                ScenarioContext.Current["office"].ToString() +
                ScenarioContext.Current["checkDigits"].ToString() +
                ScenarioContext.Current["accountNumber"].ToString();
            Assert.IsTrue(BankAccount.IsValidCCC(builtCCC));
        }
        
        [Then(@"is stored")]
        public void ThenIsStored()
        {
            BankAccount storedBankAccount = (BankAccount)ScenarioContext.Current["Bank_Account"];
            Assert.AreEqual(ScenarioContext.Current["Bank"].ToString(), storedBankAccount.BankCode);
            Assert.AreEqual(ScenarioContext.Current["office"].ToString(), storedBankAccount.OfficeCode);
            Assert.AreEqual(ScenarioContext.Current["checkDigits"].ToString(), storedBankAccount.CheckDigits);
            Assert.AreEqual(ScenarioContext.Current["accountNumber"].ToString(), storedBankAccount.AccountNumber);
        }
        
        [Then(@"The CCC ""(.*)"" is created")]
        public void ThenTheCCCIsCreated(string ccc)
        {
            Assert.AreEqual("12345678061234567890", ((BankAccount)ScenarioContext.Current["Bank_Account"]).CCC.CCC);
        }
        
        [Then(@"The spanish IBAN code ""(.*)"" is created")]
        public void ThenTheSpanishIBANCodeIsCreated(string iban)
        {
            Assert.AreEqual("ES6812345678061234567890", ((BankAccount)ScenarioContext.Current["Bank_Account"]).IBAN.IBAN);
        }
    }
}
