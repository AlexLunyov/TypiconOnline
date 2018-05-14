using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Domain.Typicon.Psalter;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon
{
    public class TypiconEntity : EntityBase<int>, IAggregateRoot
    {        
        public TypiconEntity()
        {
            Signs = new List<Sign>();
            ModifiedYears = new List<ModifiedYear>();
            CommonRules = new List<CommonRule>();
            MenologyRules = new List<MenologyRule>();
            TriodionRules = new List<TriodionRule>();
            Kathismas = new List<Kathisma>();
        }

        #region Properties

        public string Name { get; set; }

        /// <summary>
        /// Ссылка на Устав-шаблон.
        /// </summary>
        public virtual TypiconEntity Template { get; set; }

        /// <summary>
        /// Возвращает true, если у Устава нет шаблона
        /// </summary>
        public bool IsTemplate
        {
            get
            {
                return Template == null;
            }
        }

        /// <summary>
        /// Список знаков служб
        /// </summary>
        public virtual List<Sign> Signs { get; set; }

        //private TypiconFolderEntity _rulesFolder;
        /// <summary>
        /// Структуирозованное типизированное хранилище правил для правил
        /// </summary>
        //public virtual TypiconFolderEntity RulesFolder
        //{
        //    get
        //    {
        //        //if (_rulesFolder == null)
        //        //{
        //        //    _rulesFolder = new FolderEntity() { Name = "Правила (шаблон)", Owner = this };
        //        //}
        //        return _rulesFolder;
        //    }
        //    set
        //    {
        //        _rulesFolder = value;
        //        RulesFolder.Owner = this;
        //    }
        //}

        //private Sign _templateSunday;

        //public Sign TemplateSunday
        //{
        //    get
        //    {
        //        //TODO: реализовать покрасивей
        //        //должен быть добавлен признак IsTemplateSunday в Sign
        //        if (_templateSunday == null)
        //        {
        //            _templateSunday = Signs.Find(c => c.Number == 6);
        //        }
        //        return _templateSunday;
        //    }
        //}

        //private Dictionary<int, List<ModifiedRule>> _modifiedYearsDict = new Dictionary<int, List<ModifiedRule>>();

        public virtual List<ModifiedYear> ModifiedYears { get; set; }

        public virtual List<CommonRule> CommonRules { get; set; }
        public virtual List<MenologyRule> MenologyRules { get; set; }
        public virtual List<TriodionRule> TriodionRules { get; set; }

        public virtual List<Kathisma> Kathismas { get; set; }

        private TypiconSettings _settings;
        public virtual TypiconSettings Settings
        {
            get
            {
                //if (_settings == null)
                //    _settings = new TypiconSettings() { TypiconEntity = this };
                return _settings;
            }
            set
            {
                _settings = value;
                
                if (_settings != null)
                {
                    _settings.TypiconEntity = this;
                }
            }
        }

        #endregion

        protected override void Validate()
        {
            //TODO: Добавить валидацию TypiconEntity
            // GetAll().OfType - MenologyRules 

            // GetAll().OfType - TriodionRules 
            throw new NotImplementedException();
        }

        #region GetRule methods

        public MenologyRule GetMenologyRule(DateTime date)
        {
            return MenologyRules.FirstOrDefault(c => c.GetCurrentDate(date.Year).Date == date.Date);
        }

        public TriodionRule GetTriodionRule(int daysFromEaster)
        {
            return TriodionRules.FirstOrDefault(c => c.DaysFromEaster == daysFromEaster);
        }

        public CommonRule GetCommonRule(Func<CommonRule, bool> predicate)
        {
            return CommonRules.FirstOrDefault(predicate);
        }

        #endregion
    }
}
