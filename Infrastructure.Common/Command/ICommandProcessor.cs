using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.ErrorHandling;

namespace TypiconOnline.Infrastructure.Common.Command
{
    public interface ICommandProcessor
    {
        Result Execute<TCommand>(TCommand command) where TCommand : ICommand;
        Task<Result> ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
