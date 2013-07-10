using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCNGCMembersManagementAppLogic.Billing;

namespace RCNGCMembersManagementMocks
{
    public class InvoiceDataManagerMock : IInvoiceDataManager
    {
        static int lastInvoiceID = 0;
        public InvoiceDataManagerMock()
        {
        }

        public int GetNextInvoiceID()
        {
            lastInvoiceID++;
            return lastInvoiceID;
        }
    }
}
