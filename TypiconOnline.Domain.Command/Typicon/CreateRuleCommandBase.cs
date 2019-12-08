using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class CreateRuleCommandBase<T> : ICommand where T : class, ITypiconVersionChild, new()
    {
        public CreateRuleCommandBase(int typiconId)
        {
            TypiconId = typiconId;
        }
        /// <summary>
        /// TypiconId
        /// </summary>
        public int TypiconId { get; }
    }
}