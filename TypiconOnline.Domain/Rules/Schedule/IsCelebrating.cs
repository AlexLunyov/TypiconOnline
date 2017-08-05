using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Элемент, возвращающий булевское значение.
    /// Возвращает true, если в списке текстов 
    /// богослужений присутствует служба Господского или Богородиченого праздника, его предпразднства или попразднства.
    /// </summary>
    public class IsCelebrating : BooleanExpression, ICustomInterpreted
    {
        public IsCelebrating(XmlNode node) : base(node)
        {
        }

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            //if (handler.IsAuthorized<IsCelebrating>())
            //{

            //}
        }

        protected override void Validate()
        {
        }
    }
}
