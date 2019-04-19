using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.ErrorHandling;

namespace TypiconOnline.Infrastructure.Common.Command
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        //void Execute(TCommand command);
        Task<Result> ExecuteAsync(TCommand command);
    }
}
