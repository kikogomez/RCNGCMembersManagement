using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExtensionMethods;

namespace ExtensionMethodsUnitTests
{
    [TestClass]
    public class ExtensionMethodsUnitTests
    {
        [TestMethod]
        public void ArgumentExceptionMessageWhenEmpty()
        {
            ArgumentException exception = new ArgumentException("");
            Assert.AreEqual("", exception.GetMessageWithoutParamName());
        }

        [TestMethod]
        public void ArgumentExceptionMessageWhenNoParamNameIsGiven()
        {
            ArgumentException exception = new ArgumentException("Only this messasge");
            Assert.AreEqual("Only this messasge", exception.GetMessageWithoutParamName());
        }

        [TestMethod]
        public void ArgumentExceptionMessageWhenParamNameIsGiven()
        {
            ArgumentException exception = new ArgumentException("This is the message", "ParameterName");
            Assert.AreEqual("This is the message\r\nNombre del parámetro: ParameterName", exception.Message);
            Assert.AreEqual("This is the message", exception.GetMessageWithoutParamName());
        }

        [TestMethod]
        public void ArgumentNullExceptionMessageWhenParamNameIsGiven()
        {
            ArgumentNullException exception = new ArgumentNullException("ParameterName", "This is the message");
            Assert.AreEqual("This is the message\r\nNombre del parámetro: ParameterName", exception.Message);
            Assert.AreEqual("This is the message", exception.GetMessageWithoutParamName());
        }

        [TestMethod]
        public void ArgumentOutOfRangeExceptionMessageWhenParamNameIsGiven()
        {
            ArgumentOutOfRangeException exception = new ArgumentOutOfRangeException("ParameterName", "This is the message");
            Assert.AreEqual("This is the message\r\nNombre del parámetro: ParameterName", exception.Message);
            Assert.AreEqual("This is the message", exception.GetMessageWithoutParamName());
        }

    }
}
