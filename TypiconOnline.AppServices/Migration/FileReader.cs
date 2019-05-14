using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.AppServices.Interfaces;

namespace TypiconOnline.AppServices.Migration
{
    /// <summary>
    /// С помощью класса FileReader можно считывать правила для элементов TypiconOnlie.Domain
    /// </summary>
    public class FileReader : FileReaderBase
    {
        public FileReader(string folderPath) : base(folderPath)
        {
        }

        /// <summary>
        /// Возвращает строку Xml из файла. Комментарии в xml-документе опускаются
        /// </summary>
        /// <param name="name">Имя файла</param>
        /// <returns></returns>
        protected override string InnerRead(string fileName)
        {
            try
            {
                //var settings = new XmlReaderSettings() { IgnoreComments = true, IgnoreWhitespace = true };

                var settings = new XmlReaderSettings() { IgnoreComments = true/*, IgnoreWhitespace = true*/ };

                XmlReader reader = XmlReader.Create(fileName, settings);

                Doc.Load(reader);

                XmlNode node = Doc.DocumentElement;

                return (node != null) ? node.OuterXml : string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}