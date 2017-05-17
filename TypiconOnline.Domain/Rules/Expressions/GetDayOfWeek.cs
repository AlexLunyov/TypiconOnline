using System;
using System.Xml;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Rules.Expressions
{
    /// <summary>
    /// Возвращает день недели у абстрактной даты
    /// 
    /// Примеры
    /// <getdayofweek><date>--01-04</date></getdayofweek>
    /// <getdayofweek><getclosestday dayofweek="saturday" weekcount="-2"><date>--11-08</date></getclosestday></getdayofweek>
    /// </summary>
    public class GetDayOfWeek : RuleExpression
    {
        private DateExpression _childDateExp;

        public GetDayOfWeek(XmlNode node) : base(node)
        {
        }

        #region Properties

        public override Type ExpressionType
        {
            get
            {
                return typeof(ItemDayOfWeek);
            }
        }

        #endregion

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
