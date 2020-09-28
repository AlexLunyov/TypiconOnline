using DocumentFormat.OpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using DocumentFormat.OpenXml.Wordprocessing;

namespace TypiconOnline.AppServices.Viewers
{
    /// <summary>
    /// Утилита для поиска элементов в Docx-документе
    /// </summary>
    public class DocxUtility
    {
        /// <summary>
        /// Находит родителя у элемента по заданному классу родителя
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        public static OpenXmlElement FindParent<T>(OpenXmlElement element) where T : OpenXmlElement
        {
            if (element.Parent == null)
            {
                return default;
            }

            if (element.Parent is T)
            {
                return element.Parent;
            }
            else
            {
                return FindParent<T>(element.Parent);
            }
        }

        /// <summary>
        /// Производит поиск общего родителя у двух элементов по заданному классу искомого родителя
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="timePlacement"></param>
        /// <param name="worshipNamePlacement"></param>
        /// <returns>Если не найден общий родитель, возвращает null</returns>
        public static OpenXmlElement FindCommonParent<T>(Run timePlacement, Run worshipNamePlacement) where T : OpenXmlElement
        {
            var parentTime = FindParent<T>(timePlacement);

            var parentWorshipName = FindParent<T>(worshipNamePlacement);

            return (parentTime != null && parentTime == parentWorshipName)
                ? parentTime
                : default;
        }

    }
}
