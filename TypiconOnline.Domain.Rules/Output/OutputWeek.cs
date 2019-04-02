using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Extensions;

namespace TypiconOnline.Domain.Rules.Output
{
    /// <summary>
    /// Выходная форма для недели
    /// </summary>
    [Serializable]
    [XmlRoot(OutputConstants.OutputWeekNode)]
    public class OutputWeek : ILocalizable<LocalizedOutputWeek>
    {
        [XmlElement(OutputConstants.OutputWeekNameNode)]
        public ItemText Name { get; set; }

        [XmlElement(OutputConstants.OutputDayNode)]
        public List<OutputDay> Days { get; set; } = new List<OutputDay>();

        public LocalizedOutputWeek Localize(string language)
        {
            return new LocalizedOutputWeek()
            {
                Name = Name.Localize(language),
                Days = Days.Localize(language)
            };
        }
    }
}
