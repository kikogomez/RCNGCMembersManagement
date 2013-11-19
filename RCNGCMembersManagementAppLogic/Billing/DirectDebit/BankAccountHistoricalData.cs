﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Billing.DirectDebit
{
    public class BankAccountHistoricalData
    {
        BankAccount bankAccount;
        DateTime accountActivationDate;
        DateTime accountDeactivationDate;

        public BankAccountHistoricalData(BankAccount bankAccount, DateTime accountActivationDate, DateTime accountDeactivationDate)
        {
            this.bankAccount = bankAccount;
            this.accountActivationDate = accountActivationDate;
            this.accountDeactivationDate = accountDeactivationDate;
        }

        public BankAccount BankAccount
        {
            get { return bankAccount; }
        }

        public DateTime AccountActivationDate
        {
            get { return accountActivationDate; }
        }
        public DateTime AccountDeactivationDate
        {
            get { return accountDeactivationDate; }
        }
    }
}
