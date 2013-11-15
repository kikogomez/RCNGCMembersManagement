﻿namespace RCNGCMembersManagementAppLogic.Billing.DirectDebit.ISO20022Elements.CustomerDirectDebitInitiationV02
{
    /// <comentarios/>
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:iso:std:iso:20022:tech:xsd:pain.008.001.02")]
    public class GenericFinancialIdentification1
    {
        private string idField;
        private FinancialIdentificationSchemeName1Choice schmeNmField;
        private string issrField;

        //Parameterless constructor for Serialization purpose
        private GenericFinancialIdentification1() { }

        public GenericFinancialIdentification1(
            string identification,
            FinancialIdentificationSchemeName1Choice schemeName,
            string issuer)
        {
            this.idField = identification;
            this.schmeNmField = schemeName;
            this.issrField = issuer;
        }

        /// <comentarios/>
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <comentarios/>
        public FinancialIdentificationSchemeName1Choice SchmeNm
        {
            get
            {
                return this.schmeNmField;
            }
            set
            {
                this.schmeNmField = value;
            }
        }

        /// <comentarios/>
        public string Issr
        {
            get
            {
                return this.issrField;
            }
            set
            {
                this.issrField = value;
            }
        }
    }
}
