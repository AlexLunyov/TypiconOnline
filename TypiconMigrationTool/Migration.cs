using ScheduleHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using System.Data.Entity.Validation;
using TypiconOnline.Domain;
using TypiconOnline.Domain.Easter;

namespace TypiconMigrationTool
{
    public class Migration
    {
        IUnitOfWork _unitOfWork;
        ScheduleHandler _sh;

        public Migration (IUnitOfWork unitOfWork, ScheduleHandler sh)
        {
            _unitOfWork = unitOfWork;
            _sh = sh;
        }

        public void Execute()
        {
            Console.WriteLine("ClearEF()");
            ClearEF();

            Console.WriteLine("Migrate()");
            Migrate();

            Console.WriteLine("MigrateEasters()");
            MigrateEasters();

            Console.WriteLine("Saving...");

            _unitOfWork.Commit();
            Console.WriteLine("Success.");
        }

        

        private void ClearEF()
        {
            IEnumerable<RuleEntity> rulesDaySet = _unitOfWork.Repository<RuleEntity>().GetAll();
            foreach (RuleEntity element in rulesDaySet)
            {
                _unitOfWork.Repository<RuleEntity>().Delete(element);
            }

            IEnumerable<Sign> dSet = _unitOfWork.Repository<Sign>().GetAll();
            foreach (Sign element in dSet)
            {
                _unitOfWork.Repository<Sign>().Delete(element);
            }

            IEnumerable<FolderEntity> foldersDaySet = _unitOfWork.Repository<FolderEntity>().GetAll();
            foreach (FolderEntity element in foldersDaySet)
            {
                _unitOfWork.Repository<FolderEntity>().Delete(element);
            }

            IEnumerable<TypiconEntity> typiconEntitySet = _unitOfWork.Repository<TypiconEntity>().GetAll();
            foreach (TypiconEntity element in typiconEntitySet)
            {
                _unitOfWork.Repository<TypiconEntity>().Delete(element);
            }

            //IEnumerable<MenologyDay> menologyDaySet = _unitOfWork.Repository<MenologyDay>().GetAll();
            //foreach (MenologyDay element in menologyDaySet)
            //{
            //    _unitOfWork.Repository<MenologyDay>().Delete(element);
            //}

            //IEnumerable<TriodionDay> triodionDaySet = _unitOfWork.Repository<TriodionDay>().GetAll();
            //foreach (TriodionDay element in triodionDaySet)
            //{
            //    _unitOfWork.Repository<TriodionDay>().Delete(element);
            //}

            _unitOfWork.Commit();
        }

        private void Migrate()
        {
            TypiconEntity typiconEntity = new TypiconEntity()
            {
                Name = "Типикон",
            };

            typiconEntity.RulesFolder = new FolderEntity() { Name = "Правила", Owner = typiconEntity };

            string folderPath = Properties.Settings.Default.FolderPath + typiconEntity.Name + "\\Sign\\"; 

            FileReader fileReader = new FileReader(folderPath);

            int i = 1;

            foreach (ScheduleDBDataSet.ServiceSignsRow row in _sh.DataSet.ServiceSigns.Rows)
            {
                //int priority = _signPriorites.ContainsKey(row.Name) ? _signPriorites[row.Name] : row.ID;

                SignMigrator signMigrator = new SignMigrator(row.ID);

                Sign sign = new Sign()
                {
                    //Id = signMigrator.NewId,
                    Name = row.Name,
                    Number = signMigrator.NewId,
                    Priority = signMigrator.Priority,
                    TypiconEntity = typiconEntity,
                    RuleDefinition = fileReader.GetXml(row.Name)
                };

                //_unitOfWork.Repository<Sign>().Insert(sign);
                typiconEntity.Signs.Add(sign);

                i++;
            }

            _unitOfWork.Repository<TypiconEntity>().Insert(typiconEntity);

            try
            {
                _unitOfWork.Commit();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult result in ex.EntityValidationErrors)
                {
                    Console.WriteLine(result.ToString());
                }
            }
            MigrateMenologyDaysAndRules(typiconEntity);
            MigrateTriodionDaysAndRules(typiconEntity);
        }

