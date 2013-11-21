using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Billing.DirectDebit
{
    public class DirectDebitTransaction
    {
        string directDebitTransactionInternalReference;
        List<Bill> billsInTransaction;
        int internalDirectDebitReferenceNumber;
        BankAccount debtorAccount;

        decimal totalAmount;
        int numberOfBills;

        public DirectDebitTransaction(List<Bill> billsInTransaction, int internalDirectDebitReferenceNumber, BankAccount debtorAccount)
        {
            this.billsInTransaction = billsInTransaction;
            this.internalDirectDebitReferenceNumber = internalDirectDebitReferenceNumber;
            this.debtorAccount = debtorAccount;
            UpdateAmountAndNumberOfBills();
        }

        public List<Bill> BillsInTransaction
        {
            get { return billsInTransaction; }
        }

        public int InternalDirectDebitReferenceNumber
        {
            get { return internalDirectDebitReferenceNumber; }
        }

        public BankAccount DebtorAccount
        {
            get { return debtorAccount; }
        }

        public decimal Amount
        {
            get { return totalAmount; }
        }

        public int NumberOfBills
        {
            get { return numberOfBills; }
        }

        public void AddBill(Bill bill)
        {
            this.billsInTransaction.Add(bill);
            UpdateAmountAndNumberOfBills();
        }

        private void UpdateAmountAndNumberOfBills()
        {
            totalAmount = billsInTransaction.Select(bill => bill.Amount).Sum();
            numberOfBills = billsInTransaction.Count;
        }
    }
}
