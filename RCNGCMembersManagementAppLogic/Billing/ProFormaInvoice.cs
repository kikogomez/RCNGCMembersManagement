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
    public class ProFormaInvoice
    {
        string invoiceID;
        DateTime issueDate;
        string memberID;
        string clientFullName;
        List<Transaction> invoiceDetail;

        public ProFormaInvoice(ClubMember clubMember, List<Transaction> transactionsList, DateTime issueDate)
        {
            if (transactionsList.Count == 0) throw new ArgumentException("The transactions list is empty");
            this.invoiceID = GetNewInvoiceID();
            this.issueDate = issueDate;
            this.memberID = clubMember.MemberID;
            this.clientFullName = clubMember.FullName;

            invoiceDetail = transactionsList;
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
    }
}
