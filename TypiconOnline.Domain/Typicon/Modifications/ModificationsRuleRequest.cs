﻿using System;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon.Modifications
{
    public class ModificationsRuleRequest: ServiceRequestBase
    {
        public DayRule Caller { get; set; }
        public DateTime Date { get; set; }
        public int Priority { get; set; }
        public bool IsLastName { get; set; }
        public ItemTextStyled ShortName { get; set; }
        public bool AsAddition { get; set; }
        public bool UseFullName { get; set; }
        public int? SignNumber { get; set; }
        public DayWorshipsFilter Filter { get; set; }
    }
}
