using JqueryDataTables.ServerSide.AspNetCoreWeb;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class TypiconEntityModel
    {
        public int Id { get; set; }
        [SearchableString]
        [Sortable]
        public string Name { get; set; }
        [SearchableString]
        [Sortable]
        public string SystemName { get; set; }
    }

    
}