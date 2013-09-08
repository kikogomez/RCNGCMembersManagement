using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementAppLogic;
using RCNGCMembersManagementAppLogic.MembersManaging;
using RCNGCMembersManagementMocks;
using ExtensionMethods;

namespace RCNGCMembersManagementUnitTests.MembersManaging
{
    [TestClass]
    public class ClubMemberUnitTests
    {

        static ClubMemberDataManager clubMemberDataManager;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            clubMemberDataManager = ClubMemberDataManager.Instance;
            DataManagerMock clubMemberDataManagerMock = new DataManagerMock();
            clubMemberDataManager.SetDataManagerCollaborator(clubMemberDataManagerMock);
        }

        [TestMethod]
        public void InstantiatingASimpleClubMember()
        {
            ClubMember clubMember = new ClubMember("0002", "Francisco", "Gómez-Caldito", "Viseas");
            Assert.IsNotNull(clubMember);
        }

        [TestMethod]
        public void NamesAndSurnamesAreTrimmed()
        {
            ClubMember clubMember = new ClubMember("0002", "Francisco   ", "     Gómez-Caldito  ", "        ");
            Assert.AreEqual("Francisco", clubMember.Name);
            Assert.AreEqual("Gómez-Caldito", clubMember.FirstSurname);
            Assert.AreEqual("", clubMember.SecondSurname);
        }

        [TestMethod]
        public void FullNameIsWellCalculatedGivenNameAndBothSurnames()
        {
            ClubMember clubMember = new ClubMember("0002", "Francisco", "Gómez-Caldito", "Viseas");
            string expectedFullname = "Francisco Gómez-Caldito Viseas";
            Assert.AreEqual(expectedFullname, clubMember.FullName);
        }

        [TestMethod]
        public void FullNameIsWellCalculatedGivenEmptySecondSurname()
        {
            ClubMember clubMember = new ClubMember("0002", "Francisco", "Gómez-Caldito", "");
            string expectedFullname = "Francisco Gómez-Caldito";
            Assert.AreEqual(expectedFullname, clubMember.FullName);
        }

        [TestMethod]
        public void FullNameIsWellCalculatedGivenNullSecondSurname()
        {
            ClubMember clubMember = new ClubMember("0002", "Francisco", "Gómez-Caldito", null);
            string expectedFullname = "Francisco Gómez-Caldito";
            Assert.AreEqual(expectedFullname, clubMember.FullName);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void EmptyNameThrowsException()
        {
            try
            {
                ClubMember clubMebber = new ClubMember("0002", "", "Gómez-Caldito", "Viseas");
            }
            catch (ArgumentException exception)
            {
                Assert.AreEqual("Club Member name cannot be empty", exception.GetMessageWithoutParamName());
                Assert.AreEqual("name", exception.ParamName);
                throw exception;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void NameWithOnlySpacesThrowsException()
        {
            try
            {
                ClubMember clubMebber = new ClubMember("0002", "    ", "Gómez-Caldito", "Viseas");
            }
            catch (ArgumentException exception)
            {
                Assert.AreEqual("Club Member name cannot be empty", exception.GetMessageWithoutParamName());
                Assert.AreEqual("name", exception.ParamName);
                throw exception;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void NullNameThrowsException()
        {
            try
            {
                ClubMember clubMebber = new ClubMember("0002", null, "Gómez-Caldito", "Viseas");
            }
            catch (ArgumentException exception)
            {
                Assert.AreEqual("Club Member name cannot be empty", exception.GetMessageWithoutParamName());
                Assert.AreEqual("name", exception.ParamName);
                throw exception;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void EmptyFirstSurnameThrowsException()
        {
            try
            {
                ClubMember clubMebber = new ClubMember("0002", "Francisco", "", "Viseas");
            }
            catch (ArgumentException exception)
            {
                Assert.AreEqual("Club Member first surname cannot be empty", exception.GetMessageWithoutParamName());
                Assert.AreEqual("firstSurname", exception.ParamName);
                throw exception;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void FirstSurnameWithOnlySpacesThrowsException()
        {
            try
            {
                ClubMember clubMebber = new ClubMember("0002", "Francisco", "     ", "Viseas");
            }
            catch (ArgumentException exception)
            {
                Assert.AreEqual("Club Member first surname cannot be empty", exception.GetMessageWithoutParamName());
                Assert.AreEqual("firstSurname", exception.ParamName);
                throw exception;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void NullFirstSurnameThrowsException()
        {
            try
            {
                ClubMember clubMebber = new ClubMember("0002", "Francisco", null, "Viseas");
            }
            catch (ArgumentException exception)
            {
                Assert.AreEqual("Club Member first surname cannot be empty", exception.GetMessageWithoutParamName());
                Assert.AreEqual("firstSurname", exception.ParamName);
                throw exception;
            }
        }

        [TestMethod]
        public void ThereIsAMembersIDCounterWichICanSet()
        {
            clubMemberDataManager.MemberSequenceNumber = 2;
            Assert.AreEqual((uint)2, clubMemberDataManager.MemberSequenceNumber);
        }

        [TestMethod]
        public void WhenAddingANewMemberTheIDIsCreated()
        {
            clubMemberDataManager.MemberSequenceNumber = 5;
            ClubMember clubMember = new ClubMember("Francisco","Gomez-Caldito","Viseas");
            Assert.AreEqual("00005", clubMember.MemberID);
        }

        [TestMethod]
        public void WhenAddingANewMemberTheSequenceNumberIsIncreasedByOne()
        {
            clubMemberDataManager.MemberSequenceNumber = 5;
            ClubMember clubMember = new ClubMember("Francisco", "Gomez-Caldito", "Viseas");
            Assert.AreEqual((uint)6, clubMemberDataManager.MemberSequenceNumber);
        }
    }
}
