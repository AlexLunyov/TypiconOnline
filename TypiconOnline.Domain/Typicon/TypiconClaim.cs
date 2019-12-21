using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Identity;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon
{
    /// <summary>
    /// Заявка на создание Устава
    /// </summary>
    public class TypiconClaim : IHasId<int>
    {
        public int Id { get; set; }

        public virtual ItemText Name { get; set; }
        public virtual ItemText Description { get; set; }
        /// <summary>
        /// Системное имя-идентификатор, однозначно определяющее Устав, 
        /// по которому будет совершаться обращение для получения расписания
        /// </summary>
        public string SystemName { get; set; }
        /// <summary>
        /// Язык по умолчанию
        /// </summary>
        public virtual string DefaultLanguage { get; set; }
        public int? TemplateId { get; set; }
        /// <summary>
        /// Ссылка на Устав-шаблон.
        /// </summary>
        public virtual TypiconEntity Template { get; set; }
        public int OwnerId { get; set; }
        /// <summary>
        /// Владелец (создатель) Устава
        /// </summary>
        public virtual User Owner { get; set; }

        /// <summary>
        /// Результат обработки заявки
        /// </summary>
        public string ResultMesasge { get; set; }
        public TypiconClaimStatus Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public enum TypiconClaimStatus
    {
        WatingForReview,
        InProcess,
        Rejected
    }
}
