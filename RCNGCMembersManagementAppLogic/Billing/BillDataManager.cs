using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Billing
{
/*    public sealed class Singleton
    {
        private static readonly Singleton instance = new Singleton();

        private Singleton() { }

        public static Singleton Instance
        {
            get
            {
                return instance;
            }
        }
    }*/

/*    public sealed class BillDataManager
    {
        private static readonly BillDataManager instance = new BillDataManager(invoiceDataManager);
        
        static IInvoiceDataManager invoiceDataManager;

        private BillDataManager(IInvoiceDataManager invoiceDataMngr)
        {
            invoiceDataManager = invoiceDataMngr;
        }

        public static BillDataManager Instance
        {
            get { return instance; }
        }

        public int GetNextInvoiceID()
        {
            return invoiceDataManager.GetNextInvoiceID();
        }
    }
*/

    public sealed class BillDataManager
    {
        private static readonly BillDataManager instance = new BillDataManager();

        static IInvoiceDataManager invoiceDataManager;

        private BillDataManager()
        {
            //invoiceDataManager = invoiceDataMngr;
        }

        public void SetInvoiceDataManagerCollaborator(IInvoiceDataManager invoiceDataMngr)
        {
            invoiceDataManager = invoiceDataMngr;
        }

        public static BillDataManager Instance
        {
            get { return instance; }
        }

        public int GetNextInvoiceID()
        {
            return invoiceDataManager.GetNextInvoiceID();
        }
    }

/*    public static class BillDataManager
    {
        static IInvoiceDataManager _invoiceDataManager;

        public static BillDataManager(IInvoiceDataManager invoiceDataManager)
        {
            _invoiceDataManager = invoiceDataManager;
        }

        public static int GetNextInvoiceID()
        {
            return _invoiceDataManager.GetNextInvoiceID();
        }
    }
 */
}
