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
    public abstract class BaseInvoice
    {
        protected InvoiceCustomerData customerData;
        protected string invoiceID;
        protected DateTime issueDate;
        protected string clientFullName;
        protected List<Transaction> invoiceDetail;

        public BaseInvoice(List<Transaction> transactionsList, DateTime issueDate)
            : this(null, transactionsList, issueDate)
        {
            UpdateInvoiceSequenceNumber();
        }

        public BaseInvoice(string invoiceID, List<Transaction> transactionsList, DateTime issueDate)
        {
            this.invoiceID = (invoiceID == null) ? GetNewInvoiceID(): invoiceID;
            this.issueDate = issueDate;
            invoiceDetail = transactionsList;
        }

        public string InvoiceID
        {
            get { return this.invoiceID; }
        }

        public decimal NetAmount
        {
            get { return CalculateInvoiceAmounts(true); }
        }

        public abstract decimal BillsTotalAmountToCollect
        {
            get;
        }

        protected abstract string GetNewInvoiceID();

        protected abstract void UpdateInvoiceSequenceNumber();

        protected uint ExtractInvoiceSequenceNumberFromInvoiceID()
        {
            return uint.Parse(invoiceID.Substring(7));
        }

        protected decimal CalculateInvoiceAmounts(bool netAmount)
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
