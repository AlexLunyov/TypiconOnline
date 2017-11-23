using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TypiconOnline.Domain.Serialization
{
    /// <summary>
    /// 
    /// </summary>
    public class XmlNodeCreator
    {
        /// <summary>
        /// Создает XmlNode из xml строки. Игнорирует пробелы и комментарии
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public XmlNode Create(string xml)
        {
            var settings = new XmlReaderSettings() { IgnoreComments = true, IgnoreWhitespace = true };

            XmlReader reader = XmlReader.Create(new StringReader(xml), settings);

            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load(reader);
            }
            catch (XmlException ex) { }

            return doc.DocumentElement;
        }
    }
}
