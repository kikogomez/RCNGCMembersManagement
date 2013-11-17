﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Billing.DirectDebit
{
    public class DirectDebitInitiationContract
    {
        BankAccount creditorAccount;
        string creditorBusinessCode;
        string creditorID;
        CreditorAgent creditorAgent;

        public DirectDebitInitiationContract(BankAccount creditorAccount, string nIF, string creditorBusinessCode, CreditorAgent creditorAgent)
        {
            this.creditorAccount = creditorAccount;
            this.creditorBusinessCode = creditorBusinessCode;
            GenerateCreditID(nIF,creditorBusinessCode);
            this.creditorAgent = creditorAgent;
        }

        public BankAccount CreditorAcount
        {
            get { return creditorAccount; }
        }

        public string CreditorBussinessCode
        {
            get { return creditorBusinessCode; }
        }

        public CreditorAgent CreditorAgent
        {
            get { return creditorAgent; }
        }

        private void GenerateCreditID(string nIF, string creditorBusinessCode)
        {
            SEPAAttributes sEPAAttributes = new SEPAAttributes();
            creditorID = sEPAAttributes.AT02CreditorIdentifier("ES", nIF, creditorBusinessCode);
        }
    }
}