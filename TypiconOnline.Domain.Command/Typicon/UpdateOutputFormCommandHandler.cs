using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class UpdateOutputFormCommandHandler : DbContextCommandBase, ICommandHandler<UpdateOutputFormCommand>
    {
        public UpdateOutputFormCommandHandler(TypiconDBContext dbContext) : base(dbContext)
        {
        }

        public void Execute(UpdateOutputFormCommand command)
        {
            DoTheJob(command);

            DbContext.SaveChanges();
        }

        public Task ExecuteAsync(UpdateOutputFormCommand command)
        {
            DoTheJob(command);

            return DbContext.SaveChangesAsync();
        }

        private void DoTheJob(UpdateOutputFormCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var outputForms = DbContext.Set<OutputForm>()
                .Where(c => c.TypiconId == command.OutputForm.TypiconId && c.Date.Date == command.OutputForm.Date.Date);

            if (outputForms.Any())
            {
                DbContext.Set<OutputForm>().RemoveRange(outputForms);
            }

            DbContext.Set<OutputForm>().Add(command.OutputForm);
        }
    }
}
