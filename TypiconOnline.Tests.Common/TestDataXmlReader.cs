using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;

namespace TypiconOnline.Tests.Common
{
    public class TestDataXmlReader
    {
        public static string GetXmlString(string fileName)
        {
            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData");
            FileReader reader = new FileReader(folderPath);
            return reader.Read(fileName);
        }
    }
}
