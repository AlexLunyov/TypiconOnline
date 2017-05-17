using System.Data.Entity.ModelConfiguration;
using TypiconOnline.Domain;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Repository.EF.DataBase.Mapping
{
    internal class TriodionFolderMap : EntityTypeConfiguration<RuleFolderEntity<TriodionDay>>
    {
        public TriodionFolderMap()
        {
            HasKey<int>(c => c.Id);
            Property(c => c.Id).IsRequired();

            Property(c => c.Name).HasMaxLength(100).IsRequired();

            Ignore(c => c.PathName);

            HasOptional(e => e.Parent).
                WithMany();

            HasMany(e => e.Folders).
                WithRequired(m => m.Parent);

            HasMany(e => e.Rules).
                WithRequired(m => m.Folder);

            ToTable("TriodionFolder");
        }
    }
}