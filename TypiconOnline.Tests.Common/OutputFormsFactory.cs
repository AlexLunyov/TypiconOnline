﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Jobs;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Repository.EFCore.DataBase;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Tests.Common
{
    public class OutputFormsFactory
    {
        public static OutputForms Create(TypiconDBContext dbContext)
        {
            var serializerRoot = TestRuleSerializer.Create(dbContext);

            var settingsFactory = new RuleHandlerSettingsFactory(serializerRoot);

            var commandProcessor = CommandProcessorFactory.Create(dbContext);

            var nameComposer = new ScheduleDayNameComposer(serializerRoot.QueryProcessor);

            var outputFormFactory = new OutputDayFactory(new ScheduleDataCalculator(serializerRoot.QueryProcessor, settingsFactory)
                , nameComposer
                , serializerRoot.TypiconSerializer
                , new ServiceSequenceHandler());

            return new OutputForms(dbContext
            , new ScheduleDayNameComposer(serializerRoot.QueryProcessor)
            , serializerRoot.TypiconSerializer
            , new JobRepository());
        }
    }
}
