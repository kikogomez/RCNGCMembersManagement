using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Billing.DirectDebit
{
    class DirectDebit: PaymentMethod
    {
        private DirectDebitMandate directDebitMandate;

        public DirectDebit(DirectDebitMandate directDebitMandate)
            : base()
        {
            this.directDebitMandate = directDebitMandate;
        }
    }
}
