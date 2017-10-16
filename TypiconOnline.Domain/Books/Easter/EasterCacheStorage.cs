using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Caching;
using TypiconOnline.Infrastructure.Common.Configuration;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Books.Easter
{
    /// <summary>
    /// Хранилище дней Пасхи
    /// </summary>
    public class EasterCacheStorage : IEasterContext
    {
        private readonly ICacheStorage _cacheStorage;
        private readonly IConfigurationRepository _configurationRepository;

        private const string _easterStorageKey = "EasterStorageKey";

        private EasterCacheStorage()
        {
            //TODO: реализовать ссылку на интерфейсы. Пока так, напрямую
            _cacheStorage = new SystemRuntimeCacheStorage();
            _configurationRepository = new AppSettingsConfigurationRepository();
        }

        private List<EasterItem> _easterDays;

        public List<EasterItem> EasterDays { //get; set;
            get
            {
                if (_easterDays == null)
                {
                    _easterDays = _cacheStorage.Retrieve<List<EasterItem>>(_easterStorageKey);
                }
                return _easterDays;
            }
            set
            {
                _easterDays = value;
                _cacheStorage.Store(_easterStorageKey, value);
            }
        }

        public DateTime GetCurrentEaster(int year)
        {
            EasterItem easter = EasterDays.Find(c => c.Date.Year == year);
            if (easter == null)
                throw new NullReferenceException("День празднования Пасхи не определен для года " + year);

            return easter.Date;
        }

        public IEnumerable<EasterItem> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Метод сделан только для тестирования
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Возвращает ту же дату</returns>
        //public DateTime GetCurrentEaster(DateTime date)
        //{
        //    return date;
        //}

        public static EasterCacheStorage Instance
        {
            get { return Nested.instance; }
        }

        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly EasterCacheStorage instance = new EasterCacheStorage();
        }
    }
}
