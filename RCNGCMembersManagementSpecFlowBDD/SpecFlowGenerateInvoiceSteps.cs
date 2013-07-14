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
        string lastInvoiceID;
        ClubMember clubMember;
        ClubService clubService;
        Invoice invoice;
        Invoice secondInvoice;
        InvoiceDataManagerMock invoiceDataManagerMock;

        [Given(@"Last generated InvoiceID is ""(.*)""")]
        public void GivenLastGeneratedInvoiceIDIs(string lastInvoiceID)
        {
            this.lastInvoiceID = lastInvoiceID;
            invoiceDataManagerMock = new InvoiceDataManagerMock();
            BillDataManager.Instance.SetInvoiceDataManagerCollaborator(invoiceDataManagerMock);
            BillDataManager.Instance.SetInvoiceNumber(int.Parse(lastInvoiceID.Substring(7)));
        }

        [Given(@"A Club Member")]
        public void GivenAClubMember(Table table)
        {
            clubMember = new ClubMember(table.Rows[0]["MemberID"], table.Rows[0]["Name"],table.Rows[0]["FirstSurname"],table.Rows[0]["SecondSurname"]);
        }

        [Given(@"The member use a club service")]
        public void GivenTheMemberUseAClubService()
        {
            string serviceDescription = "Renting a Kajak";
            double serviceCost = 50;
            clubService = new ClubService(serviceDescription, serviceCost);
        }

        [When(@"I generate an invoice for the service")]
        public void WhenIGenerateAnInvoiceForTheService()
        {
            DateTime issueDate = DateTime.Now;
            try
            {
                invoice = new Invoice(clubMember, clubService, issueDate);
            }
            catch (Exception e)
            {
                ScenarioContext.Current.Add("Exception_On_Invoice_Creation", e);
            }
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

        [When(@"I generate a new invoice on the same year")]
        public void WhenIGenerateANewInvoiceOnTheSameYear()
        {

            DateTime issueDate = invoice.IssueDate.AddSeconds(1);
            secondInvoice = new Invoice(clubMember, clubService, issueDate);
        }

        [Then(@"the new invoice has a consecutive invoice ID")]
        public void ThenTheNewInvoiceHasAConsecutiveInvoiceID()
        {
            Assert.AreEqual("MMM2013000024", invoice.InvoiceID);
            Assert.AreEqual("MMM2013000025", secondInvoice.InvoiceID);
        }

        [Then(@"The application doesn't accept more than (.*) invoices in the year")]
        public void ThenTheApplicationDoesnTAcceptMoreThanInvoicesInTheYear(int p0)
        {
            Exception exception = (Exception)ScenarioContext.Current["Exception_On_Invoice_Creation"];
            Assert.AreEqual("Only 999999 invoices per year", exception.Message);
        }

    }
}
