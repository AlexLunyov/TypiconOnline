using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.Interfaces;

namespace TypiconOnline.AppServices.Jobs
{
    public class RecalculateCompiledSchedulesJobHandler : ICommandHandler<RecalculateCompiledSchedulesJob>
    {
        private const string YEARS_COUNT = "calculate_yearscount";

        private readonly IConfigurationRepository _configRepo;

        public RecalculateCompiledSchedulesJobHandler([NotNull] IConfigurationRepository configRepo)
        {
            _configRepo = configRepo ?? throw new ArgumentNullException(nameof(configRepo));
        }

        public void Execute(RecalculateCompiledSchedulesJob job)
        {

            ClearModifiedYears(job.TypiconId);

            DoTheJob(job.TypiconId);


        }

        public Task ExecuteAsync(RecalculateCompiledSchedulesJob command)
        {
            throw new NotImplementedException();
        }

        private void DoTheJob(int typiconId)
        {
            throw new NotImplementedException();
        }

        private void ClearModifiedYears(int typiconId)
        {
            throw new NotImplementedException();
        }

        
    }
}
