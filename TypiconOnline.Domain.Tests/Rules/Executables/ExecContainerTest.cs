using NUnit.Framework;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Rules.Executables
{
    [TestFixture]
    public class ExecContainerTest
    {
        [Test]
        public void Rules_Executables_ExecContainer_Add()
        {
            string xmlString = @"<rule>
	                                <switch>
		                                <condition>
			                                <int>-08</int>
		                                </condition>
		                                <case>
			                                <values>
				                                 <date>--11-04</date>
			                                </values>
			                                <action>
				                                <modifyday servicesign=""12"" iscustomname=""true"">
					                                <getclosestday dayofweek=""суббота"" weekcount=""-2""><date>--11-08</date></getclosestday>
				                                </modifyday>
			                                </action>
		                                </case>
		                                <default>
			                                <modifyday servicesign=""12"" iscustomname=""true"">
					                                <getclosestday dayofweek=""суббота"" weekcount=""-1""><date>--11-08</date></getclosestday>
				                                </modifyday>
		                                </default>
	                                </switch>
                                </rule>";

            ExecContainer container = TestRuleSerializer.Deserialize<ExecContainer>(xmlString);

            xmlString = @"<case>
			                <values>
				                    <date>--11-04</date>
			                </values>
			                <action>
				                <modifyday servicesign=""12"" iscustomname=""true"">
					                <getclosestday dayofweek=""суббота"" weekcount=""-2""><date>--11-08</date></getclosestday>
				                </modifyday>
			                </action>
		                </case>";

            ModifyDay dayModification = TestRuleSerializer.Deserialize<ModifyDay>(xmlString);

            container.ChildElements.Add(dayModification);

            Assert.AreEqual(container.ChildElements.Count, 2);
        }

        
    }
}
