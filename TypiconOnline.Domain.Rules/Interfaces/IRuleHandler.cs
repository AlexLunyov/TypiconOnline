using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Interfaces
{
    /// <summary>
    /// Интерфейс для обработчиков правил
    /// </summary>
    public interface IRuleHandler
    {
        //bool IsAuthorized<T>() where T : ICustomInterpreted;
        bool IsTypeAuthorized(ICustomInterpreted t);

        bool Execute(ICustomInterpreted element);
        void ClearResult();

        RuleHandlerSettings Settings { get; set; }

        //void Initialize(RuleHandlerSettings settings);

        //RenderContainer GetResult();
    }


}
