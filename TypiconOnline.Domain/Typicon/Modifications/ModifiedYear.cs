using System.Collections.Generic;
using System.Linq;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon.Modifications
{
    /// <summary>
    /// Хранилище объектов ModifiedYear, объединенных по году
    /// </summary>
    public class ModifiedYear : EntityBase<int>//, IAggregateRoot
    {
        public ModifiedYear() { }

        public int TypiconVersionId { get; set; }

        public virtual TypiconVersion TypiconVersion { get; set; }

        public int Year { get; set; }

        private List<ModifiedRule> modifiedRules = new List<ModifiedRule>();

        public virtual IEnumerable<ModifiedRule> ModifiedRules { get => modifiedRules; set => modifiedRules = value.ToList(); }


        protected override void Validate() { }

        /// <summary>
        /// Добавляет измененное правило.
        /// Вызывается из метода Execute класса ModificationsRuleHandler
        /// </summary>
        /// <param name="request"></param>
        public void AddModifiedRule(ModificationsRuleRequest request)
        {
            var modifiedRule = new ModifiedRule()
            {
                //Parent = modifiedYear,
                Date = request.Date,
                DayRuleId = request.DayRuleId,
                Priority = request.Priority,
                IsLastName = request.IsLastName,
                IsAddition = request.AsAddition,
                UseFullName = request.UseFullName,
                Filter = request.Filter,
                SignNumber = request.SignNumber,
                ShortName = (request.ShortName != null) ? new ItemTextStyled(request.ShortName) : new ItemTextStyled()
            };

            modifiedRules.Add(modifiedRule);
        }
    }
}
