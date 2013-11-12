using System;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementAppLogic.MembersManaging;
using RCNGCMembersManagementAppLogic.ClubServices;
using RCNGCMembersManagementAppLogic.ClubStore;
using RCNGCMembersManagementAppLogic.Billing;
using RCNGCMembersManagementAppLogic.Billing.DirectDebit;
using RCNGCMembersManagementMocks;

namespace RCNGCMembersManagementSpecFlowBDD
{
    [Binding, Scope(Feature = "Manage bills")]
    public class ManageBillsFeatureSteps
    {
        private readonly MembersManagementContextData membersManagementContextData;
        private readonly InvoiceContextData invoiceContextData;

        public ManageBillsFeatureSteps(
            MembersManagementContextData membersManagementContextData,
            InvoiceContextData invoiceContextData)
        {
            this.membersManagementContextData = membersManagementContextData;
            this.invoiceContextData = invoiceContextData;
        }

        [Given(@"Last generated InvoiceID is ""(.*)""")]
        public void GivenLastGeneratedInvoiceIDIs(string lastInvoiceID)
        {
            invoiceContextData.lastInvoiceID = lastInvoiceID;
            BillingSequenceNumbersMock invoiceDataManagerMock = new BillingSequenceNumbersMock();
            invoiceContextData.billDataManager.SetBillingSequenceNumberCollaborator(invoiceDataManagerMock);
            invoiceContextData.billDataManager.InvoiceSequenceNumber = uint.Parse(lastInvoiceID.Substring(7));
        }

        [Given(@"A Club Member with a default Payment method")]
        public void GivenAClubMemberWithADefaultPaymentMethod(Table clientsTable)
        {
            membersManagementContextData.clubMember = new ClubMember(clientsTable.Rows[0]["MemberID"], clientsTable.Rows[0]["Name"], clientsTable.Rows[0]["FirstSurname"], clientsTable.Rows[0]["SecondSurname"]);
            string electronicIBANString = clientsTable.Rows[0]["Spanish IBAN Bank Account"].Replace(" ","").Substring(4);
            InternationalAccountBankNumberIBAN iban = new InternationalAccountBankNumberIBAN(electronicIBANString);
            BankAccount bankAccount = new BankAccount(iban);
            DirectDebitMandate directDebitmandate = new DirectDebitMandate(bankAccount, "12345");
            PaymentMethod paymentMethod = new DirectDebit(directDebitmandate);
            membersManagementContextData.clubMember.AddDirectDebitMandate(directDebitmandate);
            membersManagementContextData.clubMember.SetDefaultPaymentMethod(paymentMethod);
        }

        [Given(@"This set of taxes")]
        public void GivenThisSetOfTaxes(Table taxes)
        {
            invoiceContextData.taxesDictionary = new Dictionary<string, Tax>();
            foreach (var row in taxes.Rows)
            {
                string key = row["Tax Type"];
                Tax tax = new Tax((string)row["Tax Type"], double.Parse(row["Tax Value"]));
                invoiceContextData.taxesDictionary.Add(key, tax);
            }
        }

        [Given(@"These services")]
        public void GivenTheseServices(Table services)
        {
            invoiceContextData.servicesDictionary = new Dictionary<string, ClubService>();
            foreach (var row in services.Rows)
            {
                string serviceName = row["Service Name"];
                double defaultCost = double.Parse(row["Default Cost"]);
                string defaultTax = row["Default Tax"];
                ClubService clubService = new ClubService(serviceName, defaultCost, invoiceContextData.taxesDictionary[defaultTax]);
                invoiceContextData.servicesDictionary.Add(serviceName, clubService);
            }
        }

        [Given(@"These products")]
        public void GivenTheseProducts(Table products)
        {

            invoiceContextData.productsDictionary = new Dictionary<string, Product>();
            foreach (var row in products.Rows)
            {
                string productName = row["Product Name"];
                double defaultCost = double.Parse(row["Default Cost"]);
                string defaultTax = row["Default Tax"];
                Product product = new Product(productName, defaultCost, invoiceContextData.taxesDictionary[defaultTax]);
                invoiceContextData.productsDictionary.Add(productName, product);
            }
        }

        [Given(@"The member uses the club service ""(.*)""")]
        public void GivenTheMemberUsesTheClubService(string serviceName)
        {
            ClubService clubService = invoiceContextData.servicesDictionary[serviceName];
            ScenarioContext.Current.Add("A_Club_Service", clubService);
        }

        [When(@"I generate an invoice for the service")]
        public void WhenIGenerateAnInvoiceForTheService()
        {
            DateTime issueDate = DateTime.Now;
            List<Transaction> serviceChargeList = new List<Transaction> { new ServiceCharge((ClubService)ScenarioContext.Current["A_Club_Service"]) };
            Invoice invoice = new Invoice(new InvoiceCustomerData(membersManagementContextData.clubMember), serviceChargeList, issueDate);
            membersManagementContextData.clubMember.AddInvoice(invoice);
            ScenarioContext.Current.Add("Invoice", invoice);
        }

        [Then(@"An invoice is created for the cost of the service: (.*)")]
        public void ThenAnInvoiceIsCreatedForTheCostOfTheService(decimal cost)
        {
            Assert.AreEqual(cost, ((Invoice)ScenarioContext.Current["Invoice"]).NetAmount);
        }

        [Then(@"A single bill To Collect is generated for the total amount of the invoice: (.*)")]
        public void ThenASingleBillToCollectIsGeneratedForTheTotalAmountOfTheInvoice(decimal totalAmount)
        {
            Assert.AreEqual(totalAmount, ((Invoice)ScenarioContext.Current["Invoice"]).BillsTotalAmountToCollect);
        }

        [Then(@"No bills are created for a pro forma invoice")]
        public void ThenNoBillsAreCreatedForAProFormaInvoice()
        {
            Assert.AreEqual(0, ((ProFormaInvoice)ScenarioContext.Current["ProFormaInvoice"]).BillsTotalAmountToCollect);
        }

        [Then(@"By default no payment method is associated to bill")]
        public void ThenByDefaultNoPaymentMethodIsAssociatedToBill()
        {
            Assert.IsNull(((Invoice)ScenarioContext.Current["Invoice"]).Bills.Values.ElementAt(0).PaymentMethod);
        }

        [Given(@"I have a bill to collect")]
        public void GivenIHaveABillToCollect()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"The bill is paid in cash")]
        public void WhenTheBillIsPaidInCash()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"The bill state is set to ""(.*)""")]
        public void ThenTheBillStateIsSetTo(string billState)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"The bill payment method is set to ""(.*)""")]
        public void ThenTheBillPaymentMethodIsSetTo(string paymentMethod)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"The bill payment date is stored")]
        public void ThenTheBillPaymentDateIsStored()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"The bill amount is deduced form the invoice total amount")]
        public void ThenTheBillAmountIsDeducedFormTheInvoiceTotalAmount()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"The bill is paid by bank transfer")]
        public void WhenTheBillIsPaidByBankTransfer()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"The transferor account is stored")]
        public void ThenTheTransferorAccountIsStored()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"The transferee account is stored")]
        public void ThenTheTransfereeAccountIsStored()
        {
            ScenarioContext.Current.Pending();
        }



    }
}
