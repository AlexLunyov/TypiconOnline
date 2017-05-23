using System.Xml;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Schedule;

namespace TypiconOnline.Domain.Rules.Factories
{
    public static class RuleFactory
    {
        public static RuleExpression CreateExpression(XmlNode node)
        {
            RuleExpression outputEl = CreateDateExpression(node);

            if (outputEl == null)
            {
                outputEl = CreateIntExpression(node);
            }

            if (outputEl == null)
            {
                switch (node.Name)
                {
                    case RuleConstants.GetDayOfWeekNodeName:
                        outputEl = new GetDayOfWeek(node);
                        break;
                }
            }
            return outputEl;
        }

        public static IntExpression CreateIntExpression(XmlNode node)
        {
            IntExpression outputEl = null;
            switch (node.Name)
            {
                case RuleConstants.DaysFromEasterNodeName:
                    outputEl = new DaysFromEaster(node);
                    break;
                case RuleConstants.IntNodeName:
                    outputEl = new Int(node);
                    break;
            }
            return outputEl;
        }

        public static DateExpression CreateDateExpression(XmlNode node)
        {
            DateExpression outputEl = null;
            switch (node.Name)
            {
                case RuleConstants.DateNodeName:
                    outputEl = new Date(node);
                    break;
                case RuleConstants.GetClosestDayNodeName:
                    outputEl = new GetClosestDay(node);
                    break;
                case RuleConstants.DateByDaysFromEasterNodeName:
                    outputEl = new DateByDaysFromEaster(node);
                    break;
                    //default:
                    //    throw new DefinitionsParsingException("Ошибка: предполагается элемент с выходным типом данных: дата");
            }
            return outputEl;
        }

        public static RuleElement CreateElement(XmlNode node)
        {
            RuleElement outputEl = CreateExecutable(node);

            if (outputEl == null)
            {
                switch (node.Name)
                {
                    case RuleConstants.NoticeNodeName:
                        outputEl = new Notice(node);
                        break;
                }
            }
            return outputEl;
        }

        public static RuleExecutable CreateExecutable(XmlNode node)
        {
            RuleExecutable outputEl = CreateRuleContainer(node);

            if (outputEl == null)
            {
                switch (node.Name)
                {
                    case RuleConstants.SwitchNodeName:
                        outputEl = new Switch(node);
                        break;
                    case RuleConstants.ModifyDayNodeName:
                        outputEl = new ModifyDay(node);
                        break;
                    case RuleConstants.ModifyReplacedDayNodeName:
                        outputEl = new ModifyReplacedDay(node);
                        break;
                }
            }
            return outputEl;
        }

        public static RuleContainer CreateRuleContainer(XmlNode node) 
        {
            RuleContainer outputEl = CreateExecContainer(node);

            if (outputEl == null)
            {
                switch (node.Name)
                {
                    case RuleConstants.ServiceNodeName:
                        outputEl = new Service(node);
                        break;
                }
            }
            return outputEl;
        }

        public static ExecContainer CreateExecContainer(XmlNode node)
        {
            ExecContainer outputEl = null;
            switch (node.Name)
            {
                case RuleConstants.ExecContainerNodeName:
                case RuleConstants.ActionNodeName:
                    outputEl = new ExecContainer(node);
                    break;
            }
            return outputEl;
        }
    }
}
