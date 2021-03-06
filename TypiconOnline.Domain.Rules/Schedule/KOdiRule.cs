﻿using System;
using System.Collections.Generic;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Schedule.Extensions;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Элемент определяет последовательность тропарей для N-ой песни канона.
    /// Используется в KanonasRule
    /// </summary>
    public class KOdiRule : ExecContainer, IAsAdditionElement, ICustomInterpreted
    {
        public KOdiRule(string name, KanonasRule parent) : base(name)
        {
            Parent = parent ?? throw new ArgumentNullException("KanonasRule in KOdiRule");
        }

        #region Properties

        /// <summary>
        /// Номер песни. Если null, то содержимое элемента является общим для всех песней по умолчанию
        /// </summary>
        public int? Number { get; set; }

        private List<Kanonas> _kanonesCalc = new List<Kanonas>();

        /// <summary>
        /// Вычисленные каноны правила
        /// </summary>
        public IReadOnlyList<Kanonas> Kanones => _kanonesCalc.AsReadOnly();

        #region IAsAdditionElement implementation 
        /// <summary>
        /// Ссылка на KanonasRule
        /// </summary>
        public IAsAdditionElement Parent { get; }

        public string AsAdditionName
        {
            get
            {
                string result = $"{Parent.AsAdditionName}/{ElementName}";

                if (Number != null)
                {
                    result += $"?{RuleConstants.KOdiRuleNumberAttrName}={Number}";
                }

                return result;
            }
        }
        public AsAdditionMode AsAdditionMode { get; set; }

        /// <summary>
        /// Переписывает только номер песни.
        /// </summary>
        /// <param name="source"></param>
        public void RewriteValues(IAsAdditionElement source)
        {
            if (source is KOdiRule s)
            {
                if (s.Number != null)
                {
                    Number = s.Number;
                }
            }
        }

        #endregion

        #endregion

        protected override void InnerInterpret(IRuleHandler handler)
        {
            if (handler.IsTypeAuthorized(this) && !this.AsAdditionHandled(handler))
            {
                //используем специальный обработчик для KKatavasiaRule
                var katavasia = GetChildElements<KKatavasiaRule>(handler.Settings);

                //используем специальный обработчик для KanonasItem,
                //чтобы создать список источников канонов на обработку
                var kanones = GetChildElements<KKanonasItemRule>(handler.Settings);

                _kanonesCalc.Calculate(handler.Settings, kanones, katavasia, (Parent as KanonasRule).IsOrthros);

                handler.Execute(this);
            }
        }

        protected override void Validate()
        {
            base.Validate();

            if (Number != null && (Number < 1 || Number > 9))
            {
                AddBrokenConstraint(KOdiBusinessConstraint.OdiNumberInvalid);
            }
        }
    }

    public class KOdiBusinessConstraint
    {
        public static readonly BusinessConstraint OdiNumberInvalid = new BusinessConstraint("Номер песни канона должен быть в диапазоне от 1 до 9.");
    }
}