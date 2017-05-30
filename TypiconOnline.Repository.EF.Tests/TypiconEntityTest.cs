using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Easter;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;

namespace TypiconOnline.Repository.EF.Tests
{
    [TestFixture]
    public class TypiconEntityTest
    {
        [Test]
        public void TypiconEntity_FromDB()
        {
            EFUnitOfWork _unitOfWork = new EFUnitOfWork();

            TypiconEntity typiconEntity = _unitOfWork.Repository<TypiconEntity>().Get(c => c.Name == "Типикон");

            Assert.AreEqual(typiconEntity.Signs.Count, 14);
        }

        [Test]
        public void TypiconEntity_Delete()
        {
            EFUnitOfWork _unitOfWork = new EFUnitOfWork();

            TypiconEntity typiconEntity = new TypiconEntity()
            {
                Name = "Устав для удаления"
            };

            typiconEntity.RulesFolder.AddFolder(new TypiconFolderEntity()
            {
                Name = "Папка для удаления",
                Owner = typiconEntity
            });

            _unitOfWork.Repository<TypiconEntity>().Insert(typiconEntity);

            _unitOfWork.Commit();

            _unitOfWork.Repository<TypiconEntity>().Delete(typiconEntity);

            _unitOfWork.Commit();

            Assert.Pass("Deleted.");
        }

        [Test]
        public void TypiconEntity_GetModifiedRule()
        {
            EFUnitOfWork _unitOfWork = new EFUnitOfWork();

            EasterStorage.Instance.EasterDays = _unitOfWork.Repository<EasterItem>().GetAll().ToList();

            TypiconEntity typiconEntity = _unitOfWork.Repository<TypiconEntity>().Get(c => c.Name == "Типикон");
            List<ModifiedRule> modifiedDays = typiconEntity.GetModifiedRules(new DateTime(2017, 10, 28));

            _unitOfWork.Commit();

            Assert.AreEqual(typiconEntity.ModifiedYears.Count, 1);
        }

        //[Test]
        //public void TypiconEntity_ClearModifiedRules()
        //{
        //    EFUnitOfWork _unitOfWork = new EFUnitOfWork();

        //    EasterStorage.Instance.EasterDays = _unitOfWork.Repository<EasterItem>().GetAll().ToList();

        //    TypiconEntity typiconEntity = _unitOfWork.Repository<TypiconEntity>().Get(c => c.Name == "Типикон");
        //    //ModifiedRule modifiedDay = typiconEntity.GetModifiedRule(new DateTime(2038, 4, 5));

        //    _unitOfWork.Commit();

        //    typiconEntity.ClearModifiedRules();

        //    _unitOfWork.Commit();

        //    Assert.Pass("Success.");
        //}

        [Test]
        public void TypiconEntity_GetModifiedRule2()
        {
            EFUnitOfWork _unitOfWork = new EFUnitOfWork();

            EasterStorage.Instance.EasterDays = _unitOfWork.Repository<EasterItem>().GetAll().ToList();

            MenologyDay day = new MenologyDay()
            {
                Date = new ItemDate("--04-06"),
                DateB = new ItemDate("--04-06"),
                Name = "Предпразднство Благовещения Пресвятой Богородицы."
            };

            Sign sign = new Sign()
            {
                Name = "Без знака",
                RuleDefinition = @"<rule>
	                                <switch>
		                                <condition>
			                                <daysfromeaster><date>--04-06</date></daysfromeaster>
		                                </condition>
		                                <case>
			                                <values>
				                                <int>-19</int>
			                                </values>
			                                <action>
				                                <modifyday servicesign=""7"" daymove=""-1"" iscustomname=""true""/>
			                                </action>
		                                </case>
		                                <case>
				                            <values>
					                            <int>-17</int>
				                            </values>
			                                <action>
				                                <modifyday servicesign=""8"" daymove=""-1"" iscustomname=""true""/>
			                                </action>
		                                </case>
	                                </switch>
                                </rule>"
            };

            TypiconEntity typiconEntity = new TypiconEntity();

            typiconEntity.RulesFolder = new TypiconFolderEntity()
            {
                Owner = typiconEntity
            };
            typiconEntity.RulesFolder.AddRule(new MenologyRule()
            {
                Day = day,
                Template = sign,
            });

            List<ModifiedRule> modifiedDays = typiconEntity.GetModifiedRules(new DateTime(2038, 4, 5));

            Assert.IsNotNull(modifiedDays);
        }
    }
}
