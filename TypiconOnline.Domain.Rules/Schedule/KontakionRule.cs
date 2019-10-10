using System;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Domain.Rules.Schedule.Extensions;
using TypiconOnline.Domain.Rules.Output;
using TypiconOnline.Domain.Rules.Output.Messaging;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Правило для использования кондака Утрени
    /// </summary>
    public class KontakionRule : SourceHavingRuleBase, IViewModelElement, ICustomInterpreted, IYmnosStructureRuleElement
    {
        public KontakionRule(string name, ITypiconSerializer serializer, IQueryProcessor queryProcessor, 
            IElementViewModelFactory<KontakionRule> viewModelFactory) : base(name, serializer, queryProcessor)
        {
            ViewModelFactory = viewModelFactory ?? throw new ArgumentNullException("IElementViewModelFactory in KontakionRule");
        }

        public KontakionPlace Place { get; set; }

        /// <summary>
        /// Место для вставки песнопения
        /// </summary>
        public YmnosRuleKind Kind { get; set; }

        /// <summary>
        /// Признак, показывать ли вместе с кондаком и Икос. По умолчанию - false
        /// </summary>
        public bool ShowIkos { get; set; } = false;
        protected IElementViewModelFactory<KontakionRule> ViewModelFactory { get; }

        protected override void InnerInterpret(IRuleHandler handler)
        {
            if (handler.IsTypeAuthorized(this))
            {
                handler.Execute(this);
            }
        }

        /// <summary>
        /// Считаем, что этот метод вызывается, когда кондак является дочерним элементом Структуры
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public YmnosStructure GetStructure(RuleHandlerSettings settings)
        {
            return (Calculate(settings) is Kontakion k) 
                ? k.ToYmnosStructure(ShowIkos && (Kind == YmnosRuleKind.Ymnos)) 
                : new YmnosStructure();
        }

        public override DayElementBase Calculate(RuleHandlerSettings settings)
        {
            Kontakion result = null;

            if (!ThrowExceptionIfInvalid(settings))
            {
                //разбираемся с source
                DayContainer day = GetDayContainer(settings);

                var kontakia = day?.Orthros?.Kontakia;

                //не выдаем ошибки, если день не найден
                if (kontakia != null)
                {
                    //теперь разбираемся с place
                    switch (Place)
                    {
                        case KontakionPlace.orthros1:
                            {
                                result = (kontakia.Count > 0) ? kontakia[0] : null;
                            }
                            break;
                        case KontakionPlace.orthros2:
                            {
                                result = (kontakia.Count > 1) ? kontakia[1] : null;
                            }
                            break;
                    }
                }
            }

            return result;
        }

        public void CreateViewModel(IRuleHandler handler, Action<OutputSectionModelCollection> append)
        {
            ViewModelFactory.Create(new CreateViewModelRequest<KontakionRule>()
            {
                Element = this,
                Handler = handler,
                AppendModelAction = append
            });
        }

        protected override void Validate()
        {
            //nothing
        }
    }
}
