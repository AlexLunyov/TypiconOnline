using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.Rules.Days;
using System.IO;
using TypiconOnline.AppServices.Implementations;

namespace TypiconOnline.Domain.Tests.Rules.Days
{
    [TestFixture]
    public class SixHourTest
    {
        [Test]
        public void SixHourTest_Deserialization()
        {
            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData");
            FileReader reader = new FileReader(folderPath);
            string xml = reader.Read("SixHourTest.xml");

            TypiconSerializer ser = new TypiconSerializer();
            SixHour element = ser.Deserialize<SixHour>(xml);

            Assert.AreEqual(1, element.Troparion.YmnosStructureCount);
            Assert.AreEqual(2, element.Prokeimeni.Count);
            Assert.AreEqual(1, element.Paroimies.Count);
            Assert.AreEqual(10, element.Paroimies[0].Stihoi.Count);
        }

        [Test]
        public void SixHourTest_Deserialization_TroparionRequired()
        {
            #region xml
            string xmlString = @"<sixhour>
		                            <prokeimeni>
			                            <prokeimenon ihos=""4"">
				                            <stihos>
					                            <item language=""cs-ru"">[item] [sign] 6-й час. Прокимен 1. Стих 1</item>
				                            </stihos>
				                            <stihos>
					                            <item language=""cs-ru"">[item] [sign] 6-й час. Прокимен 1. Стих 2</item>
				                            </stihos>
			                            </prokeimenon>
			                            <prokeimenon ihos=""6"">
				                            <stihos>
					                            <item language=""cs-ru"">[item] [sign] 6-й час. Прокимен 2. Стих 1</item>
				                            </stihos>
				                            <stihos>
					                            <item language=""cs-ru"">[item] [sign] 6-й час. Прокимен 2. Стих 2</item>
				                            </stihos>
			                            </prokeimenon>
		                            </prokeimeni>
		                            <paroimies>
			                            <paroimia quote=""3.1-14"">
				                            <bookname>
					                            <item language=""cs-ru"">Пророчества Исаиина чтение</item>
				                            </bookname>
				                            <stihos>
					                            <item language=""cs-ru"">[item] [sign] 6-й час. Паремия 1. Стих 1</item>
				                            </stihos>
				                            <stihos>
					                            <item language=""cs-ru"">[item] [sign] 6-й час. Паремия 1. Стих 2</item>
				                            </stihos>
				                            <stihos>
					                            <item language=""cs-ru"">[item] [sign] 6-й час. Паремия 1. Стих 3</item>
				                            </stihos>
				                            <stihos>
					                            <item language=""cs-ru"">[item] [sign] 6-й час. Паремия 1. Стих 4</item>
				                            </stihos>
				                            <stihos>
					                            <item language=""cs-ru"">[item] [sign] 6-й час. Паремия 1. Стих 5</item>
				                            </stihos>
				                            <stihos>
					                            <item language=""cs-ru"">[item] [sign] 6-й час. Паремия 1. Стих 6</item>
				                            </stihos>
				                            <stihos>
					                            <item language=""cs-ru"">[item] [sign] 6-й час. Паремия 1. Стих 7</item>
				                            </stihos>
				                            <stihos>
					                            <item language=""cs-ru"">[item] [sign] 6-й час. Паремия 1. Стих 8</item>
				                            </stihos>
				                            <stihos>
					                            <item language=""cs-ru"">[item] [sign] 6-й час. Паремия 1. Стих 9</item>
				                            </stihos>
				                            <stihos>
					                            <item language=""cs-ru"">[item] [sign] 6-й час. Паремия 1. Стих 10</item>
				                            </stihos>
			                            </paroimia>
		                            </paroimies>
	                            </sixhour>";
            #endregion
            TypiconSerializer ser = new TypiconSerializer();
            SixHour element = ser.Deserialize<SixHour>(xmlString);

            Assert.IsFalse(element.IsValid);
        }

