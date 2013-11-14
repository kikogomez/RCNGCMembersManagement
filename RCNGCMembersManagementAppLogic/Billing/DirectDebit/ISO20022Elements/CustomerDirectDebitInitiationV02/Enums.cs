using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Billing.DirectDebit.ISO20022Elements.CustomerDirectDebitInitiationV02
{
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
}
