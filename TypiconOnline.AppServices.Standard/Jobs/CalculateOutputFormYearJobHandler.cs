using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Common;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Typicon;
using TypiconOnline.Domain.Command.Typicon;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.AppServices.Jobs
{
    /// <summary>
    /// Вычисляет выходные формы для каждого дня указанного года по версии Устава
    /// </summary>
    public class CalculateOutputFormYearJobHandler : ICommandHandler<CalculateOutputFormYearJob>
    {
        private readonly IOutputFormFactory _outputFormFactory;
        private readonly ICommandProcessor _commandProcessor;

        public CalculateOutputFormYearJobHandler(IOutputFormFactory outputFormFactory
            , ICommandProcessor commandProcessor)
        {
            _outputFormFactory = outputFormFactory ?? throw new ArgumentNullException(nameof(outputFormFactory));
            _commandProcessor = commandProcessor ?? throw new ArgumentNullException(nameof(commandProcessor));
        }

        public void Execute(CalculateOutputFormYearJob command)
        {
            DoTheJob(command);
        }

        public Task ExecuteAsync(CalculateOutputFormYearJob command)
        {
            return DoTheJob(command);
        }

        private Task DoTheJob(CalculateOutputFormYearJob command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            EachDayPerYear.Perform(command.Year, async date =>
            {
                var (OutputForm, Day) = _outputFormFactory.Create(new OutputFormCreateRequest()
                {
                    TypiconId = command.TypiconId,
                    TypiconVersionId = command.TypiconVersionId,
                    Date = date,
                    HandlingMode = HandlingMode.AstronomicDay
                });

                await _commandProcessor.ExecuteAsync(new UpdateOutputFormCommand(OutputForm));
            });

            return Task.CompletedTask;
        }


    }
}
