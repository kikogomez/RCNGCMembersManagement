using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Billing.DirectDebit
{
    public class Creditor
    {
        string nIF;
        string name;
        Dictionary<string, DirectDebitInitiationContract> directDebitInitiationContracts;

        public Creditor(string nIF, string name)
        {
            this.nIF = nIF;
            this.name = name;
            directDebitInitiationContracts = new Dictionary<string, DirectDebitInitiationContract>();
        }

        public string NIF
        {
            get { return nIF; }
        }

        public string Name
        {
            get { return name; }
        }

        public Dictionary<string, DirectDebitInitiationContract> DirectDebitInitiationContracts
        {
            get { return directDebitInitiationContracts; }
        }

        public void AddDirectDebitInitiacionContract(DirectDebitInitiationContract creditorAgentDirectDebitInitiationContract)
        {
            directDebitInitiationContracts.Add(
                creditorAgentDirectDebitInitiationContract.CreditorBussinessCode,
                creditorAgentDirectDebitInitiationContract);
        }

/*        public void AddCreditorAgent(CreditorAgent creditorAgent)
        {
            //creditorAgents.Add(creditorAgent.
        }*/
    }
}
