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
        public ServiceSequence(string name) : base(name) { }

        public ServiceSequence(XmlNode node) : base(node)
        {
            if (Enum.TryParse(node.Name, true, out ServiceSequenceKind kind))
            {
                ServiceSequenceKind = kind;
            }
        }

        #region Properties

        public ServiceSequenceKind ServiceSequenceKind { get; set; }

        #endregion

        public ElementViewModel CreateViewModel(IRuleHandler handler)
        {
            return new ServiceSequenceViewModel(this, handler);
        }
    }
}
