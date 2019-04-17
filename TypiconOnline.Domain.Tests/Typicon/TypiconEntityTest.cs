using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;

namespace TypiconOnline.Domain.Tests.Typicon
{
    [TestFixture]
    public class TypiconVersionTest
    {
        [Test]
        public void Typicon_TypiconVersion_Creature()
        {
            DayWorship dayService = new DayWorship();
            dayService.WorshipName.AddOrUpdate("cs-ru", "Благовещение");

            MenologyDay menologyday = new MenologyDay() {
                Id = 1,
                DayWorships = new List<DayWorship>() { dayService },
                Date = new ItemDate("--04-07"),
                LeapDate = new ItemDate("--04-07")
            };

            TypiconVersion typiconEntity = new TypiconVersion()
            {
                Id = 1,
                //Name = new ItemText()
                //{
                //    Items = new List<ItemTextUnit>() { new ItemTextUnit("cs-ru", "Типикон") }
                //},
                Signs = new List<Sign>() { new Sign() { Id = 1 } }
            };

            //FolderEntity folder = new FolderEntity()
            //{
            //    Name = "Минея",
            //    Folders = new List<FolderEntity>()
            //    {
            //        new FolderEntity()
            //        {
            //            Name = "Благовещение папка",
            //            Rules = new List<RuleEntity>()
            //            {
            //new MenologyRule()
            //{
            //    Id = 1,
            //    //Name = "Благовещение правило",
            //    DayServices = new List<DayService>() { dayService },
            //    Template = typiconEntity.Signs[0]
            //}
            //            }
            //        }
            //    }
            //};

            MenologyRule rule = new MenologyRule()
            {
                Id = 1,
                //Name = "Благовещение правило",
                DayRuleWorships = new List<DayRuleWorship>() { new DayRuleWorship() { DayWorship = dayService } },
                Template = typiconEntity.Signs[0]
            };

            typiconEntity.MenologyRules.Add(rule);

            Assert.Pass("Your first passing test");
        }

        [Test]
        public void Typicon_TypiconVersion_ModifiedYear()
        {
            TypiconVersion typicon = new TypiconVersion();

            ModifiedYear year = typicon.ModifiedYears.FirstOrDefault(c => c.Year == 2017);

            Assert.IsNull(year);
        }
    }
}
