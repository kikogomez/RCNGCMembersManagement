﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCNGCMembersManagementAppLogic.XML;
using RCNGCMembersManagementAppLogic.Billing.DirectDebit;
using RCNGCMembersManagementAppLogic.Billing.DirectDebit.ISO20022Elements.CustomerDirectDebitInitiationV02;
using RCNGCMembersManagementUnitTests.DirectDebitPOCOClasses;
using ExtensionMethods;

namespace RCNGCMembersManagementUnitTests
{
    [TestClass]
    public class CustomerDirectDebitInitiationTests
    {
        static Creditor_POCOTestClass creditor;
        static CreditorAgent_POCOTestClass creditorAgent;
        static BankCodes bankCodes;
        static DirectDebitTransactionInfo_POCOTestClass directDebitMandateInfo1;
        static DirectDebitTransactionInfo_POCOTestClass directDebitMandateInfo2;
        static List<DirectDebitTransactionInfo_POCOTestClass> directDebitMandateInfoList;
        static string xMLNamespace;
        static string xSDFilePath;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            SEPAAttributes sEPAAttributes = new SEPAAttributes();
            bankCodes = new BankCodes(@"XMLFiles\SpanishBankCodes.xml", BankCodes.BankCodesFileFormat.XML);
            creditor = new Creditor_POCOTestClass(
                "Real Club Náutico de Gran Canaria",
                sEPAAttributes.AT02CreditorIdentifier("ES", "G35008770", "777"),
                sEPAAttributes.AT07IBAN_Spanish("12345678061234567890"));
            creditorAgent = new CreditorAgent_POCOTestClass(bankCodes.BankDictionaryByLocalBankCode["3183"].BankBIC);
            directDebitMandateInfo1 = new DirectDebitTransactionInfo_POCOTestClass(
                "Pedro Piqueras",
                "InternalID2510201300099",
                new string[] { "Cuota Mensual Numerario Septiembre 2013", "Cuota Mensual Numerario Octubre 2013" },
                (double)158,
                sEPAAttributes.AT01MandateReference("000001102345"),
                sEPAAttributes.AT07IBAN_Spanish("01000100709999999999"),
                sEPAAttributes.AT25DateOfMandateSigning_MigrationValue,
                sEPAAttributes.AT01MandateReference("000001101111"),
                sEPAAttributes.AT07IBAN_Spanish("01000100761234567890"));

            directDebitMandateInfo2 = new DirectDebitTransactionInfo_POCOTestClass(
                "Manuel Moreno",
                "InternalID2510201300100",
                new string[] { "Cuota Mensual Numerario Octubre 2013" },
                (double)79,
                sEPAAttributes.AT01MandateReference("000001102346"),
                sEPAAttributes.AT07IBAN_Spanish("01821234861234567890"),
                sEPAAttributes.AT25DateOfMandateSigning_MigrationValue,
                null,
                null);

            directDebitMandateInfoList = new List<DirectDebitTransactionInfo_POCOTestClass>() { directDebitMandateInfo1, directDebitMandateInfo2 };

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

        [TestMethod]
        public void OrganisationIdentification4IsCorrectlyCreated()
        {
            OrganisationIdentificationSchemeName1Choice orgIDSchemeNameChoice_schmeNm = new OrganisationIdentificationSchemeName1Choice(
                "SEPA", ItemChoiceType.Prtry);

            GenericOrganisationIdentification1 genericOrganisationIdentification_othr = new GenericOrganisationIdentification1(
                creditor.Identification, orgIDSchemeNameChoice_schmeNm, null);

            OrganisationIdentification4 organisationIdentification_orgiD = new OrganisationIdentification4(
                null,
                new GenericOrganisationIdentification1[] { genericOrganisationIdentification_othr });

            string xmlString = XMLSerializer.XMLSerializeToString<OrganisationIdentification4>(organisationIdentification_orgiD, "OrgId", xMLNamespace);
            string validatingErrors = XMLValidator.ValidateXMLNodeThroughModifiedXSD(
                "OrgId", "OrganisationIdentification4", xMLNamespace, xmlString, xSDFilePath);
            Assert.AreEqual("", validatingErrors);
        }

