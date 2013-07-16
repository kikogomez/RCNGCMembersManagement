using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCNGCMembersManagementAppLogic.MembersManaging;
using RCNGCMembersManagementAppLogic.ClubServices;


namespace RCNGCMembersManagementAppLogic.Billing
{
    public class Invoice
    {
        string invoiceID;
        DateTime issueDate;
        string memberID;
        string clientFullName;
        List<Transaction> invoiceDetail;
        List<Bill> invoiceBills;

        public Invoice(ClubMember clubMember, ClubService clubService, DateTime issueDate)
        {
            this.invoiceID = GetNewInvoiceID();
            this.issueDate = issueDate;
            this.memberID = clubMember.MemberID;
            this.clientFullName = clubMember.FullName;
            invoiceDetail = new List<Transaction>();
            Transaction simpleServiceTransaction = new Transaction(clubService, clubService.Description, 1, clubService.Cost,clubService.Tax,0);
            invoiceDetail.Add(simpleServiceTransaction);
            invoiceBills = new List<Bill>();
            AddBillForInvoiceTotal(clubService.Description, issueDate, issueDate.AddDays(30));
            UpdateInvoiceSequenceNumber();
        }

        public Invoice(ClubMember clubMember, List<Transaction> transactionsList, DateTime issueDate)
        {
            this.invoiceID = GetNewInvoiceID();
            this.issueDate = issueDate;
            this.memberID = clubMember.MemberID;
            this.clientFullName = clubMember.FullName;
            invoiceDetail = transactionsList;
            invoiceBills = new List<Bill>();
            AddBillForInvoiceTotal("Club Services", issueDate, issueDate.AddDays(30));
            UpdateInvoiceSequenceNumber();
        }

        public string InvoiceID
        {
            get { return this.invoiceID; }
        }

        public decimal NetAmount
        {
            get { return CalculateInvoiceAmounts(true); }
        }

        public decimal GrossAmount
        {
            get { return CalculateInvoiceAmounts(false); }
        }

        public decimal BillsTotalAmountToCollect
        {
            get { return GetBillsTotalAmount(Bill.billPaymentResult.ToCollect); }
        }

        public DateTime IssueDate
        {
            get { return this.issueDate; }
        }


        public void AddBillForInvoiceTotal(string description, DateTime issueDate, DateTime dueDate)
        {
            Bill invoiceBill = new Bill(invoiceID + "/1", description, NetAmount, issueDate, dueDate);
            invoiceBills.Add(invoiceBill);
        }

        private string GetNewInvoiceID()
        {
            string invoicePrefix = "MMM";
            string invoiceYear = "2013";
            return invoicePrefix + invoiceYear + BillDataManager.Instance.GetNextInvoiceSequenceNumber().ToString("000000");
        }

        private void UpdateInvoiceSequenceNumber()
        {
            uint currentInvoiceSequenceNumber=ExtractInvoiceSequenceNumberFromInvoiceID();
            BillDataManager.Instance.SetInvoiceNumber(currentInvoiceSequenceNumber);
        }

        private uint ExtractInvoiceSequenceNumberFromInvoiceID()
        {
            return uint.Parse(invoiceID.Substring(7));
        }

        private decimal CalculateInvoiceAmounts(bool netAmount)
        {
            decimal amount = 0;
            foreach (Transaction line in invoiceDetail)
            {
                amount += (netAmount ? (decimal)line.NetAmount : line.GrossAmount);
            }
            return amount;
        }

        private decimal GetBillsTotalAmount(Bill.billPaymentResult paymentResult)
        {
            decimal amount = 0;
            foreach (Bill bill in invoiceBills)
            {
                if (bill.BillPaymentResult == paymentResult) amount += bill.Amount;
            }
            return amount;
        }
    }
}
