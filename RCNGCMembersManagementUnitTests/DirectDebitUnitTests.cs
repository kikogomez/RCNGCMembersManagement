using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementAppLogic.Billing;
using RCNGCMembersManagementAppLogic.Billing.DirectDebit;
using RCNGCMembersManagementMocks;

namespace RCNGCMembersManagementUnitTests
{
    [TestClass]
    public class DirectDebitUnitTests
    {
        static BillDataManager billDataManager;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            DataManagerMock directDebitDataManagerMock = new DataManagerMock();
            billDataManager = BillDataManager.Instance;
            billDataManager.SetDataManagerCollaborator(directDebitDataManagerMock);
        }


        [TestMethod]
        public void GivenAReferenceNumberAndABankAccountADirectDebitOrderIsCreatedAndReferenceNumberIsAccesible()
        {
            string referenceNumber = "002645";
            ClientAccountCodeCCC ccc = new ClientAccountCodeCCC("12345678061234567890");
            BankAccount bankAccount = new BankAccount(ccc);
            DirectDebitMandate directDebitMandate = new DirectDebitMandate(bankAccount, referenceNumber);
            Assert.AreEqual(referenceNumber, directDebitMandate.ReferenceNumber);
        }

        [TestMethod]
        public void GivenAReferenceNumberAndABankAccountADirectDebitOrderIsCreatedAndBankAccountIsAccesible()
        {
            string referenceNumber = "002645";
            ClientAccountCodeCCC ccc = new ClientAccountCodeCCC("12345678061234567890");
            BankAccount bankAccount = new BankAccount(ccc);
            DirectDebitMandate directDebitMandate = new DirectDebitMandate(bankAccount, referenceNumber);
            Assert.AreEqual("12345678061234567890", directDebitMandate.BankAcount.CCC.CCC);
        }

        [TestMethod]
        public void TheReferenceNumberOfADirectDebitOrderCanBeChanged()
        {
            string referenceNumber = "002645";
            ClientAccountCodeCCC ccc = new ClientAccountCodeCCC("12345678061234567890");
            BankAccount bankAccount = new BankAccount(ccc);
            DirectDebitMandate directDebitMandate = new DirectDebitMandate(bankAccount, referenceNumber);
            directDebitMandate.ReferenceNumber = "111111111111";
            Assert.AreEqual("111111111111", directDebitMandate.ReferenceNumber);
        }

        [TestMethod]
        public void TheBankAccountOfADirectDebitOrderCanBeChanged()
        {
            string referenceNumber = "002645";
            ClientAccountCodeCCC ccc = new ClientAccountCodeCCC("12345678061234567890");
            BankAccount bankAccount = new BankAccount(ccc);
            DirectDebitMandate directDebitMandate = new DirectDebitMandate(bankAccount, referenceNumber);
            BankAccountFields bankAccountFields = new BankAccountFields("0128", "0035", "69", "0987654321");
            BankAccount newBankAccount= new BankAccount(bankAccountFields);
            directDebitMandate.BankAcount = newBankAccount;
            Assert.AreEqual("01280035690987654321", directDebitMandate.BankAcount.CCC.CCC);
        }

/*
        [TestMethod]
        public void ProvidingTheLastDirectDebitReferenceNumberWas100TheNextAssignedMustBe101()
        {
            uint lastDirectDebitReferenceNumber = 100;
            billDatamanager.SetLastInvoiceNumber(lastDirectDebitReferenceNumber);
            Assert.AreEqual((uint)101, (uint)100);
        }*/
    }
}