        [TestMethod]
        public void Party6ChoiceIsCorrectlyCreated()
        {
            OrganisationIdentificationSchemeName1Choice orgIDSchemeNameChoice_schmeNm = new OrganisationIdentificationSchemeName1Choice(
                "SEPA", ItemChoiceType.Prtry);

            GenericOrganisationIdentification1 genericOrganisationIdentification_othr = new GenericOrganisationIdentification1(
                creditor.Identification, orgIDSchemeNameChoice_schmeNm, null);

            OrganisationIdentification4 organisationIdentification_orgiD = new OrganisationIdentification4(
                null,   //<BICOrBEI> - We dont use BIC nor BEI
                new GenericOrganisationIdentification1[] { genericOrganisationIdentification_othr }); //<Othr>

            Party6Choice organisationOrPrivateIdentification_id = new Party6Choice(
                organisationIdentification_orgiD);    //<OrgId>

            string xmlString = XMLSerializer.XMLSerializeToString<Party6Choice>(organisationOrPrivateIdentification_id, "Id", xMLNamespace);
            string validatingErrors = XMLValidator.ValidateXMLNodeThroughModifiedXSD(
                "Id", "Party6Choice", xMLNamespace, xmlString, xSDFilePath);
            Assert.AreEqual("", validatingErrors);
        }

        [TestMethod]
        public void InitiationPartyElement_InitPty_IsCorrectlyCreated()
        {
            OrganisationIdentificationSchemeName1Choice orgIDSchemeNameChoice_schmeNm = new OrganisationIdentificationSchemeName1Choice(
                "SEPA", ItemChoiceType.Prtry);

            GenericOrganisationIdentification1 genericOrganisationIdentification_othr = new GenericOrganisationIdentification1(
                creditor.Identification, orgIDSchemeNameChoice_schmeNm, null);

            OrganisationIdentification4 organisationIdentification_orgiD = new OrganisationIdentification4(
                null,
                new GenericOrganisationIdentification1[] { genericOrganisationIdentification_othr });

            Party6Choice organisationOrPrivateIdentification_id = new Party6Choice(organisationIdentification_orgiD);

            PartyIdentification32 initiationParty_initgPty = new PartyIdentification32(
                creditor.Name,                              //<Nm>
                null,                                       //<PstlAdr> - Not used in SEPA
                organisationOrPrivateIdentification_id,     //<OrgID> or <PrvtId>
                null,                                       //<CtryOfRes> - Not used in SEPA
                null);                                      //<CtctDtls> - Not used in SEPA

            string xmlString = XMLSerializer.XMLSerializeToString<PartyIdentification32>(initiationParty_initgPty, "InitgPty", xMLNamespace);
            string validatingErrors = XMLValidator.ValidateXMLNodeThroughModifiedXSD(
                "InitgPty", "PartyIdentification32", xMLNamespace, xmlString, xSDFilePath);
            Assert.AreEqual("", validatingErrors);
        }

        [TestMethod]
        public void InitiationPartyElement_InitPty_IsCorrectlyDeserialized()
        {
            PartyIdentification32 initiatingParty = XMLSerializer.XMLDeserializeFromFile<PartyIdentification32>(@"XML Test Files\InitiatingParty.xml", "InitgPty", xMLNamespace);
            Assert.AreEqual("Real Club Náutico de Gran Canaria", initiatingParty.Nm);
            OrganisationIdentification4 orgId = (OrganisationIdentification4)initiatingParty.Id.Item;
            string genericOrganisationInformationId = orgId.Othr[0].Id;
            Assert.AreEqual("ES90777G35008770", genericOrganisationInformationId);
        }

        [TestMethod]
        public void GroupHeader_GrpHdr_IsCorrectlyCreated()
        {
            PartyIdentification32 initiationParty_initgPty = XMLSerializer.XMLDeserializeFromFile<PartyIdentification32>(@"XML Test Files\InitiatingParty.xml", "InitgPty", xMLNamespace);

            Authorisation1Choice[] authorisation_authstn = new Authorisation1Choice[] { null };

            DateTime creatingdDateTime =
                DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).Truncate(TimeSpan.FromSeconds(1));

