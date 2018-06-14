using TypiconOnline.Domain.Rules.Schedule;

namespace TypiconOnline.Domain.Rules.Handlers.CustomParameters
{
    /// <summary>
    /// кастомная настройка для класса KekragariaRule
    /// </summary>
    public class KekragariaCustomParameter : ApplyParameterBase<KekragariaRule>
    {
        public bool? ShowPsalm { get; set; }

        public int? TotalYmnosCount { get; set; }

        protected override void InnerApply(KekragariaRule element)
        {
            if (ShowPsalm != null)
            {
                element.ShowPsalm = (bool) ShowPsalm;
            }

            if (TotalYmnosCount != null)
            {
                element.TotalYmnosCount = (int) TotalYmnosCount;
            }
        }
    }
}
