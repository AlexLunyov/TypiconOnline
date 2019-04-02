using System;
using TypiconOnline.Domain.ItemTypes;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Extensions;

namespace TypiconOnline.Domain.Rules.Output
{
    /// <summary>
    /// Выходная форма Дня с коллекцией Последовательностей богослужений.
    /// Используется для хранения в БД
    /// </summary>
    [Serializable]
    [XmlRoot(OutputConstants.OutputDayNode)]
    public class OutputDay : ILocalizable<LocalizedOutputDay>
    {
        [XmlElement(OutputConstants.OutputDayNameNode)]
        public ItemText Name { get; set; }
        [XmlElement(OutputConstants.OutputDayDateNode, DataType = "date")]
        public virtual DateTime Date { get; set; }
        /// <summary>
        /// Номер знака службы
        /// </summary>
        [XmlElement(OutputConstants.OutputDaySignNumberNode)]
        public int SignNumber { get; set; }
        /// <summary>
        /// Наименование знака службы
        /// </summary>
        [XmlElement(OutputConstants.OutputDaySignNameNode)]
        public ItemText SignName { get; set; }

        /// <summary>
        /// Коллекция элементов расписания, которые являются контейнерами для последовательностей боослужений
        /// </summary>
        [XmlElement(OutputConstants.OutputWorshipNodeName)]
        public List<OutputWorship> Worships { get; set; } = new List<OutputWorship>();

        /// <summary>
        /// Возвращает элемент по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OutputWorship this[string id] => Worships.FirstOrDefault(c => c.Id == id);

        public OutputWorship this[int index] => Worships.ElementAtOrDefault(index);

        public LocalizedOutputDay Localize(string language)
        {
            return new LocalizedOutputDay()
            {
                Name = Name.Localize(language),
                Date = Date,
                SignName = SignName.Localize(language),
                SignNumber = SignNumber,
                Worships = Worships.Localize(language)
            };
        }
    }
}

