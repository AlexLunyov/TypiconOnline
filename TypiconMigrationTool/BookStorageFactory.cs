using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Books.Apostol;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Domain.Books.Evangelion;
using TypiconOnline.Domain.Books.TheotokionApp;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Books.OldTestament;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Repository.EF;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Domain.Books.Katavasia;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.Services;
using TypiconOnline.AppServices.Implementations;

namespace TypiconMigrationTool
{
    public static class ScheduleServiceFactory
    {
        public static ScheduleService Create() => Create(new EFUnitOfWork());

        public static ScheduleService Create(IUnitOfWork unitOfWork)
        {
            return Create(unitOfWork, new EasterContext(unitOfWork));
        }

        public static ScheduleService Create(IUnitOfWork unitOfWork, IEasterContext easterContext)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");

            BookStorage bookStorage = new BookStorage(new EvangelionContext(unitOfWork),
                                    new ApostolContext(unitOfWork),
                                    new OldTestamentContext(unitOfWork),
                                    new PsalterContext(unitOfWork),
                                    new OktoikhContext(unitOfWork, easterContext),
                                    new TheotokionAppContext(unitOfWork),
                                    easterContext,
                                    new KatavasiaContext(unitOfWork));

            IRuleSerializerRoot serializerRoot = new RuleSerializerRoot(bookStorage);

            return new ScheduleService(new RuleHandlerSettingsFactory(), serializerRoot);
        }
    }
}
