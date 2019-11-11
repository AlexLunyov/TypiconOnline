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
    public class CreateExplicitAddRuleCommandHandler : CreateRuleCommandHandlerBase<ExplicitAddRule>, ICommandHandler<CreateExplicitAddRuleCommand>
    {
        public CreateExplicitAddRuleCommandHandler(TypiconDBContext dbContext, CollectorSerializerRoot serializerRoot) : base(dbContext, serializerRoot) { }

        public async Task<Result> ExecuteAsync(CreateExplicitAddRuleCommand command)
        {
            return await base.ExecuteAsync(command);
        }

        protected override ExplicitAddRule Create(CreateRuleCommandBase<ExplicitAddRule> command, TypiconVersion typiconVersion)
        {
            var c = command as CreateExplicitAddRuleCommand;

            var entity = new ExplicitAddRule()
            {
                Date = c.Date,
                RuleDefinition = c.RuleDefinition,
                TypiconVersion = typiconVersion,
            };

            entity.SyncRuleVariables(SerializerRoot);

            return entity;
        }
    }
}
