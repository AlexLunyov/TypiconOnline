using NUnit.Framework;
using System.IO;
using System.Xml;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Tests.Rules.Serialization
{
    [TestFixture]
    public class XmlNodeCreatorTest
    {
        [Test]
        public void XmlNodeCreatorTest_EmptyString()
        {
            XmlNodeCreator creator = new XmlNodeCreator();
            XmlNode node = creator.Create(string.Empty);

            Assert.IsNull(node);
        }

        [Test]
        public void XmlNodeCreatorTest_Valid()
        {
            string xml = @"<and>
		                        <equals><int>1</int><int>1</int></equals>
		                        <more><int>2</int><int>1</int></more>
	                        </and>";
            XmlNodeCreator creator = new XmlNodeCreator();
            XmlNode node = creator.Create(xml);

            Assert.AreEqual("and", node.Name);
            Assert.AreEqual(2, node.ChildNodes.Count);
        }

        [Test]
        public void XmlNodeCreatorTest_Comments()
        {
            string xml = @"<and>
                                <!-- ПРАВИЛА ДЛЯ КАНОНОВ-->
		                        <equals><int>1</int><int>1</int></equals>
		                        <more><int>2</int><int>1</int></more>
	                        </and>";
            XmlNodeCreator creator = new XmlNodeCreator();
            XmlNode node = creator.Create(xml);

            Assert.AreEqual("and", node.Name);
            Assert.AreEqual(2, node.ChildNodes.Count);
        }

        [Test]
        public void XmlNodeCreatorTest_Kanonas()
        {
            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData");
            FileReader reader = new FileReader(folderPath);
            string xml = reader.Read("KanonasRuleTest.xml");

            for (int i = 0; i < 1000; i++)
            {
                XmlNode node = new XmlNodeCreator().Create(xml);
            }
        }
    }
}
