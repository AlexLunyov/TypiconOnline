using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Jobs;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.WebServices.Hosting
{
    /// <summary>
    /// Сервис, синхронно обрабатывающий задания
    /// </summary>
    public class JobHostedService : BackgroundService
    {
        public JobHostedService(IJobRepository jobs, ICommandProcessor processor)
        {
            Jobs = jobs ?? throw new ArgumentNullException(nameof(jobs));
            Processor = processor ?? throw new ArgumentNullException(nameof(processor));
        }

        protected IJobRepository Jobs { get; }
        protected ICommandProcessor Processor { get; }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                foreach (var job in Jobs.GetAll())
                {
                    Processor.ExecuteAsync(job);
                    //Task.Factory.StartNew(() => _processor.ExecuteAsync(job));
                }

                Thread.Sleep(1000);
            }

            return Task.CompletedTask;
        }
    }
}
