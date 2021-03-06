﻿using System;
using System.Collections.Generic;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon.Print;
using TypiconOnline.Domain.Typicon.Variable;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon
{
    public class Sign : ModRuleEntity, ITemplateHavingEntity
    {
        public Sign() { }

        public virtual int? TemplateId { get; set; }
        public virtual Sign Template { get; set; }
        public virtual bool IsAddition { get; set; }

        
        public int? PrintTemplateId { get; set; }
        /// <summary>
        /// Предустановленный Печатный шаблон, согласно знакам служб Типикона. 
        /// Например, для служб без знака, 6-ричных, славословных, полиелейных, бденных и т.д.
        /// Используется в для формирования печатной формы Расписания.
        /// </summary>
        public virtual PrintDayTemplate PrintTemplate { get; set; }

        

        public int Priority { get; set; }

        private ItemText _signName;
        /// <summary>
        /// Наименование знака службы на нескольких языках
        /// </summary>
        public virtual ItemText SignName //{ get; set; }// = new ItemText();
        {
            get
            {
                if (_signName == null)
                {
                    _signName = new ItemText();
                }
                return _signName;
            }
            set => _signName = value;
        }

        /// <summary>
        /// Список на используемые в данном Правиле Переменные Устава
        /// </summary>
        public virtual List<VariableModRuleLink<Sign>> VariableLinks { get; set; }

        /// <summary>
        /// Список на используемые в определении данного Правила Печатные шаблоны
        /// </summary>
        public virtual List<PrintTemplateModRuleLink<Sign>> PrintTemplateLinks { get; set; }

        public string GetNameByLanguage(string language)
        {
            return SignName.FirstOrDefault(language).Text;
        }

        protected override void Validate(IRuleSerializerRoot serializerRoot)
        {
            base.Validate(serializerRoot);

            //если нет Шаблона, то Правило обязательно должно быть определено
            if ((!TemplateId.HasValue || TemplateId == 0 || Template == null)
                && string.IsNullOrEmpty(RuleDefinition))
            {
                AddBrokenConstraint(new BusinessConstraint("Правило должно быть определено.", nameof(RuleDefinition)));
            }

            if (!SignName.IsValid)
            {
                AppendAllBrokenConstraints(SignName, "Sign");
            }
        }

        /// <summary>
        /// Возвращает номер предустановленного Знака службы. Если сам не имеет такового, смотрит у родителя.
        /// Если таквойо отсутствует, возвращает 0.
        /// </summary>
        /// <returns></returns>
        //public int GetNumber()
        //{
        //    int result = 0;

        //    if (PrintTemplate != null)
        //    {
        //        result = PrintTemplate.Number;
        //    }
        //    else if (Template != null)
        //    {
        //        result = Template.GetNumber();
        //    }

        //    return result;
        //}

        public Sign GetPredefinedTemplate()
        {
            if (PrintTemplate != null)
            {
                return this;
            }

            if (Template != null)
            {
                return Template.GetPredefinedTemplate();
            }

            return default;
        }
    }
}

