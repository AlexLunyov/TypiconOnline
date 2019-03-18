using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.AppServices.Messaging
{
    public class UserInfo
    {
        public UserInfo(string login, string password)
        {
            if (string.IsNullOrEmpty(login))
            {
                throw new ArgumentException("Логин не может иметь пустого значения", nameof(login));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Пароль не может быть пустым", nameof(password));
            }

            Login = login;
            Password = password;
        }

        public string Login { get; }
        public string Password { get; }
    }
}
