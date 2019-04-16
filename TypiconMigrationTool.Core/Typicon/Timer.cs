using System;

namespace TypiconMigrationTool.Typicon
{
    /// <summary>
    /// Таймер
    /// </summary>
    public class Timer
    {
        private DateTime startDate;

        private TimeSpan timePeriod;

        public void Start()
        {
            startDate = DateTime.Now;
        }

        /// <summary>
        /// Возвращает пройденное время
        /// </summary>
        /// <returns></returns>
        public TimeSpan Stop()
        {
            timePeriod = DateTime.Now.Subtract(startDate);
            return timePeriod;
        }

        public string GetStringValue()
        {
            return String.Format("Времени прошло: {0} часов, {1} минут, {2} секунд.", timePeriod.Hours, timePeriod.Minutes, timePeriod.Seconds);
        }
    }
}
