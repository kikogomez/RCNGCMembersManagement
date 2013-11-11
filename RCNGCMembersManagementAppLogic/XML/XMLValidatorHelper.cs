using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace RCNGCMembersManagementAppLogic.XML
{
    public static class XMLValidatorHelper
    {
        static string validatorErrors;

        public static void AddElementToSchema(XmlSchema xmlSchema, string elementName, string elementType, string xmlNamespace)
        {
            XmlSchemaElement testNode = new XmlSchemaElement();
            testNode.Name = elementName;
            testNode.Namespaces.Add("", xmlNamespace);
            testNode.SchemaTypeName = new XmlQualifiedName(elementType, xmlNamespace);
            xmlSchema.Items.Add(testNode);
            XmlSchemaSet schemaSet = new XmlSchemaSet();
            schemaSet.Add(xmlSchema);
            schemaSet.Compile();
        }

        public static void XMLValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                validatorErrors += ("WARNING: " + Environment.NewLine);
                validatorErrors += (e.Message + Environment.NewLine);
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                validatorErrors += ("ERROR: " + Environment.NewLine);
                validatorErrors += (e.Message + Environment.NewLine);
            }
        }
    }
}
