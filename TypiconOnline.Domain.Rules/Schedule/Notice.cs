using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Interfaces;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class Notice : WorshipRule
    {
        public Notice(string name, IAsAdditionElement parent) : base(name, parent) { }

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