            GroupHeader39 groupHeader_grpHdr = new GroupHeader39(
                "TestSEPARemitance0001",    //<MsgID>
                creatingdDateTime,          //<CreDtTm>
                authorisation_authstn,      //<Authstn> - Not used in SEPA. Array of null instead of null to avoid null reference exception
                "2",                        //<NbOfTxs>
                (decimal)100.50,            //<CtrlSum>
                true,                       //Control sum is specified
                initiationParty_initgPty,   //<InitgPty>
                null);                      //<FwdgAgt> - Not used by creditor in SEPA COR

            string xmlString = XMLSerializer.XMLSerializeToString<GroupHeader39>(groupHeader_grpHdr, "GrpHdr", xMLNamespace);
            string validatingErrors = XMLValidator.ValidateXMLNodeThroughModifiedXSD(
                "GrpHdr", "GroupHeader39", xMLNamespace, xmlString, xSDFilePath);
            Assert.AreEqual("", validatingErrors);
        }

        [TestMethod]
        public void GroupHeader_GrpHdr_IsCorrectlyDeserialized()
        {
            GroupHeader39 groupHeader = XMLSerializer.XMLDeserializeFromFile<GroupHeader39>(@"XML Test Files\GroupHeader.xml", "GrpHdr", xMLNamespace);
            Assert.AreEqual("TestSEPARemitance0001", groupHeader.MsgId);
            string deserializedDateToISO0861DateString = groupHeader.CreDtTm.ToString("s", System.Globalization.CultureInfo.InvariantCulture);
            Assert.AreEqual("2013-10-28T11:45:54", deserializedDateToISO0861DateString);
            Assert.AreEqual(Convert.ToDecimal("237"), groupHeader.CtrlSum);
            Assert.AreEqual("2", groupHeader.NbOfTxs);
            PartyIdentification32 initiatingParty = groupHeader.InitgPty;
            Assert.AreEqual("Real Club Náutico de Gran Canaria", initiatingParty.Nm);
            OrganisationIdentification4 orgId = (OrganisationIdentification4)initiatingParty.Id.Item;
            string genericOrganisationInformationId = orgId.Othr[0].Id;
            Assert.AreEqual("ES90777G35008770", genericOrganisationInformationId);
        }

        [TestMethod]
        public void PaymentIdentification_PmtID_IsCorrectlyCreated()
        {
            PaymentIdentification1 paymentIdentification_PmtID = new PaymentIdentification1(
                directDebitMandateInfo1.TxInternalId,    //<InstrID>
                directDebitMandateInfo1.TxInternalId);   //<EndToEndID>

            string xmlString = XMLSerializer.XMLSerializeToString<PaymentIdentification1>(paymentIdentification_PmtID, "PmtID", xMLNamespace);
            string validatingErrors = XMLValidator.ValidateXMLNodeThroughModifiedXSD(
                "PmtID", "PaymentIdentification1", xMLNamespace, xmlString, xSDFilePath);
            Assert.AreEqual("", validatingErrors);
        }

        [TestMethod]
        public void InstructedAmount_InstdAmt_IsCorrectlyCreated()
        {
            ActiveOrHistoricCurrencyAndAmount instructedAmount_InstdAmt = new ActiveOrHistoricCurrencyAndAmount(
                "EUR",                                      //<InstdAmt> ""CCY" atribute value
                (decimal)directDebitMandateInfo1.Amount);   //<InstdAmt>

            string xmlString = XMLSerializer.XMLSerializeToString<ActiveOrHistoricCurrencyAndAmount>(instructedAmount_InstdAmt, "InstdAmt", xMLNamespace);
            string validatingErrors = XMLValidator.ValidateXMLNodeThroughModifiedXSD(
                "InstdAmt", "ActiveOrHistoricCurrencyAndAmount", xMLNamespace, xmlString, xSDFilePath);
            Assert.AreEqual("", validatingErrors);
        }

