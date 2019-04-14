using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Serialization
{
    public class TypiconSerializer : ITypiconSerializer
    {
        public T Deserialize<T>(string expression) where T : class
        {
            return Deserialize<T>(expression, string.Empty);
        }

        public T Deserialize<T>(string expression, string rootElement) where T : class
        {
            if (string.IsNullOrEmpty(expression))
            {
                return default(T);
            }

            XmlSerializer serializer = (string.IsNullOrEmpty(rootElement)) 
                ? new XmlSerializer(typeof(T)) 
                : new XmlSerializer(typeof(T), new XmlRootAttribute(rootElement));

            XmlReaderSettings settings = new XmlReaderSettings();

            // No settings need modifying here

            using (StringReader textReader = new StringReader(expression))
            {
                using (XmlReader xmlReader = XmlReader.Create(textReader, settings))
                {
                    return (T)serializer.Deserialize(xmlReader);
                }
            }
        }

        public string Serialize<T>(T value) where T : class
        {
            return Serialize(value, string.Empty);
        }

        public string Serialize<T>(T value, string rootElement) where T : class
        {
            XmlSerializer serializer = (string.IsNullOrEmpty(rootElement))
                ? new XmlSerializer(typeof(T))
                : new XmlSerializer(typeof(T), new XmlRootAttribute(rootElement));

            XmlWriterSettings settings = new XmlWriterSettings
            {
                Encoding = new UnicodeEncoding(false, false), // no BOM in a .NET string
                Indent = false,
                OmitXmlDeclaration = false
            };

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            using (StringWriter textWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
                {
                    serializer.Serialize(xmlWriter, value, ns);
                }
                return textWriter.ToString();
            }
        }
    }
}
