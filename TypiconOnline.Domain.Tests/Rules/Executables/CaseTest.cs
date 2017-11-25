using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Rules.Executables;

namespace TypiconOnline.Domain.Tests.Rules.Executables
{
    [TestFixture]
    public class CaseTest
    {
        [Test]
        public void Rules_Executables_Case_Creature()
        {
            string xmlString = @"<case>
			                        <values>
				                        <int>-29</int>
				                        <int>-22</int>
			                        </values>
			                        <action>
				                        <modifyday servicesign=""6"" daymove=""0"" iscustomname=""true""/>
                                    </action>
                                </case> ";

            var element = TestRuleSerializer.Deserialize<Case>(xmlString);

            Assert.Pass("Success");
        }

        [Test]
        public void Rules_Executables_Case_DifferentTypes()
        {
            string xmlString = @"<case>
			                        <values>
				                        <date>--02-15</date>
				                        <int>-22</int>
			                        </values>
			                        <action>
				                        <modifyday servicesign=""6"" daymove=""0"" iscustomname=""true""/>
                                    </action>
                                </case> ";

            var element = TestRuleSerializer.Deserialize<Case>(xmlString);

            Assert.IsFalse(element.IsValid);

            //try
            //{
            //    Case element = new Case(xmlDoc.FirstChild);
            //}
            //catch (DefinitionsParsingException ex)
            //{
            //    Assert.Pass(ex.Message);
            //}
        }
    }
}
