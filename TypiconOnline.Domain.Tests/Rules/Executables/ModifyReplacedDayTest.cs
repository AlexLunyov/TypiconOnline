using NUnit.Framework;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Rules.Executables
{
    [TestFixture]
    public class ModifyReplacedDayTest
    {
        [Test]
        public void ModifyReplacedDay_Xml()
        {
            string xmlString = @"<modifyreplacedday daymove=""0"" kind=""menology""/>";

            var serializer = TestRuleSerializer.Create();

            var element = serializer.Container<ModifyReplacedDay>()
                .Deserialize(xmlString);

            Assert.AreEqual(KindOfReplacedDay.Menology, element.Kind);
        }
    }
}
