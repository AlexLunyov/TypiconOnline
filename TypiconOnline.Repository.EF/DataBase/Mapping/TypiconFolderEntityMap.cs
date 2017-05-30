using System.Data.Entity.ModelConfiguration;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Repository.EF.DataBase.Mapping
{
    public class TypiconFolderEntityMap : EntityTypeConfiguration<TypiconFolderEntity>
    {
        public TypiconFolderEntityMap()
        {
            HasOptional(c => c.Owner).
                WithRequired();
        }
    }
}
