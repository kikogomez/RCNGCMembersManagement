using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Billing.DirectDebit
{
    public class DirectDebitTransactionGroupPaymentInfo
    {
        string paymentInformationID;
        string localInstrument;
        List<DirectDebitTransactionInfo> drctDbtTXList;

        public DirectDebitTransactionGroupPaymentInfo(string localInstrument)
        {
        }
    }
}
