using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TypiconOnline.WebServices.Hosting
{
    public class TimedHostedService<TService> : IHostedService, IDisposable
    where TService : class
    {
        private readonly Container container;
        private readonly Settings settings;
        //private readonly ILogger logger;
        private readonly Timer timer;

        public TimedHostedService(Container container, Settings settings/*, ILogger logger*/)
        {
            this.container = container;
            this.settings = settings;
            //this.logger = logger;
            this.timer = new Timer(callback: _ => this.DoWork());
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Verify that TService can be resolved
            this.container.GetRegistration(typeof(TService), true);
            // Start the timer
            this.timer.Change(dueTime: TimeSpan.Zero, period: this.settings.Interval);
            return Task.CompletedTask;
        }

        private void DoWork()
        {
            try
            {
                using (ThreadScopedLifestyle.BeginScope(this.container))
                {
                    var service = this.container.GetInstance<TService>();
                    this.settings.Action(service);
                }
            }
            catch (Exception ex)
            {
                //this.logger.LogError(ex, ex.Message);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.timer.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose() => this.timer.Dispose();

        public class Settings
        {
            public readonly TimeSpan Interval;
            public readonly Action<TService> Action;

            public Settings(TimeSpan interval, Action<TService> action)
            {
                this.Interval = interval;
                this.Action = action;
            }
        }
    }
}
