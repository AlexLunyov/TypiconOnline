﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon.Modifications
{
    public class ModifiedRule : EntityBase<int>/*, IAggregateRoot*/, IComparable<ModifiedRule>, IDayRule
    {
        public virtual int ModifiedYearId { get; set; }
        public virtual ModifiedYear Parent { get; set; }
        public virtual DayRule RuleEntity { get; set; }

        public virtual DateTime Date { get; set; }
        //public DayType Day { get; set; }

        public virtual int Priority { get; set; }

        public virtual bool IsLastName { get; set; }

        public virtual string ShortName { get; set; }

        public virtual bool IsAddition { get; set; }

        public virtual bool UseFullName { get; set; }

        public virtual DayWorshipsFilter Filter { get; set; }

        /// <summary>
        /// Список служб, отфильтрованный согласно настройкам ModifiedRule
        /// </summary>
        public List<DayWorship> DayWorships
        {
            get
            {
                return (RuleEntity?.DayWorships != null) ? ApplyFilter(RuleEntity.DayWorships) : new List<DayWorship>();
            }
        }

        //public Sign Template => RuleEntity?.Template;

        //public string RuleDefinition => RuleEntity?.RuleDefinition;

        private List<DayWorship> ApplyFilter(List<DayWorship> dayWorships)
        {
            List<DayWorship> result = new List<DayWorship>();

            if (Filter?.IsValid == true)
            {
                for (int i = 0; i < dayWorships.Count; i++)
                {
                    var item = dayWorships[i];

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

        protected override void Validate()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Сравнение идет по свойству Priority, чем меньше целочисленное значение, тем выше приоритет
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(ModifiedRule other)
        {
            if ((Priority < other.Priority) || 
                ((Priority == other.Priority) && ((RuleEntity is TriodionRule) && (other.RuleEntity is MenologyRule))))
                return -1;
            else if ((other.Priority == Priority) && !((RuleEntity is MenologyRule) && (other.RuleEntity is TriodionRule)))
                return 0;
            else return 1;
        }
    }
}

