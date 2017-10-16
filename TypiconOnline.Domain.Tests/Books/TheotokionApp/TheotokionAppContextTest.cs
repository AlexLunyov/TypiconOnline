using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Domain.Books.TheotokionApp;

namespace TypiconOnline.Domain.Tests.Books.TheotokionApp
{
    [TestFixture]
    public class TheotokionAppContextTest
    {
        [Test]
        public void TestMethod()
        {
            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData");
            FileReader reader = new FileReader(folderPath);
            string xml = reader.Read("YmnosGroupTest.xml");

            Domain.Books.TheotokionApp.TheotokionApp element = new Domain.Books.TheotokionApp.TheotokionApp()
            {
                //TODO: Доделать!
            };
        }
    }
}
