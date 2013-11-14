using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementUnitTests.DirectDebitPOCOClasses
{
    public class CreditorAgent
    {
        string bIC;

        public CreditorAgent(string bIC)
        {
            this.bIC= bIC;
        }

        public string BIC
        {
            get { return bIC; }
        }
    }
}
