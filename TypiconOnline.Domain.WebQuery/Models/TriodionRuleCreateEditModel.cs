using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Infrastructure.Common.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class TriodionRuleCreateEditModel : DayRuleModelBase<TriodionDayWorshipModel>
    {
        public int DaysFromEaster { get; set; }
        public bool IsTransparent { get; set; }
        public ModelMode Mode { get; set; }
    }

    public enum ModelMode
    {
        Create,
        Edit
    }
}
