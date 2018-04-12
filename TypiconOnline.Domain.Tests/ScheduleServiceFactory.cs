using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.Services;

namespace TypiconOnline.Domain.Tests
{
    public class ScheduleServiceFactory
    {
        public static ScheduleService Create()
        {
            throw new NotImplementedException();
            //var optionsBuilder = new DbContextOptionsBuilder<TypiconDBContext>();

            ////string path = $"Data Source=31.31.196.160;Initial Catalog=u0351320_Typicon;Integrated Security=False;User Id=u0351320_defaultuser;Password=DDOR0YUMg519DbT2ebzN;MultipleActiveResultSets=True";
            //string path = $"Data Source=(LocalDB)\\MSSQLLocalDB;Database=TypiconDB;Integrated Security=True;Connect Timeout=30";
            //optionsBuilder.UseSqlServer(path);

            //var context = new EFCacheDBContext(optionsBuilder.Options, cacheServiceProvider);
            //var uof = new UnitOfWork(context);
            //IRuleSerializerRoot serializerRoot = new RuleSerializerRoot(BookStorageFactory.Create());

            //return new ScheduleService(new RuleHandlerSettingsFactory(), serializerRoot);
        }
    }
}
