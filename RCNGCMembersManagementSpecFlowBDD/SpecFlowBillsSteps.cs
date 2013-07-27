using System;
using System.Linq;
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
        private readonly MembersManagementContextData membersManagementContextData;

        public SpecFlowBillsSteps(MembersManagementContextData membersManagementContextData)
        {
            this.membersManagementContextData = membersManagementContextData;
        }

        [Given(@"A Club Member with a default Payment method")]
        public void GivenAClubMemberWithADefaultPaymentMethod(Table clientsTable)
        {
            membersManagementContextData.clubMember = new ClubMember(clientsTable.Rows[0]["MemberID"], clientsTable.Rows[0]["Name"], clientsTable.Rows[0]["FirstSurname"], clientsTable.Rows[0]["SecondSurname"]);
            string electronicIBANString = clientsTable.Rows[0]["Spanish IBAN Bank Account"].Replace(" ","").Substring(4);
            InternationalAccountBankNumberIBAN iban = new InternationalAccountBankNumberIBAN(electronicIBANString);
            BankAccount bankAccount = new BankAccount(iban);
            DirectDebitMandate directDebitmandate = new DirectDebitMandate(bankAccount, "12345");
            PaymentMethod paymentMethod = new DirectDebit(directDebitmandate);
            membersManagementContextData.clubMember.AddDirectDebitMandate(directDebitmandate);
            membersManagementContextData.clubMember.SetDefaultPaymentMethod(paymentMethod);
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

        [Then(@"By default no payment method is associated to bill")]
        public void ThenByDefaultNoPaymentMethodIsAssociatedToBill()
        {
            Assert.IsNull(((Invoice)ScenarioContext.Current["Invoice"]).Bills.Values.ElementAt(0).PaymentMethod);
        }


    }
}
