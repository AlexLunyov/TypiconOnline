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

        public YmnosRule ReferenceYmnos { get; set; }

        protected override void Validate()
        {
            base.Validate();

            if (ReferenceYmnos == null 
                && ((Place != PlaceYmnosSource.kekragaria_theotokion)
                && (Place != PlaceYmnosSource.kekragaria_stavrostheotokion)
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

        public override DayElementBase Calculate(RuleHandlerSettings settings)
        {
            YmnosStructure result = null;

            if (!IsValid)
            {
                return null;
            }

            if (Source == YmnosSource.Irmologion)
            {
                int calcIhos = (ReferenceYmnos.Calculate(settings) as YmnosStructure).Ihos;

                GetTheotokionResponse response = theotokionApp.Get(
                    new GetTheotokionRequest() { Place = Place.Value, Ihos = calcIhos, DayOfWeek = settings.Date.DayOfWeek });

                if (response.Exception == null && response.BookElement != null)
                {
                    result = new YmnosStructure();
                    result.Theotokion.Add(response.BookElement);
                }
            }
            else
            {
                result = base.Calculate(settings) as YmnosStructure;
            }

            return result;
        }
    }
}
