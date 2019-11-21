using System;
using System.Collections.Generic;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.AppServices.Migration.Typicon
{
    [Serializable]
    public class SignProjection: TemplateHavingRuleProjection
    {
        public SignProjection() { }
        /// <summary>
        /// Сериализуем Id, т.к. на него будут ссылаться другие Правила
        /// </summary>
        public int Id { get; set; }
        public bool IsAddition { get; set; }
        public int? Number { get; set; }
        public int Priority { get; set; }
        public ItemText Name { get; set; }
    }
}