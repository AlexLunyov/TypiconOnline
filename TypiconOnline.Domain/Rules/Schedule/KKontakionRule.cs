using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.ViewModels;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Правило для использования кондака для правила канона 
    /// </summary>
    public class KKontakionRule : RuleExecutable, ICustomInterpreted, ICalcStructureElement, IViewModelElement//<Kontakion>
    {
        public KKontakionRule(string name) : base(name) { }

        #region Properties

        /// <summary>
        /// Источник книги, откуда брать текст
        /// </summary>
        public KanonasSource? Source { get; set; }

        /// <summary>
        /// Место в тексте богослужения для выбора канона
        /// </summary>
        public KanonasKind? Kanonas { get; set; }

        #endregion

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            if (handler.IsAuthorized<KKontakionRule>())
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

        public virtual DayElementBase Calculate(DateTime date, RuleHandlerSettings settings)
        {
            return (IsValid) ? GetKanonas(settings)?.Kontakion : null;
        }

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
                return (day.GetElement().Orthros?.Kanones?.Count > index) ? day.GetElement().Orthros.Kanones[index] : null;
            }
        }

        public void CreateViewModel(IRuleHandler handler, Action<ElementViewModel> append)
        {
            throw new NotImplementedException();
        }
    }
}
