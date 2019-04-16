using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon
{
    /// <summary>
    /// Ломаное правило для TypiconVersion
    /// </summary>
    public class TypiconVersionError : BusinessConstraint, IHasId<int> 
    {
        private TypiconVersionError() : base(string.Empty) { }

        public TypiconVersionError(int typiconVersionId, string principleDescription, string entityName = null, int? entityId = null) : base(principleDescription)
        {
            TypiconVersionId = typiconVersionId;
            EntityName = entityName;
            EntityId = entityId;
        }

        public TypiconVersionError(BusinessConstraint source, int typiconVersionId, string entityName = null, int? entityId = null)
            : this (typiconVersionId, source.ConstraintFullDescription, entityName, entityId)
        {
        }

        public int Id { get; set; }
        public virtual TypiconVersion TypiconVersion { get; set; }
        public int TypiconVersionId { get; set; }
        /// <summary>
        /// Наименование контроллера для переходы по ссылке
        /// </summary>
        public string EntityName { get; set; }
        /// <summary>
        /// Id сущности
        /// </summary>
        public int? EntityId { get; set; }
    }
}
