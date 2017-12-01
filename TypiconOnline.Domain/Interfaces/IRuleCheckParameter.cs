using TypiconOnline.Domain.Rules;

namespace TypiconOnline.Domain.Interfaces
{
    /// <summary>
    /// Gараметр для элементов Правил, имеющий метод проверки его свойств. Если результат отрицательный, элемент не интерпретируется.
    /// </summary>
    public interface IRuleCheckParameter : IRuleCustomParameter
    {
        bool Check(RuleElement element);
    }
}