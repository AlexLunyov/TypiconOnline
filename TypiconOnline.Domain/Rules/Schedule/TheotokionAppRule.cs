using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Books.TheotokionApp;
using TypiconOnline.Domain.Books.WeekDayApp;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Правило для Богородичного текста в стихирах
    /// </summary>
    public class TheotokionAppRule : RuleExecutable, ICustomInterpreted, IYmnosStructureRuleElement
    {
        ITheotokionAppContext theotokionApp;

        public TheotokionAppRule(string name, ITheotokionAppContext context) : base(name)
        {
            theotokionApp = context ?? throw new ArgumentNullException("ITheotokionAppContext");
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

                GetTheotokionResponse response = theotokionApp.Get(
                    new GetTheotokionRequest() { Place = Place, Ihos = calcIhos, DayOfWeek = settings.Date.DayOfWeek });

                if (response.Exception == null && response.BookElement != null)
                {
                    result.Theotokion.Add(response.BookElement);
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
