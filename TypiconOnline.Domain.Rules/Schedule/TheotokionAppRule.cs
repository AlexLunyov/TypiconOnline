using JetBrains.Annotations;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Query.Books;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Правило для Богородичного текста в стихирах
    /// </summary>
    public class TheotokionAppRule : RuleExecutable, ICustomInterpreted, IYmnosStructureRuleElement
    {
        IDataQueryProcessor queryProcessor;

        public TheotokionAppRule(string name, [NotNull] IDataQueryProcessor queryProcessor) : base(name)
        {
            this.queryProcessor = queryProcessor;
        }

        /// <summary>
        /// Значение всегда = Theotokion
        /// </summary>
        public YmnosRuleKind Kind { get => YmnosRuleKind.Theotokion; set { } }

        /// <summary>
        /// Место в приложении Ирмология
        /// </summary>
        public TheotokionAppPlace Place { get; set; }

        /// <summary>
        /// Дочерний элемент, по которому вычисляется глас песнопения
        /// </summary>
        public YmnosRule ReferenceYmnos { get; set; }

        protected override void InnerInterpret(IRuleHandler handler)
        {
            if (handler.IsAuthorized<TheotokionAppRule>())
            {
                handler.Execute(this);
            }
        }

        public YmnosStructure GetStructure(RuleHandlerSettings settings)
        {
            YmnosStructure result = new YmnosStructure();

            if (!ThrowExceptionIfInvalid(settings))
            {
                int calcIhos = (ReferenceYmnos.GetStructure(settings) as YmnosStructure).Ihos;

                var response = queryProcessor.Process(
                    new TheotokionAppQuery(Place, calcIhos, settings.Date.DayOfWeek));

                if (response != null)
                {
                    result.Theotokion.Add(response);
                }
            }
            return result;
        }

        protected override void Validate()
        {
            if (ReferenceYmnos == null)
            {
                //если дочерний элемент не определен
                AddBrokenConstraint(TheotokionRuleBusinessConstraint.ChildRequired, ElementName);
            }
        }
    }

    public class TheotokionRuleBusinessConstraint
    {
        public static readonly BusinessConstraint ChildRequired = new BusinessConstraint("Отсутствуют определение дочернего элемента.");
    }
}
