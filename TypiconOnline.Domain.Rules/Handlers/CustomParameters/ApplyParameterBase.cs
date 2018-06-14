using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Rules.Handlers.CustomParameters
{
    public abstract class ApplyParameterBase<T> : IRuleApplyParameter where T: IRuleElement
    {
        //protected Type _type;
        //public CustomParameterBase(Type type)
        //{
        //    _type = type;
        //}

        public void Apply(IRuleElement element)
        {
            if (element is T t)
            {
                InnerApply(t);
            }
        }

        protected abstract void InnerApply(T element);
    }
}
