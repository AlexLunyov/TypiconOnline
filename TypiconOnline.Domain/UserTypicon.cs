using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Domain
{
    /// <summary>
    /// Класс соединения Пользователей и Уставов
    /// </summary>
    public class UserTypicon
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int TypiconId { get; set; }
        public virtual Typicon.Typicon Typicon { get; set; }
    }
}