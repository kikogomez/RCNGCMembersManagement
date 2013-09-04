using System;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        }

        [Given(@"These names ""(.*)"", ""(.*)"", ""(.*)""")]
        public void GivenTheseNames(string givenName, string firstSurname, string secondSurname)
        {
            membersManagementContextData.givenName = givenName;
            membersManagementContextData.firstSurname = firstSurname;
            membersManagementContextData.secondSurname = secondSurname;
        }
        
        [When(@"I process the names")]
        public void WhenIProcessTheNames()
        {
            ClubMember clubMember;
            try
            {
                clubMember = new ClubMember(
                    "0002",
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
        
        [Then(@"The name is considered ""(.*)""")]
        public void ThenTheNameIsConsidered(string validity)
        {
            bool valid = (validity == "valid" ? true : false);
            Assert.AreEqual(membersManagementContextData.clubMember != null, valid);
        }
    }
}
