using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;

namespace TypiconOnline.AppServices.Migration
{
    public abstract class MigrationFileReaderBase
    {
        protected readonly IFileReader _fileReader;

        public MigrationFileReaderBase(IFileReader fileReader)
        {
            if (fileReader == null) throw new ArgumentNullException("fileReader");

            _fileReader = fileReader;
        }
    }
}
