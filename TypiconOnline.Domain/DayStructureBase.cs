using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain
{
    /// <summary>
    /// Базовый класс для определения богослужебных текстов
    /// </summary>
    public abstract class DayStructureBase : BookElementBase<DayContainer>
    {
    }
}
