using NUnit.Framework;
using System;
using System.Linq;
using System.Xml;
using TypiconOnline.Domain.Easter;
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
		                                <condition>
			                                <daysfromeaster><date>--04-06</date></daysfromeaster>
		                                </condition>
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

            XmlDocument xmlDoc = new XmlDocument();

            EFUnitOfWork _unitOfWork = new EFUnitOfWork();

            EasterStorage.Instance.EasterDays = _unitOfWork.Repository<EasterItem>().GetAll().ToList();

            xmlDoc.LoadXml(xmlString);

            ExecContainer element = new ExecContainer(xmlDoc.FirstChild);

            element.Interpret(DateTime.Today, BypassHandler.Instance);

            Assert.Pass("Success");
        }

        [Test]
        public void Rules_Executables_Switch_DimitrySaturday()
        {
            EFUnitOfWork _unitOfWork = new EFUnitOfWork();

            EasterStorage.Instance.EasterDays = _unitOfWork.Repository<EasterItem>().GetAll().ToList();

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
