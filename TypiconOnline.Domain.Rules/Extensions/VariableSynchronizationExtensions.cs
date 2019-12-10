using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Domain.Rules.Serialization;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Variable;
using TypiconOnline.Infrastructure.Common.ErrorHandling;

namespace TypiconOnline.Domain.Rules.Extensions
{
    public static class VariableSynchronizationExtensions
    {
        public static Result SyncRuleVariables<T>(this T entity, CollectorSerializerRoot serializerRoot)
            where T : RuleEntity//, new()
        {
            return InnerSyncVariables(entity, serializerRoot, entity.RuleDefinition, DefinitionType.Rule);
        }

        public static Result SyncModRuleVariables<T>(this T entity, CollectorSerializerRoot serializerRoot)
            where T : ModRuleEntity//, new()
        {
            return InnerSyncVariables(entity, serializerRoot, entity.ModRuleDefinition, DefinitionType.ModRule);
        }

        private static Result InnerSyncVariables<T>(T entity, CollectorSerializerRoot serializerRoot, string definition, DefinitionType definitionType)
            where T: RuleEntity//, new()
        {
            //получаем все переменные Устава
            var globalVariables = entity.TypiconVersion.TypiconVariables;

            //очищаем все ссылки на используемые ранее Переменные данного типа
            globalVariables.ForEach(c => c.ClearLinks(entity, definitionType));

            //получаем локальные переменные
            var localVariables = GetLocalVariables(serializerRoot, definition);

            var errors = new List<string>();

            foreach (var v in localVariables)
            {
                var found = globalVariables.FirstOrDefault(c => c.Name == v.Name);

                if (found != null)
                {
                    //type mismatch?
                    if (found.Type == v.Type)
                    {
                        found.AddLink(entity, definitionType);
                    }
                    else
                    {
                        errors.Add($"Ошибка: Переменная {found.Name} уже зарегистрирована в Уставе с типом переменной {found.Type}.");
                    }
                }
                else
                {
                    //нет такой переменной еще - значит добавляем
                    var variable = new TypiconVariable()
                    {
                        TypiconVersion = entity.TypiconVersion,
                        Name = v.Name,
                        Type = v.Type
                    };
                    variable.AddLink(entity, definitionType);

                    entity.TypiconVersion.TypiconVariables.Add(variable);
                }
            }

            //удаляем Переменные Устава, у которых нет ссылок
            entity.TypiconVersion.TypiconVariables
                .RemoveAll(c =>
                    c.CommonRuleLinks.Count == 0
                    && c.ExplicitAddRuleLinks.Count == 0
                    && c.MenologyRuleLinks.Count == 0
                    && c.TriodionRuleLinks.Count == 0
                    && c.SignLinks.Count == 0);

            return (errors.Count > 0) ? Result.Fail(string.Join('\n', errors)) : Result.Ok();
        }

        private static IEnumerable<(string Name, VariableType Type)> GetLocalVariables(CollectorSerializerRoot serializerRoot, string definition)
        {
            serializerRoot.ClearElements();
            serializerRoot.Container<RootContainer>().Deserialize(definition);

            var result = serializerRoot.GetElements<IHavingVariables>();

            var localVariables = new List<(string Name, VariableType Type)>();

            foreach (var r in result)
            {
                localVariables.AddRange(r.GetVariableNames());
            }

            //получили все переменные в этом Правиле
            return localVariables.Distinct();
        }
    }
}
