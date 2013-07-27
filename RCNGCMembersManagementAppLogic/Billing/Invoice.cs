﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCNGCMembersManagementAppLogic.MembersManaging;
using RCNGCMembersManagementAppLogic.ClubServices;
using RCNGCMembersManagementAppLogic.ClubStore;

namespace RCNGCMembersManagementAppLogic.Billing
{
    public class Invoice: BaseInvoice
    {
        Dictionary<string, Bill> invoiceBills;
        InvoicePaymentState invoiceState;
        InvoiceCustomerData invoiceCustomerData;

        public Invoice(ClubMember clubMember, List<Transaction> transactionsList, DateTime issueDate)
            : this(clubMember, transactionsList, null, issueDate)
        {
        }

        
        public Invoice(string invoiceID, ClubMember clubMember, List<Transaction> transactionsList, DateTime issueDate)
            : this(invoiceID, clubMember, transactionsList, null, issueDate)
        {
        }

        public Invoice(ClubMember clubMember, List<Transaction> transactionsList, List<Bill> billsList, DateTime issueDate)
            :base(clubMember,transactionsList, issueDate)
        {
            InitializeInvoice(clubMember, billsList);
        }

        public Invoice(string invoiceID, ClubMember clubMember, List<Transaction> transactionsList, List<Bill> billsList, DateTime issueDate)
            : base(invoiceID, clubMember, transactionsList, issueDate)
        {
            InitializeInvoice(clubMember, billsList);
        }

        public enum InvoicePaymentState { ToBePaid, Paid, Unpaid, Cancelled, Uncollectible }

        public string CustomerFullName
        {
            get { return invoiceCustomerData.FullName; }
        }

        public Dictionary<string, Bill> Bills
        {
            get { return invoiceBills; }
        }

        public override decimal BillsTotalAmountToCollect
        {
            get { return GetBillsTotalAmount(Bill.BillPaymentResult.ToCollect); }
        }

        public InvoicePaymentState InvoiceState
        {
            get { return invoiceState; }
        }

        public void AcceptBillsPaymentAgreement (string agreementTerms, DateTime agreementDate, string[] billsToRenegotiateIDs, List<Bill> billsToAdd)
        {
        }

        /*public void ReplaceBills(string billID, List<Bill> billsList)
        {
            int billsCounter = this.Bills.Count;
            invoiceBills[billID].PaymentResult = Bill.BillPaymentResult.Renegotiated;
            foreach (Bill bill in billsList)
            {
                billsCounter++;
                string newBillID = invoiceID + "/" + billsCounter.ToString("000");
                bill.BillID = newBillID;
                invoiceBills.Add(newBillID, bill);
            }
        }*/

        public void Cancel()
        {
            this.invoiceState = InvoicePaymentState.Cancelled;
        }

        protected override string GetNewInvoiceID()
        {
            BillDataManager billDataManager = BillDataManager.Instance;
            string invoicePrefix = "MMM";
            string invoiceYear = "2013";
            return invoicePrefix + invoiceYear + billDataManager.NextInvoiceSequenceNumber.ToString("000000");
        }

        protected override void UpdateInvoiceSequenceNumber()
        {
            uint currentInvoiceSequenceNumber=ExtractInvoiceSequenceNumberFromInvoiceID();
            BillDataManager.Instance.SetLastInvoiceNumber(currentInvoiceSequenceNumber);
        }

        private decimal GetBillsTotalAmount(Bill.BillPaymentResult paymentResult)
        {
            decimal amount = 0;
            foreach (KeyValuePair<string, Bill> bill in invoiceBills)
            {
                if (bill.Value.PaymentResult == paymentResult) amount += bill.Value.Amount;
            }
            return amount;
        }

        private void InitializeInvoice(ClubMember clubMember, List<Bill> billsList)
        {
            this.invoiceCustomerData = new InvoiceCustomerData(clubMember);
            if (invoiceDetail.Count == 0) throw new ArgumentException("The transactions list is empty");
            invoiceBills = new Dictionary<string, Bill>();
            if (billsList == null) billsList = new List<Bill> { CreateASingleBillForInvoiceTotal() };
            AddBillsToInvoice(billsList);
            invoiceState = InvoicePaymentState.ToBePaid;
        }

        private Bill CreateASingleBillForInvoiceTotal()
        {
            string billID = invoiceID + "/001";
            string description = invoiceDetail[0].Description;
            DateTime dueDate = issueDate.AddDays(30);
            return new Bill(billID, description, NetAmount, issueDate, dueDate);
        }

        private void AddBillsToInvoice(List<Bill> billsList)
        {
            int billsCounter = 0;
            foreach (Bill bill in billsList)
            {
                billsCounter++;
                if (bill.BillID == null) bill.BillID = invoiceID + "/" + billsCounter.ToString("000");
                invoiceBills.Add(bill.BillID, bill);
            }
        }
    }
}
