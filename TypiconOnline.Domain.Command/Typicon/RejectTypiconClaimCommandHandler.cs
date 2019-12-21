using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Identity;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class RejectTypiconClaimCommandHandler : DbContextCommandBase, ICommandHandler<RejectTypiconClaimCommand>
    {
        public RejectTypiconClaimCommandHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public async Task<Result> ExecuteAsync(RejectTypiconClaimCommand command)
        {
            var found = DbContext.Set<TypiconClaim>().FirstOrDefault(c => c.Id == command.ClaimId);

            if (found == null)
            {
                return Result.Fail($"Заявка на создание Устава с Id {command.ClaimId} не найдена.");
            }

            found.Status = TypiconClaimStatus.Rejected;
            found.EndDate = DateTime.Now;
            found.ResultMesasge = command.ResultMessage;

            DbContext.Set<TypiconClaim>().Update(found);

            //await DbContext.SaveChangesAsync();

            return Result.Ok();
        }
    }
}
