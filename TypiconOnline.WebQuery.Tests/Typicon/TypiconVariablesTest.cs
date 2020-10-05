using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using TypiconOnline.Domain.Common;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Models;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.WebQuery.Books;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Domain.WebQuery.Typicon;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.WebQuery.Tests.Typicon
{
    public class TypiconVariablesTest
    {
        [Test]
        public void TypiconVariablesTest_Serialization()
        {
            //создаем объект
            var container = TestWorshipContainer;

            var serializer = TestRuleSerializer.Create();
            //сериализуем его
            var xml = serializer.TypiconSerializer.Serialize(container);

            //пытаемся скормить для десериализации в реальные правила
            var c = serializer.Container<ExecContainer>().Deserialize(xml);

            //проверяем значения

            var worships = c.GetChildElements<WorshipRule>(new RuleHandlerSettings());

            Assert.AreEqual(1, worships.Count(c => c.Mode == WorshipMode.DayBefore));
            Assert.AreEqual(2, worships.Count(c => c.Mode == WorshipMode.ThisDay));
        }

        [Test]
        public void TypiconVariablesTest_Json()
        {
            //создаем объект
            var container = TestWorshipContainer;

            var str = JsonSerializer.Serialize(container.Worships);

            var worships = JsonSerializer.Deserialize<List<WorshipModel>>(str);

            Assert.AreEqual(1, worships.Count(c => c.Mode == WorshipMode.DayBefore));
            Assert.AreEqual(2, worships.Count(c => c.Mode == WorshipMode.ThisDay));
        }

        private WorshipContainer TestWorshipContainer =>
            new WorshipContainer()
            {
                Worships = new List<WorshipModel>()
                {
                    new WorshipModel()
                    {
                        Name = new ItemTextStyled()
                        {
                            Items = new List<ItemTextUnit>()
                            {
                                new ItemTextUnit(CommonConstants.DefaultLanguage, "Вечернее")
                            }
                        },
                        Mode = WorshipMode.DayBefore,
                        Time = "16.00"
                    },
                    new WorshipModel()
                    {
                        Name = new ItemTextStyled()
                        {
                            Items = new List<ItemTextUnit>()
                            {
                                new ItemTextUnit(CommonConstants.DefaultLanguage, "Часы")
                            }
                        },
                        Mode = WorshipMode.ThisDay,
                        Time = "08.40"
                    },
                    new WorshipModel()
                    {
                        Name = new ItemTextStyled()
                        {
                            Items = new List<ItemTextUnit>()
                            {
                                new ItemTextUnit(CommonConstants.DefaultLanguage, "Утренне")
                            }
                        },
                        Mode = WorshipMode.ThisDay,
                        Time = "09.00"
                    }
                }
            };

    }
}
