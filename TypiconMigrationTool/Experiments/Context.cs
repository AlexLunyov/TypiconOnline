using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconMigrationTool.Experiments
{
    public class Context : DbContext
    {
        public DbSet<User> UserSet { get; set; }
        public DbSet<Folder> FolderSet { get; set; }

        public Context() : base("ContextDB")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Context>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasRequired(e => e.Root).
                WithOptional(m => m.Owner);

            modelBuilder.Entity<Folder>().HasMany(e => e.Children).
                WithOptional(m => m.Parent);
        }
    }

    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Folder Root { get; set; }
    }

    public class Folder
    {
        public int Id { get; set; }

        public string Name { get; set; } 
        public User Owner { get; set; }
        public Folder Parent { get; set; }
        public List<Folder> Children { get; set; }
    }
}
