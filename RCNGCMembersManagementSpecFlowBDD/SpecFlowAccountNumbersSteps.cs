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
        
        [When(@"I process the bank account")]
        public void WhenIProcessTheBankAccount()
        {
            BankAccount bankAccount;
            try
            {
                bankAccount = new BankAccount(
                    ScenarioContext.Current["Bank"].ToString(),
                    ScenarioContext.Current["office"].ToString(),
                    ScenarioContext.Current["checkDigits"].ToString(),
                    ScenarioContext.Current["accountNumber"].ToString());
                ScenarioContext.Current.Add("Bank_Account", bankAccount);
            }
            catch
            {
                ScenarioContext.Current.Add("Bank_Account", null);
            }           
        }

        [Then(@"It is considered ""(.*)""")]
        public void ThenItIsConsidered(string validity)
        {
            bool valid = (validity == "valid" ? true : false);
            string builtCCC =
                ScenarioContext.Current["Bank"].ToString() +
                ScenarioContext.Current["office"].ToString() +
                ScenarioContext.Current["checkDigits"].ToString() +
                ScenarioContext.Current["accountNumber"].ToString();
            Assert.AreEqual(valid, BankAccount.IsValidCCC(builtCCC));
        }
        
        [Then(@"the bank account is ""(.*)""")]
        public void ThenTheBankAccountIs(string storage)
        {
            bool isStored = (storage == "stored" ? true : false);
            if (ScenarioContext.Current["Bank_Account"] == null)
            {
                Assert.AreEqual(isStored, false);
            }
            else
            {
                BankAccount storedBankAccount = (BankAccount)ScenarioContext.Current["Bank_Account"];
                Assert.AreEqual(isStored, ScenarioContext.Current["Bank"].ToString() == storedBankAccount.BankCode);
                Assert.AreEqual(isStored, ScenarioContext.Current["office"].ToString() == storedBankAccount.OfficeCode);
                Assert.AreEqual(isStored, ScenarioContext.Current["checkDigits"].ToString() == storedBankAccount.CheckDigits);
                Assert.AreEqual(isStored, ScenarioContext.Current["accountNumber"].ToString() == storedBankAccount.AccountNumber);
            }
        }
     
        [Then(@"The CCC ""(.*)"" is created")]
        public void ThenTheCCCIsCreated(string ccc)
        {
            if (ScenarioContext.Current["Bank_Account"] == null)
            {
                Assert.AreEqual("null", ccc);
            }
            else
            {
                Assert.AreEqual(ccc, ((BankAccount)ScenarioContext.Current["Bank_Account"]).CCC.CCC ?? "null");
            }
        }
        
        [Then(@"The spanish IBAN code ""(.*)"" is created")]
        public void ThenTheSpanishIBANCodeIsCreated(string iban)
        {
            if (ScenarioContext.Current["Bank_Account"] == null)
            {
                Assert.AreEqual("null", iban);
            }
            else
            {
                Assert.AreEqual(iban, ((BankAccount)ScenarioContext.Current["Bank_Account"]).IBAN.IBAN ?? "null");
            }
        }

        [Then(@"No CCC is created")]
        public void ThenNoCCCIsCreated()
        {
            Assert.IsNull(((BankAccount)ScenarioContext.Current["Bank_Account"]).CCC.CCC);
        }

        [Then(@"No spanish IBAN is created")]
        public void ThenNoSpanishIBANIsCreated()
        {
            Assert.IsNull(((BankAccount)ScenarioContext.Current["Bank_Account"]).IBAN.IBAN);
        }
    }
}
