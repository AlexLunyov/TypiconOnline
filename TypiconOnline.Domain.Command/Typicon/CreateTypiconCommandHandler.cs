using System;
using System.Collections.Generic;
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
    public class CreateTypiconCommandHandler : DbContextCommandBase, ICommandHandler<CreateTypiconCommand>
    {
        public CreateTypiconCommandHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public async Task<Result> ExecuteAsync(CreateTypiconCommand command)
        {
            var obj = Create(command);
            DbContext.Set<TypiconEntity>().Add(obj);

            await DbContext.SaveChangesAsync();

            return Result.Ok();
        }

        private TypiconEntity Create(CreateTypiconCommand command)
        {
            return new TypiconEntity()
            {
                Name = new ItemText() { Items = new List<ItemTextUnit>() { new ItemTextUnit(command.DefaultLanguage, command.Name) } },
                DefaultLanguage = command.DefaultLanguage,
                TemplateId = command.TemplateId,
                OwnerId = command.OwnerId
            };
        }
    }
}
