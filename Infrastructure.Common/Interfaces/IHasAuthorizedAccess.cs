using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Infrastructure.Common.Interfaces
{
    /// <summary>
    /// Интерфейс указывает, что необходимо проверять права доступа для объекта класса
    /// </summary>
    public interface IHasAuthorizedAccess//<T> where T: IAuthorizeKey
    {
        /// <summary>
        /// Ключ для доступа к ресурсам
        /// </summary>
        IAuthorizeKey Key { get; }
    }
}
