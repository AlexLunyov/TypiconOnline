using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.AppServices.Jobs
{
    /// <summary>
    /// Базовый абстрактный класс для заданий
    /// </summary>
    public class JobStateHolder
    {
        public JobStateHolder()
        {
            CDate = DateTime.Now;
        }

        /// <summary>
        /// Дата создания задания
        /// </summary>
        public DateTime CDate { get; set; }
        /// <summary>
        /// Дата начала обработки задания
        /// </summary>
        public DateTime BDate { get; set; }
        /// <summary>
        /// Дата окончания обработки задания
        /// </summary>
        public DateTime EDate { get; set; }
        /// <summary>
        /// Статус состояния задания
        /// </summary>
        public JobStatus Status { get; set; } = JobStatus.Created;
        /// <summary>
        /// Сообщение о результате обработки задания
        /// </summary>
        public string StatusMessage { get; set; }
    }

    public enum JobStatus
    {
        Created = 0,
        Started = 1,
        Finished = 2,
        Failed = 3
    }
}
