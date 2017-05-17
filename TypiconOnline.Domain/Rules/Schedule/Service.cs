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
    public class Service : Notice
    {
        private ItemTime _time = new ItemTime();

        public Service(XmlNode node) : base(node)
        {
            if (node.Attributes.Count > 0)
            {
                XmlAttribute attr = node.Attributes[RuleConstants.ServiceTimeAttrName];
                if (attr != null)
                {
                    _time = new ItemTime(attr.Value);
                }
            }
        }

        public ItemTime Time
        {
            get
            {
                return _time;
            }
        }

        public override void Interpret(DateTime date, IRuleHandler handler)
        {
            if (IsValid && handler.IsAuthorized<Notice>())
            {
                //handler.Execute(this);

                base.Interpret(date, handler);

                _isInterpreted = true;
            }
        }

        protected override void Validate()
        {
            if (!_time.IsValid)
            {
                AddBrokenConstraint(ServiceBusinessConstraint.TimeTypeMismatch, ElementName);
            }
        }
    }
}

