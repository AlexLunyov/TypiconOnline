using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconMigrationTool.Core
{
    public class SQLite : SQLiteDBContext
    {
        public SQLite() : base(@"Data\SQLiteDB.db") { }
    }
    //}
    //namespace TypiconMigrationTool.Core.MSSql
    //{ 
    //public class MSSql : MSSqlDBContext
    //{
    //    public MSSql() : base($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={Directory.GetCurrentDirectory()}\Data\TypiconDB.mdf;Database=TypiconDB;Integrated Security=True;Trusted_Connection=True")
    //    {
    //        //Database.EnsureDeleted();
    //    }
    //}
}
