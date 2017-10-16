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

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Правило для Богородичного текста в стихирах
    /// </summary>
    public class TheotokionRule : YmnosRule
    {
        private YmnosRule _ymnos;

        public int CalculatedIhos { get; protected set; }

        public TheotokionRule(XmlNode node) : base(node)
        {
            if (node.HasChildNodes && node.FirstChild.Name == RuleConstants.YmnosRuleNode)
            {
                _ymnos = RuleFactory.CreateYmnosRule(node.FirstChild);
            }
        }

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            base.InnerInterpret(date, handler);

            CalculatedIhos = _ymnos.CalculateYmnosStructure(date, handler).Ihos;
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

        public override YmnosStructure CalculateYmnosStructure(DateTime date, IRuleHandler handler)
        {
            YmnosStructure result = null;

            if (Source.Value == YmnosSource.Irmologion)
            {
                GetTheotokionResponse response = BookStorage.Instance.TheotokionApp.Get(
                    new GetTheotokionRequest() { Place = Place.Value, Ihos = CalculatedIhos });

                if (response.Exception == null && response.BookElement != null)
                {
                    result = new YmnosStructure();
                    result.Theotokion.Add(response.BookElement);
                }
            }
            else
            {
                result = base.CalculateYmnosStructure(date, handler);
            }

            return result;
        }
    }
}
