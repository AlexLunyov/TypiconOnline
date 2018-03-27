using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Repository.EFCore
{
    public class SQLiteUnitOfWork : UnitOfWorkBase
    {
        public SQLiteUnitOfWork() : this (@"Data\SQLiteDB.db") { }

        public SQLiteUnitOfWork(string connection)
        {
            _dbContext = new SQLiteDBContext(connection);
        }
    }
}
