using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementAppLogic.Billing;
using RCNGCMembersManagementAppLogic.Billing.DirectDebit;
using RCNGCMembersManagementAppLogic.MembersManaging;
using RCNGCMembersManagementMocks;

namespace RCNGCMembersManagementUnitTests.Billing
{
    [TestClass]
    public class BillUnitTests
    {
        static List<Transaction> transactionList;
        static List<Bill> unassignedBillsList;
        static ClubMember clubMember;
        static InvoiceCustomerData invoiceCustomerData;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            BillingSequenceNumbersMock invoiceDataManagerMock = new BillingSequenceNumbersMock();
            BillingDataManager.Instance.SetBillingSequenceNumberCollaborator(invoiceDataManagerMock);

            transactionList = new List<Transaction>()
            {
                {new Transaction("Monthly Fee",1,80,new Tax("NOIGIC",0),0)},
                {new Transaction("Renting a Kajak",1,50,new Tax("NOIGIC",0),0)},
                {new Transaction("Blue cup",2,10,new Tax("NOIGIC",0),0)},
                {new Transaction("BIG Mouring",1,500,new Tax("NOIGIC",0),0)}
            };

            unassignedBillsList = new List<Bill>()
            {
                {new Bill("First Instalment", 200, DateTime.Now, DateTime.Now.AddDays(30))},
                {new Bill("Second Instalment", 200, DateTime.Now, DateTime.Now.AddDays(60))},
                {new Bill("Third Instalment", 250, DateTime.Now, DateTime.Now.AddDays(90))}
            };

            clubMember = new ClubMember("0002", "Francisco", "Gomez", "");
            invoiceCustomerData = new InvoiceCustomerData(clubMember);
        }

        [TestMethod]
        public void ABillShouldHaveABillID()
        {
            Bill bill = new Bill("MMM201300015/001", "An easy to pay bill", 1, DateTime.Now, DateTime.Now.AddYears(10));
            Assert.AreEqual("MMM201300015/001", bill.BillID);
        }

        [TestMethod]
        public void BillIDCanBeNullForLaterInitializationWhenAssignedToInvoices()
        {
            Bill bill = new Bill("An easy to pay bill", 1, DateTime.Now, DateTime.Now.AddYears(10));
            Assert.IsNull(bill.BillID);
        }

        [TestMethod]
        public void WhenCreatingANewInvoiceASingleBillIsCreated()
        {
            BillingDataManager billingDataManager = BillingDataManager.Instance;
            billingDataManager.InvoiceSequenceNumber = 5000;
            Invoice invoice = new Invoice(invoiceCustomerData, transactionList, DateTime.Now);
            Assert.AreEqual(1, invoice.Bills.Count);
        }

        [TestMethod]
        public void ABillOf650IsAutomaticallyCreatedForAnInvoiceOf650NetAmount()
        {
            BillingDataManager billingDataManager = BillingDataManager.Instance;
            billingDataManager.InvoiceSequenceNumber = 5000;
            Invoice invoice = new Invoice(invoiceCustomerData, transactionList, DateTime.Now);
            Assert.AreEqual(650, invoice.Bills.Values.ElementAt(0).Amount);
        }

        [TestMethod]
        public void WhenCreatingAnInvoiceItProvidesTheBillIDtoItsAssociatedBill()
        {
            BillingDataManager billingDataManager = BillingDataManager.Instance;
            billingDataManager.InvoiceSequenceNumber = 5000;
            Invoice invoice = new Invoice(invoiceCustomerData, transactionList, DateTime.Now);
            Assert.IsNotNull(invoice.Bills.Values.ElementAt(0).BillID);
        }

        [TestMethod]
        public void IfTheInvoiceIDIsDINV2013005000ThenTheAutomaticallyCreatedBillIDIsINV2013005000_001()
        {
            BillingDataManager billingDataManager = BillingDataManager.Instance;
            billingDataManager.InvoiceSequenceNumber = 5000;
            Invoice invoice = new Invoice(invoiceCustomerData, transactionList, DateTime.Now);
            Assert.AreEqual("INV2013005000/001", invoice.Bills.Values.ElementAt(0).BillID);
        }

        [TestMethod]
        public void ICanLoadAListOfExistingBillsWhenCreatingAnInvoice()
        {
            string invoiceID = "MMM2013005001";
            List<Bill> assignedBillsList = new List<Bill>(unassignedBillsList);
            assignedBillsList[0].BillID = "MMM2013005001/001";
            assignedBillsList[1].BillID = "MMM2013005001/002";
            assignedBillsList[2].BillID = "MMM2013005001/003";
            Invoice invoice = new Invoice(invoiceID, invoiceCustomerData, transactionList, assignedBillsList, DateTime.Now);
            Assert.AreEqual(3, invoice.Bills.Count);
        }

