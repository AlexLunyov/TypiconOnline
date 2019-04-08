using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.AppServices.Interfaces;

namespace TypiconOnline.AppServices.Common
{
    public class XmlToFileSaver : IXmlSaver
    {
        private string _fileName;
        public XmlToFileSaver(string fileName)
        {
            _fileName = fileName;
        }

        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
            }
        }

        public XmlDocument Load()
        {
            XmlDocument doc = new XmlDocument();
            FileInfo fileInfo = new FileInfo(_fileName);
            if (fileInfo.Exists)
            {
                doc.Load(_fileName);
            }
            return doc;
        }

        public void Save(XmlDocument doc)
        {
            string dirPath = Path.GetDirectoryName(_fileName);
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            File.WriteAllText(_fileName, doc.OuterXml);
        }
    }
}