        [Test]
        public void SixHourTest_Deserialization_ProkeimenRequired()
        {
            #region xml
            string xmlString = @"<sixhour>
		                            <troparion>
			                            <group ihos=""5"">
				                            <ymnos>
					                            <text>
						                            <item language=""cs-ru"">[item] [sign] 6-й час. Тропарь пророчества</item>
					                            </text>
				                            </ymnos>
			                            </group>
		                            </troparion>
		                            <prokeimeni>
			                            
		                            </prokeimeni>
		                            <paroimies>
			                            <paroimia quote=""3.1-14"">
				                            <bookname>
					                            <item language=""cs-ru"">Пророчества Исаиина чтение</item>
				                            </bookname>
				                            <stihos>
					                            <item language=""cs-ru"">[item] [sign] 6-й час. Паремия 1. Стих 1</item>
				                            </stihos>
				                            <stihos>
					                            <item language=""cs-ru"">[item] [sign] 6-й час. Паремия 1. Стих 2</item>
				                            </stihos>
				                            <stihos>
					                            <item language=""cs-ru"">[item] [sign] 6-й час. Паремия 1. Стих 3</item>
				                            </stihos>
				                            <stihos>
					                            <item language=""cs-ru"">[item] [sign] 6-й час. Паремия 1. Стих 4</item>
				                            </stihos>
				                            <stihos>
					                            <item language=""cs-ru"">[item] [sign] 6-й час. Паремия 1. Стих 5</item>
				                            </stihos>
				                            <stihos>
					                            <item language=""cs-ru"">[item] [sign] 6-й час. Паремия 1. Стих 6</item>
				                            </stihos>
				                            <stihos>
					                            <item language=""cs-ru"">[item] [sign] 6-й час. Паремия 1. Стих 7</item>
				                            </stihos>
				                            <stihos>
					                            <item language=""cs-ru"">[item] [sign] 6-й час. Паремия 1. Стих 8</item>
				                            </stihos>
				                            <stihos>
					                            <item language=""cs-ru"">[item] [sign] 6-й час. Паремия 1. Стих 9</item>
				                            </stihos>
				                            <stihos>
					                            <item language=""cs-ru"">[item] [sign] 6-й час. Паремия 1. Стих 10</item>
				                            </stihos>
			                            </paroimia>
		                            </paroimies>
	                            </sixhour>";
            #endregion
            TypiconSerializer ser = new TypiconSerializer();
            SixHour element = ser.Deserialize<SixHour>(xmlString);

            Assert.IsFalse(element.IsValid);
        }

        [Test]
        public void SixHourTest_Deserialization_ParoimiaRequired()
        {
            #region xml
            string xmlString = @"<sixhour>
		                            <troparion>
			                            <group ihos=""5"">
				                            <ymnos>
					                            <text>
						                            <item language=""cs-ru"">[item] [sign] 6-й час. Тропарь пророчества</item>
					                            </text>
				                            </ymnos>
			                            </group>
		                            </troparion>
		                            <prokeimeni>
			                            <prokeimenon ihos=""4"">
				                            <stihos>
					                            <item language=""cs-ru"">[item] [sign] 6-й час. Прокимен 1. Стих 1</item>
				                            </stihos>
				                            <stihos>
					                            <item language=""cs-ru"">[item] [sign] 6-й час. Прокимен 1. Стих 2</item>
				                            </stihos>
			                            </prokeimenon>
			                            <prokeimenon ihos=""6"">
				                            <stihos>
					                            <item language=""cs-ru"">[item] [sign] 6-й час. Прокимен 2. Стих 1</item>
				                            </stihos>
				                            <stihos>
					                            <item language=""cs-ru"">[item] [sign] 6-й час. Прокимен 2. Стих 2</item>
				                            </stihos>
			                            </prokeimenon>
		                            </prokeimeni>
		                            <paroimies>
			                        </paroimies>    
	                            </sixhour>";
            #endregion
            TypiconSerializer ser = new TypiconSerializer();
            SixHour element = ser.Deserialize<SixHour>(xmlString);

            Assert.IsFalse(element.IsValid);
        }
    }
}
