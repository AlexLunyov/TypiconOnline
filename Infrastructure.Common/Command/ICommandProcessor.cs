using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Infrastructure.Common.Command
{
    public interface ICommandProcessor
    {
        void Execute<TCommand>(TCommand command) where TCommand : ICommand;
        Task ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
