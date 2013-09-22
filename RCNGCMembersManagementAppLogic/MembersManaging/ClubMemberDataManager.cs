using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.MembersManaging
{
    public sealed class ClubMemberDataManager
    {
        private static readonly ClubMemberDataManager instance = new ClubMemberDataManager();

        static IDataManager dataManagerCollaborator;

        static uint memberIDLowerLimit = 1;
        static uint memberIDUpperLimit = 100000;

        private ClubMemberDataManager()
        {
        }

        public static ClubMemberDataManager Instance
        {
            get { return instance; }
        }

        public uint MemberIDSequenceNumber
        {
            get { return GetMemberIDSequenceNumber(); }
            set { SetMemberIDSequenceNumber(value); }
        }

        public bool AvailableMembersIDAreExhausted
        {
            get { return (MemberIDSequenceNumber == memberIDUpperLimit); }
        }

        public uint AssingnMemberIDSequenceNumber()
        {
            if (MemberIDSequenceNuberIsInRange(MemberIDSequenceNumber))
            {
                return MemberIDSequenceNumber++;
            }
            else
            {
                throw new ArgumentOutOfRangeException(
                    "memberIDSequenceNumber", 
                    "Member ID out of range (" + memberIDLowerLimit.ToString() + "-" + (memberIDUpperLimit-1).ToString() + ")"); 
            }
        }

        public void SetDataManagerCollaborator(IDataManager dataManagerCollaborator)
        {
            ClubMemberDataManager.dataManagerCollaborator = dataManagerCollaborator;
        }

        private uint GetMemberIDSequenceNumber()
        {
            uint memberSequenceNumber=dataManagerCollaborator.GetMemberIDSequenceNumber();
            return memberSequenceNumber;
        }

        private void SetMemberIDSequenceNumber(uint memberIDSequenceNumber)
        {
            if (MemberIDSequenceNuberIsInRange(memberIDSequenceNumber) || memberIDSequenceNumber == memberIDUpperLimit)
            {
                dataManagerCollaborator.SetMemberIDSequenceNumber(memberIDSequenceNumber);
                return;
            }
            else
            {
                throw new ArgumentOutOfRangeException(
                    "memberIDSequenceNumber",
                    "Member ID out of range (" + memberIDLowerLimit.ToString() + "-" + (memberIDUpperLimit - 1).ToString() + ")");
            }
        }

        private bool MemberIDSequenceNuberIsInRange(uint memberID)
        {
            return (memberIDLowerLimit <= memberID && memberID < (memberIDUpperLimit));
        }
    }
}
