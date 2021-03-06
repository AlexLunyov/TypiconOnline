﻿using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Days
{
    /// <summary>
    /// Описание текста службы празднику
    /// </summary>
    public class DayWorship : DayStructureBase
    {
        private ItemTextStyled _worshipName;
        private ItemText _worshipShortName;

        public DayWorship() { }

        /// <summary>
        /// Наименование праздника
        /// </summary>
        public virtual ItemTextStyled WorshipName //{ get; set; }// = new ItemTextStyled();
        {
            get
            {
                if (_worshipName == null)
                {
                    _worshipName = new ItemTextStyled();
                }
                return _worshipName;
            }
            set => _worshipName = value;
        }
        /// <summary>
        /// Краткое наименование праздника (для Недель - используется при формировании расписания)
        /// </summary>
        public virtual ItemText WorshipShortName //{ get; set; }// = new ItemTextStyled();
        {
            get
            {
                if (_worshipShortName == null)
                {
                    _worshipShortName = new ItemText();
                }
                return _worshipShortName;
            }
            set => _worshipShortName = value;
        }
        /// <summary>
        /// Признак, использовать ли полное имя при составлении расписания
        /// Например, имеет значение true для праздника Недели по Рождестве
        /// </summary>
        public virtual bool UseFullName { get; set; }
        /// <summary>
        /// Признак Господского или Богородичного праздника, его предпразднества или попразднества
        /// </summary>
        public virtual bool IsCelebrating { get; set; }
        /// <summary>
        /// День Минеи или Триоди, которому принадлежит данное описание текста службы
        /// </summary>
        public virtual Day Parent { get; set; }

        protected override void Validate(ITypiconSerializer serializer)
        {
            base.Validate(serializer);

            if (!WorshipName.IsValid)//(ServiceName?.IsValid == false)
            {
                //TODO: Исправить
                //AppendAllBrokenConstraints(WorshipName);
            }
        }
    }
}

