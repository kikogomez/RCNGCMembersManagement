using System;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementAppLogic;
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
        InvoicesManager invoicesManager;
        BillsManager billsManager;

        public ManageBillsFeatureSteps(
            MembersManagementContextData membersManagementContextData,
            InvoiceContextData invoiceContextData)
        {
            this.membersManagementContextData = membersManagementContextData;
            this.invoiceContextData = invoiceContextData;
            invoicesManager = new InvoicesManager();
            billsManager = new BillsManager();
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
            DirectDebitMandate directDebitmandate = new DirectDebitMandate(DateTime.Now.Date, bankAccount, "12345");
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
            invoicesManager.AddInvoiceToClubMember(invoice, membersManagementContextData.clubMember);
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
            Invoice invoice = (Invoice)ScenarioContext.Current["Invoice"];
            Assert.AreEqual(1, invoice.Bills.Count);
            ScenarioContext.Current.Add("UniqueBillID", invoice.Bills.ElementAt(0).Key);
            Assert.AreEqual(totalAmount, ((Invoice)ScenarioContext.Current["Invoice"]).BillsTotalAmountToCollect);
        }

        [Then(@"The bill ID is ""(.*)""")]
        public void ThenTheBillIDIs(string billID)
        {
            Assert.AreEqual(billID, ScenarioContext.Current["UniqueBillID"].ToString());
        }

        [Then(@"By default no payment method is associated to bill")]
        public void ThenByDefaultNoPaymentMethodIsAssociatedToBill()
        {
            Assert.IsNull(((Invoice)ScenarioContext.Current["Invoice"]).Bills.Values.ElementAt(0).AssignedPaymentMethod);
        }


        [When(@"I generate an pro-forma invoice for the service")]
        public void WhenIGenerateAnPro_FormaInvoiceForTheService()
        {
            DateTime issueDate = DateTime.Now;
            List<Transaction> serviceChargeList = new List<Transaction> { new ServiceCharge((ClubService)ScenarioContext.Current["A_Club_Service"]) };
            ProFormaInvoice proFormaInvoice = new ProFormaInvoice(new InvoiceCustomerData(membersManagementContextData.clubMember), serviceChargeList, issueDate);
            invoicesManager.AddProFormaInvoiceToClubMember(proFormaInvoice, membersManagementContextData.clubMember);
            ScenarioContext.Current.Add("ProFormaInvoice", proFormaInvoice);
        }

        [Then(@"A pro-forma invoice is created for the cost of the service: (.*)")]
        public void ThenAPro_FormaInvoiceIsCreatedForTheCostOfTheService(decimal cost)
        {
            Assert.AreEqual(cost, ((ProFormaInvoice)ScenarioContext.Current["ProFormaInvoice"]).NetAmount);
        }

        [Then(@"No bills are created for a pro-forma invoice")]
        public void ThenNoBillsAreCreatedForAPro_FormaInvoice()
        {
            Assert.AreEqual(0, ((ProFormaInvoice)ScenarioContext.Current["ProFormaInvoice"]).BillsTotalAmountToCollect);
        }


        [Given(@"I have an invoice with cost (.*) with a single bill with ID ""(.*)""")]
        public void GivenIHaveAnInvoiceWithCostWithASingleBillWithID(int invoiceNetAmout, string billID)
        {
            List<Transaction> transactionList = new List<Transaction>()
            {
                {new Transaction("Monthly Fee",1,80,new Tax("NOIGIC",0),0)},
                {new Transaction("Renting a Kajak",1,50,new Tax("NOIGIC",0),0)},
                {new Transaction("Blue cup",2,10,new Tax("NOIGIC",0),0)},
                {new Transaction("BIG Mouring",1,500,new Tax("NOIGIC",0),0)}
            };
            DateTime issueDate = DateTime.Now.Date;
            Invoice invoice = new Invoice(new InvoiceCustomerData(membersManagementContextData.clubMember), transactionList, issueDate);
            invoicesManager.AddInvoiceToClubMember(invoice, membersManagementContextData.clubMember);
            ScenarioContext.Current.Add("Invoice", invoice);
            ScenarioContext.Current.Add("InvoiceNetAmount", invoiceNetAmout);
            ScenarioContext.Current.Add("BillID",billID);
            ScenarioContext.Current.Add("IssueDate", issueDate);
        }

        [When(@"I renegotiate the bill ""(.*)"" into three instalments: (.*), (.*), (.*) to pay in (.*), (.*) and (.*) days with agreement terms ""(.*)""")]
        public void WhenIRenegotiateTheBillIntoThreeInstalmentsToPayInAndDaysWithAgreementTerms(
            string billID,
            decimal firstInstalmentAmount,
            decimal secondInstalmentAmount,
            decimal thirdInstalmentAmount,
            int firstInstalmentDueDays,
            int secondInstalmentDueDays,
            int thirdInstalmentDueDays,
            string agreementTerms)
        {
            Invoice invoice = (Invoice)ScenarioContext.Current["Invoice"];
            List<Bill> billsToRenegotiate = new List<Bill>() { invoice.Bills[ScenarioContext.Current["BillID"].ToString()] };
            List<Bill> billsToAdd = new List<Bill>()
            {
                {new Bill("First Instalment", 200, DateTime.Now, DateTime.Now.AddDays(firstInstalmentDueDays))},
                {new Bill("Second Instalment", 200, DateTime.Now, DateTime.Now.AddDays(secondInstalmentDueDays))},
                {new Bill("Third Instalment", 250, DateTime.Now, DateTime.Now.AddDays(thirdInstalmentDueDays))}
            };
            string authorizingPerson = "Club President";
            DateTime agreementDate = DateTime.Now;
            ScenarioContext.Current.Add("AgreementDate", agreementDate);
            PaymentAgreement paymentAgreement = new PaymentAgreement(authorizingPerson, agreementTerms, agreementDate);
            invoicesManager.RenegotiateBillsOnInvoice(invoice, paymentAgreement, billsToRenegotiate, billsToAdd);
        }

        [Then(@"The bill ""(.*)"" is marked as renegotiated")]
        public void ThenTheBillIsMarkedAsRenegotiated(string renegotiatedBillID)
        {
            Invoice invoice = (Invoice)ScenarioContext.Current["Invoice"];
            Assert.AreEqual(Bill.BillPaymentResult.Renegotiated, invoice.Bills[renegotiatedBillID].PaymentResult);
        }

        [Then(@"The renegotiated bill ""(.*)"" has associated the agreement terms ""(.*)"" to it")]
        public void ThenTheRenegotiatedBillHasAssociatedTheAgreementTermsToIt(string renegotiatedBillID, string agreemetTerms)
        {
            Invoice invoice = (Invoice)ScenarioContext.Current["Invoice"];
            Assert.AreEqual(agreemetTerms, invoice.Bills[renegotiatedBillID].RenegotiationAgreement.AgreementTerms);
        }

        [Then(@"A bill with ID ""(.*)"" and cost of (.*) to be paid in (.*) days is created")]
        public void ThenABillWithIDAndCostOfToBePaidInDaysIsCreated(string createdBillID, decimal billAmount, int daysToDue)
        {
            Invoice invoice = (Invoice)ScenarioContext.Current["Invoice"];
            Assert.AreEqual(billAmount, invoice.Bills[createdBillID].Amount);
            Assert.AreEqual(((DateTime)ScenarioContext.Current["IssueDate"]).AddDays(daysToDue), invoice.Bills[createdBillID].DueDate);
        }

        [Then(@"The new bill ""(.*)"" has associated the agreement terms ""(.*)"" to it")]
        public void ThenTheNewBillHasAssociatedTheAgreementTermsToIt(string newBillID, string agreementTerms)
        {
            Invoice invoice = (Invoice)ScenarioContext.Current["Invoice"];
            DateTime agreementDate = (DateTime)ScenarioContext.Current["AgreementDate"];
            Assert.AreEqual(agreementTerms, invoice.Bills[newBillID].PaymentAgreements[DateTime.Now.Date].AgreementTerms);
        }

        [Given(@"I have an invoice with some bills")]
        public void GivenIHaveAnInvoiceWithSomeBills()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"I have a bill to collect in the invoice")]
        public void GivenIHaveABillToCollectInTheInvoice()
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

        [Then(@"If the invoice total to be paid is (.*) the invoice is marked as ""(.*)""")]
        public void ThenIfTheInvoiceTotalToBePaidIsTheInvoiceIsMarkedAs(int p0, string p1)
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

        [When(@"The bill is paid by direct debit")]
        public void WhenTheBillIsPaidByDirectDebit()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"The direct debit initiation ID is stored")]
        public void ThenTheDirectDebitInitiationIDIsStored()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"The bill is past due date")]
        public void WhenTheBillIsPastDueDate()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"The bill is marked as ""(.*)""")]
        public void ThenTheBillIsMarkedAs(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"The invoice containig the bill is marked as ""(.*)""")]
        public void ThenTheInvoiceContainigTheBillIsMarkedAs(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"The bill has associated a payment agreement")]
        public void GivenTheBillHasAssociatedAPaymentAgreement()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"The associated payment agreement is set to ""(.*)"" for all bills involved on the agreement")]
        public void ThenTheAssociatedPaymentAgreementIsSetToForAllBillsInvolvedOnTheAgreement(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"The associated payment agreement is set to ""(.*)"" for the invoice")]
        public void ThenTheAssociatedPaymentAgreementIsSetToForTheInvoice(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I renew the due date")]
        public void WhenIRenewTheDueDate()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"The new due date is assigned to the bill")]
        public void ThenTheNewDueDateIsAssignedToTheBill()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"The bill is past due date")]
        public void GivenTheBillIsPastDueDate()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"If there are no other bills marked as ""(.*)"" the invoice is marked ""(.*)""")]
        public void ThenIfThereAreNoOtherBillsMarkedAsTheInvoiceIsMarked(string p0, string p1)
        {
            ScenarioContext.Current.Pending();
        }










    }
}
