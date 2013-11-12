using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic
{
    public interface IMembersSequenceNumberManager
    {
        uint GetMemberIDSequenceNumber();
        void SetMemberIDSequenceNumber(uint memberIDSequenceNumber);
    }
}
