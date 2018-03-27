using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Services;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Books.Apostol;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Domain.Books.Evangelion;
using TypiconOnline.Domain.Books.Katavasia;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Books.OldTestament;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Domain.Books.TheotokionApp;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.Services;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore;

namespace TypiconOnline.WebApi
{
    /// <summary>
    /// Не используется. Оставлен на всякий случай в качестве примера использования стандартных средств DI
    /// </summary>
    public static class DIExtension
    {
        public static void AddTypiconOnlineService(this IServiceCollection services)
        {
            //kernel.Bind<IUnitOfWork>().To<EFUnitOfWork>().InRequestScope();
            //kernel.Bind<ICacheStorage>().To<SystemRuntimeCacheStorage>();
            //kernel.Bind<IConfigurationRepository>().To<AppSettingsConfigurationRepository>();
            //kernel.Bind<ITypiconEntityService>().To<EnrichedTypiconEntityService>();
            //kernel.Bind<ITypiconEntityService>().To<TypiconEntityService>().WhenInjectedInto<EnrichedTypiconEntityService>();
            //kernel.Bind<IEvangelionContext>().To<EvangelionContext>();
            //kernel.Bind<IApostolContext>().To<ApostolContext>();
            //kernel.Bind<IOldTestamentContext>().To<OldTestamentContext>();
            //kernel.Bind<IPsalterContext>().To<PsalterContext>();
            //kernel.Bind<IOktoikhContext>().To<OktoikhContext>();
            //kernel.Bind<ITheotokionAppContext>().To<TheotokionAppContext>();
            //kernel.Bind<IEasterContext>().To<EasterContext>();
            //kernel.Bind<IKatavasiaContext>().To<KatavasiaContext>();
            //kernel.Bind<IRuleHandlerSettingsFactory>().To<RuleHandlerSettingsFactory>();
            //kernel.Bind<IScheduleService>().To<ScheduleService>();
            //kernel.Bind<IRuleSerializerRoot>().To<RuleSerializerRoot>();

            services.AddTransient<IUnitOfWork, SQLiteUnitOfWork>();
            services.AddTransient<ITypiconEntityService, TypiconEntityService>();
            services.AddTransient<IEvangelionContext, EvangelionContext>();
            services.AddTransient<IApostolContext, ApostolContext>();
            services.AddTransient<IOldTestamentContext, OldTestamentContext>();
            services.AddTransient<IPsalterContext, PsalterContext>();
            services.AddTransient<IOktoikhContext, OktoikhContext>();
            services.AddTransient<ITheotokionAppContext, TheotokionAppContext>();
            services.AddTransient<IEasterContext, EasterContext>();
            services.AddTransient<IKatavasiaContext, KatavasiaContext>();
            services.AddTransient<IRuleHandlerSettingsFactory, RuleHandlerSettingsFactory>();
            services.AddTransient<IScheduleService, ScheduleService>();
            services.AddTransient<IRuleSerializerRoot, RuleSerializerRoot>();
            services.AddTransient<BookStorage>();


        }
    }
}
