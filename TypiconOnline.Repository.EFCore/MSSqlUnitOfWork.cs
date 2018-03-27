using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Repository.EFCore
{
    public class MSSqlUnitOfWork : UnitOfWorkBase
    {
        public MSSqlUnitOfWork() : this ("DBTypicon") { }

        public MSSqlUnitOfWork(string connection)
        {
            _dbContext = new MSSqlDBContext(connection);
        }
    }
}
