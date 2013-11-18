using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Billing.DirectDebit.ISO20022Elements.CustomerDirectDebitInitiationV02
{
    /// <comentarios/>
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:iso:std:iso:20022:tech:xsd:pain.008.001.02")]
    public enum AddressType2Code
    {

        /// Postal
        ADDR,

        /// POBox
        PBOX,

        /// Residential
        HOME,

        /// Business
        BIZZ,

        /// MailTo
        MLTO,

        /// DeliveryTo
        DLVY,
    }

    /// Authorisation Code
    /// Specifies the authorisation, in a coded form
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:iso:std:iso:20022:tech:xsd:pain.008.001.02")]
    public enum Authorisation1Code
    {

        /// PreAuthorisedFile
        AUTH,

        /// FileLevelAuthorisationDetails
        FDET,

        /// FileLevelAuthorisationSummary
        FSUM,

        /// InstructionLevelAuthorisation
        ILEV,
    }

    /// Account type, in a coded form
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:iso:std:iso:20022:tech:xsd:pain.008.001.02")]
    public enum CashAccountType4Code
    {

        /// CashPayment
        CASH,

        /// Charges
        CHAR,

        /// Commission
        COMM,

        /// Tax
        TAXE,

        /// CashIncome
        CISH,

        /// CashTrading
        TRAS,

        /// Settlement
        SACC,

        /// Current
        CACC,

        /// Savings
        SVGS,

        /// OverNightDeposit
        ONDP,

        /// MarginalLending
        MGLD,

        /// NonResidentExternal
        NREX,

        /// MoneyMarket
        MOMA,

        /// Loan
        LOAN,

        /// Salary
        SLRY,

        /// Overdraft
        ODFT,
    }

    /// Regularity with which direct debit instructions are to be created and processed
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:iso:std:iso:20022:tech:xsd:pain.008.001.02")]
    public enum Frequency1Code
    {

        /// Yearly
        YEAR,

        /// Monthly
        MNTH,

        /// Quarterly
        QURT,

        /// SemiAnnual
        MIAN,

        /// Weekly
        WEEK,

        /// Daily
        DAIL,

        /// Adhoc
        ADHO,

        /// IntraDay
        INDA,
    }

    /// Choice option: Coded ("Cd") or Propietary("Prtry")
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:iso:std:iso:20022:tech:xsd:pain.008.001.02", IncludeInSchema = false)]
    public enum ItemChoiceType
    {

        /// Coded
        Cd,

        /// Propietary
        Prtry,
    }

    /// Name Prefix (address)
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:iso:std:iso:20022:tech:xsd:pain.008.001.02")]
    public enum NamePrefix1Code
    {

        /// Doctor
        DOCT,

        /// Mister
        MIST,

        /// Miss
        MISS,

        /// Madam
        MADM,
    }

    /// Identifies the direct debit sequence, such as first, recurrent, final or one-off.
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:iso:std:iso:20022:tech:xsd:pain.008.001.02")]
    public enum SequenceType1Code
    {
        /// First
        FRST,

        /// Recurring
        RCUR,

        /// Final
        FNAL,

        /// OneOff
        OOFF,
    }


}
