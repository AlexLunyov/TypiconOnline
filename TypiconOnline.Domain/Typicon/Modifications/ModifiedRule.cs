using System;
using System.Linq;
using System.Collections.Generic;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Domain.Typicon.Print;

namespace TypiconOnline.Domain.Typicon.Modifications
{
    public class ModifiedRule : IHasId<int>, IComparable<ModifiedRule>, IDayRule
    {
        public int Id { get; set; }

        public virtual int ModifiedYearId { get; set; }
        public virtual ModifiedYear Parent { get; set; }
        public virtual int DayRuleId { get; set; }
        public virtual DayRule DayRule { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual int Priority { get; set; }

        public virtual bool IsLastName { get; set; }

        public virtual bool IsAddition { get; set; }

        public virtual bool UseFullName { get; set; }

        public virtual int? PrintDayTemplateId { get; set; }
        /// <summary>
        /// Ссылка на печатный шаблон
        /// </summary>
        public virtual PrintDayTemplate PrintDayTemplate { get; set; }

        ItemTextStyled _shortName;
        public virtual ItemTextStyled ShortName
        {
            get
            {
                if (_shortName == null)
                {
                    _shortName = new ItemTextStyled();
                }
                return _shortName;
            }
            set => _shortName = value;
        }

        DayWorshipsFilter _filter;
        public virtual DayWorshipsFilter Filter
        {
            get
            {
                if (_filter == null)
                {
                    _filter = new DayWorshipsFilter();
                }
                return _filter;
            }
            set => _filter = value;
        }

        /// <summary>
        /// Список служб, отфильтрованный согласно настройкам ModifiedRule
        /// </summary>
        public IReadOnlyList<DayWorship> DayWorships
        {
            get
            {
                return (DayRule?.DayWorships != null) ? ApplyFilter(DayRule.DayWorships) : new List<DayWorship>();
            }
        }

        //public Sign Template => RuleEntity?.Template;

        //public string RuleDefinition => RuleEntity?.RuleDefinition;

        private List<DayWorship> ApplyFilter(IReadOnlyList<DayWorship> dayWorships)
        {
            var result = new List<DayWorship>();

            if (Filter?.IsValid == true)
            {
                for (int i = 0; i < dayWorships.Count(); i++)
                {
                    var item = dayWorships.ElementAt(i);

                    //смотрим включенную службу - если определена - добавляем
                    //если не определена - смотрим исключенную
                    if (Filter.IncludedItem != null)
                    {
                        if (i == ((int)Filter.IncludedItem - 1))
                        {
                            AddDayWorship(item);
                        }
                    }
                    else if (Filter.ExcludedItem != null)
                    {
                        if (i != ((int)Filter.ExcludedItem - 1))
                        {
                            AddDayWorship(item);
                        }
                    }
                    else
                    {
                        AddDayWorship(item);
                    }
                }
            }

            void AddDayWorship(DayWorship dayWorship)
            {
                //если IsCelebrating определен и не совпадает - не добавляем такую службу
                if (Filter.IsCelebrating != null
                    && Filter.IsCelebrating != dayWorship.IsCelebrating)
                {
                    return;
                }
                result.Add(dayWorship);
            };

            return result;
        }

        /// <summary>
        /// Сравнение идет по свойству Priority, чем меньше целочисленное значение, тем выше приоритет
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(ModifiedRule other)
        {
            if ((Priority < other.Priority) || 
                ((Priority == other.Priority) && ((DayRule is TriodionRule) && (other.DayRule is MenologyRule))))
                return -1;
            else if ((other.Priority == Priority) && !((DayRule is MenologyRule) && (other.DayRule is TriodionRule)))
                return 0;
            else return 1;
        }
    }
}

