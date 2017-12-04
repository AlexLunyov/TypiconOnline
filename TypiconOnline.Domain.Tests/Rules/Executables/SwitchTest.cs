using NUnit.Framework;
using System;
using System.Linq;
using System.Xml;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Repository.EF;

namespace TypiconOnline.Domain.Tests.Rules.Executables
{
    [TestFixture]
    public class SwitchTest
    {
        [Test]
        public void Rules_Executables_Switch_NormalParsing()
        {
            string xmlString = @"<rule>
	                                <switch>
		                                <expression>
			                                <daysfromeaster><date>--04-06</date></daysfromeaster>
		                                </expression>
		                                <case>
			                                <values>
				                                <int>-19</int>
			                                </values>
			                                <action>
				                                <modifyday daymove=""-1"" />
			                                </action>
		                                </case>
		                                <case>
				                            <values>
					                            <int>-17</int>
				                            </values>
			                                <action>
				                                <modifyday daymove=""-1"" />
			                                </action>
		                                </case>
	                                </switch>
                                </rule>";

            var element = TestRuleSerializer.Deserialize<ExecContainer>(xmlString);

            element.Interpret(BypassHandler.Instance);

            Assert.Pass("Success");
        }

        [Test]
        public void Rules_Executables_Switch_DimitrySaturday()
        {
            BookStorage.Instance = BookStorageFactory.Create();

            string xmlString = @"<rule>
	                                <switch>
		                                <expression>
			                                <getclosestday dayofweek=""суббота"" weekcount=""-1""><date>--11-08</date></getclosestday>
		                                </expression>
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

            var element = TestRuleSerializer.Deserialize<ExecContainer>(xmlString);

            element.Interpret(BypassHandler.Instance);

            Assert.Pass("Success");
        }

        [Test]
        public void Rules_Executables_Switch_Condition_and_case_have_Differenttypes()
        {
            string xmlString = @"<rule>
	                                <switch>
		                                <expression>
			                                <int>-08</int>
		                                </expression>
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

            var element = TestRuleSerializer.Deserialize<ExecContainer>(xmlString);

            Assert.IsFalse(element.IsValid);

            //try
            //{
            //    ExecContainer element = new ExecContainer(xmlDoc.FirstChild);
            //}
            //catch (DefinitionsParsingException ex)
            //{
            //    Assert.Pass(ex.Message);
            //}
        }

        [Test]
        public void Rules_Executables_Switch_Condition_check()
        {
            string xmlString = @"<rule>
	                                <switch>
		                                <expression>
			                                <getdayofweek><date>--01-07</date></getdayofweek>
		                                </expression>
		                                <case>
			                                <values>
					                                <getdayofweek name=""понедельник""></getdayofweek>
			                                </values>
			                                <action>
				                                <service time=""17.30"" name=""9-й час. Вечерня. малое повечерие."" isdaybefore=""true""/>
				                                <service time=""06.00"" name=""Полунощница. Утреня.""></service>
				                                <service time=""08.00"" name=""Великие Часы. Изобразительны. ""></service>
				                                <notice name=""Божественная литургия не совершается.""/>
			                                </action>
		                                </case>
		                                <default>
			                                <service time=""17.30"" name=""9-й час. Вечерня. Малое повечерие."" isdaybefore=""true""/>
			                                <service time=""06.00"" name=""Полуношница. Утреня. Часы.""></service>
			                                <service time=""08.00"" name=""Божественная литургия.""></service>
		                                </default>
	                                </switch>
                                </rule>";

            var element = TestRuleSerializer.Deserialize<ExecContainer>(xmlString);

            Assert.IsTrue(element.IsValid);
        }
    }
}
