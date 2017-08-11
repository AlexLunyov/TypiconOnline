using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Описание стихир Господи воззвах в последовательности богослужений
    /// </summary>
    public class YmnosStructureRule : ExecContainer, ICustomInterpreted
    {
        private YmnosStructure _stichera;
        

        public YmnosStructureRule(XmlNode node) : base(node)
        {
            
        }

        #region Properties

        /// <summary>
        /// Вычисленная последовательность богослужебных текстов
        /// </summary>
        public YmnosStructure CalculatedYmnosStructure
        {
            get
            {
                return _stichera;
            }
        }

        #endregion

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            if (IsValid && handler.IsAuthorized<YmnosStructureRule>())
            {
                //используем специальный обработчик для YmnosStructureRule,
                //чтобы создать список источников стихир на обработку
                YmnosStructureRuleHandler kekragariaHandler = new YmnosStructureRuleHandler();

                foreach (RuleElement elem in ChildElements)
                {
                    elem.Interpret(date, kekragariaHandler);
                }

                RuleContainer container = kekragariaHandler.GetResult();

                if (container != null)
                {
                    CalculateYmnosStructure(date, handler, container);
                }
            }
        }

        private void CalculateYmnosStructure(DateTime date, IRuleHandler handler, RuleContainer container)
        {
            _stichera = new YmnosStructure();
            foreach (YmnosRule ymnosRule in container.ChildElements)
            {
                if (ymnosRule.Source.Value == YmnosSource.Irmologion)
                {
                    //TODO: добавляем Богородичны из приложений Ирмология
                }
                else
                {
                    //разбираемся с source
                    DayService dayService = null;
                    switch (ymnosRule.Source.Value)
                    {
                        case YmnosSource.Item1:
                            dayService = (handler.Settings.DayServices.Count > 0) ? handler.Settings.DayServices[0] : null;
                            break;
                        case YmnosSource.Item2:
                            dayService = (handler.Settings.DayServices.Count > 1) ? handler.Settings.DayServices[1] : null;
                            break;
                        case YmnosSource.Item3:
                            dayService = (handler.Settings.DayServices.Count > 2) ? handler.Settings.DayServices[2] : null;
                            break;
                        case YmnosSource.Oktoikh:
                            dayService = BookStorage.Oktoikh.GetOktoikhDay(date);
                            break;
                    }

                    if (dayService == null)
                    {
                        throw new KeyNotFoundException("YmnosStructureRule source not found: " + ymnosRule.Source.Value.ToString());
                    }

                    //теперь разбираемся с place И kind
                    switch (ymnosRule.YmnosKind.Value)
                    {
                        case YmnosKind.Ymnos:
                            _stichera.Groups.AddRange(dayService.GetYmnosStructure(ymnosRule.Place.Value, ymnosRule.Count.Value, ymnosRule.StartFrom.Value).Groups);
                            break;
                        case YmnosKind.Doxastichon:
                            _stichera.Doxastichon = dayService.GetYmnosStructure(ymnosRule.Place.Value, ymnosRule.Count.Value, ymnosRule.StartFrom.Value).Doxastichon;
                            break;
                        case YmnosKind.Theotokion:
                            _stichera.Theotokion = dayService.GetYmnosStructure(ymnosRule.Place.Value, ymnosRule.Count.Value, ymnosRule.StartFrom.Value).Theotokion;
                            break;
                    }
                    
                }

                //handler.Settings.DayServices
            }
        }

        protected override void Validate()
        {
            base.Validate();
            //TODO: добавить проверку на наличие элементов stichira в дочерних элементах
        }
    }
}