        [TestMethod]
        public void AmendmentInformationDetails_AmdmntInfDtls_IsCorrectlyCreated()
        {
            AccountIdentification4Choice originalAccountID_Id = new AccountIdentification4Choice(
                directDebitMandateInfo1.PreviuosIBAN);   //<IBAN>

            CashAccount16 originalDebtorAccount_OrgnlDbtrAcct = new CashAccount16(
                originalAccountID_Id,   //<Id>
                null,           //<Tp> - Not used by creditor in SEPA COR
                null,           //<Ccy> - Not used by creditor in SEPA COR
                null);          //<Nm> - Not used by creditor in SEPA COR

            AmendmentInformationDetails6 ammendmentInformationDetails_AmdmntInfDtls = new AmendmentInformationDetails6(
                directDebitMandateInfo1.PreviousMandateID,  //<OrgnlMndtId>
                null,                                       //<OrgnlCdtrSchemeId> keep original creditor data
                null,                                       //<OrgnlCreditorAgent> - Not used by creditor in SEPA COR
                null,                                       //<OrgnlCreditorAgentAccount> - Not used by creditor in SEPA COR
                null,                                       //<OrgnlDbtr> - Not used by creditor in SEPA COR
                originalDebtorAccount_OrgnlDbtrAcct,        //<OrgnlDbtrAcc>
                null,                                       //<OrgnlaDbtrAgt> - Not necessario. It's not the BIC
                null,                                       //<OrgnlDbtrAgtAcct> - Not used by creditor in SEPA COR
                DateTime.MaxValue,                          //<OrgnlFnlColltnDt> - Not used by creditor in SEPA COR
                false,                                      //<OrgnlFnlColltnDt> will not be serialized
                Frequency1Code.MNTH,                        //<OrgnlFrqcy> - Not used by creditor in SEPA COR
                false);                                     //<OrgnlFrqcy> will not be serialized

            string xmlString = XMLSerializer.XMLSerializeToString<AmendmentInformationDetails6>(ammendmentInformationDetails_AmdmntInfDtls, "AmdmntInfDtls", xMLNamespace);
            string validatingErrors = XMLValidator.ValidateXMLNodeThroughModifiedXSD(
                "AmdmntInfDtls", "AmendmentInformationDetails6", xMLNamespace, xmlString, xSDFilePath);
            Assert.AreEqual("", validatingErrors);
        }

        [TestMethod]
        public void MandateRelatedInformation_MndtRltdInf_IsCorrectlyCreated()
        {
            AccountIdentification4Choice originalAccountID_Id = new AccountIdentification4Choice(
                directDebitMandateInfo1.PreviuosIBAN);

            CashAccount16 originalDebtorAccount_OrgnlDbtrAcct = new CashAccount16(
                originalAccountID_Id, null, null, null);

            AmendmentInformationDetails6 ammendmentInformationDetails_AmdmntInfDtls = new AmendmentInformationDetails6(
                directDebitMandateInfo1.PreviousMandateID, null, null, null, null, originalDebtorAccount_OrgnlDbtrAcct,
                null, null, DateTime.MaxValue, false, Frequency1Code.MNTH, false);

            MandateRelatedInformation6 mandateRelatedInformation_MndtRltdInf = new MandateRelatedInformation6(
                directDebitMandateInfo1.MandateID,              //<MndtID>
                directDebitMandateInfo1.MandateSignatureDate,   //<DtOfSgntr>
                true,                                           //<DtOfSgntr> will be serialized
                true,                                           //<AmdmntInd> - There is ammendment information
                true,                                           //<AmdmntInd> will be serialize
                ammendmentInformationDetails_AmdmntInfDtls,     //<AmdmntInfDtls>
                null,                                           //<ElctrncSgntr> - No electronic signature 
                DateTime.MinValue,                              //<FrstColltnDt> - Not used by creditor in SEPA COR, but can't be null
                false,                                          //<FrstColltnDt> will not be serialized
                DateTime.MaxValue,                              //<FnlColltnDt> - Not used by creditor in SEPA COR, but can't be null
                false,                                          //<FnlColltnDt> will not be serialized
                Frequency1Code.MNTH,                            //<Frqcy> - Not used by creditor in SEPA COR, but can't be null
                false);                                         //<Frqcy> will not be serialized 

            string xmlString = XMLSerializer.XMLSerializeToString<MandateRelatedInformation6>(mandateRelatedInformation_MndtRltdInf, "MndtRltdInf", xMLNamespace);
            string validatingErrors = XMLValidator.ValidateXMLNodeThroughModifiedXSD(
                "MndtRltdInf", "MandateRelatedInformation6", xMLNamespace, xmlString, xSDFilePath);
            Assert.AreEqual("", validatingErrors);
        }

