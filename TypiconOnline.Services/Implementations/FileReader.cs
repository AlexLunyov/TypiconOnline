using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TypiconOnline.AppServices.Implementations
{
    /// <summary>
    /// С помощью класса FileReader можно считывать правила для элементов TypiconOnlie.Domain
    /// </summary>
    public class FileReader
    {
        public string FolderPath;
        XmlDocument doc = new XmlDocument();

        public FileReader(string folderPath)
        {
            FolderPath = folderPath;
        }

        /// <summary>
        /// Возвращает строку Xml из файла
        /// </summary>
        /// <param name="name">Имя файла</param>
        /// <returns></returns>
        public string GetXml(string name)
        {
            try
            {
                if (name.EndsWith("."))
                {
                    name = name.Substring(0, name.Length - 1);
                }

                string fileName = FolderPath + name + ".xml";
                FileInfo fileInfo = new FileInfo(fileName);
                if (fileInfo.Exists)
                {
                    doc.Load(fileName);
                    XmlNode node = doc.DocumentElement;

                    return (node != null) ? node.OuterXml : "";
                }
            }
            catch { }

            return "";
        }
        public IEnumerable<FilesSearchResponse> GetXmlsFromDirectory()
        {
            List<FilesSearchResponse> result = new List<FilesSearchResponse>();

            string[] files = Directory.GetFiles(FolderPath, "*.xml");

            foreach (string fileName in files)
            {
                FileInfo fileInfo = new FileInfo(fileName);
                FilesSearchResponse response = new FilesSearchResponse();

                response.Name = Path.GetFileNameWithoutExtension(fileInfo.Name);

                doc.Load(fileName);
                XmlNode node = doc.DocumentElement;

                response.Xml = (node != null) ? node.OuterXml : string.Empty;

                result.Add(response);
            }

            return result;
        }
    }

    public class FilesSearchResponse
    {
        public string Name;
        public string Xml;
    }
}