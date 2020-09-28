using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.AppServices.Implementations
{
    public class ExplicitDataCalculator : ScheduleDataCalculatorBase
    { 
        private readonly IQueryProcessor _queryProcessor;
        private readonly IScheduleDataCalculator _innerCalculator;
        private readonly IRuleHandlerSettingsFactory _settingsFactory;

        public ExplicitDataCalculator(IQueryProcessor queryProcessor
            , IScheduleDataCalculator innerCalculator
            , IRuleHandlerSettingsFactory settingsFactory)
        {
            _queryProcessor = queryProcessor ?? throw new ArgumentNullException(nameof(queryProcessor));
            _innerCalculator = innerCalculator ?? throw new ArgumentNullException(nameof(innerCalculator));
            _settingsFactory = settingsFactory ?? throw new ArgumentNullException(nameof(settingsFactory));
        }

        public override Result<ScheduleDataCalculatorResponse> Calculate(ScheduleDataCalculatorRequest request)
        {
            var result = _innerCalculator.Calculate(request);

            if (result.Failure)
            {
                return result;
            }

            //находим ExplicitAddRule
            var explicitAddRule = _queryProcessor.Process(new ExplicitAddRuleQuery(request.TypiconVersionId, request.Date));

            if (explicitAddRule != null)
            {
                var create = _settingsFactory.CreateExplicit(new CreateExplicitRuleSettingsRequest(request)
                {
                    Rule = explicitAddRule
                });

                if (create.Success && create.Value is RuleHandlerSettings settings)
                {
                    var lastAddition = GetLastAddition(result.Value.Settings);

                    //и задаем результат у последнего найденного Addition
                    settings.Menologies = lastAddition.Menologies;
                    settings.Triodions = lastAddition.Triodions;
                    settings.OktoikhDay = lastAddition.OktoikhDay;

                    lastAddition.Addition = settings;
                }
            }

            return result;
        }
    }
}
