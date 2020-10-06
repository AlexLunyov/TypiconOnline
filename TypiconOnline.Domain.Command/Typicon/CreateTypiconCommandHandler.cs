using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Common;
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

        public Task<Result> ExecuteAsync(CreateTypiconCommand command)
        {
            var obj = Create(command);
            DbContext.Set<TypiconClaim>().Add(obj);

            //await DbContext.SaveChangesAsync();

            return Task.FromResult(Result.Ok());
        }

        private TypiconClaim Create(CreateTypiconCommand command)
        {
            return new TypiconClaim()
            {
                Name = new ItemText(new ItemTextUnit(CommonConstants.DefaultLanguage, command.Name)),
                Description = new ItemText(new ItemTextUnit(CommonConstants.DefaultLanguage, command.Description)),
                SystemName = command.SystemName.ToLower(),
                DefaultLanguage = command.DefaultLanguage,
                TemplateId = command.TemplateId,
                OwnerId = command.OwnerId,
                CreateDate = DateTime.Now,
                Status = TypiconClaimStatus.WatingForReview
            };
        }
    }
}
