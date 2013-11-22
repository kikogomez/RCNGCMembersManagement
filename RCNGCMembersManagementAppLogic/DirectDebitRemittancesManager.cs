using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCNGCMembersManagementAppLogic.Billing;
using RCNGCMembersManagementAppLogic.Billing.DirectDebit;
using RCNGCMembersManagementAppLogic.Billing.DirectDebit.ISO20022Elements.CustomerDirectDebitInitiationV02;
using ExtensionMethods;

namespace RCNGCMembersManagementAppLogic
{
    public class DirectDebitRemittancesManager
    {
        public DirectDebitRemittancesManager()
        {
        }

        public DirectDebitRemittance CreateADirectDebitRemmitance(DateTime creationDateTime, DirectDebitInitiationContract directDebitInitiationContract)
        {
            DirectDebitRemittance directDebitRemmitance = new DirectDebitRemittance(creationDateTime, directDebitInitiationContract);
            return directDebitRemmitance;
        }

        public DirectDebitTransactionsGroupPayment CreateANewGroupOfDirectDebitTransactions(string localInstrument)
        {
            DirectDebitTransactionsGroupPayment directDebitTransactionsGroupPayment = new DirectDebitTransactionsGroupPayment(localInstrument);
            return directDebitTransactionsGroupPayment;
        }

        public DirectDebitTransaction CreateANewEmptyDirectDebitTransaction(DirectDebitMandate directDebitmandate)
        {
            DirectDebitTransaction directDebitTransaction = new DirectDebitTransaction(
                directDebitmandate.InternalReferenceNumber,
                directDebitmandate.BankAccount,
                directDebitmandate.DirectDebitMandateCreationDate);
            return directDebitTransaction;
        }

        public DirectDebitTransaction CreateANewDirectDebitTransactionFromAGroupOfBills(DirectDebitMandate directDebitmandate, List<Bill> billsList)
        {
            DirectDebitTransaction directDebitTransaction  = new DirectDebitTransaction(
                billsList,
                directDebitmandate.InternalReferenceNumber,
                directDebitmandate.BankAccount,
                directDebitmandate.DirectDebitMandateCreationDate);
            return directDebitTransaction;
        }

        public void AddBilllToExistingDirectDebitTransaction(DirectDebitTransaction directDebitTransaction, Bill bill)
        {
            directDebitTransaction.AddBill(bill);
        }

        public void AddDirectDebitTransactionToGroupPayment(
            DirectDebitTransaction directDebitTransaction,
            DirectDebitTransactionsGroupPayment directDebitTransactionsGroupPayment)
        {
            directDebitTransactionsGroupPayment.AddDirectDebitTransaction(directDebitTransaction);
        }

        public void AddDirectDebitTransactionGroupPaymentToDirectDebitRemittance(
            DirectDebitRemittance directDebitRemmitance,
            DirectDebitTransactionsGroupPayment directDebitTransactionsGroupPayment)
        {
            directDebitRemmitance.AddDirectDebitTransactionsGroupPayment(directDebitTransactionsGroupPayment);
        }

        public string GenerateISO20022CustomerDirectDebitInitiationMessage(
            DateTime generationDateTime,
            Creditor creditor,
            CreditorAgent creditorAgent,
            DirectDebitInitiationContract directDebitInitiationContract,
            DirectDebitRemittance directDebitRemmitance)
        {
            PartyIdentification32 initiationParty_InitPty = GenerateInitiationParty_InitPty(creditor, directDebitInitiationContract);
            GroupHeader39 groupHeader_GrpHdr = GenerateGroupHeader_GrpHdr(generationDateTime, directDebitRemmitance, initiationParty_InitPty);

            foreach (DirectDebitTransactionsGroupPayment directDebitTransactionsGroupPayment in directDebitRemmitance.DirectDebitTransactionGroupPaymentCollection)
            {

                foreach (DirectDebitTransaction directDebitTransaction in directDebitTransactionsGroupPayment.DirectDebitTransactionsCollection)
                {
                    DirectDebitTransactionInformation9 directDebitTransactionInfo_DrctDbtTxInf = GenerateDirectDebitTransactionInfo_DrctDbtTxInf(
                        creditorAgent,
                        directDebitTransaction);
                }
            }
            return "";
        }

        private PartyIdentification32 GenerateInitiationParty_InitPty(
            Creditor creditor,
            DirectDebitInitiationContract directDebitInitiationContract)
        {
            OrganisationIdentificationSchemeName1Choice orgIDSchemeNameChoice_schmeNm = new OrganisationIdentificationSchemeName1Choice(
                "SEPA", ItemChoiceType.Prtry);

            GenericOrganisationIdentification1 genericOrganisationIdentification_othr = new GenericOrganisationIdentification1(
                directDebitInitiationContract.CreditorID,           //<Id>
                orgIDSchemeNameChoice_schmeNm,                      //<SchemeNm>
                null);                                              //<Issr> - No issuer

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

            return initiationParty_initgPty;
        }

