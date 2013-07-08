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
            invoice = new Invoice(clubMember, clubService);
        }
        
        [Then(@"An invoice is created for the cost of the service")]
        public void ThenAnInvoiceIsCreatedForTheCostOfTheService()
        {
            Assert.AreEqual("50", invoice.AmountWithTaxes);
        }
    }
}
