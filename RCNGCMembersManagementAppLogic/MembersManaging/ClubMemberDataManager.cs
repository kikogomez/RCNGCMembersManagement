using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.MembersManaging
{
    class ClubMemberDataManager
    {
        private static readonly ClubMemberDataManager instance = new ClubMemberDataManager();

        static IDataManager dataManager;

        private ClubMemberDataManager()
        {
        }

        public static ClubMemberDataManager Instance
        {
            get { return instance; }
        }

        public uint MemberSequenceNumber
        {
            get { return GetMemberIDSequenceNumber(); }
            set { SetMemberIDSequenceNumber(value); }
        }

        public void SetDataManagerCollaborator(IDataManager dataMngr)
        {
            dataManager = dataMngr;
        }

        private uint GetMemberIDSequenceNumber()
        {
            uint invoiceSequenceNumber=dataManager.GetInvoiceSequenceNumber();
            if (invoiceSequenceNumber >= 1000000)
                throw new ArgumentOutOfRangeException("memberIDSequenceNumber", "Max 99999 members");
            return invoiceSequenceNumber;
        }

        private void SetMemberIDSequenceNumber(uint invoiceSequenceNumber)
        {
            if (!MemberIDSequenceNuberIsInRange(invoiceSequenceNumber))
                throw new ArgumentOutOfRangeException("memberIDSequenceNumber", "Member ID out of range (1-99999)");
            dataManager.SetInvoiceSequenceNumber(invoiceSequenceNumber);
        }

        private bool MemberIDSequenceNuberIsInRange(uint invoiceNumber)
        {
            return (1 <= invoiceNumber && invoiceNumber < 1000000);
        }
    }
}