        [TestMethod]
        public void TheBillsListTotalMustEqualTheTheInvoiceNetAmoutWhenProvided()
        {  
            string invoiceID = "MMM2013005001";
            List<Bill> assignedBillsList = new List<Bill>(unassignedBillsList);
            assignedBillsList[0].BillID = "MMM2013005001/001";
            assignedBillsList[1].BillID = "MMM2013005001/002";
            assignedBillsList[2].BillID = "MMM2013005001/003";
            decimal billsTotalAmount = assignedBillsList.Select(bill => bill.Amount).Sum();
            Invoice invoice = new Invoice(invoiceID, invoiceCustomerData, transactionList, assignedBillsList, DateTime.Now);
            decimal invoiceInitialAmount = invoice.NetAmount;
            Assert.AreEqual(billsTotalAmount, invoiceInitialAmount);
        }

        [TestMethod]
        public void APaymentAgreementIsCorrectlyCreated()
        {
            PaymentAgreement paymentAgreement = new PaymentAgreement("Club President", "New Payment Agreement", new DateTime(2013, 11, 11));
            Assert.AreEqual("Club President", paymentAgreement.AuthorizingPerson);
            Assert.AreEqual("New Payment Agreement", paymentAgreement.AgreementTerms);
            Assert.AreEqual(new DateTime(2013,11,11), paymentAgreement.AgreementDate);
        }

        [TestMethod]
        public void ICanReplaceASetOfBillsInAnInvoiceWithASetOfNewBillsThatAddTheSameAmountByAddingABillPaymentAgreement()
        {
            decimal invoiceInitialAmount;
            BillingDataManager billingDataManager = BillingDataManager.Instance;
            billingDataManager.InvoiceSequenceNumber = 5000;
            Invoice invoice = new Invoice(invoiceCustomerData, transactionList, DateTime.Now);
            invoiceInitialAmount = invoice.NetAmount;
            string authorizingPerson = "Club President";
            string agreementTerms = "New Payment Agreement";
            DateTime agreementDate = DateTime.Now;
            PaymentAgreement paymentAgreement = new PaymentAgreement(authorizingPerson, agreementTerms, agreementDate);
            List<Bill> billsToRenegotiate = new List<Bill>() {invoice.Bills["INV2013005000/001"]};
            List<Bill> billsToAdd = new List<Bill>(unassignedBillsList);
            invoice.AcceptBillsPaymentAgreement(paymentAgreement, billsToRenegotiate, billsToAdd);
            Assert.AreEqual(invoiceInitialAmount, invoice.NetAmount);
            billsToRenegotiate.ForEach(bill => Assert.AreEqual(Bill.BillPaymentResult.Renegotiated, bill.PaymentResult));
            billsToRenegotiate.ForEach(bill => Assert.AreEqual("New Payment Agreement", bill.PaymentAgreement.AgreementTerms));
            billsToAdd.ForEach(bill => Assert.AreEqual("New Payment Agreement", bill.PaymentAgreement.AgreementTerms));
        }

        [TestMethod]
        public void TheBillIDOfTheReplpacingBillsAreCalculatedByTheInvoiceAndHaveConsecutiveNumbers()
        {
            /*decimal invoiceInitialAmount;
            BillDataManager.Instance.SetInvoiceNumber(5000);
            Invoice invoice = new Invoice(clubMember, transactionList, DateTime.Now);
            invoiceInitialAmount = invoice.NetAmount;
            string billToReplace = "MMM2013005001/001";
            invoice.ReplaceBills(billToReplace, unassignedBillsList);
            Assert.AreEqual("MMM2013005001/002", invoice.Bills.ElementAt(0).Value.BillID);
            Assert.AreEqual("MMM2013005001/003", invoice.Bills.ElementAt(1).Value.BillID);
            Assert.AreEqual("MMM2013005001/004", invoice.Bills.ElementAt(2).Value.BillID);*/
            Assert.Inconclusive();
        }


        [TestMethod]
        public void ByDefaultABillIsGeneratedWithoutAPaymentMethod()
        {
            Bill bill = new Bill("MMM201300015/001","An easy to pay bill", 1, DateTime.Now, DateTime.Now.AddYears(10));
            Assert.IsNull(bill.PaymentMethod);
        }

        [TestMethod]
        public void AsigningAPaymentMethodToANewlyCreatedBill()
        {
            Bill bill = new Bill("MMM201300015/001", "An easy to pay bill", 1, DateTime.Now, DateTime.Now.AddYears(10));
            bill.PaymentMethod = new CashPayment();
            Assert.AreEqual(typeof(CashPayment),bill.PaymentMethod.GetType());
        }

        [TestMethod]
        public void WhenPayingABillTheBillIsSetAsPaid()
        {
            Assert.Inconclusive();
        }

    }
}
