using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypiconOnline.Tests.Common;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Domain.Query.Typicon;

namespace TypiconOnline.Domain.Query.Tests.Typicon
{
    /// <summary>
    /// Summary description for SignQueryHandlerTest
    /// </summary>
    [TestClass]
    public class SignQueryHandlerTest : QueryTestBase
    {
        [TestMethod]
        public void SignQueryHandler_Template()
        {
            var sign = Processor.Process(new SignQuery(10));

            Assert.IsNotNull(sign.Template);
        }
    }
}
