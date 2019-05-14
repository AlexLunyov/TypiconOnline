using NUnit.Framework;
using System.IO;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Migration;

namespace TypiconOnline.Domain.Tests.Books.TheotokionApp
{
    [TestFixture]
    public class TheotokionAppContextTest
    {
        [Test]
        public void TestMethod()
        {
            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData");
            var reader = new FileReader(folderPath);
            string xml = reader.Read("YmnosGroupTest.xml");

            Domain.Books.TheotokionApp.TheotokionApp element = new Domain.Books.TheotokionApp.TheotokionApp()
            {
                //TODO: Доделать!
            };
        }
    }
}
