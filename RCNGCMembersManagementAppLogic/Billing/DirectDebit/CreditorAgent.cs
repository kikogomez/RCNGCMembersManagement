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
        //Dictionary<string, DirectDebitInitiationContract> directDebitInitiationContracts;

        public CreditorAgent(BankCode bankCode)
        {
            this.bankCode = bankCode;
            //directDebitInitiationContracts = new Dictionary<string, DirectDebitInitiationContract>();
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
/*
        public Dictionary<string, DirectDebitInitiationContract> DirectDebitInitiationContracts
        {
            get { return directDebitInitiationContracts; }
        }

        public void AddDirectDebitInitiacionContract(DirectDebitInitiationContract creditorAgentDirectDebitInitiationContract)
        {
            directDebitInitiationContracts.Add(
                creditorAgentDirectDebitInitiationContract.CreditorBussinessCode,
                creditorAgentDirectDebitInitiationContract);
        }*/
    }
}
