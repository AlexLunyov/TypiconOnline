﻿using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Serialization;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Tests.Common
{
    public static class TestRuleSerializer
    {
        public static T Deserialize<T>(string description) where T: IRuleElement
        {
            var serializer = Create();

            return serializer.Container<T>().Deserialize(description);
        }

        public static IRuleSerializerRoot Create() => Create(TypiconDbContextFactory.Create());

        public static IRuleSerializerRoot Create(TypiconDBContext dbContext)
        {
            return new RuleSerializerRoot(DataQueryProcessorFactory.Create(dbContext), new TypiconSerializer());
        }

        public static CollectorSerializerRoot CreateCollectorSerializerRoot() => CreateCollectorSerializerRoot(TypiconDbContextFactory.Create());

        public static CollectorSerializerRoot CreateCollectorSerializerRoot(TypiconDBContext dbContext)
        {
            return new CollectorSerializerRoot(DataQueryProcessorFactory.Create(dbContext), new TypiconSerializer());
        }
    }
}
