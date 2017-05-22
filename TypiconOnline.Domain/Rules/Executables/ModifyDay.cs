using System;
using System.Xml;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Executables
{
    //
    //  EXAMPLES
    //
    //  <modifyday servicesign="12" iscustomname="true">
    //	    <getclosestday dayofweek = "saturday" weekcount="-2"><date>--11-08</date></getclosestday>
    //  </modifyday>
    //
    //  <modifyday servicesign="6" daymove="0" priority="2" islastname="true" iscustomname="false"/>

    public class ModifyDay : RuleExecutable, ICustomInterpreted
    {
        private string _shortName;
        private ItemBoolean _isLastName;
        private ItemBoolean _asAddition;
        private ItemBoolean _useFullName;
        private ItemInt _dayMoveCount;
        private DateTime _moveDateCalculated;
        private ItemInt _priority;
        private DateExpression _childDateExp;

        public ModifyDay(XmlNode node) : base(node)
        {
            if (node.Attributes != null)
            {
                string _attrString = "false";

                XmlAttribute attr = node.Attributes[RuleConstants.ShortNameAttrName];

                if (attr != null)
                {
                    _shortName = attr.Value;
                }

                _attrString = "false";

                attr = node.Attributes[RuleConstants.IsLastNameAttrName];

                if (attr != null)
                {
                    _attrString = attr.Value;
                }

                _isLastName = new ItemBoolean(_attrString);

                _attrString = "false";

                attr = node.Attributes[RuleConstants.AsAdditionAttrName];

                if (attr != null)
                {
                    _attrString = attr.Value;
                }

                _asAddition = new ItemBoolean(_attrString);

                _attrString = "true";

                attr = node.Attributes[RuleConstants.UseFullNameAttrName];

                if (attr != null)
                {
                    _attrString = attr.Value;
                }

                _useFullName = new ItemBoolean(_attrString);

                attr = node.Attributes[RuleConstants.DayMoveAttrName];

                if (attr != null)
                {
                    _dayMoveCount = new ItemInt(attr.Value);
                }

                _attrString = "0";

                attr = node.Attributes[RuleConstants.PriorityAttrName];

                if (attr != null)
                {
                    _attrString = attr.Value;
                }

                _priority = new ItemInt(_attrString);

            }

            if (node.HasChildNodes)
            {
                _childDateExp = Factories.RuleFactory.CreateDateExpression(node.FirstChild);
            }
        }

        #region Properties

        public ItemInt DayMoveCount
        {
            get
            {
                return _dayMoveCount;
            }
        }

        public ItemInt Priority
        {
            get
            {
                return _priority;
            }
        }

        public string ShortName
        {
            get
            {
                return _shortName;
            }
        }

        public ItemBoolean IsLastName
        {
            get
            {
                return _isLastName;
            }
        }

        public ItemBoolean AsAddition
        {
            get
            {
                return _asAddition;
            }
        }

        public ItemBoolean UseFullName
        {
            get
            {
                return _useFullName;
            }
        }

        public ItemDate MoveDateExpression
        {
            get
            {
                //if (!IsInterpreted)
                //    throw new DefinitionsNotInterpretedException();

                //if (!IsInterpreted || (_childDateExp == null))
                //    return DateTime.MinValue;

                return (ItemDate)_childDateExp.ValueExpression;
            }
        }

        public DateTime MoveDateCalculated
        {
            get
            {
                return _moveDateCalculated;
            }
        }

        #endregion

        #region Methods

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            if (IsValid && handler.IsAuthorized<ModifyDay>())
            {
                if (_childDateExp != null)
                {
                    _childDateExp.Interpret(date, handler);
                    _moveDateCalculated = (DateTime)_childDateExp.ValueCalculated;
                }
                else
                {
                    _moveDateCalculated = date.AddDays(DayMoveCount.Value);
                }

                handler.Execute(this);
            }
        }

        protected override void Validate()
        {
            if (!_isLastName.IsValid)
            {
                AddBrokenConstraint(ModifyDayBusinessConstraint.IsLastNameTypeMismatch, ElementName);
            }

            if (!_asAddition.IsValid)
            {
                AddBrokenConstraint(ModifyDayBusinessConstraint.IsLastNameTypeMismatch, ElementName);
            }

            if (!_useFullName.IsValid)
            {
                AddBrokenConstraint(ModifyDayBusinessConstraint.UseFullNameTypeMismatch, ElementName);
            }


            if ((_dayMoveCount != null) && (_childDateExp != null))
            {
                AddBrokenConstraint(ModifyDayBusinessConstraint.DateDoubleDefinition, ElementName);
            }
            else if((_dayMoveCount == null) && (_childDateExp == null))
            {
                AddBrokenConstraint(ModifyDayBusinessConstraint.DateAbsense, ElementName);
            }
            else if (((_dayMoveCount == null) || !_dayMoveCount.IsValid) &&
                    (_childDateExp == null))
            {
                AddBrokenConstraint(ModifyDayBusinessConstraint.DayMoveTypeMismatch, ElementName);
            }

            //добавляем ломаные правила к родителю
            if ((_childDateExp != null) && !_childDateExp.IsValid)
            {
                foreach (BusinessConstraint brokenRule in _childDateExp.GetBrokenConstraints())
                {
                    AddBrokenConstraint(brokenRule, ElementName + "." + RuleConstants.DateNodeName + "." + brokenRule.ConstraintPath);
                }
            }
        }

        #endregion
    }
}

