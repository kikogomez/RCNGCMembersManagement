using System;
using System.Globalization;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementAppLogic.MembersManaging;
using RCNGCMembersManagementAppLogic.ClubServices;
using RCNGCMembersManagementAppLogic.ClubStore;
using RCNGCMembersManagementAppLogic.Billing;
using RCNGCMembersManagementMocks;

namespace RCNGCMembersManagementSpecFlowBDD
{
    [Binding]
    public class SpecFlowBillsSteps
    {
        [Given(@"A Club Member with a default Payment method")]
        public void GivenAClubMemberWithADefaultPaymentMethod(Table table)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"A single bill To Collect is generated for the total amount of the invoice: (.*)")]
        public void ThenASingleBillToCollectIsGeneratedForTheTotalAmountOfTheInvoice(decimal totalAmount)
        {
            Assert.AreEqual(totalAmount, ((Invoice)ScenarioContext.Current["Invoice"]).BillsTotalAmountToCollect);
        }

        [Then(@"No bills are created for a pro forma invoice")]
        public void ThenNoBillsAreCreatedForAProFormaInvoice()
        {
            Assert.AreEqual(0, ((ProFormaInvoice)ScenarioContext.Current["ProFormaInvoice"]).BillsTotalAmountToCollect);
        }

        [Then(@"The bill has associated a payment method designated by the member")]
        public void ThenTheBillHasAssociatedAPaymentMethodDesignatedByTheMember()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
