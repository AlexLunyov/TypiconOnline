using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IFileReader
    {
        string FolderPath { get; set; }
        string Read(string fileName);
        byte[] ReadBinary(string fileName);
        string Read(params string[] names);
        IEnumerable<(string name, string content)> ReadAllFromDirectory();
    }
}
