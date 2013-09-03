using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    public static class MyExtensions
    {
        public static string GetMessageWithoutParamName(this ArgumentException argumentException)
        {
            string[] messageLines = argumentException.Message.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            return messageLines[0];
        }
    }
}
