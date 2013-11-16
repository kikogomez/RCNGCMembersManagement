using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Billing.DirectDebit
{
    public class CreditorAgent
    {
        BankCode bankCode;
        Dictionary<string, CreditorAgentDirectDebitInitiationContract> directDebitInitiationContracts;

        public CreditorAgent(BankCode bankCode)
        {
            this.bankCode = bankCode;
            directDebitInitiationContracts = new Dictionary<string, CreditorAgentDirectDebitInitiationContract>();
        }

        public string LocalBankCode
        {
            get { return bankCode.LocalBankCode; }
        }

        public string BankName
        {
            get { return bankCode.BankName; }
        }

        public string BankBIC
        {
            get { return bankCode.BankBIC; }
        }

        public Dictionary<string, CreditorAgentDirectDebitInitiationContract> DirectDebitInitiationContracts
        {
            get { return directDebitInitiationContracts; }
        }

        public void AddDirectDebitInitiacionContract(CreditorAgentDirectDebitInitiationContract creditorAgentDirectDebitInitiationContract)
        {
            directDebitInitiationContracts.Add(
                creditorAgentDirectDebitInitiationContract.CreditorBussinessCode,
                creditorAgentDirectDebitInitiationContract);
        }
    }
}
