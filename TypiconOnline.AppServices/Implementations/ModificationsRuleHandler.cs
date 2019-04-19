using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Infrastructure.Common.Query;
using JetBrains.Annotations;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Repository.EFCore.DataBase;
using TypiconOnline.AppServices.Extensions;

namespace TypiconOnline.AppServices.Implementations
{
    /// <summary>
    /// Обработчик правил. Только добавляет ModifiedRule
    /// </summary>
    public class ModificationsRuleHandler : RuleHandlerBase
    {
        private readonly TypiconDBContext _dbContext;
        private readonly int _typiconVersionId;
        private readonly ModifiedYear _modifiedYear;

        public ModificationsRuleHandler(TypiconDBContext dbContext, int typiconVersionId, ModifiedYear modifiedYear) 
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _typiconVersionId = typiconVersionId;
            _modifiedYear = modifiedYear ?? throw new ArgumentNullException(nameof(modifiedYear));

            AuthorizedTypes = new List<Type>()
            {
                typeof(ModifyDay)
            };
        }
        
        /// <summary>
        /// Правило дня, для которого в данный момент совершается операция модификации.
        /// Назначается извне перед вызовом метода Interpret у элемента правила.
        /// </summary>
        public DayRule ProcessingDayRule { get; set; }

        public override bool Execute(ICustomInterpreted element)
        {
            bool result = false;

            var dayRule = GetDayRule(element);

            if (dayRule != null)
            {
                int? priority = (element as ModifyDay).Priority;

                if (priority == null)
                {
                    priority = dayRule.Template.Priority;
                }

                var request = CreateRequest(dayRule, element as ModifyDay, (int)priority);

                _modifiedYear.AddModifiedRule(request);

                result = true;
            }

            return result;
        }

        private ModificationsRuleRequest CreateRequest(DayRule dayRule, ModifyDay md, int priority)
        {
            return new ModificationsRuleRequest()
            {
                DayRuleId = dayRule.Id,
                Date = md.MoveDateCalculated,
                Priority = priority,
                ShortName = md.ShortName,
                AsAddition = md.AsAddition,
                IsLastName = md.IsLastName,
                UseFullName = md.UseFullName,
                SignNumber = md.SignNumber,
                Filter = md.Filter
            };
        }

        private DayRule GetDayRule(ICustomInterpreted element)
        {
            DayRule result = null;

            if (element is ModifyReplacedDay modifyReplacedDay)
            {
                if (modifyReplacedDay.Kind == KindOfReplacedDay.Menology)
                {
                    result = _dbContext.GetMenologyRule(_typiconVersionId, modifyReplacedDay.DateToReplaceCalculated);
                }
                else
                {
                    result = _dbContext.GetTriodionRule(_typiconVersionId, modifyReplacedDay.DateToReplaceCalculated);
                }
            }
            else if ((element is ModifyDay modifyDay)
                && (modifyDay.MoveDateCalculated.Year == _modifiedYear.Year))
            {
                result = ProcessingDayRule;
            }

            return result;
        }

        public override void ClearResult()
        {
            //nothing
        }

        //private DayRule FindDayRule(RuleHandlerSettings settings)
        //{
        //    return (settings.TypiconRule is DayRule) ? settings.TypiconRule as DayRule : FindDayRule(settings.Addition);
        //}
    }
}
