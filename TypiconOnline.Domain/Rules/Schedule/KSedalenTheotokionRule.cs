using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Правило для определения богородична в седальне по 3-ей песне в каноне
    /// </summary>
    public class KSedalenTheotokionRule : KSedalenRule
    {
        public KSedalenTheotokionRule(string name) : base(name) { }

        public override DayElementBase Calculate(DateTime date, RuleHandlerSettings settings)
        {
            YmnosStructure result = null;

            Kanonas kanonas = GetKanonas(settings);

            if (kanonas != null)
            {
                switch (Place)
                {
                    case KanonasPlaceKind.sedalen:
                        if (kanonas.Sedalen?.Groups?.Count > 0
                            && kanonas.Sedalen?.Groups[0]?.Ymnis?.Count > 0)
                        {
                            result = new YmnosStructure();
                            YmnosGroup th = new YmnosGroup()
                            {
                                Annotation = new ItemText(kanonas.Sedalen.Groups[0].Annotation),
                                Ihos = kanonas.Sedalen.Groups[0].Ihos,
                                Prosomoion = new Prosomoion(kanonas.Sedalen.Groups[0].Prosomoion)
                            };
                            th.Ymnis.Add(new Ymnos(kanonas.Sedalen.Groups[0].Ymnis[0]));
                            result.Theotokion.Add(th);
                        }
                        break;
                    case KanonasPlaceKind.sedalen_theotokion:
                        if (kanonas.Sedalen?.Theotokion != null)
                        {
                            result = new YmnosStructure() { Theotokion = kanonas.Sedalen.Theotokion };
                        }
                        break;
                    case KanonasPlaceKind.sedalen_stavrostheotokion:
                        if (kanonas?.Sedalen?.Theotokion != null
                        && kanonas.Sedalen.Theotokion.Exists(c => c.Kind == YmnosGroupKind.Stavros))
                        {
                            //Оставляем только крестобородичен
                            result = new YmnosStructure() { Theotokion = kanonas.Sedalen.Theotokion };
                            result.Theotokion.RemoveAll(c => c.Kind == YmnosGroupKind.Undefined);
                        }
                        break;
                    default:
                        result = base.Calculate(date, settings) as YmnosStructure;
                        break;
                }
            }

            return result;
        }
    }
}
