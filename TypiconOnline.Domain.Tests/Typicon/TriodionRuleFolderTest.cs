using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Domain.Tests.Typicon
{
    [TestFixture]
    public class TriodionRuleFolderTest
    {
        //[Test]
        //public void TriodionRuleFolder_Creature()
        //{
        //    TriodionDay triodionDay = new TriodionDay()
        //    {
        //        Id = 1,
        //        Name = "Неделя о мытаре и фарисее",
        //        RuleDefinition = "<rule></rule>"
        //    };

        //    FolderEntity folder = new FolderEntity()
        //    {
        //        Name = "Root",
        //        Folders = new List<FolderEntity>()
        //        {
        //            new FolderEntity()
        //            {
        //                Name = "Постная Триодь",
        //                Rules = new List<RuleEntity>()
        //                {
        //                    new TriodionRule()
        //                    {
        //                        Name = "TriodionRule 1",
        //                        Day = triodionDay
        //                    }
        //                }
        //            }
        //        }
        //    };

        //    TriodionRule triodionRule = new TriodionRule()
        //    {
        //        Name = "TriodionRule 2",
        //        Day = triodionDay
        //    };

        //    folder.Folders.First(c => c.Name == "Постная Триодь").Rules.Add(triodionRule);

        //    TriodionRule rule = (TriodionRule) folder.FindRule(c => c.Name == "TriodionRule 2");

        //    Assert.IsNotNull(rule);
        //}

        //[Test]
        //public void FolderEntity_Creature()
        //{
        //    TriodionDay triodionDay = new TriodionDay()
        //    {
        //        Id = 1,
        //        Name = "Неделя о мытаре и фарисее",
        //        RuleDefinition = "<rule></rule>"
        //    };

        //    MenologyDay menologyDay = new MenologyDay()
        //    {
        //        Id=1,
        //        Name = "Благовещение",
        //        RuleDefinition = "<rule></rule>"
        //    };

        //    FolderEntity folder = new FolderEntity()
        //    {
        //        Name = "Root",
        //        Folders = new List<FolderEntity>()
        //        {
        //            new FolderEntity()
        //            {
        //                Name = "Постная Триодь",
        //                Rules = new List<RuleEntity>()
        //                {
        //                    new TriodionRule()
        //                    {
        //                        Name = "TriodionRule 1",
        //                        Day = triodionDay
        //                    }
        //                }
        //            }
        //        }
        //    };

        //    TriodionRule triodionRule = new TriodionRule()
        //    {
        //        Name = "TriodionRule 2",
        //        Day = triodionDay
        //    };

        //    folder.Folders.First(c => c.Name == "Постная Триодь").Rules.Add(triodionRule);
        //    folder.Folders.First(c => c.Name == "Постная Триодь").Rules.Add(menologyDay);

        //    TriodionRule rule = (TriodionRule)folder.FindRule(c => c.Name == "TriodionRule 2");

        //    Assert.IsNotNull(rule);
        //}
    }
}
