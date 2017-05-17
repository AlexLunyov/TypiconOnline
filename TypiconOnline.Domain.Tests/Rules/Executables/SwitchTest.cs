using NUnit.Framework;
using System;
using System.Xml;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Typicon;

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
		                                <condition>
			                                <daysfromeaster><date>--04-06</date></daysfromeaster>
		                                </condition>
		                                <case>
			                                <values>
				                                <int>-19</int>
			                                </values>
			                                <action>
				                                <daymodification servicesign=""7"" daymove=""-1"" iscustomname=""true""/>
			                                </action>
		                                </case>
		                                <case>
				                            <values>
					                            <int>-17</int>
				                            </values>
			                                <action>
				                                <daymodification servicesign=""8"" daymove=""-1"" iscustomname=""true""/>
			                                </action>
		                                </case>
	                                </switch>
                                </rule>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            ExecContainer element = new ExecContainer(xmlDoc.FirstChild);

            element.Interpret(DateTime.Today, BypassHandler.Instance);

            Assert.Pass("Success");
        }

        [Test]
        public void Rules_Executables_Switch_DimitrySaturday()
        {
            string xmlString = @"<rule>
	                                <switch>
		                                <condition>
			                                <getclosestday dayofweek=""суббота"" weekcount=""-1""><date>--11-08</date></getclosestday>
		                                </condition>
		                                <case>
			                                <values>
				                                 <date>--11-04</date>
			                                </values>
			                                <action>
				                                <daymodification servicesign=""12"" iscustomname=""true"">
					                                <getclosestday dayofweek=""суббота"" weekcount=""-2""><date>--11-08</date></getclosestday>
				                                </daymodification>
			                                </action>
		                                </case>
		                                <default>
			                                <daymodification servicesign=""12"" iscustomname=""true"">
					                                <getclosestday dayofweek=""суббота"" weekcount=""-1""><date>--11-08</date></getclosestday>
				                                </daymodification>
		                                </default>
	                                </switch>
                                </rule>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            ExecContainer element = new ExecContainer(xmlDoc.FirstChild);

            element.Interpret(DateTime.Today, BypassHandler.Instance);

            Assert.Pass("Success");
        }

        [Test]
        public void Rules_Executables_Switch_Condition_and_case_have_Differenttypes()
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
				                                <daymodification servicesign=""12"" iscustomname=""true"">
					                                <getclosestday dayofweek=""суббота"" weekcount=""-2""><date>--11-08</date></getclosestday>
				                                </daymodification>
			                                </action>
		                                </case>
		                                <default>
			                                <daymodification servicesign=""12"" iscustomname=""true"">
					                                <getclosestday dayofweek=""суббота"" weekcount=""-1""><date>--11-08</date></getclosestday>
				                                </daymodification>
		                                </default>
	                                </switch>
                                </rule>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            ExecContainer element = new ExecContainer(xmlDoc.FirstChild);

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
    }
}