        [TestMethod]
        public void DirectDebitTransaction_DrctDbtTx_IsCorrectlyCreated()
        {
            AccountIdentification4Choice originalAccountID_Id = new AccountIdentification4Choice(
                directDebitMandateInfo1.PreviuosIBAN);

            CashAccount16 originalDebtorAccount_OrgnlDbtrAcct = new CashAccount16(
                originalAccountID_Id, null, null, null);

            AmendmentInformationDetails6 ammendmentInformationDetails_AmdmntInfDtls = new AmendmentInformationDetails6(
                directDebitMandateInfo1.PreviousMandateID, null, null, null, null, originalDebtorAccount_OrgnlDbtrAcct,
                null, null, DateTime.MaxValue, false, Frequency1Code.MNTH, false);

            MandateRelatedInformation6 mandateRelatedInformation_MndtRltdInf = new MandateRelatedInformation6(
                directDebitMandateInfo1.MandateID, directDebitMandateInfo1.MandateSignatureDate, true, true, true,
                ammendmentInformationDetails_AmdmntInfDtls, null, DateTime.MinValue, false,
                DateTime.MaxValue, false, Frequency1Code.MNTH, false);

            DirectDebitTransaction6 directDebitTransaction_DrctDbtTx = new DirectDebitTransaction6(
                mandateRelatedInformation_MndtRltdInf,  //<MndtRltdInf>
                null,                                   //<CdtrSchmeId> - No. Only one creditor scheme per payment information <PmtInf> group  
                null,                                   //<PreNtfctnId> - Not used by creditor in SEPA COR
                DateTime.MinValue,                      //<PreNtfctnDt> - Not used by creditor in SEPA COR, but can't be null
                false);                                 //<PreNtfctnDt> will not be serialized 

            string xmlString = XMLSerializer.XMLSerializeToString<DirectDebitTransaction6>(directDebitTransaction_DrctDbtTx, "DrctDbtTx", xMLNamespace);
            string validatingErrors = XMLValidator.ValidateXMLNodeThroughModifiedXSD(
                "DrctDbtTx", "DirectDebitTransaction6", xMLNamespace, xmlString, xSDFilePath);
            Assert.AreEqual("", validatingErrors);
        }

        [TestMethod]
        public void FinancialInstitutuinIdentification_FinInstnID_IsCorrectlyCreated()
        {
            FinancialInstitutionIdentification7 financialInstitutuinIdentification_FinInstnID = new FinancialInstitutionIdentification7(
                creditorAgent.BIC,  //<BIC>
                null,               //<ClrYsMmbId> - Not used by creditor in SEPA COR
                null,               //<Nm> Not used by creditor in SEPA COR
                null,               //<PstlAdr> - Not used by creditor in SEPA COR
                null);              //<Othr> - Not used by creditor in SEPA COR

            string xmlString = XMLSerializer.XMLSerializeToString<FinancialInstitutionIdentification7>(financialInstitutuinIdentification_FinInstnID, "FinInstnID", xMLNamespace);
            string validatingErrors = XMLValidator.ValidateXMLNodeThroughModifiedXSD(
                "FinInstnID", "FinancialInstitutionIdentification7", xMLNamespace, xmlString, xSDFilePath);
            Assert.AreEqual("", validatingErrors);
        }

        [TestMethod]
        public void DebtorAgent_DbtrAgt_IsCorrectlyCreated()
        {
            FinancialInstitutionIdentification7 financialInstitutuinIdentification_FinInstnID = new FinancialInstitutionIdentification7(
                creditorAgent.BIC, null, null, null, null);

            BranchAndFinancialInstitutionIdentification4 debtorAgent_DbtrAgt = new BranchAndFinancialInstitutionIdentification4(
                financialInstitutuinIdentification_FinInstnID,  //<FinInstnId>
                null);                                          //<BrcnhID> - Not used by creditor in SEPA COR

            string xmlString = XMLSerializer.XMLSerializeToString<BranchAndFinancialInstitutionIdentification4>(debtorAgent_DbtrAgt, "DbtrAgt", xMLNamespace);
            string validatingErrors = XMLValidator.ValidateXMLNodeThroughModifiedXSD(
                "DbtrAgt", "BranchAndFinancialInstitutionIdentification4", xMLNamespace, xmlString, xSDFilePath);
            Assert.AreEqual("", validatingErrors);
        }

