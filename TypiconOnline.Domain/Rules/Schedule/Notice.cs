using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class Notice : WorshipRule
    {
        public Notice(string name, IRewritableElement parent) : base(name, parent) { }

        protected override void InnerInterpret(IRuleHandler handler)
        {
            if (handler.IsAuthorized<Notice>())
            {
                handler.Execute(this);

                //base.Interpret(date, handler);
            }
        }
    }
}

