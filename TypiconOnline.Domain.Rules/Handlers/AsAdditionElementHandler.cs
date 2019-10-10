using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.Rules.Interfaces;

namespace TypiconOnline.Domain.Rules.Handlers
{
    public class AsAdditionElementHandler : CollectorRuleHandler<IAsAdditionElement>
    {
        public AsAdditionElementHandler(IAsAdditionElement element, Func<IAsAdditionElement, bool> predicate) : base()
        {
            Element = element ?? throw new ArgumentNullException(nameof(element));
            Condition = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        public IAsAdditionElement Element { get; }

        public Func<IAsAdditionElement, bool> Condition { get; }

        public override bool IsTypeAuthorized(ICustomInterpreted t)
        {
            if (t is IAsAdditionElement elemToMatch)
            {
                return Element.IsMatch(elemToMatch) != AsAdditionMatchingResult.Fail;
            }

            return false;
        }

        /// <summary>
        /// нельзя этот метод вызывать - будет отрабатывать неккоректно
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <returns></returns>
        //public override bool IsAuthorized<U>()
        //{
        //    throw new InvalidOperationException();
        //}

        public override bool Execute(ICustomInterpreted t)
        {
            if (t is IAsAdditionElement elemToMatch
                && Element.IsMatch(elemToMatch) == AsAdditionMatchingResult.Success
                && Condition(elemToMatch))
            {
                return base.Execute(t);
            }

            return false;
        }
    }
}
