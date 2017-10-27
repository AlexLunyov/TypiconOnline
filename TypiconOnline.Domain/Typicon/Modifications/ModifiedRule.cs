﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon.Modifications
{
    public class ModifiedRule : EntityBase<int>/*, IAggregateRoot*/, IComparable<ModifiedRule>
    {
        public virtual DayRule RuleEntity { get; set; }

        public virtual DateTime Date { get; set; }
        //public DayType Day { get; set; }

        public virtual int Priority { get; set; }

        public virtual bool IsLastName { get; set; }

        public virtual string ShortName { get; set; }

        public virtual bool IsAddition { get; set; }

        public virtual bool UseFullName { get; set; }

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

