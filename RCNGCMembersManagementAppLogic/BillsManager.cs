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
        public void RenegotiateBills(Invoice invoice, List<Bill> billsToRenegotiate, List<Bill> billsToAdd, PaymentAgreement paymentAgreement)
        {
            invoice.AcceptBillsPaymentAgreement(paymentAgreement, billsToRenegotiate, billsToAdd);
        }
        
        public void PayBill(Bill billToPay, PaymentMethod paymentMethod, DateTime paymentDate)
        {
        }
    }
}
