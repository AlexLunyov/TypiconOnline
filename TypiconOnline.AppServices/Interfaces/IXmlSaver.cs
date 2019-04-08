using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IXmlSaver
    {
        void Save(XmlDocument doc);
        XmlDocument Load();
    }
}
