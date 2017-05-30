using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using TypiconOnline.Domain;

namespace TypiconOnline.Repository.EF.DataBase.Mapping
{
    internal class FolderEntityMap : EntityTypeConfiguration<FolderEntity>
    {
        public FolderEntityMap()
        {
            HasKey<int>(c => c.Id);
            Property(c => c.Id).IsRequired();

            Property(c => c.Name).HasMaxLength(100).IsRequired();

            Ignore(c => c.PathName);

            //HasOptional(e => e.Parent).
            //    WithMany(m => m.Folders).HasForeignKey(m => m.ParentId)/*.
            //    HasForeignKey(m => m.TemplateId)*/;

            

            HasMany(e => e.Folders).
                WithOptional(m => m.Parent)/*.WillCascadeOnDelete(true)/*.
                HasForeignKey(m => m.ParentId)*/;

            HasMany(e => e.Rules).
                WithOptional(m => m.Folder).WillCascadeOnDelete(true);

            ToTable("Folders");
        }
    }
}