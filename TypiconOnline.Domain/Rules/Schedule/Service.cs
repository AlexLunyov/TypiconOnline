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
    public class Service : RuleContainer, ICustomInterpreted
    {
        private ItemTime _time = new ItemTime();
        private string _name;
        private ItemBoolean _isDayBefore = new ItemBoolean();
        private string _additionalName;

        public Service(XmlNode node) : base(node)
        {
            if (node.Attributes.Count > 0)
            {
                XmlAttribute attr = node.Attributes[RuleConstants.ServiceTimeAttrName];
                if (attr != null)
                {
                    _time = new ItemTime(attr.Value);
                }

                attr = node.Attributes[RuleConstants.ServiceNameAttrName];
                if (attr != null)
                {
                    _name = attr.Value;
                }

                attr = node.Attributes[RuleConstants.ServiceIsDayBeforeAttrName];
                if (attr != null)
                {
                    _isDayBefore = new ItemBoolean(attr.Value);
                }

                attr = node.Attributes[RuleConstants.ServiceAdditionalNameAttrName];
                if (attr != null)
                {
                    _additionalName = attr.Value;
                }
            }
        }

        #region Properties

        public ItemTime Time
        {
            get
            {
                return _time;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
        }
        public ItemBoolean IsDayBefore
        {
            get
            {
                return _isDayBefore;
            }
        }
        public string AdditionalName
        {
            get
            {
                return _additionalName;
            }
        }

        #endregion

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            if (IsValid && handler.IsAuthorized<Service>())
            {
                handler.Execute(this);

                base.InnerInterpret(date, handler);
            }
        }

        protected override void Validate()
        {
            if (!_time.IsValid)
            {
                AddBrokenConstraint(ServiceBusinessConstraint.TimeTypeMismatch, ElementName);
            }

            if (string.IsNullOrEmpty(_name))
            {
                AddBrokenConstraint(ServiceBusinessConstraint.NameReqiured, ElementName);
            }

            if (!_isDayBefore.IsValid)
            {
                AddBrokenConstraint(ServiceBusinessConstraint.IsDayBeforeTypeMismatch, ElementName);
            }

            foreach (RuleElement element in ChildElements)
            {
                //добавляем ломаные правила к родителю
                if (!element.IsValid)
                {
                    foreach (BusinessConstraint brokenRule in element.GetBrokenConstraints())
                    {
                        AddBrokenConstraint(brokenRule, ElementName + "." + brokenRule.ConstraintPath);
                    }
                }
            }
        }
    }
}

