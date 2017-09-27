﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Domain.Rules.Days;

namespace TypiconOnline.Domain.Tests.Days
{
    [TestFixture]
    public class EsperinosTest
    {
        [Test]
        public void EsperinosTest_Serialization()
        {
            FileReader fileReader = new FileReader(@"C:\Users\Монастырь\Documents\Visual Studio 2015\Projects\TypiconOnline\TypiconOnline.Domain.Tests\");
            string xml = fileReader.GetXml("Esperinos");

            #region xml
            /*string xml = @"<Esperinos>
                              <kekragaria>
                                <group ihos=""1"">
                                  <ymnos>
                                    <text>
                                      <cs-ru>[item] [sign] Господи воззвах, 1 стихира </cs-ru>
                                    </text>
                                  </ymnos>
                                  <ymnos kind=""martyrion"">
                                    <text>
                                      <cs-ru>[item] [sign] Господи воззвах, 2 стихира </cs-ru>
                                    </text>
                                  </ymnos>
                                </group>
                                <group ihos=""2"">
                                  <ymnos>
                                    <text>
                                      <cs-ru>[item] [sign] Господи воззвах, 3 стихира </cs-ru>
                                    </text>
                                  </ymnos>
                                </group>
                                <group ihos=""3"">
                                  <ymnos>
                                    <text>
                                      <cs-ru>[item] [sign] Господи воззвах, 4 стихира </cs-ru>
                                    </text>
                                  </ymnos>
                                </group>
                                <group ihos=""5"">
                                  <prosomoion>
                                    <cs-ru>Преподо́бне о́тче:</cs-ru>
                                  </prosomoion>
                                  <annotation>
                                    <cs-ru>господина Феодора</cs-ru>
                                  </annotation>
                                  <ymnos>
                                    <text>
                                      <cs-ru>[item] [sign] Господи воззвах, 4 стихира </cs-ru>
                                    </text>
                                  </ymnos>
                                </group>
                              </kekragaria>
                              <prokeimeni>
                                <prokeimenon ihos=""4"">
                                  <stihos>
                                    <cs-ru>[item] [sign] Вечерня. Прокимен1</cs-ru>
                                  </stihos>
                                  <stihos>
                                    <cs-ru>[item] [sign] Вечерня. Прокимен1. Стих</cs-ru>
                                  </stihos>
                                </prokeimenon>
                                <prokeimenon ihos=""3"">
                                  <stihos>
                                    <cs-ru>[item] [sign] Вечерня. Прокимен2</cs-ru>
                                  </stihos>
                                  <stihos>
                                    <cs-ru>[item] [sign] Вечерня. Прокимен2. Стих</cs-ru>
                                  </stihos>
                                </prokeimenon>
                              </prokeimeni>
                              <paroimies>
                                <paroimia quote=""3.1-14"">
                                  <bookname>
                                    <cs-ru>Пророчества Исаиина чтение</cs-ru>
                                  </bookname>
                                  <stihos>
                                    <cs-ru>[item] [sign] Вечерня. Паремия 1. Стих 1</cs-ru>
                                  </stihos>
                                  <stihos>
                                    <cs-ru>[item] [sign] Вечерня. Паремия 1. Стих 2</cs-ru>
                                  </stihos>
                                  <stihos>
                                    <cs-ru>[item] [sign] Вечерня. Паремия 1. Стих 3</cs-ru>
                                  </stihos>
                                  <stihos>
                                    <cs-ru>[item] [sign] Вечерня. Паремия 1. Стих 4</cs-ru>
                                  </stihos>
                                  <stihos>
                                    <cs-ru>[item] [sign] Вечерня. Паремия 1. Стих 5</cs-ru>
                                  </stihos>
                                  <stihos>
                                    <cs-ru>[item] [sign] Вечерня. Паремия 1. Стих 6</cs-ru>
                                  </stihos>
                                  <stihos>
                                    <cs-ru>[item] [sign] Вечерня. Паремия 1. Стих 7</cs-ru>
                                  </stihos>
                                  <stihos>
                                    <cs-ru>[item] [sign] Вечерня. Паремия 1. Стих 8</cs-ru>
                                  </stihos>
                                  <stihos>
                                    <cs-ru>[item] [sign] Вечерня. Паремия 1. Стих 9</cs-ru>
                                  </stihos>
                                  <stihos>
                                    <cs-ru>[item] [sign] Вечерня. Паремия 1. Стих 10</cs-ru>
                                  </stihos>
                                </paroimia>
                                <paroimia quote=""5.15-57, 6.1-8"">
                                  <bookname>
                                    <cs-ru>Пророчества Исаиина чтение</cs-ru>
                                  </bookname>
                                  <stihos>
                                    <cs-ru>[item] [sign] Вечерня. Паремия 2. Стих 1</cs-ru>
                                  </stihos>
                                  <stihos>
                                    <cs-ru>[item] [sign] Вечерня. Паремия 2. Стих 2</cs-ru>
                                  </stihos>
                                  <stihos>
                                    <cs-ru>[item] [sign] Вечерня. Паремия 2. Стих 3</cs-ru>
                                  </stihos>
                                  <stihos>
                                    <cs-ru>[item] [sign] Вечерня. Паремия 2. Стих 4</cs-ru>
                                  </stihos>
                                  <stihos>
                                    <cs-ru>[item] [sign] Вечерня. Паремия 2. Стих 5</cs-ru>
                                  </stihos>
                                  <stihos>
                                    <cs-ru>[item] [sign] Вечерня. Паремия 2. Стих 6</cs-ru>
                                  </stihos>
                                  <stihos>
                                    <cs-ru>[item] [sign] Вечерня. Паремия 2. Стих 7</cs-ru>
                                  </stihos>
                                  <stihos>
                                    <cs-ru>[item] [sign] Вечерня. Паремия 2. Стих 8</cs-ru>
                                  </stihos>
                                  <stihos>
                                    <cs-ru>[item] [sign] Вечерня. Паремия 2. Стих 9</cs-ru>
                                  </stihos>
                                  <stihos>
                                    <cs-ru>[item] [sign] Вечерня. Паремия 2. Стих 10</cs-ru>
                                  </stihos>
                                </paroimia>
                              </paroimies>
                              <aposticha>
                                <group ihos=""2"">
                                  <ymnos>
                                    <text>
                                      <cs-ru>[item] [sign] Вечерня. На стиховне, 1 стихира.</cs-ru>
                                    </text>
                                  </ymnos>
                                </group>
                                <group ihos=""3"">
                                  <ymnos>
                                    <text>
                                      <cs-ru>[item] [sign] Вечерня. На стиховне, 2 стихира.</cs-ru>
                                    </text>
                                  </ymnos>
                                </group>
                                <theotokion ihos=""3"">
                                  <ymnos>
                                    <text>
                                      <cs-ru>[item] [sign] Вечерня. На стиховне, Богородичен.</cs-ru>
                                    </text>
                                  </ymnos>
                                </theotokion>
                              </aposticha>
                            </Esperinos>";*/

            #endregion
            TypiconSerializer ser = new TypiconSerializer();

            Esperinos esperinos = ser.Deserialize<Esperinos>(xml);

            Assert.IsNotNull(esperinos);
            Assert.AreEqual(esperinos.Kekragaria.Groups[0].Ihos, 1);
            Assert.AreEqual(esperinos.Paroimies.Count, 2);
        }
    }
}
