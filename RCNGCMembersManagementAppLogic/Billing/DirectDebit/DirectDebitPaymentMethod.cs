using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Billing.DirectDebit
{
    class DirectDebitPaymentMethod: PaymentMethod
    {
        private DirectDebitMandate directDebitMandate;

        public DirectDebitPaymentMethod(DirectDebitMandate directDebitMandate)
            : base()
        {
            this.directDebitMandate = directDebitMandate;
        }
    }
}
