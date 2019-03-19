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
        /// <summary>
        /// Максимум заданий, обрабатываемых одновременно
        /// </summary>
        //private const int MAX_TASKS = 6;

        private readonly IQueue _queue;
        private readonly ICommandProcessor _processor;

        public JobHostedService(IQueue queue, ICommandProcessor processor)
        {
            _queue = queue ?? throw new ArgumentNullException(nameof(queue));
            _processor = processor ?? throw new ArgumentNullException(nameof(processor));
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                foreach (var job in _queue.ExtractAll<IJob>())
                {
                    _processor.Execute(job);
                    //Task.Factory.StartNew(() => _processor.ExecuteAsync(job));
                }

                Thread.Sleep(1000);
            }

            return Task.CompletedTask;
        }
    }
}
