using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementAppLogic.Billing;
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
        public void TheBillsRelationOfAInvoiceIsReadable()
        {
            BillDataManager.Instance.SetInvoiceNumber(5000);
            Invoice invoice = new Invoice(clubMember, transactionList, DateTime.Now);
            Assert.AreEqual(1, invoice.Bills.Count);
        }

        [TestMethod]
        public void TheBillIDOfANewBillISCreatedAutomaticallyByTheInvoiceWhenAddedtoIt()
        {
            BillDataManager.Instance.SetInvoiceNumber(5000);
            Invoice invoice = new Invoice(clubMember, transactionList, DateTime.Now);
            Assert.AreEqual("MMM2013005001/001", invoice.Bills[0].BillID);
        }

        [TestMethod]
        public void TheDefaultPaymentMethodOfANewBillisCashPayment()
        {
            Bill bill = new Bill("MMM201300015/001","An easy to pay bill", 1, DateTime.Now, DateTime.Now.AddYears(10));
            Assert.AreEqual(typeof(CashPayment), bill.PaymentMethod.GetType());
        }
    }
}
