using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementUnitTests.DirectDebitPOCOClasses
{
    public class Creditor
    {
        string name;
        string identification;
        string iBAN;

        public Creditor(string name, string identification, string iBAN)
        {
            this.name = name;
            this.identification = identification;
            this.iBAN = iBAN;
        }

        public string Name
        {
            get { return name; }
        }

        public string Identification
        {
            get { return identification; }
        }

        public string IBAN
        {
            get { return iBAN; }
        }
    }
}
