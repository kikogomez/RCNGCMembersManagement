using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Billing
{
    class AmendingInvoice: BaseInvoice
    {
        string amendedInvoiceID;

        public AmendingInvoice(Invoice invoiceToAmend)
            :base(null,DateTime.Now)
        {
            BillingDataManager billDataManager = BillingDataManager.Instance;
            this.amendedInvoiceID = invoiceToAmend.InvoiceID.Replace(billDataManager.InvoicePrefix, billDataManager.AmendingInvoicePrefix); ;
            invoiceDetail = CreateAmendingTransactions(invoiceToAmend);
        }

        private List<Transaction> CreateAmendingTransactions(Invoice invoiceToAmend)
        {
            List<Transaction> amendingInvoiceDetail = new List<Transaction>();
            Tax voidTax = new Tax("",0);
            Transaction originalInvoiceReference = new Transaction("Amending invoice " + invoiceToAmend.InvoiceID + "as detailed", 1, 0, voidTax, 0);
            amendingInvoiceDetail.Add(originalInvoiceReference);
            foreach (Transaction transaction in invoiceToAmend.InvoiceDetail)
            {
                Transaction amendedTransaction = new Transaction(
                    "Amending " + transaction.Description,-transaction.Units, transaction.UnitCost, transaction.Tax, transaction.Discount);
                amendingInvoiceDetail.Add(amendedTransaction);
            }
            return amendingInvoiceDetail;
        }

        protected override string GetNewInvoiceID()
        {
            return null;
        }

        protected override void UpdateInvoiceSequenceNumber()
        {
        }
    }
}
