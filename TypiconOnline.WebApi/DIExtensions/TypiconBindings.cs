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
using TypiconOnline.Repository.EFSQLite;
using Microsoft.Extensions.Configuration;

namespace TypiconOnline.WebApi.DIExtensions
{
    public static class TypiconBindings
    {
        public static void BindTypiconServices(this IKernel kernel, IConfiguration configuration)
        {
            string con = configuration.GetConnectionString("DBTypicon");

            kernel.Bind<IUnitOfWork>().To<EFSQLiteUnitOfWork>().WithConstructorArgument("connection", con);

            kernel.Bind<ITypiconEntityService>().To<TypiconEntityService>();
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
            kernel.Bind<IRuleSerializerRoot>().To<RuleSerializerRoot>();

        }
    }
}
