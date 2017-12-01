using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Rules.Handlers.CustomParameters
{
    public abstract class CheckParameterBase<T> : IRuleCheckParameter where T: RuleElement
    {
        public bool Check(RuleElement element)
        {
            return (element is T t) ? InnerCheck(t) : true;
        }

        protected abstract bool InnerCheck(T element);
    }
}
