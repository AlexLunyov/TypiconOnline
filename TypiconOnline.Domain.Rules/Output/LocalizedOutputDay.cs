using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Rules.Output
{
    [Serializable]
    [XmlRoot(OutputConstants.LocalizedOutputDayNode)]
    public class LocalizedOutputDay
    {
        [XmlElement(OutputConstants.LocalizedOutputDayNameNode)]
        public ItemTextUnit Name { get; set; }
        [XmlElement(OutputConstants.LocalizedOutputDayDateNode, DataType = "date")]
        public virtual DateTime Date { get; set; }
        /// <summary>
        /// Номер знака службы
        /// </summary>
        [XmlElement(OutputConstants.LocalizedOutputDaySignNumberNode)]
        public int SignNumber { get; set; }
        /// <summary>
        /// Наименование знака службы
        /// </summary>
        [XmlElement(OutputConstants.LocalizedOutputDaySignNameNode)]
        public ItemTextUnit SignName { get; set; }

        /// <summary>
        /// Коллекция элементов расписания, которые являются контейнерами для последовательностей боослужений
        /// </summary>
        [XmlElement(OutputConstants.LocalizedOutputWorshipNodeName)]
        public List<LocalizedOutputWorship> Worships { get; set; } = new List<LocalizedOutputWorship>();

        /// <summary>
        /// Возвращает элемент по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LocalizedOutputWorship this[string id] => Worships.FirstOrDefault(c => c.Id == id);

        public LocalizedOutputWorship this[int index] => Worships.ElementAtOrDefault(index);
    }
}
