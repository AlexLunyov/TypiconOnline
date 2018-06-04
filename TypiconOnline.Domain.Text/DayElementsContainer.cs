using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TypiconOnline.Domain.Rules.Days
{
    public class DayElementsContainer : RuleContainer
    {
        public DayElementsContainer(XmlNode node) : base(node)
        {

        }

        protected override void Validate()
        {
            base.Validate();

            foreach (RuleElement element in ChildElements)
            {
                if (!(element is DayElement))
                {
                    AddBrokenConstraint(DayElementsContainerBusinessConstraint.OnlyDayElementsAlowed, ElementName);
                }
            }
        }
    }
}
