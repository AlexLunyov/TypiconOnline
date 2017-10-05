using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.ViewModels
{
    public class ContainerViewModel : ElementViewModel
    {
        public List<ElementViewModel> ChildElements { get; set; }

        public ContainerViewModel()
        {
            ChildElements = new List<ElementViewModel>();
        }

        public ContainerViewModel(ExecContainer container, IRuleHandler handler) : this()
        {
            if (container == null) throw new ArgumentNullException("Rule");
            if (handler == null) throw new ArgumentNullException("handler");

            container.ThrowExceptionIfInvalid();

            FillChildElements(container, handler);
        }

        /// <summary>
        /// Заполняет дочерними элементами свойство ChildElements
        /// </summary>
        /// <param name="container">Правило, имеющее дочерние элементы</param>
        /// <param name="handler">обработчик</param>
        protected void FillChildElements(ExecContainer container, IRuleHandler handler)
        {
            foreach (RuleElement element in container.ChildElements)
            {
                if ((element is IViewModelElement)
                    && (element is ICustomInterpreted) && handler.IsTypeAuthorized(element as ICustomInterpreted))
                {
                    ChildElements.Add((element as IViewModelElement).CreateViewModel(handler));
                }
            }
        }
    }
}
