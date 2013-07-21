using System;
using System.Globalization;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementAppLogic.MembersManaging;
using RCNGCMembersManagementAppLogic.ClubServices;
using RCNGCMembersManagementAppLogic.ClubStore;
using RCNGCMembersManagementAppLogic.Billing;
using RCNGCMembersManagementAppLogic.Billing.DirectDebit;
using RCNGCMembersManagementMocks;

namespace RCNGCMembersManagementSpecFlowBDD
{
    [Binding]
    public class SpecFlowBillsSteps
    {
        string lastInvoiceID;
        ClubMember clubMember;
        Dictionary<string, Tax> taxesDictionary;
        Dictionary<string, ClubService> servicesDictionary;
        Dictionary<string, Product> productsDictionary;
        DataManagerMock invoiceDataManagerMock;

        [Given(@"A Club Member with a default Payment method")]
        public void GivenAClubMemberWithADefaultPaymentMethod(Table clientsTable)
        {
            clubMember = new ClubMember(clientsTable.Rows[0]["MemberID"], clientsTable.Rows[0]["Name"], clientsTable.Rows[0]["FirstSurname"], clientsTable.Rows[0]["SecondSurname"]);
            string iban = clientsTable.Rows[0]["Spanish IBAN Bank Account"];

            //DirectDebitMandate directDebitMandate = new DirectDebitMandate();
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

        [Then(@"The bill payment method is the default one associated to the member")]
        public void ThenTheBillPaymentMethodIsTheDefaultOneAssociatedToTheMember()
        {
            ScenarioContext.Current.Pending();
        }

    }
}
