using System;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementAppLogic.MembersManaging;
using RCNGCMembersManagementAppLogic.ClubServices;
using RCNGCMembersManagementAppLogic.Billing;


namespace RCNGCMembersManagementSpecFlowBDD
{
    [Binding]
    public class SpecFlowGenerateInvoiceSteps
    {
        ClubMember clubMember;
        ClubService clubService;
        Invoice invoice;

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
        
        [When(@"I bill a this service")]
        public void WhenIBillAThisService()
        {
            DateTime issueDate = DateTime.Now;
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

    }
}
