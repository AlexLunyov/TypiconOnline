using System;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Interfaces;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Абстрактный базовый класс для элементов правил, использующий как основание богослужебный текст Канон
    /// </summary>
    public abstract class KanonasItemRuleBase : RuleExecutable, ICustomInterpreted, ICalcStructureElement//<Kontakion>
    {
        protected KanonasItemRuleBase(string name, ITypiconSerializer serializer) : base(name)
        {
            Serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        #region Properties

        /// <summary>
        /// Источник книги, откуда брать текст
        /// </summary>
        public KanonasSource? Source { get; set; }

        /// <summary>
        /// Место в тексте богослужения для выбора канона
        /// </summary>
        public KanonasKind? Kanonas { get; set; }

        protected ITypiconSerializer Serializer { get; }

        #endregion

        protected override void InnerInterpret(IRuleHandler handler)
        {
            if (handler.IsAuthorized<KanonasItemRuleBase>())
            {
                handler.Execute(this);
            }
        }

        protected override void Validate()
        {
            if (Source == null)
            {
                AddBrokenConstraint(KKontakionRuleBusinessConstraint.SourceRequired, ElementName);
            }
            //else if (!Source.IsValid)
            //{
            //    AppendAllBrokenConstraints(Source);
            //}

            if (Kanonas == null)
            {
                AddBrokenConstraint(KKontakionRuleBusinessConstraint.KanonasRequired, ElementName);
            }
            //else if (!Kanonas.IsValid)
            //{
            //    AppendAllBrokenConstraints(Kanonas);
            //}
        }

        public abstract DayElementBase Calculate(RuleHandlerSettings settings);

        protected Kanonas GetKanonas(RuleHandlerSettings settings)
        {
            Kanonas result = null;

            //разбираемся с source
            DayStructureBase dayWorship = null;
            switch (Source)
            {
                case KanonasSource.Item1:
                    dayWorship = (settings.DayWorships.Count > 0) ? settings.DayWorships[0] : null;
                    break;
                case KanonasSource.Item2:
                    dayWorship = (settings.DayWorships.Count > 1) ? settings.DayWorships[1] : null;
                    break;
                case KanonasSource.Item3:
                    dayWorship = (settings.DayWorships.Count > 2) ? settings.DayWorships[2] : null;
                    break;
                case KanonasSource.Oktoikh:
                    dayWorship = settings.OktoikhDay;
                    break;
            }

            if (dayWorship != null)
            {
                switch (Kanonas)
                {
                    case KanonasKind.Apodipno:
                        //TODO: добавить реализацию
                        break;
                    case KanonasKind.Mesoniktiko:
                        //TODO: добавить реализацию
                        break;
                    case KanonasKind.Orthros1:
                        result = GetOrthrosKanonas(dayWorship, 0);
                        break;
                    case KanonasKind.Orthros2:
                        result = GetOrthrosKanonas(dayWorship, 1);
                        break;
                    case KanonasKind.Orthros3:
                        result = GetOrthrosKanonas(dayWorship, 2);
                        break;
                }
            }

            return result;

            Kanonas GetOrthrosKanonas(DayStructureBase day, int index)
            {
                var element = day.GetElement(Serializer);

                return (element.Orthros?.Kanones?.Count > index) ? element.Orthros.Kanones[index] : default(Kanonas);
            }
        }        
    }
}
