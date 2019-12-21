using System;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon.Print;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon.Modifications
{
    public class ModificationsRuleRequest: ServiceRequestBase
    {
        public int DayRuleId { get; set; }
        public DateTime Date { get; set; }
        public int Priority { get; set; }
        public bool IsLastName { get; set; }
        public ItemTextStyled ShortName { get; set; }
        public bool AsAddition { get; set; }
        public bool UseFullName { get; set; }
        public int? PrintDayTemplateId { get; set; }
        public DayWorshipsFilter Filter { get; set; }
    }
}
