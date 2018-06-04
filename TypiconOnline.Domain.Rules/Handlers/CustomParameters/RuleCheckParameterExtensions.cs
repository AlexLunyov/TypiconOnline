using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;

namespace TypiconOnline.Domain.Rules.Handlers.CustomParameters
{
    public static class RuleCheckParameterExtensions
    {
        /// <summary>
        /// Возвращает копию коллеции параметров с указанным HandlingMode
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static CustomParamsCollection<IRuleCheckParameter> SetModeParam(this CustomParamsCollection<IRuleCheckParameter> parameters, HandlingMode mode)
        {
            //если существует, копируем элементы, иначе - создаем коллекцию
            var list = (parameters != null) ? new CustomParamsCollection<IRuleCheckParameter>(parameters) : new CustomParamsCollection<IRuleCheckParameter>();

            //находим необходимый элемент

            if (parameters.FirstOrDefault(c => c is WorshipRuleCheckModeParameter) is WorshipRuleCheckModeParameter param)
            {
                list.Remove(param);
            }

            param = new WorshipRuleCheckModeParameter() { Mode = mode};
            list.Add(param);

            return list;
        }

        public static HandlingMode GetMode(this CustomParamsCollection<IRuleCheckParameter> parameters)
        {
            WorshipRuleCheckModeParameter param = parameters?.FirstOrDefault(c => c is WorshipRuleCheckModeParameter) as WorshipRuleCheckModeParameter;

            return (param != null) ? param.Mode : HandlingMode.All;
        }
    }
}
