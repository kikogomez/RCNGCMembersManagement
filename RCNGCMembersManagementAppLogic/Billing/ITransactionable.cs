using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Billing
{
    public interface ITransactionable
    {
        Transaction CreateDefaultTransaction();
    }
}
