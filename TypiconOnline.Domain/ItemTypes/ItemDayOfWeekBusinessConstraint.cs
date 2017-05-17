using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.ItemTypes
{
    class ItemDayOfWeekBusinessConstraint
    {
        public static readonly BusinessConstraint DayOfWeekTypeMismatch = new BusinessConstraint("Неверный формат дня недели.");
    }
}
