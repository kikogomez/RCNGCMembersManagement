using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementUnitTests.DirectDebitPOCOClasses
{
    public class PaymentInfo
    {
        string paymentInformationID;
        string localInstrument;
        List<DirectDebitTransactionInfo> drctDbtTXList;

        public PaymentInfo(
            string paymentInformationID, 
            string localInstrument,
            List<DirectDebitTransactionInfo> drctDbtTXList)
        {
            this.paymentInformationID = paymentInformationID;
            this.localInstrument = localInstrument;
            this.drctDbtTXList = drctDbtTXList;
        }

        public string PaymentInformationID
        {
            get { return paymentInformationID; }
        }

        public string LocalInstrument
        {
            get { return localInstrument; }
        }

        public List<DirectDebitTransactionInfo> DrctDbtTXList
        {
            get { return drctDbtTXList; }
        }
    }
}
