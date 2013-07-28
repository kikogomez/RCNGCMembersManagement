using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Billing
{
    class AmendingInvoice
    {
        string ammededInvoiceID;


        public AmendingInvoice(Invoice invoiceToAmmend)
        {
            this.ammededInvoiceID = invoiceToAmmend.InvoiceID;
        }
    }
}
