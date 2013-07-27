using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Billing
{
    class BillPaymentAgreement
    {
        string agreementTerms;
        DateTime agreementDate;
        string[] renegotiatedBillsIDs;
        string[] addedBillsIDs;

        public BillPaymentAgreement(string agreementTerms, DateTime agreementDate, string[] renegotiatedBillsIDs, string[] addedBillsIDs)
        {
            this.agreementTerms = agreementTerms;
            this.agreementDate = agreementDate;
            renegotiatedBillsIDs.CopyTo(this.renegotiatedBillsIDs,0);
            addedBillsIDs.CopyTo(this.addedBillsIDs, 0);
        }
    }
}
