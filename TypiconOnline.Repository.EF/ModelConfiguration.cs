using System.Data.Entity;
using TypiconOnline.Domain.Days;

namespace TypiconOnline.Repository.EF
{
    public class ModelConfiguration
    {
        public static void Configure(DbModelBuilder modelBuilder)
        {
            ConfigureMenologyDayEntity(modelBuilder);
            
        }

        private static void ConfigureMenologyDayEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenologyDay>().ToTable("MenologyDay");
            
        }

        
    }
}
