using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.MembersManaging
{
    public class ClubMember
    {
        string memberID;
        string name;
        string firstSurname;
        string secondSurname;

        public ClubMember(string memberID, string name, string firstSurname, string secondSurname)
        {
            this.memberID=memberID;
            this.name=name;
            this.firstSurname=firstSurname;
            this.secondSurname=secondSurname;
        }

        public string MemberID
        {
            get {return memberID;}
        }

        public string FullName
        {
            get { return GetFullname(); }
        }

        private string GetFullname()
        {
            return (name ?? "" + " " + firstSurname ?? "" + " " + secondSurname ?? "").Trim();
        }
    }
}
