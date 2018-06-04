using System;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Executables;

namespace TypiconOnline.Domain.Core.Typicon
{
    public class Sign : TypiconRule
    {
        public Sign() { }

        /// <summary>
        /// Предустановленный номер, согласно знакам служб Типикона. 
        /// Например, для служб без знака, 6-ричных, славословных, полиелейных, бденных и т.д.
        /// Используется в Расписании.
        /// </summary>
        public int? Number { get; set; }
        public int Priority { get; set; }

        public bool IsTemplate { get; set; }

        /// <summary>
        /// Наименование знака службы на нескольких языках
        /// </summary>
        public ItemText SignName { get; set; } = new ItemText();

        public override string GetNameByLanguage(string language)
        {
            return SignName.FirstOrDefault(language).Text;
        }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Возвращает номер предустановленного Знака службы. Если сам не имеет такового, смотрит у родителя.
        /// Если таквойо отсутствует, возвращает 0.
        /// </summary>
        /// <returns></returns>
        public int GetNumber()
        {
            int result = 0;

            if (Number != null)
            {
                result = (int)Number;
            }
            else if (Template != null)
            {
                result = Template.GetNumber();
            }

            return result;
        }

        /// <summary>
        /// Возвращает предустановленный Знак службы. Если сам не является таковым, смотрит у родителя.
        /// Если таковой отсутствует, возвращает Null.
        /// </summary>
        /// <returns></returns>
        public Sign GetPredefinedTemplate()
        {
            return (Number != null) ? this : Template?.GetPredefinedTemplate();
        }
    }
}

