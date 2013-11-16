using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementUnitTests.DirectDebitPOCOClasses
{
    public class PaymentInfo_POCOTestClass
    {
        string paymentInformationID;
        string localInstrument;
        List<DirectDebitTransactionInfo_POCOTestClass> drctDbtTXList;

        public PaymentInfo_POCOTestClass(
            string paymentInformationID, 
            string localInstrument,
            List<DirectDebitTransactionInfo_POCOTestClass> drctDbtTXList)
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

        public List<DirectDebitTransactionInfo_POCOTestClass> DrctDbtTXList
        {
            get { return drctDbtTXList; }
        }
    }
}
