using JetBrains.Annotations;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Query;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Rules.Models;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Variable;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    /// <summary>
    /// Возвращает TypiconVariable для редактирования
    /// </summary>
    public class VariableEditQueryHandler : DbContextQueryBase, IQueryHandler<VariableEditQuery, Result<VariableEditModelBase>>
    {
        private readonly ITypiconSerializer _serializer;
        public VariableEditQueryHandler(TypiconDBContext dbContext, ITypiconSerializer serializer)
            : base(dbContext)
        {
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public Result<VariableEditModelBase> Handle([NotNull] VariableEditQuery query)
        {
            var found = DbContext.Set<TypiconVariable>().FirstOrDefault(c => c.Id == query.Id);

            if (found != null)
            {
                return GetModel(found);
            }
            else
            {
                return Result.Fail<VariableEditModelBase>($"Переменная Устава с Id={query.Id} не найдена.");
            }
        }

        private Result<VariableEditModelBase> GetModel(TypiconVariable variable)
        {
            VariableEditModelBase result = null;

            var links = GetLinks(variable);

            switch (variable.Type)
            {
                case VariableType.Worship:
                    {
                        result = new VariableEditWorshipModel()
                        {
                            Id = variable.Id,
                            TypiconId = variable.TypiconVersion.TypiconId,
                            IsTemplate = variable.TypiconVersion.IsTemplate,
                            Name = variable.Name,
                            Type = variable.Type,
                            Description = variable.Description,
                            Count = links.Count,
                            Links = links,
                            Value = Compose(variable.Value)
                        };

                        break;
                    }
                case VariableType.Time:
                default:
                    {
                        result = new VariableEditTimeModel()
                        {
                            Id = variable.Id,
                            TypiconId = variable.TypiconVersion.TypiconId,
                            IsTemplate = variable.TypiconVersion.IsTemplate,
                            Name = variable.Name,
                            Type = variable.Type,
                            Description = variable.Description,
                            Count = links.Count,
                            Links = links,
                            Value = variable.Value
                        };
                        break;
                    }
            }

            return (result != null) ? Result.Ok(result) : Result.Fail<VariableEditModelBase>("Ошибка при формировании объекта.");
        }

        private string Compose(string value)
        {
            try
            {
                var obj = _serializer.Deserialize<WorshipContainer>(value);

                var result = JsonSerializer.Serialize(obj.Worships);

                return result;
            }
            catch (Exception ex)
            {
                return "[]";
            }
        }

        private string HandleValue(TypiconVariable found)
        {
            switch (found.Type)
            {
                case VariableType.Time:
                    {
                        return found.Value;
                    }
                case VariableType.Worship:
                    {
                        throw new NotImplementedException();
                    }
                default:
                    {
                        return found.Value;
                    }
            }
        }

        private List<VariableLinkModel> GetLinks(TypiconVariable found)
        {
            var result = new List<VariableLinkModel>();

            result.AddRange(found.CommonRuleLinks
                .Select(c => new VariableLinkModel()
                {
                    EntityId = c.EntityId,
                    EntityName = nameof(CommonRule),
                    DefinitionType = DefinitionType.Rule
                }));

            result.AddRange(found.ExplicitAddRuleLinks
                .Select(c => new VariableLinkModel()
                {
                    EntityId = c.EntityId,
                    EntityName = nameof(ExplicitAddRule),
                    DefinitionType = DefinitionType.Rule
                }));

            result.AddRange(found.SignLinks
                .Select(c => new VariableLinkModel()
                {
                    EntityId = c.EntityId,
                    EntityName = nameof(Sign),
                    DefinitionType = c.DefinitionType
                }));

            result.AddRange(found.MenologyRuleLinks
                .Select(c => new VariableLinkModel()
                {
                    EntityId = c.EntityId,
                    EntityName = nameof(MenologyRule),
                    DefinitionType = c.DefinitionType
                }));

            result.AddRange(found.TriodionRuleLinks
                .Select(c => new VariableLinkModel()
                {
                    EntityId = c.EntityId,
                    EntityName = nameof(TriodionRule),
                    DefinitionType = c.DefinitionType
                }));

            return result;
        }
    }
}
