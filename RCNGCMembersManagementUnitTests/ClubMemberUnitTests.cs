using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementAppLogic.MembersManaging;

namespace RCNGCMembersManagementUnitTests.MembersManaging
{
    [TestClass]
    public class ClubMemberUnitTests
    {
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
                string exceptionMessageWithoutParamName = exception.Message.Split(new string[] { Environment.NewLine }, StringSplitOptions.None)[0];
                Assert.AreEqual("Club Member name cannot be empty", exceptionMessageWithoutParamName);
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
                string exceptionMessageWithoutParamName = exception.Message.Split(new string[] { Environment.NewLine }, StringSplitOptions.None)[0];
                Assert.AreEqual("Club Member name cannot be empty", exceptionMessageWithoutParamName);
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
                string exceptionMessageWithoutParamName = exception.Message.Split(new string[] { Environment.NewLine }, StringSplitOptions.None)[0];
                Assert.AreEqual("Club Member name cannot be empty", exceptionMessageWithoutParamName);
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
                string exceptionMessageWithoutParamName = exception.Message.Split(new string[] { Environment.NewLine }, StringSplitOptions.None)[0];
                Assert.AreEqual("Club Member first surname cannot be empty", exceptionMessageWithoutParamName);
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
                string exceptionMessageWithoutParamName = exception.Message.Split(new string[] { Environment.NewLine }, StringSplitOptions.None)[0];
                Assert.AreEqual("Club Member first surname cannot be empty", exceptionMessageWithoutParamName);
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
                string exceptionMessageWithoutParamName = exception.Message.Split(new string[] { Environment.NewLine }, StringSplitOptions.None)[0];
                Assert.AreEqual("Club Member first surname cannot be empty", exceptionMessageWithoutParamName);
                Assert.AreEqual("firstSurname", exception.ParamName);
                throw exception;
            }
        }
    }
}
