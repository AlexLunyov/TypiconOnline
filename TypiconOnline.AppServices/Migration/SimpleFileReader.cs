using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using TypiconOnline.AppServices.Interfaces;

namespace TypiconOnline.AppServices.Migration
{
    public class SimpleFileReader : FileReaderBase
    {
        public SimpleFileReader(string folderPath) : base(folderPath)
        {
        }

        public SimpleFileReader(): base(string.Empty) { }

        protected override string InnerRead(string fileName)
        {
            try
            {
                return File.ReadAllText(fileName);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
