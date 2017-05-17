using System.Data.Entity;
using SQLite.CodeFirst;


namespace TypiconOnline.Repository.EF
{
    internal class TypiconOnlineDbInitializer : SqliteDropCreateDatabaseWhenModelChanges<TypiconOnlineDbContext>
    {
        public TypiconOnlineDbInitializer(DbModelBuilder modelBuilder)
            : base(modelBuilder)
        { }


    }
}