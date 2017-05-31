using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Folders;

namespace TypiconOnline.Repository.EF.DataBase.Mapping.Folders
{
    public class TypiconFolderMap : EntityTypeConfiguration<TypiconFolder>
    {
        public TypiconFolderMap()
        {
            Property(c => c.Name).HasMaxLength(100).IsRequired();

            HasOptional(c => c.Owner).
                WithRequired();

            HasMany(e => e.Folders).
                WithOptional(m => m.Parent);

            HasMany(e => e.ChildElements).
                WithOptional(m => m.Folder).WillCascadeOnDelete(true);

            Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("TypiconFolders");
            });

            //ToTable("DayFolder");
        }
    }
}
