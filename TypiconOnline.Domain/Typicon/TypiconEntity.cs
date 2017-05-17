using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain;
using TypiconOnline.Domain.Easter;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon
{
    public class TypiconEntity : EntityBase<int>, IAggregateRoot
    {
        public TypiconEntity()
        {
            Signs = new List<Sign>();
            ModifiedYears = new List<ModifiedYear>();
            CommonRules = new List<Domain.RuleEntity>();
            MenologyRules = new List<Typicon.MenologyRule>();
            TriodionRules = new List<Typicon.TriodionRule>();
        }

        #region Properties

        public string Name { get; set; }

        /// <summary>
        /// Ссылка на Устав-шаблон.
        /// </summary>
        public virtual TypiconEntity Template { get; set; }

        /// <summary>
        /// Список знаков служб
        /// </summary>
        public virtual List<Sign> Signs { get; set; }

        private FolderEntity _rulesFolder;
        /// <summary>
        /// Структуирозованное типизированное хранилище правил для правил
        /// </summary>
        public virtual FolderEntity RulesFolder
        {
            get
            {
                //if (_rulesFolder == null)
                //{
                //    _rulesFolder = new FolderEntity() { Name = "Правила (шаблон)", Owner = this };
                //}
                return _rulesFolder;
            }
            set
            {
                _rulesFolder = value;
                RulesFolder.Owner = this;
            }
        }

        public Sign TemplateSunday
        {
            get
            {
                //TODO: реализовать покрасивей
                //должен быть добавлен признак IsTemplateSunday в Sign
                return Signs.Find(c => c.Number == 6);
            }
        }

        //private Dictionary<int, List<ModifiedRule>> _modifiedYearsDict = new Dictionary<int, List<ModifiedRule>>();

        public virtual List<ModifiedYear> ModifiedYears { get; set; }

        public virtual List<RuleEntity> CommonRules { get; set; }
        public virtual List<MenologyRule> MenologyRules { get; set; }
        public virtual List<TriodionRule> TriodionRules { get; set; }

        #endregion

        protected override void Validate()
        {
            //TODO: Добавить валидацию TypiconEntity
            // GetAll().OfType - MenologyRules 

            // GetAll().OfType - TriodionRules 
            throw new NotImplementedException();
        }

        #region ModifiedRules methods

        /// <summary>
        /// Возвращает список измененных правил для конкретной даты
        /// </summary>
        /// <param name="date">Конкретная дата</param>
        /// <returns></returns>
        public List<ModifiedRule> GetModifiedRules(DateTime date)
        {
            //определяем год
            //??? если дата до 7 января - смотрим прошлый год
            //int year = date.Year;
            int year = GetYearKey(date);

            ModifiedYear modifiedYear = ModifiedYears.FirstOrDefault(m => m.Year == year);

            if (modifiedYear == null)
            {
                //По умолчанию добавляем год, пусть он и останется пустым
                modifiedYear = new ModifiedYear() { Year = year };

                ModifiedYears.Add(modifiedYear);

                DateTime indexDate = new DateTime(year, 9, 1);

                //формируем список дней для изменения до 7 января будущего года
                DateTime endDate = new DateTime(year + 1, 9, 1);
                while (indexDate != endDate)
                {
                    //Menology

                    //находим правило для конкретного дня Минеи
                    MenologyRule menologyRule = GetMenologyRule(indexDate);

                    if (menologyRule == null)
                        throw new ArgumentNullException("MenologyRule");

                    InterpretMenologyRule(menologyRule, indexDate);

                    indexDate = indexDate.AddDays(1);
                }

                //теперь обрабатываем переходящие минейные праздники
                //у них не должны быть определены даты. так их и найдем

                MenologyRules.FindAll(c => (c.Day.Date.IsEmpty && c.Day.DateB.IsEmpty)).
                    ForEach(a =>
                    {
                        InterpretMenologyRule(a, date);

                        //не нашел другого способа, как только два раза вычислять изменяемые дни
                        InterpretMenologyRule(a, date.AddYears(1));
                    });

                //Triodion

                //найти текущую Пасху
                //Для каждого правила выполнять interpret(), где date = текущая Пасха. AddDays(Day.DaysFromEaster)
                DateTime easter = EasterStorage.Instance.GetCurrentEaster(indexDate.Year);

                TriodionRules.
                    ForEach(a =>
                    {
                        if (a.Rule != null)
                        {
                            ModificationsRuleHandler handler = new ModificationsRuleHandler(
                                new RuleHandlerRequest(a));

                            int i = a.Day.DaysFromEaster;
                            a.Rule.Interpret(easter.AddDays(i), handler);
                        }
                    });
            }

            return modifiedYear.ModifiedRules.FindAll(d => d.Date == date);
        }

        private void InterpretMenologyRule(MenologyRule menologyRule, DateTime date)
        {
            if (menologyRule != null)
            {
                ModificationsRuleHandler handler = new ModificationsRuleHandler(
                    new RuleHandlerRequest(menologyRule));
                //выполняем его
                menologyRule.Rule.Interpret(date, handler);
            }
        }

        /// <summary>
        /// Метод определяет ключ для хранения измененных дней
        /// Если дата до 1 сентября - смотрим прошлый год
        /// Год начинается с началом Индикта, как в церковном календаре
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private int GetYearKey(DateTime date)
        {
            return (date.Month >= 9) ? date.Year : date.Year - 1;
            //return (date.DayOfYear > 7) ? date.Year : date.Year - 1;
        }

        /// <summary>
        /// Добавляет измененное правило.
        /// Вызывается из метода Execute класса ModificationsRuleHandler
        /// </summary>
        /// <param name="request"></param>
        internal void AddModifiedRule(ModificationsRuleRequest request)
        {
            //определяем год
            //??? если дата до 7 января - смотрим прошлый год
            //int year = date.Year;
            int year = GetYearKey(request.Date);

            ModifiedYear modifiedYear = ModifiedYears.FirstOrDefault(m => m.Year == year);

            if (modifiedYear == null)
            {
                modifiedYear = new ModifiedYear() { Year = year };
                ModifiedYears.Add(modifiedYear);
            }

            //ModifiedRule

            if (request.Caller is MenologyRule)
            {
                modifiedYear.ModifiedRules.Add(new ModifiedMenologyRule()
                {
                    Date = request.Date,
                    RuleEntity = (MenologyRule)request.Caller,
                    Priority = request.Priority,
                    IsLastName = request.IsLastName,
                    AsAddition = request.AsAddition,
                    ShortName = request.ShortName
                });
            }

            if (request.Caller is TriodionRule)
            {
                modifiedYear.ModifiedRules.Add(new ModifiedTriodionRule()
                {
                    Date = request.Date,
                    RuleEntity = (TriodionRule)request.Caller,
                    Priority = request.Priority,
                    IsLastName = request.IsLastName,
                    ShortName = request.ShortName,
                    AsAddition = request.AsAddition
                });
            }
        }
        #endregion

        #region GetRule methods

        public MenologyRule GetMenologyRule(DateTime date)
        {
            return MenologyRules.FirstOrDefault(c => c.Day.GetCurrentDate(date.Year) == date);
        }

        public TriodionRule GetTriodionRule(int daysFromEaster)
        {
            return TriodionRules.FirstOrDefault(c => c.DaysFromEaster == daysFromEaster);
        }

        public RuleEntity GetCommonRule(Func<RuleEntity, bool> predicate)
        {
            return CommonRules.FirstOrDefault(predicate);
        }

        #endregion
    }
}
