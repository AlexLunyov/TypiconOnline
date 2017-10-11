using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Domain.Books.Irmologion;

namespace TypiconOnline.Domain.Tests.Books.Irmologion
{
    [TestFixture]
    public class IrmologionServiceTest
    {
        [Test]
        public void TestMethod()
        {
            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData");
            FileReader reader = new FileReader(folderPath);
            string xml = reader.Read("YmnosGroupTest.xml");

            IrmologionTheotokion element = new IrmologionTheotokion()
            {
                //TODO: Доделать!
            };
        }
    }
}
