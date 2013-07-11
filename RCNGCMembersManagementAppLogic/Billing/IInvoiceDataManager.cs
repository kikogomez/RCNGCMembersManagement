using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Billing
{
    public interface IInvoiceDataManager
    {
        int GetNextInvoiceNumber();
        void SetInvoiceNumber(int invoiceNumber);
    }
}
