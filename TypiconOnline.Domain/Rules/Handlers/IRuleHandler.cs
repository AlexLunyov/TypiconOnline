using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.Rules.Handlers
{
    /// <summary>
    /// Интерфейс для обработчиков правил
    /// </summary>
    public interface IRuleHandler
    {
        bool IsAuthorized<T>() where T : ICustomInterpreted;
        void Execute(ICustomInterpreted element);

        void Initialize(RuleHandlerRequest request);

        RuleContainer GetResult();
                
        //DateTime GetCurrentEaster(int year);
    }

    public enum HandlingMode {
        All = 0,
        ThisDay = 1,
        DayBefore = 2,
        AstronimicDay = 3
    }
}
