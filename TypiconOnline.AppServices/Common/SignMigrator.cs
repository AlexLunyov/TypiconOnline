using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.AppServices.Common
{
    public class SignMigrator
    {
        //key - oldID
        static readonly Dictionary<int, Item> _signs = new Dictionary<int, Item>()
        {
            { 13, new Item() { Name = "Пасха", Priority = 1, TemplateId = 5 } },
            { 10, new Item() { Name = "День Светлой седмицы", Priority = 2, TemplateId = 4, Number = 0, Icon = 0 } },
            { 5, new Item() { Name = "Бдение с литией", Priority = 3, Number = 5, Icon = 5, IsRed = true } },
            { 4, new Item() { Name = "Бдение", Priority = 3, Number = 4, Icon = 4, IsRed = true } },
            { 3, new Item() { Name = "Полиелей", Priority = 3, Number = 3, Icon = 3 } },
            { 8, new Item() { Name = "Воскресный день", Priority = 3, Number = 8, IsRed = true } },
            { 9, new Item() { Name = "Воскресенье с Соборованием", Priority = 3, TemplateId = 8 } },
            { 6, new Item() { Name = "Аллилуиа", Priority = 4, Number = 6 } },
            { 7, new Item() { Name = "Литургия Преждеосвященных Даров", Priority = 4, Number = 7 } },
            { 11, new Item() { Name = "Поминовение усопших", Priority = 4,TemplateId = 0 } },
            { 12, new Item() { Name = "Поминовение усопших (Постом)", Priority = 3, TemplateId = 0 } },
            { 2, new Item() { Name = "Славословная", Priority = 5,TemplateId = 0, Number = 2, Icon = 2 } },
            { 1, new Item() { Name = "Шестеричная", Priority = 5, TemplateId = 0, Number = 1, Icon = 1 } },
            { 0, new Item() { Name = "Без знака", Priority = 5, Number = 0 } },
            { 14, new Item() { Name = "Двунадесятый Господский праздник", Priority = 1, TemplateId = 5 } },
            { 15, new Item() { Name = "День Страстной седмицы", Priority = 2, TemplateId = 7 } },
        };

        private int _oldID;

        public SignMigrator(int oldID)
        {
            if (!_signs.ContainsKey(oldID)) throw new ArgumentOutOfRangeException("oldID");

            _oldID = oldID;
        }

        public static SignMigrator Instance(int oldID)
        {
            return new SignMigrator(oldID);
        }

        /// <summary>
        /// Возвращает Id (из старой БД) предустановленных знаков служб
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static int GetOldId(Func<KeyValuePair<int, Item>, bool> condition )
        {
            return _signs.Where(condition).Select(k => k.Key).FirstOrDefault();
        }

        public static int GetOldTemplateId(Func<KeyValuePair<int, Item>, bool> condition)
        {
            var valuePair = _signs.Where(condition).FirstOrDefault();

            while (valuePair.Value.TemplateId != null)
            {
                valuePair = _signs.Where(c => c.Key == valuePair.Value.TemplateId).FirstOrDefault();
            }

            return valuePair.Key;
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

        //public int NewId
        //{
        //    get
        //    {
        //        return _signs[_oldID].NewID;
        //    }
        //}

        public int? TemplateId
        {
            get
            {
                return _signs[_oldID].TemplateId;
            }
        }
        public int? Number
        {
            get
            {
                return _signs[_oldID].Number;
            }
        }

        public int? Icon
        {
            get
            {
                return _signs[_oldID].Icon;
            }
        }

        public bool IsRed
        {
            get
            {
                return _signs[_oldID].IsRed;
            }
        }

        /// <summary>
        /// Возвращает имя базового шаблона (элемента, у которого TemplateId == null)
        /// </summary>
        public string MajorTemplateName
        {
            get
            {
                return GetMajorItem(_oldID)?.Name;
            }
        }

        private Item GetMajorItem(int id)
        {
            Item item = _signs[id];

            while (item?.TemplateId != null)
            {
                item = _signs.Where(c => c.Key == item.TemplateId).FirstOrDefault().Value;
            }

            return item;
        }

        public class Item
        {
            public string Name;
            public int Priority;
            public int? TemplateId;
            public int? Number;
            public int? Icon;
            public bool IsRed = false;
        }
    }
}
