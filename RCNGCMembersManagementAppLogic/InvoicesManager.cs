using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCNGCMembersManagementAppLogic.Billing;
using RCNGCMembersManagementAppLogic.MembersManaging;

namespace RCNGCMembersManagementAppLogic
{
    public class InvoicesManager
    {
        public void AddInvoiceToClubMember(Invoice invoice, ClubMember debtor)
        {
            debtor.AddInvoice(invoice);
        }

        public void CancelInvoice(Invoice invoiceToCancel, ClubMember debtor)
        {
            invoiceToCancel.Cancel();
            AmendingInvoice amendingInvoice = new AmendingInvoice(invoiceToCancel);
            debtor.AddAmendingInvoice(amendingInvoice);
        }
    }
}
