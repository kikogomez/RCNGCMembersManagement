using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementUnitTests.XMLPOCOClasses
{
    [Serializable]
    public class DateTimeTest
    {
        [System.Xml.Serialization.XmlElement("DaateTime")]
        public DateTime dateTime;
    }
}
