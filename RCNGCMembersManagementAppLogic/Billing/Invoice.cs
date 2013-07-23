using System;
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
        int billsCounter;

        public Invoice(ClubMember clubMember, List<Transaction> transactionsList, DateTime issueDate)
            :base(clubMember,transactionsList,issueDate)
        {
            if (transactionsList.Count == 0) throw new ArgumentException("The transactions list is empty");
            invoiceBills = new Dictionary<string, Bill>();
            billsCounter = 0;
            AddBillForInvoiceTotal("Club Services", issueDate, issueDate.AddDays(30));
            invoiceState = InvoicePaymentState.ToBePaid;
        }

        public Invoice(string invoiceID, ClubMember clubMember, List<Transaction> transactionsList, DateTime issueDate)
            : base(invoiceID, clubMember, transactionsList, issueDate)
        {
            if (transactionsList.Count == 0) throw new ArgumentException("The transactions list is empty");
            invoiceBills = new Dictionary<string, Bill>();
            billsCounter = 0;
            AddBillForInvoiceTotal("Club Services", issueDate, issueDate.AddDays(30));
            invoiceState = InvoicePaymentState.ToBePaid;
        }

        public enum InvoicePaymentState { ToBePaid, Paid, Unpaid, Cancelled, Uncollectible }

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

        public void AddBillForInvoiceTotal(string description, DateTime issueDate, DateTime dueDate)
        {
            billsCounter++;
            string billID = invoiceID + "/" + billsCounter.ToString("000");
            Bill invoiceBill = new Bill(billID, description, NetAmount, issueDate, dueDate);
            invoiceBills.Add(billID, invoiceBill);
        }

        public void ReplaceBills(string billID, List<Bill> billsList)
        {
            invoiceBills.Remove(billID);
            foreach (Bill bill in billsList)
            {
                billsCounter++;
                string newBillID = invoiceID + "/" + billsCounter.ToString("000");
                bill.BillID = newBillID;
                invoiceBills.Add(newBillID, bill);
            }
        }

        public void Cancel()
        {
            this.invoiceState = InvoicePaymentState.Cancelled;
        }

        protected override string GetNewInvoiceID()
        {
            string invoicePrefix = "MMM";
            string invoiceYear = "2013";
            return invoicePrefix + invoiceYear + BillDataManager.Instance.GetNextInvoiceSequenceNumber().ToString("000000");
        }

        protected override void UpdateInvoiceSequenceNumber()
        {
            uint currentInvoiceSequenceNumber=ExtractInvoiceSequenceNumberFromInvoiceID();
            BillDataManager.Instance.SetInvoiceNumber(currentInvoiceSequenceNumber);
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
    }
}
