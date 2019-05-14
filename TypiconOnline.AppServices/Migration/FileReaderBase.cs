using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using TypiconOnline.AppServices.Interfaces;

namespace TypiconOnline.AppServices.Migration
{
    public abstract class FileReaderBase : IFileReader
    {
        protected XmlDocument Doc { get; } = new XmlDocument();

        public FileReaderBase(string folderPath)
        {
            FolderPath = folderPath;
        }
        public string FolderPath { get; set; }

        public string Read(string name)
        {
            if (name.EndsWith("."))
            {
                name = name.Substring(0, name.Length - 1);
            }
            if (!name.EndsWith(".xml"))
            {
                name = name + ".xml";
            }

            string fileName = Path.Combine(FolderPath, name);
            FileInfo fileInfo = new FileInfo(fileName);

            return (fileInfo.Exists) ? InnerRead(fileName) : string.Empty;
        }

        public string Read(params string[] names)
        {
            string name = GetFileName(names);

            return Read(name);
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

                Doc.Load(fileName);
                XmlNode node = Doc.DocumentElement;

                string xml = (node != null) ? node.OuterXml : string.Empty;

                var response = (Path.GetFileNameWithoutExtension(fileInfo.Name), xml);

                result.Add(response);
            }

            return result;
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

        protected abstract string InnerRead(string fileName);
    }
}
