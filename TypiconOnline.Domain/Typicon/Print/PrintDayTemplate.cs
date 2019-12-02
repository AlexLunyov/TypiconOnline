using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon.Print
{
    public class PrintDayTemplate : IHasId<int>
    {
        public int Id { get; set; }

        /// <summary>
        /// Id Устава (TypiconVersion)
        /// </summary>
        public virtual int TypiconVersionId { get; set; }

        public virtual TypiconVersion TypiconVersion { get; set; }

        /// <summary>
        /// Уникальный номер в пределах версии Устава (указывается в определениях Правил)
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Наименование. Используется исключительно для информативности
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Символ отображения знака службы. Используется в веб-версии расписания
        /// </summary>
        public char Sign { get; set; }
        /// <summary>
        /// Word-документ с определенными в нем полями для отображения дня Выходной формы
        /// </summary>
        public byte[] PrintForm { get; set; }
    }
}