        [TestMethod]
        public void Debtor_Dbtr_IsCorrectlyCreated()
        {
            PartyIdentification32 debtor_Dbtr = new PartyIdentification32(
                directDebitMandateInfo1.DebtorName,     //<Nm>
                null,                                   //<PstlAdr> - No postal address needed
                null,                                   //<Id> - No extra ID needed
                null,                                   //<CtryOfRes> - Not used by creditor in SEPA COR
                null);                                  //<CtctDtls> - Not used by creditor in SEPA COR

            string xmlString = XMLSerializer.XMLSerializeToString<PartyIdentification32>(debtor_Dbtr, "Dbtr", xMLNamespace);
            string validatingErrors = XMLValidator.ValidateXMLNodeThroughModifiedXSD(
                "Dbtr", "PartyIdentification32", xMLNamespace, xmlString, xSDFilePath);
            Assert.AreEqual("", validatingErrors);
        }

        [TestMethod]
        public void AccountID_Id_IsCorrectlyCreated()
        {
            AccountIdentification4Choice accountID_Id = new AccountIdentification4Choice(
                directDebitMandateInfo1.IBAN);   //<IBAN>

            string xmlString = XMLSerializer.XMLSerializeToString<AccountIdentification4Choice>(accountID_Id, "Id", xMLNamespace);
            string validatingErrors = XMLValidator.ValidateXMLNodeThroughModifiedXSD(
                "Id", "AccountIdentification4Choice", xMLNamespace, xmlString, xSDFilePath);
            Assert.AreEqual("", validatingErrors);
        }

        [TestMethod]
        public void DebtorAccount_DbtrAcct_IsCorrectlyCreated()
        {
            AccountIdentification4Choice accountID_Id = new AccountIdentification4Choice(
                directDebitMandateInfo1.IBAN);

            CashAccount16 debtorAccount_DbtrAcct = new CashAccount16(
                accountID_Id,   //<Id>
                null,           //<Tp> - Not used by creditor in SEPA COR
                null,           //<Ccy> - Not used by creditor in SEPA COR
                null);          //<Nm> - Not used by creditor in SEPA COR

            string xmlString = XMLSerializer.XMLSerializeToString<CashAccount16>(debtorAccount_DbtrAcct, "DbtrAcct", xMLNamespace);
            string validatingErrors = XMLValidator.ValidateXMLNodeThroughModifiedXSD(
                "DbtrAcct", "CashAccount16", xMLNamespace, xmlString, xSDFilePath);
            Assert.AreEqual("", validatingErrors);
        }

        [TestMethod]
        public void RemitanceInformation_RmtInf_IsCorrectlyCreated()
        {
            RemittanceInformation5 remitanceInformation_RmtInf = new RemittanceInformation5(
                directDebitMandateInfo1.RemitanceInformation,       //<Ustrd>
                new StructuredRemittanceInformation7[] { null });   //<Strd> - Only <Ustrd> or <Strd>

            string xmlString = XMLSerializer.XMLSerializeToString<RemittanceInformation5>(remitanceInformation_RmtInf, "RmtInf", xMLNamespace);
            string validatingErrors = XMLValidator.ValidateXMLNodeThroughModifiedXSD(
                "RmtInf", "RemittanceInformation5", xMLNamespace, xmlString, xSDFilePath);
            Assert.AreEqual("", validatingErrors);
        }

