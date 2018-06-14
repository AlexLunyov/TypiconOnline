using System;
using System.Collections.Generic;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Domain.ViewModels;

namespace TypiconOnline.Domain.Rules.ViewModels
{
    public class ContainerViewModel : ElementViewModel
    {
        public ContainerViewModel(IEnumerable<RuleElement> container, IRuleHandler handler) //: this()
        {
            if (container == null) throw new ArgumentNullException("container in ContainerViewModel");
            if (handler == null) throw new ArgumentNullException("handler in ContainerViewModel");

            FillChildElements(container, handler);
        }

        /// <summary>
        /// Заполняет дочерними элементами свойство ChildElements
        /// </summary>
        /// <param name="container">Правило, имеющее дочерние элементы</param>
        /// <param name="handler">обработчик</param>
        protected virtual void FillChildElements(IEnumerable<RuleElement> container, IRuleHandler handler)
        {
            foreach (var element in container)
            {
                if ((element is IViewModelElement v)
                    && (element is ICustomInterpreted c) && handler.IsTypeAuthorized(c))
                {
                    var viewModel = v.CreateViewModel(handler);

                    if (viewModel != null)
                    {
                        AddRange(viewModel);
                    }
                }
            }
        }
    }
}
