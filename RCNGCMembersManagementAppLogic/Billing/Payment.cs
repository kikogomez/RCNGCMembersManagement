using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Billing
{
    public class Payment
    {
        DateTime paymentDate;
        PaymentMethod paymentMethod;

        public Payment(DateTime paymentDate, PaymentMethod paymentMethod)
        {
            this.paymentDate = paymentDate;
            this.paymentMethod = paymentMethod;
        }
    }
}
