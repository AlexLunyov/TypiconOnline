﻿using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon.Output
{
    /// <summary>
    /// Выходная форма службы
    /// </summary>
    public class OutputWorship : IHasId<int>
    {
        public int Id { get; set; }
        public int OutputDayId { get; set; }
        /// <summary>
        /// Порядок в коллекции, 1-ориентированный.
        /// Необходим для сортировки в коллекции у OutputDay
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// Ссылка на День
        /// </summary>
        public virtual OutputDay OutputDay { get; set; }
        public string Time { get; set; }
        public virtual ItemTextStyled Name { get; set; }
        public virtual ItemTextStyled AdditionalName { get; set; }

        /// <summary>
        /// Дата внесения изменений в выходную форму "вручную"
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        public virtual string Definition { get; set; }
    }
}
