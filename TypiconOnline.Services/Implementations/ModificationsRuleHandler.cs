﻿using System;
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

namespace TypiconOnline.AppServices.Implementations
{
    /// <summary>
    /// Обработчик правил. Только добавляет ModifiedRule
    /// </summary>
    public class ModificationsRuleHandler : RuleHandlerBase
    {
        readonly IModifiedRuleService modifiedRuleService;
        readonly int yearToModify;

        public ModificationsRuleHandler(IModifiedRuleService modifiedRuleService, int year) : this (new RuleHandlerSettings(), modifiedRuleService, year) { }

        public ModificationsRuleHandler(RuleHandlerSettings settings, IModifiedRuleService modifiedRuleService, int year) 
        {
            _settings = settings;
            //Initialize(settings);

            AuthorizedTypes = new List<Type>()
            {
                typeof(ModifyDay)
            };

            this.modifiedRuleService = modifiedRuleService ?? throw new ArgumentNullException("modifiedRuleService in ModificationsRuleHandler");

            yearToModify = year;
        }

        public override void ClearResult()
        {
            //nothing
        }

        public override bool Execute(ICustomInterpreted element)
        {
            bool result = false;

            var typiconEntity = _settings.Rule.Owner;

            if (element is ModifyReplacedDay modifyReplacedDay)
            {
                DayRule ruleToModify;

                if (modifyReplacedDay.Kind == KindOfReplacedDay.Menology)
                {
                    ruleToModify = typiconEntity.GetMenologyRule(modifyReplacedDay.DateToReplaceCalculated);
                }
                else //if ((element as ModifyReplacedDay).Kind == RuleConstants.KindOfReplacedDay.triodion)
                {
                    int daysFromEaster = modifyReplacedDay.EasterContext.GetDaysFromCurrentEaster(modifyReplacedDay.DateToReplaceCalculated);
                    ruleToModify = typiconEntity.GetTriodionRule(daysFromEaster);
                }

                int priority = modifyReplacedDay.Priority;

                if (priority == 0)
                {
                    priority = ruleToModify.Template.Priority;
                }

                var request = CreateRequest(ruleToModify, modifyReplacedDay, priority);

                modifiedRuleService.AddModifiedRule(typiconEntity, request);

                result = true;
            }
            else if ((element is ModifyDay modifyDay) 
                && (modifyDay.MoveDateCalculated.Year == yearToModify))
            {
                int priority = modifyDay.Priority;

                //TypiconRule seniorTypiconRule = Rules[0];

                if (priority == 0)
                {
                    priority = /*seniorTypicon*/_settings.Rule.Template.Priority;
                }

                var request = CreateRequest((DayRule)_settings.Rule, modifyDay, priority);

                modifiedRuleService.AddModifiedRule(typiconEntity, request);

                result = true;
            }

            ModificationsRuleRequest CreateRequest(DayRule caller, ModifyDay md, int priority)
            {
                return new ModificationsRuleRequest()
                {
                    Caller = caller,
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

            return result;
        }

        //public override RenderContainer GetResult()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
