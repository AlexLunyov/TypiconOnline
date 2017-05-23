using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TypiconOnline.AppServices.Implementations
{
    /// <summary>
    /// С помощью класса FileReader можно считывать правила для элементов TypiconOnlie.Domain
    /// </summary>
    class FileReader
    {
        public string FolderPath;
        XmlDocument doc = new XmlDocument();

        public FileReader(string folderPath)
        {
            FolderPath = folderPath;
        }

        public string GetXml(string name)
        {
            try
            {
                if (name.EndsWith("."))
                {
                    name = name.Substring(0, name.Length - 1);
                }

                string fileName = FolderPath + name + ".xml";
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileName);
                if (fileInfo.Exists)
                {
                    doc.Load(fileName);
                    XmlNode node = doc.SelectSingleNode("rule");

                    return (node != null) ? node.OuterXml : "";
                }
            }
            catch { }

            return "";
        }
    }
}
