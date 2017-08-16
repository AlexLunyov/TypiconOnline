using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.ViewModels;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Правило для службы (Вечерня, Утреня, Литургия и т.д.)
    /// </summary>
    public class ServiceSequence : ExecContainer, ICustomInterpreted, IViewModelElement
    {
        private ItemEnumType<ServiceSequenceKind> _serviceSequenceKind;


        public ServiceSequence(XmlNode node) : base(node)
        {
            _serviceSequenceKind = new ItemEnumType<ServiceSequenceKind>(node.Name);
        }

        #region Properties

        public ItemEnumType<ServiceSequenceKind> ServiceSequenceKind
        {
            get
            {
                return _serviceSequenceKind;
            }
        }

        public ElementViewModel CreateViewModel(IRuleHandler handler)
        {
            return new ServiceSequenceViewModel(this, handler);
        }
        #endregion

        protected override void Validate()
        {
            base.Validate();

            if (!_serviceSequenceKind.IsValid)
            {
                AppendAllBrokenConstraints(_serviceSequenceKind);
            }
        }
    }
}
