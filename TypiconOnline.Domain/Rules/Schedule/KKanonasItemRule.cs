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
    public class KKanonasItemRule : KKontakionRule, ICustomInterpreted
    {
        public KKanonasItemRule(string name) : base(name) { }

        #region Properties

        /// <summary>
        /// Количество тропарей, которые берутся из выбранного источника
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Признак, использовать ли мученичны в каноне. По умолчанию - true
        /// </summary>
        public bool UseMartyrion { get; set; } = true;

        /// <summary>
        /// Количество ирмосов, которые берутся из выбранного источника. По умолчанию - 0
        /// </summary>
        public int IrmosCount { get; set; } = 0;

        /// <summary>
        /// Если true, добавляет в конец 3-,6,8 и 9-х песен ирмосы канона, в качестве катавасий.
        /// Вычисляемое. Определяется в KanonasRule
        /// По умолчанию, false
        /// </summary>
        //public bool IncludeKatavasia { get; set; } = false;

        #endregion

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            if (handler.IsAuthorized<KKanonasItemRule>())
            {
                handler.Execute(this);
            }
        }

        protected override void Validate()
        {
            base.Validate();

            if (Count < 1)
            {
                AddBrokenConstraint(KanonasItemBusinessConstraint.CountInvalid, ElementName);
            }

            if (IrmosCount < 0)
            {
                AddBrokenConstraint(KanonasItemBusinessConstraint.IrmosCountInvalid, ElementName);
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
                    Ymnos irmos = odi.Troparia.Find(c => c.Kind == YmnosKind.Irmos);

                    int troparia = IrmosCount;
                    while (troparia > 0)
                    {
                        o.Troparia.Add(irmos);
                        troparia--;
                    }

                    //добавляем тропари
                    o.Troparia.AddRange(GetYmnis(odi));

                    //if (IncludeKatavasia
                    //     && (odi.Number == 3 || odi.Number == 6 || odi.Number == 8 || odi.Number == 9))
                    //{
                    //    //добавляем ирмос в качестве катавасии
                    //    Ymnos katavasia = new Ymnos(irmos)
                    //    {
                    //        Kind = YmnosKind.Katavasia
                    //    };
                    //    o.Troparia.Add(katavasia);
                    //}

                    //добавляем саму песнь
                    result.Odes.Add(o);
                }
            }

            return result;
        }

        public DayElementBase CalculateEveryDayKatavasia(DateTime date, RuleHandlerSettings settings)
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

                    //ирмос
                    Ymnos irmos = odi.Troparia.Find(c => c.Kind == YmnosKind.Irmos);

                    if ((odi.Number == 3 || odi.Number == 6 || odi.Number == 8 || odi.Number == 9))
                    {
                        //добавляем ирмос в качестве катавасии
                        Ymnos katavasia = new Ymnos(irmos)
                        {
                            Kind = YmnosKind.Katavasia
                        };
                        o.Troparia.Add(katavasia);
                    }

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
            List<Ymnos> ymnis = odi.Troparia.FindAll(c => (UseMartyrion)
                                        ? c.Kind != YmnosKind.Irmos && c.Kind != YmnosKind.Katavasia 
                                        : c.Kind != YmnosKind.Irmos && c.Kind != YmnosKind.Katavasia && c.Kind != YmnosKind.Martyrion);

            if (ymnis == null)
            {
                return result;
            }

            //если есть ирмосы, вычитаем их количество
            int count = Count - IrmosCount;

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
