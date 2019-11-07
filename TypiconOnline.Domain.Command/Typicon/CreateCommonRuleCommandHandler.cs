using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Command.Utilities;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Serialization;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class CreateCommonRuleCommandHandler : CreateRuleCommandHandlerBase<CommonRule>, ICommandHandler<CreateCommonRuleCommand>
    {
        public CreateCommonRuleCommandHandler(TypiconDBContext dbContext, CollectorSerializerRoot serializerRoot) : base(dbContext, serializerRoot) { }

        public async Task<Result> ExecuteAsync(CreateCommonRuleCommand command)
        {
            return await base.ExecuteAsync(command);
        }

        protected override CommonRule Create(CreateRuleCommandBase<CommonRule> command, TypiconVersion typiconVersion)
        {
            var c = command as CreateCommonRuleCommand;

            var entity = new CommonRule()
            {
                Name = c.Name,
                RuleDefinition = c.RuleDefinition,
                TypiconVersionId = typiconVersion.Id
            };

            entity.SyncRuleVariables(SerializerRoot);

            return entity;
        }
    }
}
