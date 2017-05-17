using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;

namespace TypiconOnline.Domain.Typicon
{
    public class TriodionRule : TypiconRule
    {
        private TriodionDay _day;
        public virtual TriodionDay Day
        {
            get
            {
                return _day;
            }
            set
            {
                _day = value;

                if (value != null)
                {
                    _daysFromEaster = value.DaysFromEaster;
                }
            }
        }

        private int _daysFromEaster;
        public int DaysFromEaster
        {
            get
            {
                return _daysFromEaster;
            }
            
        }

        /// <summary>
        /// Назначенный знак для этого описания
        /// </summary>
        //public virtual Sign Sign { get; set; }

        //public new FolderEntity Folder { get; set; }

        //отсутвует хранение xml-формы правила

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
