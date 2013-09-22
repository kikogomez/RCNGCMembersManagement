using System;
using System.Globalization;
using System.Collections.Generic;
using RCNGCMembersManagementAppLogic.MembersManaging;
using RCNGCMembersManagementAppLogic.ClubServices;
using RCNGCMembersManagementAppLogic.ClubStore;
using RCNGCMembersManagementAppLogic.Billing;

namespace RCNGCMembersManagementSpecFlowBDD
{
    public class MembersManagementContextData
    {
        public ClubMember clubMember;
        public string givenName;
        public string firstSurname;
        public string secondSurname;
        public ClubMemberDataManager clubMemberDataManager = ClubMemberDataManager.Instance;
    }

    public class InvoiceContextData
    {
        public Dictionary<string, Tax> taxesDictionary;
        public Dictionary<string, ClubService> servicesDictionary;
        public Dictionary<string, Product> productsDictionary;
        public List<Transaction> tansactionsList;
        public string lastInvoiceID;
        public BillingDataManager billDataManager = BillingDataManager.Instance;
    }

    public class BankAccountContextData
    {
        public string bank;
        public string office;
        public string checkDigits;
        public string accountNumber;
        public string ccc;
        public string iban;
    }
}