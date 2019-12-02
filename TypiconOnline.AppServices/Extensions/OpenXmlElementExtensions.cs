using DocumentFormat.OpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using DocumentFormat.OpenXml.Wordprocessing;
using TypiconOnline.Infrastructure.Common.ErrorHandling;

namespace TypiconOnline.AppServices.Extensions
{
    public static class OpenXmlElementExtensions
    {
        /// <summary>
        /// Возвращает список элементов, содержащих искомый текст
        /// </summary>
        /// <param name="element"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public static IEnumerable<Text> FindElementsByText(this OpenXmlElement element, string search)
        {
            var result = new List<Text>();

            foreach (var child in element.ChildElements)
            {
                if (child is Text t && t.Text.Contains(search))
                {
                    result.Add(t);
                }

                result.AddRange(child.FindElementsByText(search));
            }

            return result;
        }

        public static IEnumerable<Text> FindElementsByText(this IEnumerable<OpenXmlElement> elements, string search)
        {
            var result = new List<Text>();

            foreach (var child in elements)
            {
                result.AddRange(child.FindElementsByText(search));
            }

            return result;
        }

        public static Result ReplaceElementsByText(this OpenXmlElement element, string search, string replace)
        {
            bool found = false;
            foreach (var child in element.ChildElements)
            {
                if (child is Text t && t.Text.Contains(search))
                {
                    t.Text = t.Text.Replace(search, replace);

                    found = true;
                }

                found = found || child.ReplaceElementsByText(search, replace).Success;
            }

            return (found) ? Result.Ok() : Result.Fail($"Поле для заполнения {search} не было найдено в шаблоне дня.");
        }

        public static Result ReplaceElementsByText(this IEnumerable<OpenXmlElement> elements, string search, string replace)
        {
            bool found = false;

            foreach (var child in elements)
            {
                found = found || child.ReplaceElementsByText(search, replace).Success;
            }

            return (found) ? Result.Ok() : Result.Fail($"Поле для заполнения {search} не было найдено в шаблоне дня.");
        }

        public static IEnumerable<OpenXmlElement> DeepClone(this IEnumerable<OpenXmlElement> elements)
        {
            var result = new List<OpenXmlElement>();

            foreach (var child in elements)
            {
                result.Add(child.CloneNode(true));
            }

            return result;
        }
    }
}
