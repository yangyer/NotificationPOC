using SolarWindDemo.SWIS;
using System;
using System.Net;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace SolarWindDemo
{
    class Program
    {
        static void Main()
        {
            try
            {
                const string hostname = "localhost";
                const string username = "admin";
                const string password = "";

                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

                var client = new InformationServiceClient("BasicHttpBinding_InformationService",
                                                          string.Format("https://{0}:17778/SolarWinds/InformationService/v3/OrionBasic", hostname));
                client.ClientCredentials.UserName.UserName = username;
                client.ClientCredentials.UserName.Password = password;
                client.Open();

                var result = client.QueryXml("SELECT TOP 1 AlertDefID, ActiveObject, ObjectType FROM Orion.AlertStatus WHERE Acknowledged=0 ORDER BY TriggerTimeStamp DESC RETURN XML AUTO", null);
                var result0 = client.QueryXml("SELECT N.Caption, N.Interfaces.Caption FROM Orion.Nodes N RETURN XML AUTO", null);
                var element = result.XPathSelectElement("//*[local-name()='AlertStatus']");

                var alert = new AlertInfo
                {
                    DefinitionId = element.ElementAnyNS("AlertDefID").Value,
                    ObjectType = element.ElementAnyNS("ObjectType").Value,
                    ObjectId = element.ElementAnyNS("ActiveObject").Value
                };

                var alerts = new[] { alert };

                var dcs = new DataContractSerializer(alerts.GetType());
                var doc = new XmlDocument();
                using (var writer = doc.CreateNavigator().AppendChild())
                {
                    dcs.WriteObject(writer, alerts);
                }

                var arguments = new ArrayOfXmlElement { XDocument.Load(new XmlNodeReader(doc)).Root };
                var result2 = client.Invoke("Orion.AlertStatus", "Acknowledge", arguments);

                Console.Write(result2);

                client.Close();
                Console.ReadLine();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [DataContract(Name = "AlertInfo", Namespace = "http://schemas.solarwinds.com/2008/Orion")]
        public class AlertInfo
        {
            [DataMember(Order = 1)]
            public string DefinitionId;

            [DataMember(Order = 2)]
            public string ObjectType;

            [DataMember(Order = 3)]
            public string ObjectId;
        }
    }
}
