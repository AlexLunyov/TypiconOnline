using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Books.TheotokionApp;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Правило для Богородичного текста в стихирах
    /// </summary>
    public class TheotokionRule : YmnosRule
    {
        ITheotokionAppContext theotokionApp;

        public TheotokionRule(string name, ITheotokionAppContext context) : base(name)
        {
            theotokionApp = context ?? throw new ArgumentNullException("ITheotokionAppContext");
        }

        public TheotokionRule(XmlNode node) : base(node)
        {
            if (node.HasChildNodes && node.FirstChild.Name == RuleConstants.YmnosRuleNode)
            {
                ReferenceYmnos = RuleFactory.CreateYmnosRule(node.FirstChild);
            }

            theotokionApp = BookStorage.Instance.TheotokionApp;
        }

        public YmnosRule ReferenceYmnos { get; set; }

        protected override void Validate()
        {
            base.Validate();

            if (ReferenceYmnos == null 
                && ((Place != PlaceYmnosSource.kekragaria_theotokion)
                && (Place != PlaceYmnosSource.liti_theotokion)
                && (Place != PlaceYmnosSource.aposticha_esperinos_theotokion)
                && (Place != PlaceYmnosSource.aposticha_orthros_theotokion)
                && (Place != PlaceYmnosSource.ainoi_theotokion)
                && (Place != PlaceYmnosSource.troparion)
                && (Place != PlaceYmnosSource.sedalen1_theotokion)
                && (Place != PlaceYmnosSource.sedalen2_theotokion)
                && (Place != PlaceYmnosSource.sedalen3_theotokion)
                || (Source == YmnosSource.Irmologion)))
            {
                //если дочерний элемент не определен, а место указано - либо не имеющее богородична
                //либо из ирмология - где нет привязки к гласу
                AddBrokenConstraint(TheotokionRuleBusinessConstraint.ChildRequired, ElementName);
            }
            else if (ReferenceYmnos?.IsValid == false)
            {
                AppendAllBrokenConstraints(ReferenceYmnos, ElementName);
            }


        }

        public override DayElementBase Calculate(DateTime date, RuleHandlerSettings settings)
        {
            YmnosStructure result = null;

            if (Source == YmnosSource.Irmologion)
            {
                int calcIhos = (ReferenceYmnos.Calculate(date, settings) as YmnosStructure).Ihos;

                GetTheotokionResponse response = theotokionApp.Get(
                    new GetTheotokionRequest() { Place = Place, Ihos = calcIhos, DayOfWeek = date.DayOfWeek });

                if (response.Exception == null && response.BookElement != null)
                {
                    result = new YmnosStructure();
                    result.Theotokion.Add(response.BookElement);
                }
            }
            else
            {
                result = base.Calculate(date, settings) as YmnosStructure;
            }

            return result;
        }
    }
}
