using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Typicon
{
    public abstract class TypiconRule : RuleEntity
    {
        public virtual Sign Template { get; set; }

        public int OwnerId { get; set; }
        public virtual TypiconEntity Owner { get; set; }
        /// <summary>
        /// Признак, использовать ли определение RuleDefinition как дополнение к шаблону Template
        /// </summary>
        public virtual bool IsAddition { get; set; }
        /// <summary>
        /// Возвращает Правило: либо свое, либо шаблонное
        /// </summary>
        //public override RuleElement Rule
        //{
        //    get
        //    {
        //        if ((base.Rule == null) && string.IsNullOrEmpty(RuleDefinition))
        //        {
        //            return Template.Rule;
        //        }

        //        return base.Rule;//(string.IsNullOrEmpty(RuleDefinition)) ? Template.Rule : null;
        //    }
        //}

        public override T GetRule<T>(IRuleSerializerRoot serializerRoot)
        {
            T baseRule = base.GetRule<T>(serializerRoot);

            if ((baseRule == null) && string.IsNullOrEmpty(RuleDefinition))
            {
                return Template?.GetRule<T>(serializerRoot);
            }

            return baseRule;
        }
    }
}
