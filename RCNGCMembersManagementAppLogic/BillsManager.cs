using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCNGCMembersManagementAppLogic.Billing;

namespace RCNGCMembersManagementAppLogic
{
    public class BillsManager
    {
        public void PayBill(Invoice invoiceContainingTheBill, Bill billToPay, Payment payment)
        {
            billToPay.PayBill(payment);
            invoiceContainingTheBill.CheckIfInvoiceIsFullPaid();
        }
    }
}
