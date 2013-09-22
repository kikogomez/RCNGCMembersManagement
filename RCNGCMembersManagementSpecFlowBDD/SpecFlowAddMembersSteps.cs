using System;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementMocks;
using RCNGCMembersManagementAppLogic.MembersManaging;

namespace RCNGCMembersManagementSpecFlowBDD
{
    [Binding]
    public class SpecFlowAddMembersSteps
    {
        private readonly MembersManagementContextData membersManagementContextData;

        public SpecFlowAddMembersSteps(MembersManagementContextData membersManagementContextData)
        {
            this.membersManagementContextData = membersManagementContextData;
            DataManagerMock clubMemberDataManagerMock = new DataManagerMock();
            membersManagementContextData.clubMemberDataManager.SetDataManagerCollaborator(clubMemberDataManagerMock);
        }

        [Given(@"These names ""(.*)"", ""(.*)"", ""(.*)""")]
        public void GivenTheseNames(string givenName, string firstSurname, string secondSurname)
        {
            membersManagementContextData.givenName = givenName;
            membersManagementContextData.firstSurname = firstSurname;
            membersManagementContextData.secondSurname = secondSurname;
        }

        [Given(@"The current memberID sequence number is (.*)")]
        public void GivenTheCurrentMemberIDSequenceNumberIs(uint memberIDSequenceNumber)
        {
            membersManagementContextData.clubMemberDataManager.MemberIDSequenceNumber = memberIDSequenceNumber;
        }

/*        [Given(@"I create a member with an ID ""(.*)""")]
        public void GivenICreateAMemberWithAnID(string memberID)
        {
            ClubMember clubMember;
            try
            {
                clubMember = new ClubMember(
                    memberID,
                    membersManagementContextData.givenName,
                    membersManagementContextData.firstSurname,
                    membersManagementContextData.secondSurname);
                membersManagementContextData.clubMember = clubMember;
            }
            catch
            {
                membersManagementContextData.clubMember = null;
            } 
        }*/
        
        [When(@"I process the names")]
        public void WhenIProcessTheNames()
        {
            ClubMember clubMember;
            membersManagementContextData.clubMemberDataManager.MemberIDSequenceNumber = 1;
            try
            {
                clubMember = new ClubMember(
                    membersManagementContextData.givenName,
                    membersManagementContextData.firstSurname,
                    membersManagementContextData.secondSurname);
                membersManagementContextData.clubMember=clubMember;
            }
            catch
            {
                membersManagementContextData.clubMember = null;
            }     
        }

        [When(@"I add a new member")]
        public void WhenIAddANewMember()
        {
            ClubMember clubMember;
            try
            {
                clubMember = new ClubMember("Francisco","Gomez-Caldito",null);
                membersManagementContextData.clubMember = clubMember;
            }
            catch
            {
                membersManagementContextData.clubMember = null;
            }    
        }
        
        [Then(@"The name is considered ""(.*)""")]
        public void ThenTheNameIsConsidered(string validity)
        {
            bool valid = (validity == "valid" ? true : false);
            Assert.AreEqual(membersManagementContextData.clubMember != null, valid);
        }

/*        [Then(@"The new member ID is ""(.*)""")]
        public void ThenTheNewMemberIDIs(string memberID)
        {
            Assert.AreEqual(memberID, membersManagementContextData.clubMember.MemberID);
        }*/

        [Then(@"The current memberID sequence number is (.*)")]
        public void ThenTheCurrentMemberIDSequenceNumberIs(uint memberID)
        {
            Assert.AreEqual(memberID, membersManagementContextData.clubMemberDataManager.MemberIDSequenceNumber);
        }


        [Then(@"The new member is not created")]
        public void ThenTheNewMemberIsNotCreated()
        {
            Assert.AreEqual(null, membersManagementContextData.clubMember);
        }
    }
}
