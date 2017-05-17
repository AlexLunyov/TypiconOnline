using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.ItemTypes
{
    class ItemDateBusinessConstraint
    {
        public static readonly BusinessConstraint DateTypeMismatch = new BusinessConstraint("Неверный формат даты.");
    }
}
