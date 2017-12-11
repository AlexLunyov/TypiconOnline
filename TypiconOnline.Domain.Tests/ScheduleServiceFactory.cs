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
            IRuleSerializerRoot serializerRoot = new RuleSerializerRoot(BookStorageFactory.Create());

            return new ScheduleService(new RuleHandlerSettingsFactory(), serializerRoot);
        }
    }
}
