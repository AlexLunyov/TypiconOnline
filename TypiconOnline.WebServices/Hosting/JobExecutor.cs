using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.WebServices.Hosting
{
    public class JobExecutor
    {
        /// <summary>
        /// Максимум заданий, обрабатываемых одновременно
        /// </summary>
        private const int MAX_TASKS = 6;

        public JobExecutor(IJobRepository jobs, ICommandProcessor processor)
        {
            Jobs = jobs ?? throw new ArgumentNullException(nameof(jobs));
            Processor = processor ?? throw new ArgumentNullException(nameof(processor));
        }

        protected IJobRepository Jobs { get; }
        protected ICommandProcessor Processor { get; }

        public void Execute()
        {
            foreach (var job in Jobs.Reserve(MAX_TASKS))
            {
                Task.Factory.StartNew(() => Processor.Execute(job));
            }
        }
    }
}
