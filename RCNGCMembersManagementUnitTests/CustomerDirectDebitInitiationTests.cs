using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementAppLogic.XML;
using RCNGCMembersManagementAppLogic.Billing.DirectDebit;
using RCNGCMembersManagementAppLogic.Billing.DirectDebit.ISO20022Elements.CustomerDirectDebitInitiationV02;
using RCNGCMembersManagementUnitTests.DirectDebitPOCOClasses;

namespace RCNGCMembersManagementUnitTests
{
    [TestClass]
    public class CustomerDirectDebitInitiationTests
    {
        static Creditor creditor;
        static CreditorAgent creditorAgent;
        static BankCodes bankCodes;
        static DirectDebitTransactionInfo directDebitMandateInfo1;
        static DirectDebitTransactionInfo directDebitMandateInfo2;
        static List<DirectDebitTransactionInfo> directDebitMandateInfoList;
        static string xMLNamespace;
        static string xSDFilePath;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            SEPAAttributes sEPAAttributes = new SEPAAttributes();
            bankCodes = new BankCodes(@"XMLFiles\SpanishBankCodes.xml", BankCodes.BankCodesFileFormat.XML);
            creditor = new Creditor(
                "Real Club Náutico de Gran Canaria",
                sEPAAttributes.AT02CreditorIdentifier("ES", "G35008770", "777"),
                sEPAAttributes.AT07IBAN_Spanish("12345678061234567890"));
            creditorAgent = new CreditorAgent(bankCodes.BankDictionaryByLocalBankCode["3183"].BankBIC);
            directDebitMandateInfo1 = new DirectDebitTransactionInfo(
                "Pedro Piqueras",
                "InternalID2510201300099",
                new string[] { "Cuota Mensual Numerario Septiembre 2013", "Cuota Mensual Numerario Octubre 2013" },
                (double)158,
                sEPAAttributes.AT01MandateReference("000001102345"),
                sEPAAttributes.AT07IBAN_Spanish("01000100709999999999"),
                sEPAAttributes.AT25DateOfMandateSigning_MigrationValue,
                sEPAAttributes.AT01MandateReference("000001101111"),
                sEPAAttributes.AT07IBAN_Spanish("01000100761234567890"));

            directDebitMandateInfo2 = new DirectDebitTransactionInfo(
                "Manuel Moreno",
                "InternalID2510201300100",
                new string[] { "Cuota Mensual Numerario Octubre 2013" },
                (double)79,
                sEPAAttributes.AT01MandateReference("000001102346"),
                sEPAAttributes.AT07IBAN_Spanish("01821234861234567890"),
                sEPAAttributes.AT25DateOfMandateSigning_MigrationValue,
                null,
                null);

            directDebitMandateInfoList = new List<DirectDebitTransactionInfo>() { directDebitMandateInfo1, directDebitMandateInfo2 };

            xMLNamespace = "urn:iso:std:iso:20022:tech:xsd:pain.008.001.02";
            xSDFilePath = @"XSDFiles\pain.008.001.02.xsd";
        }

        [TestMethod]
        public void OrganisationIdentificationSchemeName1Choice_SchemeNm_IsCorrectlyCreated()
        {
            OrganisationIdentificationSchemeName1Choice orgIDSchemeNameChoice_schmeNm = new OrganisationIdentificationSchemeName1Choice(
                "SEPA", // <Prtry> 
                ItemChoiceType.Prtry);

            string xmlString = XMLSerializer.XMLSerializeToString<OrganisationIdentificationSchemeName1Choice>(orgIDSchemeNameChoice_schmeNm, "SchemeNm", xMLNamespace);
            string validatingErrors = XMLValidator.ValidateXMLNodeThroughModifiedXSD(
                "SchemeNm", "OrganisationIdentificationSchemeName1Choice", xMLNamespace, xmlString, xSDFilePath);
            Assert.AreEqual("", validatingErrors);
        }

        [TestMethod]
        public void GenericOrganisationIdentification1_Othr_IsCorrectlyCreated()
        {
            OrganisationIdentificationSchemeName1Choice orgIDSchemeNameChoice_schmeNm = new OrganisationIdentificationSchemeName1Choice(
                "SEPA", ItemChoiceType.Prtry);

            GenericOrganisationIdentification1 genericOrganisationIdentification_othr = new GenericOrganisationIdentification1(
                creditor.Identification,            //<Id>
                orgIDSchemeNameChoice_schmeNm,      //<SchemeNm>
                null);                              //<Issr> - No issuer

            string xmlString = XMLSerializer.XMLSerializeToString<GenericOrganisationIdentification1>(genericOrganisationIdentification_othr, "Othr", xMLNamespace);
            string validatingErrors = XMLValidator.ValidateXMLNodeThroughModifiedXSD(
                "Othr", "GenericOrganisationIdentification1", xMLNamespace, xmlString, xSDFilePath);
            Assert.AreEqual("", validatingErrors);
        }
    }
}
