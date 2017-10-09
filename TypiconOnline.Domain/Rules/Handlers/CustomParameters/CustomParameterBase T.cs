using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Rules.Handlers.CustomParameters
{
    public abstract class CustomParameterBase<T> : IScheduleCustomParameter where T: RuleElement
    {
        //protected Type _type;
        //public CustomParameterBase(Type type)
        //{
        //    _type = type;
        //}

        public void Apply(RuleElement element)
        {
            if (element is T)
            {
                InnerApply(element as T);
            }
        }

        protected abstract void InnerApply(T element);
    }
}
