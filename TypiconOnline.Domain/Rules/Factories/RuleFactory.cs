using System.Xml;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Interfaces;
using System.IO;

namespace TypiconOnline.Domain.Rules.Factories
{
    public static class RuleFactory//: IRuleFactory
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
                case RuleConstants.IsCelebratingNodeName:
                    outputEl = new IsCelebrating(node);
                    break;
                case RuleConstants.IsTwoSaintsNodeName:
                    outputEl = new IsTwoSaints(node);
                    break;
                case RuleConstants.IsExistsNodeName:
                    outputEl = new IsExists(node);
                    break;

            }
            return outputEl;
        }

        /// <summary>
        /// Фабричный generic метод создает элементы из xml строки. Игнорирует пробелы и комментарии
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public static T CreateElement<T>(string description) where T : RuleElement
        {
            return CreateElement(description) as T;
        }

        /// <summary>
        /// Фабричный метод создает элементы из xml строки. Игнорирует пробелы и комментарии
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public static RuleElement CreateElement(string description)
        {
            RuleElement outputEl = null;

            if (string.IsNullOrEmpty(description))
            {
                return null;
            }

            var settings = new XmlReaderSettings() { IgnoreComments = true, IgnoreWhitespace = true };

            XmlReader reader = XmlReader.Create(new StringReader(description), settings);

            //try
            //{
            XmlDocument doc = new XmlDocument();

            doc.Load(reader);
            
            //doc.LoadXml(description);

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

            //if (outputEl == null)
            //{
            //    outputEl = CreateDayElement(node);
            //}

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
            RuleExecutable outputEl = CreateExecContainer(node);

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
                    case RuleConstants.TextHolderTextNode:
                        outputEl = new TextHolder(node);
                        break;
                    //YmnosRule
                    case RuleConstants.YmnosRuleNode:
                    case RuleConstants.YmnosRuleDoxastichonNode:
                        outputEl = new YmnosRule(node);
                        break;
                    //TheotokionRule
                    case RuleConstants.YmnosRuleTheotokionNode:
                        outputEl = new TheotokionRule(node);
                        break;
                    //Ektenis
                    case RuleConstants.EktenisNode:
                        outputEl = new Ektenis(node);
                        break;
                    //CommonRuleElement
                    case RuleConstants.CommonRuleNode:
                        outputEl = new CommonRuleElement(node);
                        break;
                }
            }
            return outputEl;
        }

        public static YmnosRule CreateYmnosRule(XmlNode node)
        {
            return (node.Name == RuleConstants.YmnosRuleNode) ? new YmnosRule(node) : null;
        }

        //public static RuleContainer CreateRuleContainer(XmlNode node) 
        //{
        //    RuleContainer outputEl = CreateExecContainer(node);

        //    if (outputEl == null)
        //    {
        //        switch (node.Name)
        //        {
        //            case RuleConstants.ServiceNodeName:
        //                outputEl = new Service(node);
        //                break;
        //        }
        //    }
        //    return outputEl;
        //}

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
                case RuleConstants.KekragariaRuleNode:
                    outputEl = new KekragariaRule(node);
                    break;
                case RuleConstants.AinoiNode:
                    outputEl = new AinoiRule(node);
                    break;
                case RuleConstants.ApostichaNode:
                case RuleConstants.LitiNode:
                    outputEl = new ApostichaRule(node);
                    break;
                case RuleConstants.TroparionNode:
                    outputEl = new TroparionRule(node);
                    break;
                case RuleConstants.ServiceNodeName:
                    outputEl = new Service(node);
                    break;                
            }
            return outputEl;
        }

        

    }
}
