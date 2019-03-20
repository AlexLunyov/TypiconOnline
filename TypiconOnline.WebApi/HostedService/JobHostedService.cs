using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Jobs;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.WebApi.HostedService
{
    /// <summary>
    /// Сервис, синхронно обрабатывающий задания
    /// </summary>
    public class JobHostedService : BackgroundService
    {
        public JobHostedService(IQueue queue, ICommandProcessor processor)
        {
            Queue = queue ?? throw new ArgumentNullException(nameof(queue));
            Processor = processor ?? throw new ArgumentNullException(nameof(processor));
        }

        protected IQueue Queue { get; }
        protected ICommandProcessor Processor { get; }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                foreach (var job in Queue.ExtractAll<IJob>())
                {
                    Processor.Execute(job);
                    //Task.Factory.StartNew(() => _processor.ExecuteAsync(job));
                }

                Thread.Sleep(1000);
            }

            return Task.CompletedTask;
        }
    }
}
