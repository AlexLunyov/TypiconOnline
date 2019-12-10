using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Serialization;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Domain.Typicon.Variable;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class DefineTypiconVariableCommandHandler : DeleteRuleCommandHandlerBase<TypiconVariable>, ICommandHandler<DefineTypiconVariableCommand>
    {
        public DefineTypiconVariableCommandHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public Task<Result> ExecuteAsync(DefineTypiconVariableCommand command) => Task.FromResult(Execute(command));

        protected override Result PerformAdditionalWork(TypiconVariable found, DeleteRuleCommandBase<TypiconVariable> command)
        {
            var c = command as DefineTypiconVariableCommand;

            StringBuilder strBuilder = new StringBuilder(); 

            //проходимся по всем Links и заменяем Переменную на значение
            ReplaceVariable(found.CommonRuleLinks, found.GetShortCode(), c.Value)
                .OnFailure(err =>
                {
                    strBuilder.AppendLine(err);
                });

            ReplaceVariable(found.ExplicitAddRuleLinks, found.GetShortCode(), c.Value)
                .OnFailure(err =>
                {
                    strBuilder.AppendLine(err);
                });

            ReplaceModVariable(found.SignLinks, found.GetShortCode(), c.Value)
                .OnFailure(err =>
                {
                    strBuilder.AppendLine(err);
                });

            ReplaceModVariable(found.MenologyRuleLinks, found.GetShortCode(), c.Value)
                .OnFailure(err =>
                {
                    strBuilder.AppendLine(err);
                });

            ReplaceModVariable(found.TriodionRuleLinks, found.GetShortCode(), c.Value)
                .OnFailure(err =>
                {
                    strBuilder.AppendLine(err);
                });

            return (strBuilder.Length > 0) 
                ? Result.Fail(strBuilder.ToString())
                : Result.Ok();
        }

        private Result ReplaceVariable<T>(List<VariableRuleLink<T>> ruleLinks, string varName, string value)
            where T: RuleEntity, new()
        {
            StringBuilder strBuilder = new StringBuilder();

            ruleLinks.ForEach(c =>
            {
                if (c.Entity.RuleDefinition.Contains(varName))
                {
                    c.Entity.RuleDefinition = c.Entity.RuleDefinition.Replace(varName, value);
                }
                else
                {
                    strBuilder.AppendLine($"Определение Правила {typeof(T).Name} с Id={c.Entity.Id} не содержит переменной {varName}.");
                }
            });

            return (strBuilder.Length > 0)
                ? Result.Fail(strBuilder.ToString())
                : Result.Ok();
        }

        private Result ReplaceModVariable<T>(List<VariableModRuleLink<T>> ruleLinks, string varName, string value)
            where T : ModRuleEntity, new()
        {
            StringBuilder strBuilder = new StringBuilder();

            foreach (var c in ruleLinks)
            {
                if (c.DefinitionType == DefinitionType.Rule)
                {
                    if (c.Entity.RuleDefinition.Contains(varName))
                    {
                        c.Entity.RuleDefinition = c.Entity.RuleDefinition.Replace(varName, value);
                    }
                    else
                    {
                        strBuilder.AppendLine($"Определение Правила {typeof(T).Name} с Id={c.Entity.Id} не содержит переменной {varName}.");
                    }
                }
                else
                {
                    if (c.Entity.ModRuleDefinition.Contains(varName))
                    {
                        c.Entity.ModRuleDefinition = c.Entity.ModRuleDefinition.Replace(varName, value);
                    }
                    else
                    {
                        strBuilder.AppendLine($"Определение Правила переходящих праздников {typeof(T).Name} с Id={c.Entity.Id} не содержит переменной {varName}.");
                    }
                }
            }

            return (strBuilder.Length > 0)
                ? Result.Fail(strBuilder.ToString())
                : Result.Ok();

            //bool Replace(string ruleDefinition, string vName, string val)
            //{
            //    var result = ruleDefinition.Contains(vName);
            //    if (result)
            //    {
            //        ruleDefinition = ruleDefinition.Replace(varName, value);
            //    }
            //    return result;
            //}
        }
    }
}
