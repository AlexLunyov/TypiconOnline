using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class CreateRuleCommandBase<T> : ICommand where T : RuleEntity, new()
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