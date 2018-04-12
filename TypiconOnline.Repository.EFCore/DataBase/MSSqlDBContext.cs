using Microsoft.EntityFrameworkCore;

namespace TypiconOnline.Repository.EFCore.DataBase
{
    public class MSSqlDBContext : TypiconDBContext
    {
        private string _databasePath { get; set; }

        //public MSSqlDBContext() : this ("DBTypicon") { }

        public MSSqlDBContext(string connection)
        {
            _databasePath = connection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_databasePath);

            //для теста
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
