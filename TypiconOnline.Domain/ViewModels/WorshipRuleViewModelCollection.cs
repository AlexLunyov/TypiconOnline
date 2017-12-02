using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.ViewModels
{
    /// <summary>
    /// Коллекция элементов расписания, которые являются контейнерами для последовательностей боослужений
    /// </summary>
    public class WorshipRuleViewModelCollection : List<WorshipRuleViewModel>
    {
        /// <summary>
        /// Возвращает элемент по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WorshipRuleViewModel this[string id]
        {
            get
            {
                return this.FirstOrDefault(c => c.Id == id);
            }
        }
    }
}
