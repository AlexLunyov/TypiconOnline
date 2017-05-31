using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Folders;

namespace TypiconOnline.Repository.EF.DataBase.Mapping.Folders
{
    public class DayFolderMap : EntityTypeConfiguration<DayFolder>
    {
        public DayFolderMap()
        {
            //HasKey<int>(c => c.Id);
            //Property(c => c.Id).IsRequired();

            Property(c => c.Name).HasMaxLength(100).IsRequired();

            Ignore(c => c.PathName);

            HasMany(e => e.Folders).
                WithOptional(m => m.Parent);

            HasMany(e => e.ChildElements).
                WithOptional(m => m.Folder).WillCascadeOnDelete(true);

            Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("DayFolders");
            });

            //ToTable("DayFolder");
        }
    }
}
