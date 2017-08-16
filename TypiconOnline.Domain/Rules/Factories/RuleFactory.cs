using System.Xml;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Rules.Days;

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
                outputEl = CreateBooleanExpression(node);
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

        public static BooleanExpression CreateBooleanExpression(XmlNode node)
        {
            BooleanExpression outputEl = null;
            switch (node.Name)
            {
                case RuleConstants.EqualsNodeName:
                    outputEl = new Equals(node);
                    break;
                case RuleConstants.MoreNodeName:
                    outputEl = new More(node);
                    break;
                case RuleConstants.MoreEqualsNodeName:
                    outputEl = new MoreEquals(node);
                    break;
                case RuleConstants.LessNodeName:
                    outputEl = new Less(node);
                    break;
                case RuleConstants.LessEqualsNodeName:
                    outputEl = new LessEquals(node);
                    break;
                case RuleConstants.AndNodeName:
                    outputEl = new And(node);
                    break;
                case RuleConstants.OrNodeName:
                    outputEl = new Or(node);
                    break;

            }
            return outputEl;
        }

        public static RuleElement CreateElement(string description)
        {
            RuleElement outputEl = null;

            if (string.IsNullOrEmpty(description))
            {
                return null;
            }

            //try
            //{
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(description);

            if ((doc != null) && (doc.DocumentElement != null))
            {
                XmlNode node = doc.DocumentElement;

                outputEl = CreateElement(node);
            }
            //}
            //catch (Exception ex)
            //{

            //}

            return outputEl;
        }

        public static RuleElement CreateElement(XmlNode node)
        {
            RuleElement outputEl = CreateExecutable(node);

            if (outputEl == null)
            {
                outputEl = CreateDayElement(node);
            }

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
                    case RuleConstants.IfNodeName:
                        outputEl = new If(node);
                        break;
                    //TextHolder
                    case RuleConstants.TextHolderLectorNode:
                    case RuleConstants.TextHolderChoirNode:
                    case RuleConstants.TextHolderDeaconNode:
                    case RuleConstants.TextHolderPriestNode:
                        outputEl = new TextHolder(node);
                        break;
                    //YmnosRule
                    case RuleConstants.YmnosRuleNode:
                    case RuleConstants.YmnosStructureTheotokionNode:
                    case RuleConstants.YmnosStructureDoxastichonNode:
                        outputEl = new YmnosRule(node);
                        break;
                }
            }
            return outputEl;
        }

        public static YmnosRule CreateYmnosRule(XmlNode node)
        {
            return (node.Name == RuleConstants.YmnosRuleNode) ? new YmnosRule(node) : null;
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
                case RuleConstants.MikrosEsperinosNode:
                case RuleConstants.EsperinosNode:
                case RuleConstants.OrthrosNode:
                case RuleConstants.LeitourgiaNode:
                    outputEl = new ServiceSequence(node);
                    break;
                case RuleConstants.KekragariaNode:
                case RuleConstants.ApostichaNode:
                case RuleConstants.LitiNode:
                case RuleConstants.AinoiNode:
                    outputEl = new YmnosStructureRule(node);
                    break;
            }
            return outputEl;
        }

        public static DayElement CreateDayElement(XmlNode node)
        {
            DayElement outputEl = null;
            switch (node.Name)
            {
                case RuleConstants.DayElementNode:
                    outputEl = new DayElement(node);
                    break;
            }
            return outputEl;
        }

    }
}
