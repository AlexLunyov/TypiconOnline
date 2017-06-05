using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Tests.ItemTypes
{
    [TestFixture]
    public class ItemTextCollectionTest
    {
        [Test]
        public void ItemTextCollection_3Items()
        {
            string xmlString = @"<name>
                                    <Name1>
			                            <style/>
			                            <cs-ru>Попразднство Рождества Христова.</cs-ru>
		                            </Name1>
		                            <Name2>
			                            <style/>
			                            <cs-ru>Мучеников 14 000 младенцев, от Ирода в Вифлееме избиенных. Прп. Марке́лла, игумена обители «Неусыпа́ющих».</cs-ru>
		                            </Name2>
                                    <Name3>
			                            <style/>
			                            <cs-ru>Прп. Марке́лла, игумена обители «Неусыпа́ющих».</cs-ru>
		                            </Name3>
                                </name>";
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            ItemTextCollection collection = new ItemTextCollection(xmlString);

            Assert.AreEqual(collection.Items.Count, 3);
            Assert.Pass(collection.StringExpression);
        }
    }
}
