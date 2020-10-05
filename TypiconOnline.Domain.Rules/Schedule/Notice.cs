using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class Notice : WorshipRule
    {
        public Notice(string name, IAsAdditionElement parent, IQueryProcessor queryProcessor) : base(name, parent, queryProcessor) { }

        protected override void InnerInterpret(IRuleHandler handler)
        {
            if (handler.IsTypeAuthorized(this))
            {
                handler.Execute(this);

                //base.Interpret(date, handler);
            }
        }
    }
}

