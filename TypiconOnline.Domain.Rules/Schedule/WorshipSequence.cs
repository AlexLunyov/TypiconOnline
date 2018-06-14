using System;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Interfaces;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Правило для службы (Вечерня, Утреня, Литургия и т.д.)
    /// </summary>
    public class WorshipSequence : ExecContainer, ICustomInterpreted
    {
        public WorshipSequence(IRuleSerializerRoot serializer, string name) : base(name)
        {
            Serializer = serializer ?? throw new ArgumentNullException("IRuleSerializerRoot");
        }
        #region Properties

        public WorshipSequenceKind Kind { get; set; }

        public IRuleSerializerRoot Serializer { get; }
        #endregion
    }
}
