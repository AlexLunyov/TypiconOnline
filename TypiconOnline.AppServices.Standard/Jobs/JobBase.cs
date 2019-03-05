using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.AppServices.Jobs
{
    /// <summary>
    /// Базовый абстрактный класс для заданий
    /// </summary>
    public abstract class JobBase: IJob
    {
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
        public JobStatus Status { get; set; }
        /// <summary>
        /// Сообщение о результате обработки задания
        /// </summary>
        public string StatusMessage { get; set; }
    }

    //public enum ResultCode
    //{
    //    Success = 0,
    //    Error = 1,
    //    Warning = 2
    //}
}
