using System;
using TypiconOnline.Domain.ViewModels;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.ItemTypes;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace TypiconOnline.Domain.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [XmlRoot(ViewModelConstants.ScheduleDayNode)]
    public class ScheduleDay 
    {
        [XmlElement(ViewModelConstants.ScheduleDayNameNode)]
        public ItemTextUnit Name { get; set; }
        [XmlElement(ViewModelConstants.ScheduleDayDateNode, DataType = "date")]
        public virtual DateTime Date { get; set; }
        /// <summary>
        /// Номер знака службы
        /// </summary>
        [XmlElement(ViewModelConstants.ScheduleDaySignNumberNode)]
        public int SignNumber { get; set; }
        /// <summary>
        /// Наименование знака службы
        /// </summary>
        [XmlElement(ViewModelConstants.ScheduleDaySignNameNode)]
        public ItemTextUnit SignName { get; set; }

        /// <summary>
        /// Коллекция элементов расписания, которые являются контейнерами для последовательностей боослужений
        /// </summary>
        [XmlElement(ViewModelConstants.WorshipRuleNodeName)]
        public List<WorshipRuleViewModel> Worships { get; set; } = new List<WorshipRuleViewModel>();

        /// <summary>
        /// Возвращает элемент по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WorshipRuleViewModel this[string id] => Worships.FirstOrDefault(c => c.Id == id);

        public WorshipRuleViewModel this[int index] => Worships.ElementAtOrDefault(index);
    }
}

