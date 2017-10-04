using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.Rules.Days;

namespace TypiconOnline.Domain.Tests.Rules.Days
{
    [TestFixture]
    public class SixHourTest
    {
        [Test]
        public void SixHourTest_Deserialization()
        {
            #region xml
            string xmlString = @"<sixhour>
		                            <troparion>
			                            <group ihos=""5"">
				                            <ymnos>
					                            <text>
						                            <cs-ru>[item] [sign] 6-й час. Тропарь пророчества</cs-ru>
					                            </text>
				                            </ymnos>
			                            </group>
		                            </troparion>
		                            <prokeimeni>
			                            <prokeimenon ihos=""4"">
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Прокимен 1. Стих 1</cs-ru>
				                            </stihos>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Прокимен 1. Стих 2</cs-ru>
				                            </stihos>
			                            </prokeimenon>
			                            <prokeimenon ihos=""6"">
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Прокимен 2. Стих 1</cs-ru>
				                            </stihos>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Прокимен 2. Стих 2</cs-ru>
				                            </stihos>
			                            </prokeimenon>
		                            </prokeimeni>
		                            <paroimies>
			                            <paroimia quote=""3.1-14"">
				                            <bookname>
					                            <cs-ru>Пророчества Исаиина чтение</cs-ru>
				                            </bookname>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Паремия 1. Стих 1</cs-ru>
				                            </stihos>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Паремия 1. Стих 2</cs-ru>
				                            </stihos>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Паремия 1. Стих 3</cs-ru>
				                            </stihos>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Паремия 1. Стих 4</cs-ru>
				                            </stihos>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Паремия 1. Стих 5</cs-ru>
				                            </stihos>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Паремия 1. Стих 6</cs-ru>
				                            </stihos>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Паремия 1. Стих 7</cs-ru>
				                            </stihos>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Паремия 1. Стих 8</cs-ru>
				                            </stihos>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Паремия 1. Стих 9</cs-ru>
				                            </stihos>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Паремия 1. Стих 10</cs-ru>
				                            </stihos>
			                            </paroimia>
		                            </paroimies>
	                            </sixhour>";
            #endregion
            TypiconSerializer ser = new TypiconSerializer();
            SixHour element = ser.Deserialize<SixHour>(xmlString);

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
					                            <cs-ru>[item] [sign] 6-й час. Прокимен 1. Стих 1</cs-ru>
				                            </stihos>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Прокимен 1. Стих 2</cs-ru>
				                            </stihos>
			                            </prokeimenon>
			                            <prokeimenon ihos=""6"">
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Прокимен 2. Стих 1</cs-ru>
				                            </stihos>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Прокимен 2. Стих 2</cs-ru>
				                            </stihos>
			                            </prokeimenon>
		                            </prokeimeni>
		                            <paroimies>
			                            <paroimia quote=""3.1-14"">
				                            <bookname>
					                            <cs-ru>Пророчества Исаиина чтение</cs-ru>
				                            </bookname>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Паремия 1. Стих 1</cs-ru>
				                            </stihos>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Паремия 1. Стих 2</cs-ru>
				                            </stihos>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Паремия 1. Стих 3</cs-ru>
				                            </stihos>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Паремия 1. Стих 4</cs-ru>
				                            </stihos>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Паремия 1. Стих 5</cs-ru>
				                            </stihos>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Паремия 1. Стих 6</cs-ru>
				                            </stihos>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Паремия 1. Стих 7</cs-ru>
				                            </stihos>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Паремия 1. Стих 8</cs-ru>
				                            </stihos>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Паремия 1. Стих 9</cs-ru>
				                            </stihos>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Паремия 1. Стих 10</cs-ru>
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
						                            <cs-ru>[item] [sign] 6-й час. Тропарь пророчества</cs-ru>
					                            </text>
				                            </ymnos>
			                            </group>
		                            </troparion>
		                            <prokeimeni>
			                            
		                            </prokeimeni>
		                            <paroimies>
			                            <paroimia quote=""3.1-14"">
				                            <bookname>
					                            <cs-ru>Пророчества Исаиина чтение</cs-ru>
				                            </bookname>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Паремия 1. Стих 1</cs-ru>
				                            </stihos>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Паремия 1. Стих 2</cs-ru>
				                            </stihos>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Паремия 1. Стих 3</cs-ru>
				                            </stihos>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Паремия 1. Стих 4</cs-ru>
				                            </stihos>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Паремия 1. Стих 5</cs-ru>
				                            </stihos>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Паремия 1. Стих 6</cs-ru>
				                            </stihos>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Паремия 1. Стих 7</cs-ru>
				                            </stihos>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Паремия 1. Стих 8</cs-ru>
				                            </stihos>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Паремия 1. Стих 9</cs-ru>
				                            </stihos>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Паремия 1. Стих 10</cs-ru>
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
						                            <cs-ru>[item] [sign] 6-й час. Тропарь пророчества</cs-ru>
					                            </text>
				                            </ymnos>
			                            </group>
		                            </troparion>
		                            <prokeimeni>
			                            <prokeimenon ihos=""4"">
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Прокимен 1. Стих 1</cs-ru>
				                            </stihos>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Прокимен 1. Стих 2</cs-ru>
				                            </stihos>
			                            </prokeimenon>
			                            <prokeimenon ihos=""6"">
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Прокимен 2. Стих 1</cs-ru>
				                            </stihos>
				                            <stihos>
					                            <cs-ru>[item] [sign] 6-й час. Прокимен 2. Стих 2</cs-ru>
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
