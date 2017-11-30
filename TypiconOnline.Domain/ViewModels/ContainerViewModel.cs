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
        private ExecContainer _container;
        protected IRuleHandler _handler;

        protected List<ElementViewModel> _childElements;

        public ContainerViewModel()
        {
            _container = new ExecContainer();
        }

        public ContainerViewModel(ExecContainer container, IRuleHandler handler) //: this()
        {
            _container = container ?? throw new ArgumentNullException("Rule");
            _handler = handler ?? throw new ArgumentNullException("handler");
        }

        public virtual List<ElementViewModel> ChildElements
        {
            get
            {
                if (_childElements == null)
                {
                    _childElements = new List<ElementViewModel>();

                    FillChildElements();
                }

                return _childElements;
            }
        }

        /// <summary>
        /// Заполняет дочерними элементами свойство ChildElements
        /// </summary>
        /// <param name="container">Правило, имеющее дочерние элементы</param>
        /// <param name="handler">обработчик</param>
        protected virtual void FillChildElements()
        {
            foreach (RuleElement element in _container.ChildElements)
            {
                if ((element is IViewModelElement v)
                    && (element is ICustomInterpreted c) && _handler.IsTypeAuthorized(c))
                {
                    _childElements.Add(v.CreateViewModel(_handler));
                }
            }
        }
    }
}
