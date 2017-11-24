using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace TypiconOnline.Domain.Serialization
{
    /// <summary>
    /// 
    /// </summary>
    public class XElementCreator
    {
        /// <summary>
        /// Создает XNode из xml строки. Игнорирует пробелы и комментарии
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public XElement Create(string xml)
        {
            var settings = new XmlReaderSettings() { IgnoreComments = true, IgnoreWhitespace = true };

            XmlReader reader = XmlReader.Create(new StringReader(xml), settings);

            try
            {
                return XElement.Parse(xml);
            }
            catch (InvalidOperationException) { }
            catch (XmlException) { }

            return null;
        }
    }
}
