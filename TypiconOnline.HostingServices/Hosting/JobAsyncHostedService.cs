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
    public class JobAsyncHostedService : JobHostedService
    {
        /// <summary>
        /// Максимум заданий, обрабатываемых одновременно
        /// </summary>
        private const int MAX_TASKS = 6;

        public JobAsyncHostedService(IQueue queue, ICommandProcessor processor) : base(queue, processor) { }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                foreach (var job in Queue.Extract<IJob>(MAX_TASKS))
                {
                    Task.Factory.StartNew(() => Processor.Execute(job));
                }

                Thread.Sleep(1000);
            }

            return Task.CompletedTask;
        }
    }
}
