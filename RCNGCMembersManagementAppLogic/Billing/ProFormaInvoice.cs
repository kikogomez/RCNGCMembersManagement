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
    public class ProFormaInvoice: BaseInvoice
    {
        public ProFormaInvoice(ClubMember clubMember, List<Transaction> transactionsList, DateTime issueDate)
            : base(transactionsList, issueDate)
        {
            InitializeProformaInvoice(clubMember);
        }
        
        public decimal BillsTotalAmountToCollect
        {
            get { return 0; }
        }

        public void SetNewInvoiceDetail(List<Transaction> invoiceDetail)
        {
            this.invoiceDetail = invoiceDetail;
        }

        protected override string GetNewInvoiceID()
        {
            string invoicePrefix = billingDataManager.ProFormaInvoicePrefix;
            string invoiceYear = "2013";
            return invoicePrefix + invoiceYear + billingDataManager.ProFormaInvoiceSequenceNumber.ToString("000000");
        }

        protected override void UpdateInvoiceSequenceNumber()
        {
            uint currentInvoiceSequenceNumber=ExtractInvoiceSequenceNumberFromInvoiceID();
            billingDataManager.ProFormaInvoiceSequenceNumber = currentInvoiceSequenceNumber + 1;
        }

        private void InitializeProformaInvoice(ClubMember clubMember)
        {
            CheckProFormaInvoiceDetail();
            this.customerData = new InvoiceCustomerData(clubMember);
        }

        private void CheckProFormaInvoiceDetail()
        {
            foreach (Transaction transaction in invoiceDetail)
            {
                if (transaction.Units < 1)
                    throw new System.ArgumentOutOfRangeException("units", "Pro Forma Invoice transactions must have at least one element to transact");
            }
        }
    }
}
