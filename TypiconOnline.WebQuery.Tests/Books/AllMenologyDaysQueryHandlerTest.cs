using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TypiconOnline.Domain.Typicon;
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

        //public void Expression_Test()
        //{
        //    Expression.Or
        //    Expression<Func<MenologyRule, bool>> expression;

        //    Func<MenologyRule, bool> func = expression.Compile();
        //}
    }

    //class Express
    //{
    //    public Express(Expression<Func<MenologyRule, bool>> exp)
    //    {
    //        _exp = exp;
    //    }

    //    Expression<Func<MenologyRule, bool>> _exp;

    //    public Exec()
    //    {
    //        _exp()
    //    }
    //}
}
