using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TypiconOnline.WebApi.DIExtensions
{
    public static class HostedServiceExtensions
    {
        public static IServiceCollection AddHostedServiceFromContainer<THostedService>(
            this IServiceCollection services, SimpleInjector.Container container)
            where THostedService : class, IHostedService
        {
            //The AddHostedService<T> extension method adds the services as transient so this would
            //be consistent with that.
            var lifestyle = SimpleInjector.Lifestyle.Transient;
            //Or would this be better?It's similar to what the UseMiddleware extension method is doing:
            //var lifestyle =
            //    container.Options.LifestyleSelectionBehavior.SelectLifestyle(typeof(THostedService));

            SimpleInjector.InstanceProducer<THostedService> producer =
                lifestyle.CreateProducer<THostedService>(typeof(THostedService), container);

            return services.AddTransient<IHostedService, THostedService>(c => producer.GetInstance());
            //return services.AddTransient<IHostedService, THostedService>(c => 
            //{
            //    using (AsyncScopedLifestyle.BeginScope(container))
            //    {
            //        return producer.GetInstance();
            //    }
            //});
        }
    }
}
