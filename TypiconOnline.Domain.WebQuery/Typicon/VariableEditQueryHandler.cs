using JetBrains.Annotations;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using TypiconOnline.Domain.Query;
using TypiconOnline.Domain.Query.Typicon;
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
    public class VariableEditQueryHandler : DbContextQueryBase, IQueryHandler<VariableEditQuery, Result<VariableEditModel>>
    {
        public VariableEditQueryHandler(TypiconDBContext dbContext)
            : base(dbContext)
        {
        }

        public Result<VariableEditModel> Handle([NotNull] VariableEditQuery query)
        {
            var found = DbContext.Set<TypiconVariable>().FirstOrDefault(c => c.Id == query.Id);

            if (found != null)
            {
                var links = GetLinks(found);

                return Result.Ok(new VariableEditModel()
                {
                    Id = found.Id,
                    Name = found.Name,
                    Type = found.Type,
                    Description = found.Description,
                    Count = links.Count,
                    Links = links
                });
            }
            else
            {
                return Result.Fail<VariableEditModel>($"Переменная Устава с Id={query.Id} не найдена.");
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
