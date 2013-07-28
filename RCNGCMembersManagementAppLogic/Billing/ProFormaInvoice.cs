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
            BillDataManager billDataManager = BillDataManager.Instance;
            string invoicePrefix = "PPP";
            string invoiceYear = "2013";
            return invoicePrefix + invoiceYear + billDataManager.NextInvoiceSequenceNumber.ToString("000000");
        }

        protected override void UpdateInvoiceSequenceNumber()
        {
            uint currentInvoiceSequenceNumber=ExtractInvoiceSequenceNumberFromInvoiceID();
            BillDataManager.Instance.SetLastInvoiceNumber(currentInvoiceSequenceNumber);
        }

        private void InitializeProformaInvoice(ClubMember clubMember)
        {
            this.customerData = new InvoiceCustomerData(clubMember);
        }
    }
}
