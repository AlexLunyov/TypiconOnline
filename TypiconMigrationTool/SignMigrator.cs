﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconMigrationTool
{
    public class SignMigrator
    {
        //key - oldID
        static readonly Dictionary<int, Item> _signs = new Dictionary<int, Item>()
        {
            { 20, new Item() { Name = "Пасха", Priority = 1, NewID = 1, TemplateId = 3 } },
            { 11, new Item() { Name = "День Светлой седмицы", Priority = 2, NewID = 2, TemplateId = 14 } },
            { 6, new Item() { Name = "Бдение с литией", Priority = 3, NewID = 3 } },
            { 5, new Item() { Name = "Бдение", Priority = 3, NewID = 4 } },
            { 4, new Item() { Name = "Полиелей", Priority = 3, NewID = 5 } },
            { 9, new Item() { Name = "Воскресный день", Priority = 3, NewID = 6 } },
            { 10, new Item() { Name = "Воскресенье с Соборованием", Priority = 3, NewID = 7, TemplateId = 6 } },
            { 7, new Item() { Name = "Аллилуиа", Priority = 4, NewID = 8 } },
            { 8, new Item() { Name = "Литургия Преждеосвященных Даров", Priority = 4, NewID = 9 } },
            { 14, new Item() { Name = "Поминовение усопших", Priority = 4, NewID = 10, TemplateId = 14 } },
            { 16, new Item() { Name = "Поминовение усопших (Постом)", Priority = 4, NewID = 11, TemplateId = 14 } },
            { 3, new Item() { Name = "Славословная", Priority = 5, NewID = 12 } },
            { 2, new Item() { Name = "Шестеричная", Priority = 5, NewID = 13 } },
            { 1, new Item() { Name = "Без знака", Priority = 5, NewID = 14 } },
            { 21, new Item() { Name = "Двунадесятый Господский праздник", Priority = 1, NewID = 15, TemplateId = 3 } },
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
