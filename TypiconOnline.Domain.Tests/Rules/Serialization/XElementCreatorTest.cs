using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Tests.Rules.Serialization
{
    [TestFixture]
    public class XElementCreatorTest
    {
        [Test]
        public void XElementCreatorTest_EmptyString()
        {
            var creator = new XElementCreator();
            XElement node = creator.Create(string.Empty);

            Assert.IsNull(node);
        }

        [Test]
        public void XElementCreatorTest_Valid()
        {
            string xml = @"<and>
		                        <equals><int>1</int><int>1</int></equals>
		                        <more><int>2</int><int>1</int></more>
	                        </and>";
            var creator = new XElementCreator();
            XElement node = creator.Create(xml);

            Assert.AreEqual("and", node.Name.LocalName);
            Assert.AreEqual(2, node.Elements().Count());
        }

        [Test]
        public void XElementCreatorTest_Comments()
        {
            string xml = @"<and>
                                <!-- ПРАВИЛА ДЛЯ КАНОНОВ -->
		                        <equals><int>1</int><int>1</int></equals>
		                        <more><int>2</int><int>1</int></more>
	                        </and>";
            var creator = new XElementCreator();
            XElement node = creator.Create(xml);
            
            Assert.AreEqual("and", node.Name.LocalName);
            Assert.AreEqual(2, node.Elements().Count());
        }

        [Test]
        public void XElementCreatorTest_Kanonas()
        {
            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData");
            FileReader reader = new FileReader(folderPath);
            string xml = reader.Read("KanonasRuleTest.xml");

            for (int i = 0; i < 1000; i++)
            {
                XElement node = new XElementCreator().Create(xml);
            }
        }
    }
}
