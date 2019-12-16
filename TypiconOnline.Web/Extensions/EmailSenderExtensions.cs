using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using TypiconOnline.Web.Services;

namespace TypiconOnline.Web.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Typicon.online: Подтвердите Ваш почтовый адрес",
                $"Пожалуйста, подтвердите ваш аккаунт нажатием на данную ссылку: <a href='{HtmlEncoder.Default.Encode(link)}'>ссылка</a>");
        }
    }
}
