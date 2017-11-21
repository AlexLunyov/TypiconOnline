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

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Правило для определения конкретного канона 
    /// </summary>
    public class KanonasItem : KKontakionRule, ICustomInterpreted
    {
        public KanonasItem(XmlNode node) : base(node)
        {
            XmlAttribute attr = node.Attributes[RuleConstants.KanonasCountAttrName];
            Count = new ItemInt((attr != null) ? attr.Value : "");

            attr = node.Attributes[RuleConstants.KanonasMartyrionAttrName];
            Martyrion = new ItemBoolean((attr != null) ? attr.Value : "true");

            attr = node.Attributes[RuleConstants.KanonasIrmosCountAttrName];
            IrmosCount = new ItemInt((attr != null) ? attr.Value : "0");
        }

        #region Properties

        /// <summary>
        /// Количество тропарей, которые берутся из выбранного источника
        /// </summary>
        public ItemInt Count { get; set; }

        /// <summary>
        /// Признак, использовать ли мученичны в каноне. По умолчанию - true
        /// </summary>
        public ItemBoolean Martyrion { get; set; }

        /// <summary>
        /// Количество ирмосов, которые берутся из выбранного источника. По умолчанию - 0
        /// </summary>
        public ItemInt IrmosCount { get; set; }

        #endregion

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            if (handler.IsAuthorized<KanonasItem>())
            {
                handler.Execute(this);
            }
        }

        protected override void Validate()
        {
            base.Validate();

            if (!Count.IsValid)
            {
                AppendAllBrokenConstraints(Count);
            }
            else
            {
                if (Count.Value < 1)
                {
                    AddBrokenConstraint(KanonasItemBusinessConstraint.CountInvalid, ElementName);
                }
            }

            if (!IrmosCount.IsValid)
            {
                AppendAllBrokenConstraints(Count);
            }
            else
            {

                if (IrmosCount.Value < 0)
                {
                    AddBrokenConstraint(KanonasItemBusinessConstraint.IrmosCountInvalid, ElementName);
                }
            }

            if (!Martyrion.IsValid)
            {
                AppendAllBrokenConstraints(Martyrion);
            }
        }

        public override DayElementBase Calculate(DateTime date, RuleHandlerSettings settings)
        {
            Kanonas result = null;
            Kanonas source = GetKanonas(settings);

            if (source != null)
            {
                result = new Kanonas()
                {
                    Acrostic = source.Acrostic,
                    Annotation = source.Annotation,
                    Ihos = source.Ihos,
                    Stihos = source.Stihos
                };

                foreach (Odi odi in source.Odes)
                {
                    Odi o = new Odi() { Number = odi.Number };

                    //добавляем ирмос(ы)
                    int troparia = IrmosCount.Value;
                    while (troparia > 0)
                    {
                        o.Troparia.Add(odi.Troparia.Find(c => c.Kind == YmnosKind.Irmos));
                        troparia--;
                    }

                    //добавляем тропари
                    o.Troparia.AddRange(GetYmnis(odi));
                    //добавляем саму песнь
                    result.Odes.Add(o);
                }
            }

            return result;
        }

        private IEnumerable<Ymnos> GetYmnis(Odi odi)
        {
            List<Ymnos> result = new List<Ymnos>();

            //включаем фильтрацию
            //не берем ирмосы, катавасии и мученичны - если указано 
            List<Ymnos> ymnis = odi.Troparia.FindAll(c => (Martyrion.Value)
                                        ? c.Kind != YmnosKind.Irmos && c.Kind != YmnosKind.Katavasia 
                                        : c.Kind != YmnosKind.Irmos && c.Kind != YmnosKind.Katavasia && c.Kind != YmnosKind.Martyrion);

            if (ymnis == null)
            {
                return result;
            }

            //если есть ирмосы, вычитаем их количество
            int count = Count.Value - IrmosCount.Value;

            if (ymnis.Count >= count)
            {
                result.AddRange(ymnis.Take(count));
            }
            else
            {
                /*если заявленное количество больше того, что есть, выдаем с повторами
                * например: 8 = 3 3 2
                *           10= 4 4 3
                */

                int appendedCount = 0;

                int i = 0;

                while (appendedCount < count)
                {
                    //округляем в большую сторону результат деления count на YmnosStructureCount
                    //в результате получаем, сколько раз необходимо повторять песнопение
                    int b = (int)Math.Ceiling((double)(count - appendedCount) / (ymnis.Count - i));

                    Ymnos ymnosToAdd = ymnis[i];

                    while (b > 0)
                    {
                        result.Add(new Ymnos(ymnosToAdd));

                        b--;
                        appendedCount++;
                    }

                    i++;
                }
            }

            return result;
        }
    }
}
