using System.Collections.Generic;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class ExapostilarionItemRule : SourceHavingRuleBase, IExapostilarionRuleElement, ICustomInterpreted
    {
        public ExapostilarionItemRule(string name, ITypiconSerializer serializer, IDataQueryProcessor queryProcessor) 
            : base(name, serializer, queryProcessor)
        {
        }

        /// <summary>
        /// Место, откуда брать элемент
        /// </summary>
        public ExapostilarionItemPlace Place { get; set; }

        /// <summary>
        /// Место, куда добавлять элемент
        /// </summary>
        public ExapostilarionItemKind Kind { get; set; }

        /// <summary>
        /// Количество повторов эксапостилария. По умолчанию – 1
        /// </summary>
        public int Count { get; set; } = 1;

        public override DayElementBase Calculate(RuleHandlerSettings settings) => GetStructure(settings);

        public Exapostilarion GetStructure(RuleHandlerSettings settings)
        {
            Exapostilarion result = null;

            if (!ThrowExceptionIfInvalid(settings))
            {
                //разбираемся с source
                DayContainer day = GetDayContainer(settings);

                //не выдаем ошибки, если день не найден
                if (day != null)
                {
                    //теперь разбираемся с place И kind
                    switch (Kind)
                    {
                        case ExapostilarionItemKind.Exap:
                            {
                                List<ExapostilarionItem> ymnis = GetYmnis(day);
                                if (ymnis != null)
                                {
                                    result = new Exapostilarion();
                                    result.Ymnis.AddRange(ymnis);
                                }
                            }
                            break;
                        case ExapostilarionItemKind.Theotokion:
                            {
                                ExapostilarionItem ymnos = GetYmnos(day);
                                if (ymnos != null)
                                {
                                    result = new Exapostilarion
                                    {
                                        Theotokion = ymnos
                                    };
                                }
                            }
                            break;
                    }
                }
            }

            return result;
        }

        private ExapostilarionItem GetYmnos(DayContainer day)
        {
            ExapostilarionItem result = null;

            switch (Place)
            {
                case ExapostilarionItemPlace.Exap1:
                    {
                        result = (day.Orthros?.Exapostilarion?.Ymnis.Count > 0) ? day.Orthros.Exapostilarion.Ymnis[0] : null;
                    }
                    break;
                case ExapostilarionItemPlace.Exap2:
                    {
                        result = (day.Orthros?.Exapostilarion?.Ymnis.Count > 1) ? day.Orthros.Exapostilarion.Ymnis[1] : null;
                    }
                    break;
                case ExapostilarionItemPlace.Theotokion:
                    {
                        result = day.Orthros?.Exapostilarion?.Theotokion;
                    }
                    break;
            }
            
            return result;
        }

        private List<ExapostilarionItem> GetYmnis(DayContainer day)
        {
            List<ExapostilarionItem> result = null;

            if (GetYmnos(day) is ExapostilarionItem item)
            {
                result = new List<ExapostilarionItem>();

                for (int i = Count; i > 0; i--)
                {
                    result.Add(item);
                }
            }

            return result;
        }

        protected override void InnerInterpret(IRuleHandler handler)
        {
            if (handler.IsAuthorized<ExapostilarionItemRule>())
            {
                handler.Execute(this);
            }
        }

        protected override void Validate()
        {
            if (Count <= 0)
            {
                AddBrokenConstraint(ExapostilarionItemRuleBusinessConstraint.InvalidCount);
            }
        }
    }

    public class ExapostilarionItemRuleBusinessConstraint
    {
        public static readonly BusinessConstraint InvalidCount = new BusinessConstraint("Количество должно иметь положительное значение.");

        //public static readonly BusinessConstraint KindMismatch = new BusinessConstraint("Отсутствуют определение типа песнопения (обычный, славник, богородичен...).");
        //public static readonly BusinessConstraint SourceInvalid = new BusinessConstraint("Неверный источник песнопения (книги).");
        //public static readonly BusinessConstraint PlaceInvalid = new BusinessConstraint("Неверно определено место в источнике песнопения (книги).");
    }
}