using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.AppServices.Jobs
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _repository;
        private readonly ICommandProcessor _processor;

        public JobService([NotNull] IJobRepository repository, [NotNull] ICommandProcessor processor)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _processor = processor ?? throw new ArgumentNullException(nameof(processor));
        }

        public void Work()
        {
            foreach (var job in _repository.GetAllJobsToWork())
            {
                _processor.Execute(job);
            }
        }
    }
}
