using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementAppLogic.Billing;
using RCNGCMembersManagementAppLogic.Billing.DirectDebit;
using RCNGCMembersManagementAppLogic.MembersManaging;
using RCNGCMembersManagementMocks;

namespace RCNGCMembersManagementUnitTests
{
    [TestClass]
    public class BillUnitTests
    {
        static List<Transaction> transactionList;
        static ClubMember clubMember;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            DataManagerMock invoiceDataManagerMock = new DataManagerMock();
            BillDataManager.Instance.SetDataManagerCollaborator(invoiceDataManagerMock);

            transactionList = new List<Transaction>()
            {
                {new Transaction("Monthly Fee",1,80,new Tax("NOIGIC",0),0)},
                {new Transaction("Renting a Kajak",1,50,new Tax("NOIGIC",0),0)},
                {new Transaction("Blue cup",2,10,new Tax("NOIGIC",0),0)},
                {new Transaction("BIG Mouring",1,500,new Tax("NOIGIC",0),0)}
            };
            clubMember = new ClubMember("0002", "Francisco", "Gomez", "");
        }

        [TestMethod]
        public void ABillMustHaveABillID()
        {
            Bill bill = new Bill("MMM201300015/001", "An easy to pay bill", 1, DateTime.Now, DateTime.Now.AddYears(10));
            Assert.IsNotNull(bill.BillID);
        }

        [TestMethod]
        public void WhenCreatingANewInvoiceASingleBillIsCreated()
        {
            BillDataManager.Instance.SetInvoiceNumber(5000);
            Invoice invoice = new Invoice(clubMember, transactionList, DateTime.Now);
            Assert.AreEqual(1, invoice.Bills.Count);
        }

        [TestMethod]
        public void ABillOf650IsAutomaticallyCreatedForAnInvoiceOf650NetAmount()
        {
            BillDataManager.Instance.SetInvoiceNumber(5000);
            Invoice invoice = new Invoice(clubMember, transactionList, DateTime.Now);
            Assert.AreEqual(650, invoice.Bills.Values.ElementAt(0).Amount);
        }

        [TestMethod]
        public void WhenCreatingAnInvoiceItProvidesTheBillIDItAssociatedBill()
        {
            BillDataManager.Instance.SetInvoiceNumber(5000);
            Invoice invoice = new Invoice(clubMember, transactionList, DateTime.Now);
            Assert.IsNotNull(invoice.Bills.Values.ElementAt(0).BillID);
        }

        [TestMethod]
        public void IfTheInvoiceIDIsDMMM2013005001ThenTheCreatedBillIDIsMMM2013005001_001()
        {
            BillDataManager.Instance.SetInvoiceNumber(5000);
            Invoice invoice = new Invoice(clubMember, transactionList, DateTime.Now);
            Assert.AreEqual("MMM2013005001/001", invoice.Bills.Values.ElementAt(0).BillID);
        }

        [TestMethod]
        public void ICanReplaceABillInAnInvoiceWithASetOfNewBillsThatAddTheSameAmount()
        {
            decimal invoiceInitialAmount;
            BillDataManager.Instance.SetInvoiceNumber(5000);
            Invoice invoice = new Invoice(clubMember, transactionList, DateTime.Now);
            invoiceInitialAmount=invoice.NetAmount;
            List<Bill> billsList = new List<Bill>()
            {
                {new Bill("", "First Instalment", 200, DateTime.Now, DateTime.Now.AddDays(30))},
                {new Bill("", "Second Instalment", 200, DateTime.Now, DateTime.Now.AddDays(60))},
                {new Bill("", "Third Instalment", 250, DateTime.Now, DateTime.Now.AddDays(90))}
            };
            string billToReplace="MMM2013005001/001";
            invoice.ReplaceBills(billToReplace, billsList);
            Assert.AreEqual(invoiceInitialAmount, invoice.NetAmount);
        }














/*        [TestMethod]
        public void TheSecondBillOfAInvoiceWithAnIDMMM2013005001IsMMM2013005001_002()
        {
            BillDataManager.Instance.SetInvoiceNumber(5000);
            Invoice invoice = new Invoice(clubMember, transactionList, DateTime.Now);
            Assert.AreEqual("MMM2013005001/002", invoice.Bills[1].BillID);
        }*/

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


    }
}
