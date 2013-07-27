using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCNGCMembersManagementAppLogic.Billing;

namespace RCNGCMembersManagementMocks
{
    public class DataManagerMock : IDataManager
    {
        static uint lastInvoiceSequenceNumber = 0;
        static uint lastDirectDebitReferenceSequenceNumber = 0;
        static Dictionary<string, Tax> taxes;

        public DataManagerMock()
        {
        }

        public uint GetNextInvoiceSequenceNumber()
        {
            return lastInvoiceSequenceNumber + 1;
        }

        public void SetLastInvoiceSequenceNumber(uint invoiceNumber)
        {
            DataManagerMock.lastInvoiceSequenceNumber = invoiceNumber;
        }

        public uint GetNextDirectDebitReferenceSequenceNumber()
        {
            return lastDirectDebitReferenceSequenceNumber + 1;
        }
        
        public void SetDirectDebitReferenceSequenceNumber(uint invoiceSequenceNumber)
        {
            DataManagerMock.lastDirectDebitReferenceSequenceNumber = invoiceSequenceNumber;
        }

        public Dictionary<string,Tax> LoadTaxes()
        {
            Dictionary<string, Tax> taxDictionary = new Dictionary<string, Tax>{
                {"No IGIC", new Tax("No IGIC",0)},
                {"IGIC Reducido 1", new Tax("IGIC Reducido 1",2.75)},
                {"IGIC Reducido 2", new Tax("IGIC Reducido 2",3)},
                {"IGIC General", new Tax("IGIC General",7)},
                {"IGIC Incrementado 1", new Tax("IGIC Incrementado 1",9.50)},
                {"IGIC Incrementado 2", new Tax("IGIC Incrementado 2",13.50)},
                {"IGIC Especial", new Tax("IGIC Especial",20)}};
            return taxDictionary;
        }
    }
}
