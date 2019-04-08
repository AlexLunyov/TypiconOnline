using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers.CustomParameters;

namespace TypiconOnline.AppServices.Implementations
{
    public static class RuleCheckParameterExtensions
    {
        public static List<IRuleCheckParameter> SetModeParam(this List<IRuleCheckParameter> parameters, HandlingMode mode)
        {
            WorshipRuleCheckModeParameter param = null;
            if (parameters == null)
            {
                parameters = new List<IRuleCheckParameter>();
            }
            else
            {
                param = parameters.FirstOrDefault(c => c is WorshipRuleCheckModeParameter) as WorshipRuleCheckModeParameter;
            }

            if (param == null)
            {
                param = new WorshipRuleCheckModeParameter();
                parameters.Add(param);
            }

            param.Mode = mode;

            return parameters;
        }

        public static HandlingMode GetMode(this List<IRuleCheckParameter> parameters)
        {
            WorshipRuleCheckModeParameter param = parameters?.FirstOrDefault(c => c is WorshipRuleCheckModeParameter) as WorshipRuleCheckModeParameter;

            return (param != null) ? param.Mode : HandlingMode.All;
        }
    }
}
