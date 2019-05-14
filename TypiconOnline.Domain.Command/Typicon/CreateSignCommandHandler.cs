using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class CreateSignCommandHandler : CreateRuleCommandHandlerBase<Sign>, ICommandHandler<CreateSignCommand>
    {
        public CreateSignCommandHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public async Task<Result> ExecuteAsync(CreateSignCommand command)
        {
            return await base.ExecuteAsync(command);
        }

        protected override Sign Create(CreateRuleCommandBase<Sign> command, int typiconVersionId)
        {
            var c = command as CreateSignCommand;

            return new Sign()
            {
                IsAddition = c.IsAddition,
                ModRuleDefinition = c.ModRuleDefinition,
                SignName = c.Name,
                Number = c.Number,
                Priority = c.Priority,
                RuleDefinition = c.RuleDefinition,
                TypiconVersionId = typiconVersionId,
                TemplateId = c.TemplateId
            };
        }
    }
}
