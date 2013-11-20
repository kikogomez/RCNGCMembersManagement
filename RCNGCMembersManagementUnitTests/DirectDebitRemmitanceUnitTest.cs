using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementMocks;
using RCNGCMembersManagementAppLogic;
using RCNGCMembersManagementAppLogic.Billing;
using RCNGCMembersManagementAppLogic.Billing.DirectDebit;
using RCNGCMembersManagementAppLogic.MembersManaging;


namespace RCNGCMembersManagementUnitTests
{
    [TestClass]
    public class DirectDebitRemmitanceUnitTest
    {
        static Dictionary<string, ClubMember> clubMembers;
        static Creditor creditor;
        static CreditorAgent creditorAgent;
        static DirectDebitInitiationContract directDebitInitiationContract;
        static InvoicesManager invoicesManager;

        BillingDataManager billingDataManager;
        List<Bill> unassignedBillsList;


        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            BillingSequenceNumbersMock invoiceDataManagerMock = new BillingSequenceNumbersMock();
            BillingDataManager.Instance.SetBillingSequenceNumberCollaborator(invoiceDataManagerMock);
            BillingDataManager.Instance.InvoiceSequenceNumber=5000;
            invoicesManager = new InvoicesManager();

            clubMembers = new Dictionary<string, ClubMember>()
            {
                {"00001", new ClubMember("00001", "Francisco", "Gómez-Caldito", "Viseas")},
                {"00002", new ClubMember("00002", "Pedro", "Pérez", "Gómez")}
            };
            creditor = new Creditor("G35008770", "Real Club Náutico de Gran Canaria");
            creditorAgent = new CreditorAgent(new BankCode("2100", "CaixaBank","CAIXESBBXXX"));
            directDebitInitiationContract = new DirectDebitInitiationContract(
                new BankAccount(new InternationalAccountBankNumberIBAN("ES5621001111301111111111")),
                creditor.NIF,
                "777",
                creditorAgent);

            var billsList = new[]
            {
                new {clubMemberID = "00001", Amount = 79, transactionDescription = "Cuota Social Octubre 2013" },
                new {clubMemberID = "00002", Amount = 79, transactionDescription="Cuota Social Octubre 2013" },
                new {clubMemberID = "00002", Amount = 79, transactionDescription="Cuota Social Noviembre 2013"}
            };

            foreach (var bill in billsList)
            {
                List<Transaction> transaction = new List<Transaction>()
                {
                    new Transaction(bill.transactionDescription,1,bill.Amount,new Tax("NoTAX",0),0)
                };
                ClubMember clubMember = clubMembers[bill.clubMemberID];
                InvoiceCustomerData invoiceCustomerData = new InvoiceCustomerData(clubMember);
                Invoice invoice = new Invoice(invoiceCustomerData, transaction, new DateTime(2013, 11, 11));
                invoicesManager.AddInvoiceToClubMember(invoice, clubMember);
            }
        }

        [TestMethod]
        public void ADirectDebittRemmitanceInstanceIsCorrectlyCreated()
        {
            DateTime creationDate = new DateTime(2013, 11, 30, 7, 15, 0);
            DirectDebitRemmitance directDebitRemmitance = new DirectDebitRemmitance(creationDate, directDebitInitiationContract);
            string expectedMandateId = "MSG-ES90777G35008770-2013113007:15:00";
            Assert.AreEqual(expectedMandateId, directDebitRemmitance.MessageID);
        }
    }
}
