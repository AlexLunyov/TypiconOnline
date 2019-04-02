using JetBrains.Annotations;
using System;
using System.Linq;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Domain.Query.Books;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Domain.Rules.Output;
using TypiconOnline.Domain.Rules.Output.Messaging;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Ссылка на Псалом
    /// </summary>
    public class PsalmRule : RuleExecutable, ICustomInterpreted, IViewModelElement, ICalcStructureElement
    {
        public PsalmRule(string name, [NotNull] IDataQueryProcessor queryProcessor, [NotNull] IElementViewModelFactory<PsalmRule> viewModelFactory) : base(name)
        {
            QueryProcessor = queryProcessor ?? throw new ArgumentNullException("queryProcessor in PsalmRule");
            ViewModelFactory = viewModelFactory ?? throw new ArgumentNullException("viewModelFactory in PsalmRule");
        }

        /// <summary>
        /// Ссылка на Псалом
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// Начальный стих
        /// В случае "нулевого" конечного стиха стихи Псалма используются начиная со стартового до конца
        /// </summary>
        /// <remarks>1-ориентированный</remarks>
        public int? StartStihos { get; set; }
        /// <summary>
        /// Конечный стих
        /// В случае "нулевого" начального стиха стихи Псалма используются начиная с начала до конечного стиха
        /// </summary>
        /// <remarks>1-ориентированный</remarks>
        public int? EndStihos { get; set; }

        protected IDataQueryProcessor QueryProcessor { get; }

        protected IElementViewModelFactory<PsalmRule> ViewModelFactory { get; }

        protected override void InnerInterpret(IRuleHandler handler)
        {
            if (handler.IsAuthorized<PsalmRule>())
            {
                handler.Execute(this);
            }
        }

        protected override void Validate()
        {
            if (Number < 1 || Number > 150)
            {
                AddBrokenConstraint(PsalmRuleBusinessConstraint.InvalidNumber, ElementName);
            }

            if (StartStihos != null && StartStihos < 1)
            {
                AddBrokenConstraint(PsalmRuleBusinessConstraint.NegativeStartIndex, ElementName);
            }

            if (EndStihos != null && EndStihos < 1)
            {
                AddBrokenConstraint(PsalmRuleBusinessConstraint.NegativeEndIndex, ElementName);
            }

            if (StartStihos != null && EndStihos != null && StartStihos > EndStihos)
            {
                AddBrokenConstraint(PsalmRuleBusinessConstraint.IndexesMismatch, ElementName);
            }
        }

        public void CreateViewModel(IRuleHandler handler, Action<OutputElementCollection> append)
        {
            ViewModelFactory.Create(new CreateViewModelRequest<PsalmRule>()
            {
                Element = this,
                Handler = handler,
                AppendModelAction = append
            });
        }

        public DayElementBase Calculate(RuleHandlerSettings settings)
        {
            var psalm = QueryProcessor.Process(new PsalmQuery(Number));

            BookReading psalmReading = null;

            if (psalm?.Reading != null)
            {
                psalmReading = GetPsalm(psalm.Reading);
            }

            return psalmReading;
        }

        private BookReading GetPsalm(BookReading psalm)
        {
            BookReading resultReading;

            if (StartStihos != null || EndStihos != null)
            {
                resultReading = new BookReading();

                int end = (EndStihos != null && EndStihos < psalm.Text.Count) ? (int)EndStihos : psalm.Text.Count;

                for (int start = StartStihos ?? 1; start <= end; start++)
                {
                    var stihos = psalm.Text.FirstOrDefault(c => c.StihosNumber == start);
                    if (stihos != null)
                    {
                        resultReading.Text.Add(stihos);
                    }
                }
            }
            else
            {
                resultReading = psalm;
            }

            return resultReading;
        }
    }
    

    public class PsalmRuleBusinessConstraint
    {
        public static readonly BusinessConstraint InvalidNumber = new BusinessConstraint("Неверное определение номера Псалма.");
        public static readonly BusinessConstraint NegativeStartIndex = new BusinessConstraint("Стартовый стих должен быть больше нуля.");
        public static readonly BusinessConstraint NegativeEndIndex = new BusinessConstraint("Конечный стих должен быть больше нуля.");
        public static readonly BusinessConstraint IndexesMismatch = new BusinessConstraint("Стартовый стих должен быть меньше либо равен конечному.");
    }
}
