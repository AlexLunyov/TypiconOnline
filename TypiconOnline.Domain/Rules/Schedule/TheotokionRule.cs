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
        private YmnosRule _ymnos;

        public TheotokionRule(XmlNode node) : base(node)
        {
            if (node.HasChildNodes && node.FirstChild.Name == RuleConstants.YmnosRuleNode)
            {
                _ymnos = RuleFactory.CreateYmnosRule(node.FirstChild);
            }
        }

        protected override void Validate()
        {
            base.Validate();

            if (_ymnos == null 
                && ((Place.Value != PlaceYmnosSource.kekragaria_theotokion)
                && (Place.Value != PlaceYmnosSource.liti_theotokion)
                && (Place.Value != PlaceYmnosSource.aposticha_esperinos_theotokion)
                && (Place.Value != PlaceYmnosSource.aposticha_orthros_theotokion)
                && (Place.Value != PlaceYmnosSource.ainoi_theotokion)
                && (Place.Value != PlaceYmnosSource.troparion)
                && (Place.Value != PlaceYmnosSource.sedalen1_theotokion)
                && (Place.Value != PlaceYmnosSource.sedalen2_theotokion)
                && (Place.Value != PlaceYmnosSource.sedalen3_theotokion)
                || (Source.Value == YmnosSource.Irmologion)))
            {
                //если дочерний элемент не определен, а место указано - либо не имеющее богородична
                //либо из ирмология - где нет привязки к гласу
                AddBrokenConstraint(TheotokionRuleBusinessConstraint.ChildRequired, ElementName);
            }
            else if (_ymnos?.IsValid == false)
            {
                AppendAllBrokenConstraints(_ymnos, ElementName);
            }


        }

        public override DayElementBase Calculate(DateTime date, RuleHandlerSettings settings)
        {
            YmnosStructure result = null;

            if (Source.Value == YmnosSource.Irmologion)
            {
                int calcIhos = (_ymnos.Calculate(date, settings) as YmnosStructure).Ihos;

                GetTheotokionResponse response = BookStorage.Instance.TheotokionApp.Get(
                    new GetTheotokionRequest() { Place = Place.Value, Ihos = calcIhos, DayOfWeek = date.DayOfWeek });

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
