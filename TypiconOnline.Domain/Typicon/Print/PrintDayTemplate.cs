using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon.Print
{
    public class PrintDayTemplate : ITypiconVersionChild, IHasId<int>
    {
        public int Id { get; set; }

        /// <summary>
        /// Id Устава (TypiconVersion)
        /// </summary>
        public virtual int TypiconVersionId { get; set; }

        public virtual TypiconVersion TypiconVersion { get; set; }

        /// <summary>
        /// Уникальный номер в пределах версии Устава (указывается в определениях Правил)
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Наименование. Используется исключительно для информативности
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Символ отображения знака службы. Используется в веб-версии расписания
        /// </summary>
        public int? Icon { get; set; }

        /// <summary>
        /// Отображать ли красным в веб-версии расписания
        /// </summary>
        public bool IsRed { get; set; }

        /// <summary>
        /// Word-документ с определенными в нем полями для отображения дня Выходной формы
        /// </summary>
        public byte[] PrintFile { get; set; }

        /// <summary>
        /// Имя загруженного файла
        /// </summary>
        public string PrintFileName { get; set; }

        /// <summary>
        /// Ссылки на Знаки служб
        /// </summary>
        public virtual List<Sign> SignLinks { get; set; } = new List<Sign>();

        /// <summary>
        /// Ссылки на Знаки служб, у которых в определении правила есть ссылка на данный Шаблон
        /// </summary>
        public virtual List<PrintTemplateModRuleLink<Sign>> SignPrintLinks { get; set; } = new List<PrintTemplateModRuleLink<Sign>>();
        /// <summary>
        /// Ссылки на Правила минеи, у которых в определении правила есть ссылка на данный Шаблон
        /// </summary>
        public virtual List<PrintTemplateModRuleLink<MenologyRule>> MenologyPrintLinks { get; set; } = new List<PrintTemplateModRuleLink<MenologyRule>>();
        /// <summary>
        /// Ссылки на Правила триоди, у которых в определении правила есть ссылка на данный Шаблон
        /// </summary>
        public virtual List<PrintTemplateModRuleLink<TriodionRule>> TriodionPrintLinks { get; set; } = new List<PrintTemplateModRuleLink<TriodionRule>>();

        public void AddLink<T>(T entity) where T: ModRuleEntity//, new()
        {
            if (entity is Sign)
            {
                SignPrintLinks.Add(new PrintTemplateModRuleLink<Sign>()
                {
                    Template = this,
                    Entity = entity as Sign,
                });
            }
            else if (entity is MenologyRule)
            {
                MenologyPrintLinks.Add(new PrintTemplateModRuleLink<MenologyRule>()
                {
                    Template = this,
                    Entity = entity as MenologyRule,
                });
            }
            else if (entity is TriodionRule)
            {
                TriodionPrintLinks.Add(new PrintTemplateModRuleLink<TriodionRule>()
                {
                    Template = this,
                    Entity = entity as TriodionRule,
                });
            }
        }

        public void ClearLinks<T>(T entity) where T : ModRuleEntity//, new()
        {
            if (entity is Sign)
            {
                SignPrintLinks.RemoveAll(c => c.Entity == entity);
            }
            else if (entity is MenologyRule)
            {
                MenologyPrintLinks.RemoveAll(c => c.Entity == entity);
            }
            else if (entity is TriodionRule)
            {
                TriodionPrintLinks.RemoveAll(c => c.Entity == entity);
            }
        }

        /// <summary>
        /// Возвращает true, если нет связанных доменных объектов
        /// </summary>
        /// <returns></returns>
        public bool AreLinksEmpty() => SignLinks.Count == 0
                                      && SignPrintLinks.Count == 0
                                      && MenologyPrintLinks.Count == 0
                                      && TriodionPrintLinks.Count == 0;

        /// <summary>
        /// Возвращает, может ли шаблон быть удален автоматически
        /// </summary>
        /// <returns></returns>
        public bool IsAutoDeletable() => (PrintFile == null || PrintFile.Length == 0)
                                        && AreLinksEmpty();
    }
}
