using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;

namespace TypiconOnline.Domain.Typicon
{
    public abstract class TypiconRule<T> : TypiconRule where T : Day
    {
        public virtual T Day { get; set; }

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
                string result = "";

                string[] names = null;
                if (!string.IsNullOrEmpty(SelectedNames))
                {
                    string[] separator = { ",", " " };
                    names = SelectedNames.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                }

                if ((names == null) || (names.Contains("1")))
                {
                    string s = Day.Name1.Text[Owner.Settings.DefaultLanguage];
                    if (s != null) result += s + " ";
                }
                if ((names == null) || (names.Contains("2")))
                {
                    string s = Day.Name2.Text[Owner.Settings.DefaultLanguage];
                    if (s != null) result += s + " ";
                }
                if ((names == null) || (names.Contains("3")))
                {
                    string s = Day.Name3.Text[Owner.Settings.DefaultLanguage];
                    if (s != null) result += s;
                }

                return result;
            }

            set
            {
                base.Name = value;
            }
        }
    }
}
