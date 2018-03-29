using Ninject;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Books.Apostol;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Domain.Books.Evangelion;
using TypiconOnline.Domain.Books.TheotokionApp;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Books.OldTestament;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.AppServices.Services;
using TypiconOnline.Domain.Services;
using TypiconOnline.Domain.Books.Katavasia;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Repository.EFCore;
using Microsoft.Extensions.Configuration;
using TypiconOnline.Infrastructure.Common.Interfaces;
using TypiconOnline.AppServices.Standard.Caching;
using TypiconOnline.AppServices.Standard.Configuration;
using Microsoft.Extensions.Caching.Memory;
using TypiconOnline.AppServices.Caching;
using TypiconOnline.Repository.EFCore.DataBase;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Repository.EFCore.Caching;

namespace TypiconOnline.WebApi.DIExtensions
{
    public static class TypiconBindings
    {
        public static void BindTypiconServices(this IKernel kernel, IConfiguration configuration)
        {
            //MemoryCache
            kernel.Bind<IMemoryCache>().To<MemoryCache>()
                .InSingletonScope()
                .WithConstructorArgument("optionsAccessor", new MemoryCacheOptions());
            kernel.Bind<ICacheStorage>().To<MemoryCacheStorage>();

            //Configuration
            kernel.Bind<IConfigurationRepository>().To<ConfigurationRepository>()
                .WithConstructorArgument("configuration", configuration);

            //Настройки для использования SQLite
            //string con = configuration.GetConnectionString("DBTypicon");
            //kernel.Bind<IUnitOfWork>().To<SQLiteUnitOfWork>().WithConstructorArgument("connection", con);

            //Настройки для использования SqlServer
            string con = configuration.GetConnectionString("MSSql");
            kernel.Bind<DBContextBase>().To<MSSqlDBContext>()
                //??????
                .InSingletonScope()
                .WithConstructorArgument("connection", con);

            //RepositoryFactory
            kernel.Bind<IRepositoryFactory>().To<CachingRepositoryFactory>();
            kernel.Bind<IRepositoryFactory>().To<RepositoryFactory>().WhenInjectedInto<CachingRepositoryFactory>();

            //UnitOfWork
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();

            kernel.Bind<ITypiconEntityService>().To<TypiconEntityService>();
            //kernel.Bind<ITypiconEntityService>().To<CachingTypiconEntityService>();
            //kernel.Bind<ITypiconEntityService>().To<TypiconEntityService>().WhenInjectedInto<CachingTypiconEntityService>();

            kernel.Bind<IEvangelionContext>().To<EvangelionContext>();
            kernel.Bind<IApostolContext>().To<ApostolContext>();
            kernel.Bind<IOldTestamentContext>().To<OldTestamentContext>();
            kernel.Bind<IPsalterContext>().To<PsalterContext>();
            kernel.Bind<IOktoikhContext>().To<OktoikhContext>();
            kernel.Bind<ITheotokionAppContext>().To<TheotokionAppContext>();
            kernel.Bind<IEasterContext>().To<EasterContext>();
            kernel.Bind<IKatavasiaContext>().To<KatavasiaContext>();
            kernel.Bind<IRuleHandlerSettingsFactory>().To<RuleHandlerSettingsFactory>();

            kernel.Bind<IScheduleService>().To<ScheduleService>();
            //kernel.Bind<IScheduleService>().To<CachingScheduleService>();
            //kernel.Bind<IScheduleService>().To<ScheduleService>().WhenInjectedInto<CachingScheduleService>();

            kernel.Bind<IRuleSerializerRoot>().To<RuleSerializerRoot>();

        }
    }
}
