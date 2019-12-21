using Microsoft.AspNetCore.Identity;
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
    public class DeleteTypiconClaimCommandHandler : DbContextCommandBase, ICommandHandler<DeleteTypiconClaimCommand>
    {
        UserManager<User> _userManager;

        public DeleteTypiconClaimCommandHandler(TypiconDBContext dbContext
            , UserManager<User> userManager) : base(dbContext) 
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public Task<Result> ExecuteAsync(DeleteTypiconClaimCommand command)
        {
            Result<TypiconClaim> allowed = CheckPermission(command);

            if (allowed.Failure)
            {
                return Task.FromResult(Result.Fail(allowed.Error));
            }

            DbContext.Set<TypiconClaim>().Remove(allowed.Value);

            return Task.FromResult(Result.Ok());
        }

        private Result<TypiconClaim> CheckPermission(DeleteTypiconClaimCommand command)
        {
            var found = DbContext.Set<TypiconClaim>().FirstOrDefault(c => c.Id == command.Id);
            
            if (found == null)
            {
                return Result.Fail<TypiconClaim>($"Заявка на создание Устава с Id={command.Id} не была найдена");
            }

            //нельзя редактировать, если Заявка находится в каком-то процессе

            if (found.Status == TypiconClaimStatus.InProcess)
            {
                return Result.Fail<TypiconClaim>($"Заявка не может быть удалена, потому что находится в процессе обработки");
            }

            //администартор может все
            if (command.User.IsInRole(RoleConstants.AdministratorsRole))
            {
                return Result.Ok(found);
            }
            //если не Уставщик, то не может удалять вообще
            else if (!command.User.IsInRole(RoleConstants.EditorsRole))
            {
                return Result.Fail<TypiconClaim>($"Пользователь не имеет прав удалить данную заявку");
            }
            else
            {
                //Уставщик - проверяем. ТОлько автор может удалить Заявку
                var id = _userManager.GetUserId(command.User);

                if (found.OwnerId.ToString() != id)
                {
                    return Result.Fail<TypiconClaim>($"Пользователь не имеет прав удалить данную заявку");
                }
            }

            return Result.Ok(found);
        }
    }
}
