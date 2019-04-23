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
    public class CreateSignCommandHandler : DbContextCommandBase, ICommandHandler<CreateSignCommand>
    {
        public CreateSignCommandHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public async Task<Result> ExecuteAsync(CreateSignCommand command)
        {
            var draft = DbContext.Set<TypiconVersion>()
                            .Where(c => c.TypiconId == command.Id && c.IsDraft)
                            .FirstOrDefault();

            if (draft != null)
            {
                var obj = Create(command, draft.Id);

                DbContext.Set<Sign>().Add(obj);

                await DbContext.SaveChangesAsync();

                return Result.Ok();
            }
            else
            {
                return Result.Fail($"Устав с Id = {command.Id} не был найден.");
            }
        }

        private Sign Create(CreateSignCommand command, int typiconVersionId)
        {
            return new Sign()
            {
                IsAddition = command.IsAddition,
                ModRuleDefinition = command.ModRuleDefinition,
                SignName = command.Name,
                Number = command.Number,
                Priority = command.Priority,
                RuleDefinition = command.RuleDefinition,
                TypiconVersionId = typiconVersionId,
                TemplateId = command.TemplateId
            };
        }
    }
}
