using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon.Print
{
    public class PrintWeekTemplate: ITypiconVersionChild, IHasId<int>
    {
        public int Id { get; set; }

        /// <summary>
        /// Id Устава (TypiconVersion)
        /// </summary>
        public virtual int TypiconVersionId { get; set; }

        public virtual TypiconVersion TypiconVersion { get; set; }

        /// <summary>
        /// Количество дней для расположения на одной странице шаблона
        /// </summary>
        public int DaysPerPage { get; set; }

        /// <summary>
        /// Word-документ с определенными в нем полями для отображения недели Выходной формы.
        /// Используется в качестве основного печатного документа, в который будут добавляться шаблоны дней Выходной формы
        /// </summary>
        public byte[] PrintFile { get; set; }
        /// <summary>
        /// Имя загруженного файла
        /// </summary>
        public string PrintFileName { get; set; }
    }
}
