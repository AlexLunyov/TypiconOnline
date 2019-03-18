using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.ErrorHandling;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface ITypiconVersionRepository
    {
        /// <summary>
        /// Возвращает версию Устава. Фнкционал доступен только Владельцам\Редакторам Устава.
        /// Возможно, надо будет переделать.
        /// </summary>
        /// <param name="typiconVersionId"></param>
        /// <param name="login">Логин Пользователя</param>
        /// <param name="password">Пароль Пользователя</param>
        /// <returns></returns>
        Result<TypiconVersion> Get(int typiconVersionId, string login, string password);
        Result<TypiconVersion> GetPublishedVersion(int typiconId);

        //clone
    }
}
