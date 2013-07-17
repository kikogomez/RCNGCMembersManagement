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
            : base(clubMember, transactionsList, issueDate)
        {
        }
        
        public override decimal BillsTotalAmountToCollect
        {
            get { return 0; }
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
    }
}
