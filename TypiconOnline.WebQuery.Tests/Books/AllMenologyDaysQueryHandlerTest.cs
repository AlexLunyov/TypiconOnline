using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.WebQuery.Books;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.WebQuery.Tests.Books
{
    public class AllMenologyRulesQueryHandlerTest
    {
        [Test]
        public void AllMenologyDaysQuery_Test()
        {
            var queryProcessor = DataQueryProcessorFactory.Create();

            var result = queryProcessor.Process(new AllMenologyDaysQuery("cs-ru"));

            var list = result.Value.ToList();
            
            Assert.IsNotNull(list);
        }
    }
}
