using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Repository.EFCore.DataBase;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests
{
    public abstract class TypiconHavingTestBase
    {
        private static TypiconDBContext _dbContext;

        protected static TypiconDBContext DbContext
        {
            get
            {
                if (_dbContext == null)
                {
                    _dbContext = TypiconDbContextFactory.Create();
                }

                return _dbContext;
            }
        }

        private static IRuleSerializerRoot _serializer;

        protected static IRuleSerializerRoot Serializer
        {
            get
            {
                if (_serializer == null)
                {
                    _serializer = TestRuleSerializer.Create(DbContext);
                }

                return _serializer;
            }
        }

        private static TypiconVersion _typicon;

        protected static TypiconVersion TypiconVersion
        {
            get
            {
                if (_typicon == null)
                {
                    _typicon = DbContext.Set<TypiconVersion>().FirstOrDefault(c => c.Id == 1);
                }

                return _typicon;
            }
        }
    }
}
