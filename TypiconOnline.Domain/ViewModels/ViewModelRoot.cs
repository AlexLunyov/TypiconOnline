using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TypiconOnline.Domain.ViewModels
{
    /// <summary>
    /// Класс сожержит коллекцию выходных форм последовательностей богослужений
    /// </summary>
    [Serializable]
    [XmlRoot(ViewModelConstants.ViewModelRootNodeName)]
    public class ViewModelRoot 
    {
        /// <summary>
        /// Коллекция элементов расписания, которые являются контейнерами для последовательностей боослужений
        /// </summary>
        [XmlElement(ViewModelConstants.WorshipRuleNodeName)]
        public List<WorshipRuleViewModel> Worships { get; set; } = new List<WorshipRuleViewModel>();


        /// <summary>
        /// Возвращает элемент по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WorshipRuleViewModel this[string id]
        {
            get
            {
                return this.Worships.FirstOrDefault(c => c.Id == id);
            }
        }
    }
}
