using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Domain.Interfaces
{
    /// <summary>
    /// Обобщающий интерфейс для Правил дней и Знака службы
    /// </summary>
    public interface ITemplateHavingEntity
    {
        bool IsAddition { get; set; }
        Sign Template { get; set; }
        T GetRule<T>(IRuleSerializerRoot serializerRoot) where T : IRuleElement;
    }
}
