using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.Rules.Days;

namespace TypiconOnline.Domain.Tests.Rules.Days
{
    [TestFixture]
    public class YmnosTest
    {
        [Test]
        public void Ymnos_Deserialization()
        {
            string xmlString = @"<Ymnos>
                                    <text>
						                <item language=""cs-ru"">[item] [sign] Господи воззвах, Славник</item>
					                </text>
				                </Ymnos>";
            TypiconSerializer ser = new TypiconSerializer();
            Ymnos element = ser.Deserialize<Ymnos>(xmlString);

            Assert.IsFalse(element.Text.IsEmpty);
            Assert.Pass(element.Text["cs-ru"]);
        }

        [Test]
        public void Ymnos_DeserializationFull()
        {
            string xmlString = @"<Ymnos>
                                    <annotation>
					                    <item language=""cs-ru"">Аннотация</item>
				                    </annotation>
                                    <stihos>
							            <item language=""cs-ru"">Стих 1</item>
						            </stihos>
                                    <stihos>
							            <item language=""cs-ru"">Стих 2</item>
						            </stihos>
                                    <text>
						                <item language=""cs-ru"">Текст</item>
					                </text>
				                </Ymnos>";
            TypiconSerializer ser = new TypiconSerializer();
            Ymnos element = ser.Deserialize<Ymnos>(xmlString);

            Assert.IsFalse(element.Text.IsEmpty);
            Assert.Pass(element.Text["cs-ru"]);
        }

        [Test]
        public void Ymnos_SerializationFull()
        {
            string xmlString = @"<Ymnos>
                                    <annotation>
					                    <item language=""cs-ru"">Аннотация</item>
				                    </annotation>
                                    <stihos>
							            <item language=""cs-ru"">Стих 1</item>
						            </stihos>
                                    <stihos>
							            <item language=""cs-ru"">Стих 2</item>
						            </stihos>
                                    <text>
						                <item language=""cs-ru"">Текст</item>
					                </text>
				                </Ymnos>";
            TypiconSerializer ser = new TypiconSerializer();
            Ymnos element = ser.Deserialize<Ymnos>(xmlString);

            element.Text["cs-ru"] = "Текст измененный";

            string result = ser.Serialize(element);

            Assert.Pass(result);
        }

        [Test]
        public void Ymnos_DeserializationTextRequired()
        {
            string xmlString = @"<Ymnos>
                                    <annotation>
					                    <item language=""cs-ru"">Творе́ние господи́на Пахо́мия мона́ха.</item>
				                    </annotation>
                                    <stihos>
							            <item language=""cs-ru"">[item] [sign] Вечерня. На стиховне, Стих 1-й стихиры.</item>
						            </stihos>
                                    <stihos>
							            <item language=""cs-ru"">[item] [sign] Вечерня. На стиховне, Стих 2-й стихиры.</item>
						            </stihos>
				                </Ymnos>";
            TypiconSerializer ser = new TypiconSerializer();
            Ymnos element = ser.Deserialize<Ymnos>(xmlString);

            Assert.IsFalse(element.IsValid);
        }
    }
}
