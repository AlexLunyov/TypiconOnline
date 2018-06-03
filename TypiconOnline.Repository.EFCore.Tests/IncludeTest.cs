using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Repository.EFCore.Tests
{
    [TestClass]
    public class IncludeTest
    {
        [TestMethod]
        public void Include_Test()
        {
            //.Include(c => c.Date)
            //.Include(c => c.DateB)
            //.Include(c => c.Template)
            //    .ThenInclude(c => c.Template)
            //.Include(c => c.DayRuleWorships)
            //        .ThenInclude(k => k.DayWorship)
            //            .ThenInclude(k => k.WorshipName)
            //.Include(c => c.DayRuleWorships)
            //        .ThenInclude(k => k.DayWorship)
            //            .ThenInclude(k => k.WorshipShortName)

            var element = new IncludeElement[] 
            {
                new IncludeElement() { Name = "Date" },
                new IncludeElement() { Name = "DateB" },
                new IncludeElement()
                {
                    Name = "Template",
                    Children = new IncludeElement[] 
                    {
                        new IncludeElement() { Name = "Template" }
                    }
                },
                new IncludeElement()
                {
                    Name = "DayRuleWorships",
                    Children = new IncludeElement[]
                    {
                        new IncludeElement()
                        {
                            Name = "DayWorship",
                            Children = new IncludeElement[]
                            {
                                new IncludeElement() { Name = "WorshipName" },
                                new IncludeElement() { Name = "WorshipShortName" }
                            }
                        }
                    }
                },
            };
        }
    }
}
