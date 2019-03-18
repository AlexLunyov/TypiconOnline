using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.ErrorHandling;

namespace TypiconOnline.Domain.Tests.ErrorHandling
{
    [TestFixture]
    public class ResultTest
    {
        [Test]
        public void ExceptionThrowsTest()
        {
            var result = new RequiresClass(false);
        }

        private class RequiresClass
        {
            public RequiresClass(bool value)
            {
                Contract.Requires(value);
            }
        }
    }

}
