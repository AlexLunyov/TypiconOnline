using TypiconOnline.Domain.AuthorizeKeys;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.Interfaces;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class EditRuleCommandBase<T> : ICommand, IHasAuthorizedAccess where T : class, ITypiconVersionChild, new()
    {
        public EditRuleCommandBase(int id)
        {
            Id = id;
            Key = new TypiconByRuleCanEditKey<T>(id);
        }
        public int Id { get; }

        public IAuthorizeKey Key { get; }
    }
}