﻿using System.IO;
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
            if (string.IsNullOrEmpty(xml)) return null;

            var settings = new XmlReaderSettings() { IgnoreComments = true, IgnoreWhitespace = true };

            XmlReader reader = XmlReader.Create(new StringReader(xml), settings);

            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load(reader);
            }
            catch (XmlException) { }

            return doc.DocumentElement;
        }
    }
}
