using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Domain.Rules.Days;

namespace TypiconOnline.Domain.Tests.Days
{
    [TestFixture]
    public class EsperinosTest
    {
        [Test]
        public void EsperinosTest_Serialization()
        {
            FileReader fileReader = new FileReader(@"C:\Users\Монастырь\Documents\Visual Studio 2015\Projects\TypiconOnline\TypiconOnline.Domain.Tests\");
            string xml = fileReader.GetXml("Esperinos");

            TypiconSerializer ser = new TypiconSerializer();

            Esperinos esperinos = ser.Deserialize<Esperinos>(xml);

            Assert.IsNotNull(esperinos);
            Assert.AreEqual(esperinos.Kekragaria.Groups[0].Ihos, 1);
        }
    }
}
