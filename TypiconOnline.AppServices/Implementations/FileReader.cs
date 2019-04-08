using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.AppServices.Interfaces;

namespace TypiconOnline.AppServices.Implementations
{
    /// <summary>
    /// С помощью класса FileReader можно считывать правила для элементов TypiconOnlie.Domain
    /// </summary>
    public class FileReader : IFileReader
    {
        public string FolderPath { get; set; }

        XmlDocument doc = new XmlDocument();

        public FileReader(string folderPath)
        {
            FolderPath = folderPath;
        }

        /// <summary>
        /// Возвращает строку Xml из файла. Комментарии в xml-документе опускаются
        /// </summary>
        /// <param name="name">Имя файла</param>
        /// <returns></returns>
        public string Read(string name)
        {
            try
            {
                if (name.EndsWith("."))
                {
                    name = name.Substring(0, name.Length - 1);
                }
                if (!name.EndsWith(".xml"))
                {
                    name = name + ".xml";
                }

                string fileName = Path.Combine(FolderPath,  name);
                FileInfo fileInfo = new FileInfo(fileName);
                if (fileInfo.Exists)
                {
                    var settings = new XmlReaderSettings() { IgnoreComments = true, IgnoreWhitespace = true };

                    XmlReader reader = XmlReader.Create(fileName, settings);

                    doc.Load(reader);

                    XmlNode node = doc.DocumentElement;

                    return (node != null) ? node.OuterXml : "";
                }
            }
            catch { }

            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="names">Строки, которые необходимо раположить через символ "."</param>
        /// <returns></returns>
        public string Read(params string[] names)
        {
            string name = GetFileName(names);

            return Read(name);
        }

        private string GetFileName(string[] names)
        {
            string result = "";

            for (int i = 0; i < names.Count(); i++)
            {
                var n = names[i];

                if (string.IsNullOrEmpty(n))
                {
                    continue;
                }

                if (n.EndsWith("."))
                {
                    n = n.Substring(0, n.Length - 1);
                }
                if (n.StartsWith("."))
                {
                    n = n.Substring(1, n.Length - 1);
                }

                result += n;

                if (i < names.Count() - 1)
                {
                    result += ".";
                }
            }

            return result;
        }

        /// <summary>
        /// Читает все файлы ".xml" из определенной директории
        /// </summary>
        /// <returns>name - имя файла, content - содержимое в строковом формате</returns>
        public IEnumerable<(string name, string content)> ReadAllFromDirectory()
        {
            List<(string name, string content)> result = new List<(string name, string content)>();

            string[] files = Directory.GetFiles(FolderPath, "*.xml");

            foreach (string fileName in files)
            {
                FileInfo fileInfo = new FileInfo(fileName);

                doc.Load(fileName);
                XmlNode node = doc.DocumentElement;

                string xml = (node != null) ? node.OuterXml : string.Empty;

                var response = (Path.GetFileNameWithoutExtension(fileInfo.Name), xml);

                result.Add(response);
            }

            return result;
        }
    }
}