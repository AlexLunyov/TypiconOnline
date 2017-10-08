using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.ViewModels;

namespace TypiconOnline.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс для обработчиков правил
    /// </summary>
    public interface IRuleHandler
    {
        bool IsAuthorized<T>() where T : ICustomInterpreted;
        bool IsTypeAuthorized(ICustomInterpreted t);

        void Execute(ICustomInterpreted element);

        RuleHandlerSettings Settings { get; set; }

        //void Initialize(RuleHandlerSettings settings);

        //RenderContainer GetResult();
    }


}
