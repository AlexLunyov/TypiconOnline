using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books;

namespace TypiconOnline.Domain.Tests.Rules
{
    public static class RuleTestBase
    {
        public static BookStorage BookStorage
        {
            get
            {
                return BookStorageFactory.Create();
            }
        }
    }
}
