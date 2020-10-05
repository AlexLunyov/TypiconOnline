using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class DeleteRuleCommandBase<T> : ICommand where T : class, ITypiconVersionChild, new()
    {
        public DeleteRuleCommandBase(int id)
        {
            Id = id;
        }
        public int Id { get; }
    }
}