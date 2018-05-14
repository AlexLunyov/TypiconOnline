using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class YmnosCustomRule : YmnosRuleBase, ICustomInterpreted
    {
        public YmnosCustomRule(string name) : base(name) { }

        public YmnosGroup Element { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns>YmnosStructure</returns>
        public override DayElementBase Calculate(RuleHandlerSettings settings)
        {
            if (!IsValid)
            {
                return null;
            }

            YmnosStructure result = new YmnosStructure();

            switch (Kind)
            {
                case YmnosRuleKind.YmnosRule:
                    result.Groups.Add(Element);
                    break;
                case YmnosRuleKind.DoxastichonRule:
                    result.Doxastichon = Element;
                    break;
                case YmnosRuleKind.TheotokionRule:
                    result.Theotokion.Add(Element);
                    break;
            }

            return result;
        }

        protected override void InnerInterpret(IRuleHandler handler)
        {
            if (handler.IsAuthorized<YmnosCustomRule>())
            {
                handler.Execute(this);
            }
        }

        protected override void Validate()
        {
            if (Element == null)
            {
                AddBrokenConstraint(YmnosCustomRuleBusinessConstraint.ElementRequired);
            }
        }
    }

    public class YmnosCustomRuleBusinessConstraint
    {
        public static readonly BusinessConstraint ElementRequired = new BusinessConstraint("Текст песнопения должен быть определен.");
    }
}