        private GroupHeader39 GenerateGroupHeader_GrpHdr(
            DateTime generationDateTime,
            DirectDebitRemittance directDebitRemmitance,
            PartyIdentification32 initiationParty_InitgPty)
        {

            Authorisation1Choice[] authorisation_authstn = new Authorisation1Choice[] { null };

            DateTime creatingdDateTime =
                DateTime.SpecifyKind(generationDateTime, DateTimeKind.Unspecified).Truncate(TimeSpan.FromSeconds(1));

            GroupHeader39 groupHeader_grpHdr = new GroupHeader39(
                directDebitRemmitance.MessageID,                        //<MsgID>
                creatingdDateTime,                                      //<CreDtTm>
                authorisation_authstn,                                  //<Authstn> - Not used in SEPA. Array of null instead of null to avoid null reference exception
                directDebitRemmitance.NumberOfTransactions.ToString(),  //<NbOfTxs>
                directDebitRemmitance.ControlSum,                       //<CtrlSum>
                true,                                                   //Control sum is specified
                initiationParty_InitgPty,                               //<InitgPty>
                null);                                                  //<FwdgAgt> - Not used by creditor in SEPA COR

            return groupHeader_grpHdr;
        }

        private DirectDebitTransactionInformation9 GenerateDirectDebitTransactionInfo_DrctDbtTxInf(
            CreditorAgent creditorAgent,
            DirectDebitTransaction directDebitTransaction)
        {
            PaymentIdentification1 paymentIdentification_PmtID = new PaymentIdentification1(
                directDebitTransaction.DirectDebitTransactionInternalReference,    //<InstrID>
                directDebitTransaction.DirectDebitTransactionInternalReference);   //<EndToEndID>

            ActiveOrHistoricCurrencyAndAmount instructedAmount_InstdAmt = new ActiveOrHistoricCurrencyAndAmount(
                "EUR",                                      //<InstdAmt> ""CCY" atribute value
                directDebitTransaction.Amount);             //<InstdAmt>

            /*AccountIdentification4Choice originalAccountID_Id = new AccountIdentification4Choice(
                "    ";   //<IBAN>

            CashAccount16 originalDebtorAccount_OrgnlDbtrAcct = new CashAccount16(
                originalAccountID_Id,   //<Id>
                null,           //<Tp> - Not used by creditor in SEPA COR
                null,           //<Ccy> - Not used by creditor in SEPA COR
                null);          //<Nm> - Not used by creditor in SEPA COR

            AmendmentInformationDetails6 ammendmentInformationDetails_AmdmntInfDtls = new AmendmentInformationDetails6(
                "    ",  //<OrgnlMndtId>
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
                false);                                     //<OrgnlFrqcy> will not be serialized*/

            MandateRelatedInformation6 mandateRelatedInformation_MndtRltdInf = new MandateRelatedInformation6(
                directDebitTransaction.MandateID,               //<MndtID>
                directDebitTransaction.MandateSigatureDate,     //<DtOfSgntr>
                true,                                           //<DtOfSgntr> will be serialized
                false,                                          //<AmdmntInd> - There is no amendment
                false,                                          //<AmdmntInd> will not be serialize
                null,                                           //<AmdmntInfDtls> - No amendment details
                null,                                           //<ElctrncSgntr> - No electronic signature 
                DateTime.MinValue,                              //<FrstColltnDt> - Not used by creditor in SEPA COR, but can't be null
                false,                                          //<FrstColltnDt> will not be serialized
                DateTime.MaxValue,                              //<FnlColltnDt> - Not used by creditor in SEPA COR, but can't be null
                false,                                          //<FnlColltnDt> will not be serialized
                Frequency1Code.MNTH,                            //<Frqcy> - Not used by creditor in SEPA COR, but can't be null
                false);                                         //<Frqcy> will not be serialized

            DirectDebitTransaction6 directDebitTransaction_DrctDbtTx = new DirectDebitTransaction6(
                mandateRelatedInformation_MndtRltdInf,  //<MndtRltdInf>
                null,                                   //<CdtrSchmeId> - No. Only one creditor scheme per payment information <PmtInf> group  
                null,                                   //<PreNtfctnId> - Not used by creditor in SEPA COR
                DateTime.MinValue,                      //<PreNtfctnDt> - Not used by creditor in SEPA COR, but can't be null
                false);                                 //<PreNtfctnDt> will not be serialized 

            FinancialInstitutionIdentification7 financialInstitutuinIdentification_FinInstnID = new FinancialInstitutionIdentification7(
                creditorAgent.BankBIC,  //<BIC>
                null,                   //<ClrYsMmbId> - Not used by creditor in SEPA COR
                null,                   //<Nm> Not used by creditor in SEPA COR
                null,                   //<PstlAdr> - Not used by creditor in SEPA COR
                null);                  //<Othr> - Not used by creditor in SEPA COR

            BranchAndFinancialInstitutionIdentification4 debtorAgent_DbtrAgt = new BranchAndFinancialInstitutionIdentification4(
                financialInstitutuinIdentification_FinInstnID,  //<FinInstnId>
                null);                                          //<BrcnhID> - Not used by creditor in SEPA COR

            PartyIdentification32 debtor_Dbtr = new PartyIdentification32(
                "", //directDebitMandateInfo1.DebtorName,     //<Nm>
                null,                                   //<PstlAdr> - No postal address needed
                null,                                   //<Id> - No extra ID needed
                null,                                   //<CtryOfRes> - Not used by creditor in SEPA COR
                null);                                  //<CtctDtls> - Not used by creditor in SEPA COR

            return null;
        }
    }
}
