using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconMigrationTool
{
    public class SignMigrator
    {
        //атрибуты Sign Для миграции
        //Name, Priority, OldId, NewId
        //static readonly Dictionary<string, int> _signPriorites1 = new Dictionary<string, int>()
        //{
        //    { "Пасха", 1 },
        //    { "День Светлой седмицы", 2 },
        //    //{ "Благовещение", 3 },
        //    { "Бдение", 3},
        //    { "Бдение с литией", 3 },
        //    { "Полиелей", 3 },
        //    { "Воскресный день", 3 },
        //    { "Воскресенье с Соборованием", 3 },
        //    { "Аллилуиа", 4 },
        //    { "Литургия Преждеосвященных Даров", 4 },
        //    { "Поминовение усопших", 4 },
        //    { "Поминовение усопших (Постом)", 4 },
        //    { "Славословная", 5 },
        //    { "Шестеричная", 5 },
        //    { "Без знака", 5 }
        //};

        //key - oldID
        static readonly Dictionary<int, Item> _signs = new Dictionary<int, Item>()
        {
            { 20, new Item() { Name = "Пасха", Priority = 1, NewID = 1 } },
            { 11, new Item() { Name = "День Светлой седмицы", Priority = 2, NewID = 2 } },
            { 6, new Item() { Name = "Бдение с литией", Priority = 3, NewID = 3 } },
            { 5, new Item() { Name = "Бдение", Priority = 3, NewID = 4 } },
            { 4, new Item() { Name = "Полиелей", Priority = 3, NewID = 5 } },
            { 9, new Item() { Name = "Воскресный день", Priority = 3, NewID = 6 } },
            { 10, new Item() { Name = "Воскресенье с Соборованием", Priority = 3, NewID = 7 } },
            { 7, new Item() { Name = "Аллилуиа", Priority = 4, NewID = 8 } },
            { 8, new Item() { Name = "Литургия Преждеосвященных Даров", Priority = 4, NewID = 9 } },
            { 14, new Item() { Name = "Поминовение усопших", Priority = 4, NewID = 10 } },
            { 16, new Item() { Name = "Поминовение усопших (Постом)", Priority = 4, NewID = 11 } },
            { 3, new Item() { Name = "Славословная", Priority = 5, NewID = 12 } },
            { 2, new Item() { Name = "Шестеричная", Priority = 5, NewID = 13 } },
            { 1, new Item() { Name = "Без знака", Priority = 5, NewID = 14 } },
            { 21, new Item() { Name = "Двунадесятый Господский праздник", Priority = 1, NewID = 15 } },
            { 22, new Item() { Name = "День Страстной седмицы", Priority = 2, NewID = 16, TemplateId = 9 } },
        };

        private int _oldID;

        public SignMigrator(int oldID)
        {
            _oldID = oldID;
        }

        public static SignMigrator Instance(int oldID)
        {
            return new SignMigrator(oldID);
        }

        public string Name
        {
            get
            {
                return _signs[_oldID].Name;
            }
        }

        public int Priority
        {
            get
            {
                return _signs[_oldID].Priority;
            }
        }

        public int NewId
        {
            get
            {
                return _signs[_oldID].NewID;
            }
        }

        public int? TemplateId
        {
            get
            {
                return _signs[_oldID].TemplateId;
            }
        }

        struct Item
        {
            public string Name;
            public int Priority;
            public int NewID;
            public int? TemplateId;
        }
    }
}
