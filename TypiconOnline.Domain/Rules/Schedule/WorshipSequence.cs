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
    public class WorshipSequence : ExecContainer, ICustomInterpreted, IViewModelElement
    {
        public WorshipSequence(string name) : base(name) { }

        #region Properties

        public WorshipSequenceKind Kind { get; set; }

        #endregion

        public ElementViewModel CreateViewModel(IRuleHandler handler)
        {
            return new WorshipSequenceViewModel(this, handler);
        }
    }
}
