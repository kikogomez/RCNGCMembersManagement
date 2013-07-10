using System;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementAppLogic.MembersManaging;
using RCNGCMembersManagementAppLogic.ClubServices;
using RCNGCMembersManagementAppLogic.Billing;
using RCNGCMembersManagementMocks;


namespace RCNGCMembersManagementSpecFlowBDD
{
    [Binding]
    public class SpecFlowGenerateInvoiceSteps
    {
        ClubMember clubMember;
        ClubService clubService;
        Invoice invoice;
        Invoice secondInvoice;
        InvoiceDataManagerMock invoiceDataManagerMock;

        [Given(@"I have a Club Member")]
        public void GivenIHaveAClubMember()
        {
            clubMember = new ClubMember("00001", "Francisco", "Gomez-Caldito", "Viseas");
        }
        
        [Given(@"the member use a club service")]
        public void GivenTheMemberUseAClubService()
        {
            string serviceDescription = "Renting a Kajak";
            double serviceCost = 50;
            clubService = new ClubService(serviceDescription, serviceCost);
        }
        
        [When(@"I bill this service")]
        public void WhenIBillAThisService()
        {
            DateTime issueDate = DateTime.Now;
            invoiceDataManagerMock = new InvoiceDataManagerMock();
            BillDataManager.Instance.SetInvoiceDataManagerCollaborator(invoiceDataManagerMock);
            invoice = new Invoice(clubMember, clubService, issueDate);
        }
        
        [Then(@"An invoice is created for the cost of the service")]
        public void ThenAnInvoiceIsCreatedForTheCostOfTheService()
        {
            Assert.AreEqual(50, invoice.NetAmount);
        }

        [Then(@"A single bill is generated for the total amount of the invoice")]
        public void ThenASingleBillIsGeneratedForTheTotalAmountOfTheInvoice()
        {
            Assert.AreEqual(50, invoice.BillsTotalAmountToCollect);
        }

        [Given(@"I generate an invoice")]
        public void GivenIGenerateAnInvoice()
        {
            invoiceDataManagerMock = new InvoiceDataManagerMock();
            BillDataManager.Instance.SetInvoiceDataManagerCollaborator(invoiceDataManagerMock);
            GivenIHaveAClubMember();
            GivenTheMemberUseAClubService();
            WhenIBillAThisService();
        }

        [When(@"I generate a new invoice on the same year")]
        public void WhenIGenerateANewInvoiceOnTheSameYear()
        {
            DateTime issueDate = invoice.IssueDate.AddSeconds(1); ;
            secondInvoice = new Invoice(clubMember, clubService, issueDate);
        }

        [Then(@"the new invoice has a consecutive invoice ID")]
        public void ThenTheNewInvoiceHasAConsecutiveInvoiceID()
        {
            Assert.AreEqual(int.Parse(invoice.InvoiceID.Substring(3)) + 1, int.Parse(secondInvoice.InvoiceID.Substring(3)));
        }


    }
}
