using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;

namespace TypiconOnline.Domain.Typicon
{
    public class TriodionRule : TypiconRule//<TriodionDay>
    {
        public virtual TriodionDay Day { get; set; }

        /// <summary>
        /// Строка сожержит номера имен Дня, использующихся в Правиле, разделенных запятою
        /// Пример: 
        /// 1,3
        /// 1,2,3
        /// </summary>
        public virtual string SelectedNames { get; set; }

        public override string Name
        {
            get
            {
                return GetName(Day, SelectedNames);
            }
        }

        //private int _daysFromEaster;
        //public int DaysFromEaster
        //{
        //    get
        //    {
        //        return _daysFromEaster;
        //    }

        //}

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
