using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Rules.Handlers.CustomParameters
{
    public abstract class CheckParameterBase<T> : IRuleCheckParameter where T: IRuleElement
    {
        public bool Check(IRuleElement element)
        {
            return (element is T t) ? InnerCheck(t) : true;
        }

        protected abstract bool InnerCheck(T element);
    }
}
