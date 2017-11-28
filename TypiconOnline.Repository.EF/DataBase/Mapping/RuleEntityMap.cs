using System.Data.Entity.ModelConfiguration;
using TypiconOnline.Domain;

namespace TypiconOnline.Repository.EF.DataBase.Mapping
{
    internal class RuleEntityMap : EntityTypeConfiguration<RuleEntity>
    {
        public RuleEntityMap()
        {
            HasKey<int>(c => c.Id);
            Property(c => c.Id).IsRequired();

            //HasOptional(e => e.Folder).
            //    WithMany();

            //Property(c => c.Name).HasMaxLength(200);//.IsRequired();

            Property(c => c.RuleDefinition).HasColumnType("xml");//("NVARCHAR");

            //ToTable("RuleEntity");
        }
    }
}