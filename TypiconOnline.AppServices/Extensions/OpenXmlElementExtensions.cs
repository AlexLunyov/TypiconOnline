using DocumentFormat.OpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using DocumentFormat.OpenXml.Wordprocessing;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Domain.WebQuery.OutputFiltering;
using TypiconOnline.Domain.ItemTypes;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;

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

        public static Result ReplaceElementsByWorship(this OpenXmlElement element, string search, FilteredOutputWorship p)
        {
            bool found = false;
            foreach (var child in element.ChildElements)
            {
                if (child is Run run && run.InnerText.Contains(search))
                {
                    //AdditionalName
                    if (p.AdditionalName.Text != null)
                    {
                        //клонируем элемент
                        var runAdd = run.CloneNode(true) as Run;

                        //находим текст и задаем его
                        var t = runAdd.ChildElements.First(c => c is Text) as Text;
                        t.Text = p.AdditionalName.Text.Text;
                        t.Space = SpaceProcessingModeValues.Preserve;

                        //применяем стили
                        runAdd.ApplyStyle(p.AdditionalName.Style);

                        //вставляем после текста шаблона
                        child.InsertAfterSelf(runAdd);
                    }
                    

                    //Name
                    run.ReplaceElementsByText(search, p.Name.Text.Text);
                    run.ApplyStyle(p.Name.Style);

                    found = true;
                }

                found = found || child.ReplaceElementsByWorship(search, p).Success;
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

        public static void ApplyStyle(this Run run, TextStyle style)
        {
            //RunStyle

            run.RunProperties.Bold = (style.IsBold) ? new Bold() : null;

            run.RunProperties.Color = (style.IsRed) ? new Color() { Val = "FF0000" } : null;

            run.RunProperties.Italic = (style.IsItalic) ? new Italic() : null;
        }

        public static string CopyPart(this WordprocessingDocument newDoc, string relId, MainDocumentPart mainDocumentPart)
        {
            var p = mainDocumentPart.GetPartById(relId) as ImagePart;
            var newPart = newDoc.MainDocumentPart.AddPart(p);
            newPart.FeedData(p.GetStream());
            return newDoc.MainDocumentPart.GetIdOfPart(newPart);
        }
    }
}
