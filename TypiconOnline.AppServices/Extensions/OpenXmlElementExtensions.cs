using DocumentFormat.OpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using DocumentFormat.OpenXml.Wordprocessing;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Domain.ItemTypes;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using TypiconOnline.AppServices.OutputFiltering;
using System.IO;

namespace TypiconOnline.AppServices.Extensions
{
    public static class OpenXmlElementExtensions
    {
        /// <summary>
        /// Возвращает список элементов Run, содержащих искомый текст
        /// </summary>
        /// <param name="element"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public static IEnumerable<Run> FindElementsByText(this OpenXmlElement element, string search)
        {
            var result = new List<Run>();

            foreach (var child in element.ChildElements)
            {
                if (child is Run t && t.InnerText.Contains(search))
                {
                    result.Add(t);
                }

                result.AddRange(child.FindElementsByText(search));
            }

            return result;
        }

        public static IEnumerable<Run> FindElementsByText(this IEnumerable<OpenXmlElement> elements, string search)
        {
            var result = new List<Run>();

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
                //проходим несколько раз, потому что может быть сразу несколько замен
                while (child is Run r && r.InnerText.Contains(search))
                {
                    //заменяем текст
                    r.ReplaceText(search, replace);
                    found = true;
                }

                found = found || child.ReplaceElementsByText(search, replace).Success;
            }

            return (found) ? Result.Ok() : Result.Fail($"Поле для заполнения {search} не было найдено в шаблоне дня.");
        }

        public static void ReplaceText(this Run run, string search, string replace)
        {
            //находим первый Text, в котором находятся фрагменты искомого 
            Text text = run.ChildElements.First(c => (c is Text t) && search.Contains(t.Text)) as Text;
            
            string result = text.Text;
            //задаем ему новое значение
            text.Text = replace;

            text = text.NextSibling() as Text;

            //удаляем все последующие Text, пока не соберется полное значение search
            while (result != search || text != null)
            {
                result += text.Text;

                var toRemove = text;

                text = text.NextSibling() as Text;

                toRemove.Remove();
            }
        }

        public static Result ReplaceElementsByWorship(this OpenXmlElement element, string search, FilteredOutputWorship p)
        {
            bool found = false;
            foreach (var child in element.ChildElements)
            {
                if (child is Run run && run.InnerText.Contains(search))
                {
                    //Name
                    run.ReplaceText(search, p.Name.Text.Text);
                    run.ApplyStyle(p.Name.Style);

                    //AdditionalName
                    if (p.AdditionalName.Text != null)
                    {
                        //клонируем элемент
                        var runAdd = run.CloneNode(true) as Run;

                        //находим текст и задаем его
                        runAdd.ReplaceText(p.Name.Text.Text, " " + p.AdditionalName.Text.Text);

                        //применяем стили
                        runAdd.ApplyStyle(p.AdditionalName.Style);

                        //вставляем после текста шаблона
                        run.InsertAfterSelf(runAdd);
                    }

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

        public static string CopyImagePart(this WordprocessingDocument newDoc, string relId, MainDocumentPart mainDocumentPart)
        {
            var p = mainDocumentPart.GetPartById(relId) as ImagePart;
            var newPart = newDoc.MainDocumentPart.AddImagePart(p.ContentType);
            newPart.FeedData(p.GetStream(FileMode.Open, FileAccess.Read));
            return newDoc.MainDocumentPart.CreateRelationshipToPart(newPart);
            //return newDoc.MainDocumentPart.GetIdOfPart(newPart);
        }

        /// <summary>
        /// Очищает все элементы и их 
        /// </summary>
        /// <param name="elements"></param>
        public static void ClearFromErrorElements(this IEnumerable<OpenXmlElement> elements)
        {
            foreach (var element in elements)
            {
                element.ChildElements
                .Where(c => c is SectionProperties
                         || c is BookmarkStart
                         || c is BookmarkEnd)
                .ToList()
                .ForEach(c => c.Remove());

                element.ChildElements.ClearFromErrorElements();
            }
        }

        public static void CopyRelativeElements(this OpenXmlElement element, WordprocessingDocument weekDoc, MainDocumentPart fromPart)
        {
            //images
            element.Descendants<DocumentFormat.OpenXml.Drawing.Blip>().ToList()
                .ForEach(blip =>
                {
                    var newRelation = weekDoc.CopyImagePart(blip.Embed, fromPart);
                    blip.Embed = newRelation;
                });

            element.Descendants<DocumentFormat.OpenXml.Vml.ImageData>().ToList()
                .ForEach(imageData =>
                {
                    var newRelation = weekDoc.CopyImagePart(imageData.RelationshipId, fromPart);
                    imageData.RelationshipId = newRelation;
                });
        }
    }
}