        [TestMethod]
        public void DirectDebitTransactionInfo_DrctDbtTxInf_IsCorrectlyCreated()
        {
            PaymentIdentification1 paymentIdentification_PmtID = new PaymentIdentification1(
                directDebitMandateInfo1.TxInternalId,    //<InstrID>
                directDebitMandateInfo1.TxInternalId);   //<EndToEndID>

            ActiveOrHistoricCurrencyAndAmount instructedAmount_InstdAmt = new ActiveOrHistoricCurrencyAndAmount(
                "EUR",                                      //<InstdAmt> ""CCY" atribute value
                (decimal)directDebitMandateInfo1.Amount);   //<InstdAmt>

            AccountIdentification4Choice originalDebtorAccount_ID = new AccountIdentification4Choice(
                directDebitMandateInfo1.PreviuosIBAN);

            CashAccount16 originalDebtorAccount_OrgnlDbtrAcct = new CashAccount16(
                originalDebtorAccount_ID, null, null, null);

            AmendmentInformationDetails6 ammendmentInformationDetails_AmdmntInfDtls = new AmendmentInformationDetails6(
                directDebitMandateInfo1.PreviousMandateID, null, null, null, null, originalDebtorAccount_OrgnlDbtrAcct,
                null, null, DateTime.MaxValue, false, Frequency1Code.MNTH, false);

            MandateRelatedInformation6 mandateRelatedInformation_MndtRltdInf = new MandateRelatedInformation6(
                directDebitMandateInfo1.MandateID, directDebitMandateInfo1.MandateSignatureDate, true, true, true,
                ammendmentInformationDetails_AmdmntInfDtls, null, DateTime.MinValue, false,
                DateTime.MaxValue, false, Frequency1Code.MNTH, false);

            DirectDebitTransaction6 directDebitTransaction_DrctDbtTx = new DirectDebitTransaction6(
                mandateRelatedInformation_MndtRltdInf, null, null, DateTime.MinValue, false);

            FinancialInstitutionIdentification7 financialInstitutuinIdentification_FinInstnID = new FinancialInstitutionIdentification7(
                creditorAgent.BIC, null, null, null, null);

            BranchAndFinancialInstitutionIdentification4 debtorAgent_DbtrAgt = new BranchAndFinancialInstitutionIdentification4(
                financialInstitutuinIdentification_FinInstnID, null);

            PartyIdentification32 debtor_Dbtr = new PartyIdentification32(
                directDebitMandateInfo1.DebtorName, null, null, null, null);

            AccountIdentification4Choice accountID_Id = new AccountIdentification4Choice(
                directDebitMandateInfo1.IBAN);

            CashAccount16 debtorAccount_DbtrAcct = new CashAccount16(
                accountID_Id, null, null, null);

            RemittanceInformation5 remitanceInformation_RmtInf = new RemittanceInformation5(
                directDebitMandateInfo1.RemitanceInformation,
                new StructuredRemittanceInformation7[] { null });

            DirectDebitTransactionInformation9 directDebitTransactionInfo_DrctDbtTxInf = new DirectDebitTransactionInformation9(
                paymentIdentification_PmtID,        //<PmtID>
                null,                               //<PmtTpInf> - Not used by creditor in SEPA COR 
                instructedAmount_InstdAmt,          //<InstdAmt>
                ChargeBearerType1Code.SLEV,         //<ChrgBr> - No. Only one Charge Bearer per payment information <PmtInf> group
                false,                              //<ChrgBr> will not be serialized    
                directDebitTransaction_DrctDbtTx,   //<DrctDbtTx>
                null,                               //<UltmtCdtr> - Not necessary. If son, only one Ultimate Creditor per payment information <PmtInf> group
                debtorAgent_DbtrAgt,                //<DbtrAgt>
                null,                               //<DbtrAgtAcct> - Not used by creditor in SEPA COR
                debtor_Dbtr,                        //<Dbtr>
                debtorAccount_DbtrAcct,             //<DbtrAcct>
                null,                               //<UltmtDbtr> - Only if Ultimate Debtor is different from debtor.
                null,                               //<InstrForCdtrAgt> - Not used by creditor in SEPA COR
                null,                               //<Purp> - Not mandatory. Only use to inform debtor. Is meaningless for agents.
                new RegulatoryReporting3[] { null },//<RgltryRptg> - Only needed for big payments from non residents
                null,                               //<Tax> - Not used by creditor in SEPA COR
                new RemittanceLocation2[] { null }, //<RltdRmtInf> - Not used by creditor in SEPA COR
                remitanceInformation_RmtInf);       //<RmtInf>

            string xmlString = XMLSerializer.XMLSerializeToString<DirectDebitTransactionInformation9>(directDebitTransactionInfo_DrctDbtTxInf, "DrctDbtTxInf", xMLNamespace);
            string validatingErrors = XMLValidator.ValidateXMLNodeThroughModifiedXSD(
                "DrctDbtTxInf", "DirectDebitTransactionInformation9", xMLNamespace, xmlString, xSDFilePath);
            Assert.AreEqual("", validatingErrors);
        }


    }
}
