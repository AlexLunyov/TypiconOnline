﻿using NUnit.Framework;
using System.IO;
using TypiconOnline.AppServices.Migration;

namespace TypiconOnline.Tests.Common
{
    public class TestDataXmlReader
    {
        public static string GetXmlString(string fileName)
        {
            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData");
            var reader = new FileReader(folderPath);
            return reader.Read(fileName);
        }
    }
}
