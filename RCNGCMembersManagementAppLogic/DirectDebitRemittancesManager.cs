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
                directDebitInitiationContract.CreditorID,
                orgIDSchemeNameChoice_schmeNm, null);

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
            DirectDebitTransaction directDebitTransaction)
        {
 


            return null;
        }
    }
}
