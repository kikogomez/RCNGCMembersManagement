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
        static int lastInvoiceNumber = 0;

        public InvoiceDataManagerMock()
        {
        }

        public int GetNextInvoiceNumber()
        {
            lastInvoiceNumber++;
            return lastInvoiceNumber;
        }

        public void SetInvoiceNumber(int invoiceNumber)
        {
            InvoiceDataManagerMock.lastInvoiceNumber = invoiceNumber;
        }
    }
}
