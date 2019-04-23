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
    public class EditSignCommandHandler : EditRuleCommandHandlerBase<Sign>, ICommandHandler<EditSignCommand>
    {
        public EditSignCommandHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public async Task<Result> ExecuteAsync(EditSignCommand command)
        {
            return await base.ExecuteAsync(command);
        }

        protected override void UpdateValues(Sign entity, EditRuleCommandBase<Sign> command)
        {
            var c = command as EditSignCommand;
            //не возможно просто присвоить значение, потому как ef core 
            //будет думать, что TypiconEntity удалена
            entity.SignName.ReplaceValues(c.Name);

            entity.TemplateId = c.TemplateId;
            entity.IsAddition = c.IsAddition;
            entity.Number = c.Number;
            entity.Priority = c.Priority;
            entity.RuleDefinition = c.RuleDefinition;
            entity.ModRuleDefinition = c.ModRuleDefinition;
        }
    }
}