        private void MigrateMenologyDaysAndRules(TypiconEntity typiconEntity)
        {
            Console.WriteLine("MigrateMenologyDaysAndRules()");

            FolderEntity folder = new FolderEntity() { Name = "Минея" };
            typiconEntity.RulesFolder.AddFolder(folder);

            FolderEntity childFolder = new FolderEntity() { Name = "Минея 1" };

            folder.AddFolder(childFolder);

            string folderPath = Properties.Settings.Default.FolderPath + typiconEntity.Name + "\\Menology\\";

            FileReader fileReader = new FileReader(folderPath);

            foreach (ScheduleDBDataSet.MineinikRow mineinikRow in _sh.DataSet.Mineinik.Rows)
            {
                
                MenologyDay menologyDay = new MenologyDay()
                {
                    Name = mineinikRow.Name,
                    Date = (mineinikRow.IsDateNull()) ? new ItemDate() : new ItemDate(mineinikRow.Date.Month, mineinikRow.Date.Day),
                    DateB = (mineinikRow.IsDateBNull()) ? new ItemDate() : new ItemDate(mineinikRow.DateB.Month, mineinikRow.DateB.Day),
                };

                _unitOfWork.Repository<MenologyDay>().Insert(menologyDay);

                string ruleDefinition = "";

                if (!mineinikRow.IsDateBNull())
                {
                    //TODO: изменил алгоритм. Надо поменять все названия xml-файлов правил для Минеи
                    string fileName = menologyDay.DateB.Expression + "." + menologyDay.Name;
                    ruleDefinition = fileReader.GetXml(fileName);
                }
                else
                {
                    ruleDefinition = fileReader.GetXml(menologyDay.Name);
                }

                MenologyRule menologyRule = new MenologyRule()
                {
                    Day = menologyDay,
                    Name = menologyDay.Name,
                    Owner = typiconEntity,
                    Template = typiconEntity.Signs.First(c => c.Number == SignMigrator.Instance(mineinikRow.SignID).NewId),
                    RuleDefinition = ruleDefinition
                };

                folder.AddRule(menologyRule);
                typiconEntity.MenologyRules.Add(menologyRule);
            }

            //_unitOfWork.Commit();
        }

        private void MigrateTriodionDaysAndRules(TypiconEntity typiconEntity)
        {
            Console.WriteLine("MigrateTriodionDaysAndRules()");

            FolderEntity folder = new FolderEntity() { Name = "Триодь" };

            typiconEntity.RulesFolder.AddFolder(folder);

            foreach (ScheduleDBDataSet.TriodionRow row in _sh.DataSet.Triodion.Rows)
            {
                TriodionDay day = new TriodionDay()
                {
                    Name = row.Name,
                    DaysFromEaster = (int) row.DayFromEaster,
                };
                //day.Sign = _unitOfWork.Repository<Sign>().Get(c => c.Id == row.SignID);

                _unitOfWork.Repository<TriodionDay>().Insert(day);

                string folderPath = Properties.Settings.Default.FolderPath + typiconEntity.Name + "\\Triodion\\";

                FileReader fileReader = new FileReader(folderPath);

                TriodionRule rule = new TriodionRule()
                {
                    Day = day,
                    Name = day.Name,
                    Owner = typiconEntity,
                    Template = typiconEntity.Signs.First(c => c.Number == SignMigrator.Instance(row.SignID).NewId),
                    RuleDefinition = fileReader.GetXml(row.DayFromEaster.ToString())
                };

                folder.AddRule(rule);
                typiconEntity.TriodionRules.Add(rule);
            }

            //_unitOfWork.Commit();
        }

        private void MigrateEasters()
        {            
            foreach (ScheduleDBDataSet.PaskhaliaRow row in _sh.DataSet.Paskhalia)
            {
                _unitOfWork.Repository<EasterItem>().Insert(new EasterItem { Date = row.Date });
            }
            
            //_unitOfWork.Repository<EasterStorage>().Insert(EasterStorage.Instance);
        }
    }
}
