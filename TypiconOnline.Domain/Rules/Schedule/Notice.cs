using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class Notice : Service
    {
        public Notice(XmlNode node) : base(node) { }

        public override void Interpret(DateTime date, IRuleHandler handler)
        {
            if (IsValid && handler.IsAuthorized<Notice>())
            {
                //handler.Execute(this);

                base.Interpret(date, handler);

                _isInterpreted = true;
            }
        }
    }
}

